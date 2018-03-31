using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    [Header("HUD")]
    public Text[] timer;
    public Text RussiaScore;
    public Text USAScore;
    public Text[] rounds;
    public Text fps;

    [Header("Timer")]
    public time_format format = time_format.mm_ss;

    public enum time_format
    {
        mm_ss,
        m_ss,
        ss,
        m
    }

    public static HUD instance;
    
    void Start()
    {
        instance = this;

        foreach (Text _timer in timer) { _timer.text = "0:00"; }
        foreach (Text _rounds in rounds) { _rounds.text = "ROUND 0"; }
        RussiaScore.text = "0";
        USAScore.text = "0";
    }

    void Update()
    {
        fps.text = ((int)(1.0d / (double)Time.deltaTime)).ToString();
    }

    public void newRound()
    {
        int _currentRounds = int.Parse(rounds[0].text.Replace("ROUND", ""));
        foreach (Text _rounds in rounds) { _rounds.text = "ROUND " + (_currentRounds + 1); }
    }
    public void setRound(int round)
    {
        foreach (Text _rounds in rounds) { _rounds.text = "ROUND " + round; }
    }

    public void setUSAcore(int score)
    {
        USAScore.text = score.ToString();
    }
    public void setRussiaScore(int score)
    {
        RussiaScore.text = score.ToString();
    }

    public void setTimer(int _seconds)
    {
        int seconds = 0;
        int minutes = 0;
        
        seconds = _seconds % 60;
        minutes = (int) ((_seconds - seconds) / 60);

        string _sec = seconds.ToString();
        string _min = minutes.ToString();

        if ((format == time_format.mm_ss || format == time_format.m_ss || format == time_format.ss) && seconds < 10) _sec = "0" + seconds;
        if (format == time_format.mm_ss && minutes < 10) _min = "0" + minutes;

        foreach(Text _timer in timer) { _timer.text = _min + ":" + _sec; }
    }
}
