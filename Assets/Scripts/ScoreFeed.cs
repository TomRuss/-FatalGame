using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFeed : MonoBehaviour {

	public static ScoreFeed instance;

	[Header("Prefabs")]
	public GameObject scorePrefab;
	public Transform container;

	[Header("Parameters")]
	public int maxAmount = 7;
	public float stayTime = 7f;
	public float killYspace;
	public float infoYspace;

	[Header("Fonts / Sizes")]
	public int killFontSize;
	public int infoFontSize;

    [Header("Animation")]
    public AnimationClip drawAniamtion;

    List<Score> scores = new List<Score>();
    List<Score> delete = new List<Score>();

	public class Score
	{
		public GameObject entity;
		public ScoreFeed.state currentState;
		public ScoreFeed.level messageLevel;
		public string text;
		public int coins;
        public float actualElapsedTime = 0;

		public float currentIndex = 0;
		public bool invisible = true;

		public Score(string _text, GameObject _gameObject, int _coins, ScoreFeed.level _level)
		{
			currentState = ScoreFeed.state.SPAWNING;
			messageLevel = _level;
			entity = _gameObject;
			coins = _coins;
			text = _text;
        }

		public void Despawn()
		{
			currentState = ScoreFeed.state.DESPAWNING;
			invisible = false;
			currentIndex = 0;
		}
	}
	public enum state {
		SPAWNING,
		STAYING,
		DESPAWNING
	}
	public enum level {
		KILL,
		INFO
	}

    private void Awake() {
        instance = this;
    }
	void Update() {
        UpdateScore();

		if (Input.GetKeyDown (KeyCode.T))
        {
            Info("DOUBLE KILL", 50);
            Info("HEADSHOT BONUS", 100);
            queue.Add(new QuedScore("AK-47", "Fatal_Developer", 100));
        }

        int spawningSize = 0;
		foreach(Score score in scores)
		{
            if(score.entity.GetComponent<Text>().text.ToCharArray().Length < score.text.ToCharArray().Length)
            {
                // Making the text appear
                if (score.messageLevel == level.INFO)
                {
                    int index = score.text.ToCharArray().Length;
                    int indexToShowUp = score.entity.GetComponent<Text>().text.ToCharArray().Length;
                    score.currentIndex += index * Time.deltaTime / 5f;
                    indexToShowUp += (int)score.currentIndex;

                    string _text = "";
                    for (int _i = 0; _i <= indexToShowUp; _i++)
                    {
                        if (_i < score.text.ToCharArray().Length)
                            _text = _text + score.text.ToCharArray()[_i].ToString();
                    }
                    score.entity.GetComponent<Text>().text = _text;
                }
            }

			if(score.currentState == state.SPAWNING)
            {
                spawningSize++;
                // Making the other texts lower
                float decrease = killYspace;
				if (score.messageLevel == level.INFO)
					decrease = infoYspace;
				foreach(Score _scores in scores)
				{
                    if(_scores != score)
    					_scores.entity.transform.localPosition -= new Vector3(0f, decrease * Time.deltaTime * 2f, 0f);
				}

                // Making it stop after the 1/2second
                score.actualElapsedTime += Time.deltaTime * 2f;
                if(score.actualElapsedTime >= 1)
                    score.currentState = state.STAYING;
            }
			else if (score.currentState == state.STAYING)
			{
				// NOTHING
			}
			else if (score.currentState == state.DESPAWNING)
			{
				Destroy (score.entity);
                delete.Add(score);
			}
		}
        foreach(Score toDelete in delete)
        {
            scores.Remove(toDelete);
        }
        delete.Clear();

        if(spawningSize == 0 && queue.Count > 0)
        {
            if (queue[0].Level == level.INFO)
                LaterInfo(queue[0].info_or_Weapon, queue[0].coins);
            else if (queue[0].Level == level.KILL)
                LaterKill(queue[0].info_or_Weapon, queue[0].victim, queue[0].coins);

            queue.Remove(queue[0]);
        }
	}

    List<QuedScore> queue = new List<QuedScore>();
    public class QuedScore
    {
        public ScoreFeed.level Level;
        public string info_or_Weapon;
        public string victim;
        public int coins;

        public QuedScore(string info, int _coins)
        {
            info_or_Weapon = info;
            victim = null;
            coins = _coins;
            Level = ScoreFeed.level.INFO;
        }
        public QuedScore(string weapon, string _victim, int _coins)
        {
            info_or_Weapon = weapon;
            victim = _victim;
            coins = _coins;
            Level = ScoreFeed.level.KILL;
        }
    }

    public void Info(string info, int coins) {
        queue.Add(new QuedScore(info, coins));
    }

    void LaterKill(string _weapon, string victim, int coins)
	{
		GameObject _score = Instantiate (scorePrefab, container) as GameObject;
		_score.GetComponent<Text> ().fontSize = killFontSize;
		_score.GetComponent<Text> ().text = "[" + _weapon + "] <color=#FF3A00>" + victim + "</color><color=#ffffff>   +" + coins + "</color>";
        _score.transform.localPosition = Vector3.zero;
        _score.GetComponent<Animation>().Play(drawAniamtion.name);
        
        scores.Add (new Score ("", _score, coins, level.KILL));

		StartCoroutine (Unspawn(scores[scores.Count -1], stayTime));
		StartCoroutine (SetVisible (scores [scores.Count - 1]));

		if (scores.Count >= maxAmount)
			scores [0].Despawn ();

        addScore(coins);
    }
	void LaterInfo(string info, int coins)
	{
		GameObject _score = Instantiate (scorePrefab, container) as GameObject;
		_score.GetComponent<Text> ().fontSize = infoFontSize;
        _score.GetComponent<Text>().text = "";
        _score.transform.localPosition = Vector3.zero;
        _score.GetComponent<Animation>().Play(drawAniamtion.name);

        string _text = info + "   " + "+" + coins.ToString();
        scores.Add (new Score (_text, _score, coins, level.INFO));

		StartCoroutine (Unspawn(scores[scores.Count -1], stayTime));
		StartCoroutine (SetVisible (scores [scores.Count - 1]));

		if (scores.Count >= maxAmount)
			scores [0].Despawn ();

        addScore(coins);
	}

	IEnumerator Unspawn(Score _score, float time)
	{
		yield return new WaitForSeconds (time);
		_score.Despawn ();
	}
	IEnumerator SetVisible(Score _score)
	{
		yield return new WaitForSeconds (0.5f);
		_score.invisible = false;
	}

    [Header("SCORE")]
    public Text totalScore;
    public int targetScore = 0;
    public int displayedScore = 0;

    double alpha = 0d;
    void UpdateScore()
    {
        if (targetScore != 0 && alpha < 1)  alpha += 1d * Time.deltaTime * 2f;
        if(targetScore == 0 && alpha > 0) alpha -= 1d * Time.deltaTime * 2f;
        
        totalScore.color = new Color(1f, 1f, 1f, (float)alpha);
        displayedScore = (int)Vector3.Lerp(new Vector3(displayedScore, 0f, 0f), new Vector3(targetScore, 0f, 0f), Time.deltaTime * 5f).x;
        totalScore.text = "+" + displayedScore;
    }
    
    void despawnTotalScore()
    {
        targetScore = 0;
    }
    void addScore(int _score)
    {
        targetScore += _score;
        CancelInvoke();
        Invoke("despawnTotalScore", 7f);
    }
}
