using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayTimeCalculator : MonoBehaviour {

	public float playTimeTotal;
	public float playTimeCasual;
	public float playTimeRanked;
	[Space]
	public Text playTimeTotalText;
	public Text playTimeCasualText;
	public Text playTimeRankedText;


	void Start ()
	{
		playTimeCasual = PlayerPrefs.GetFloat("PlaytimeCasual");
		playTimeTotal = PlayerPrefs.GetFloat("PlaytimeTotal");
	}

	void Update ()
	{
		var timeSpan = System.TimeSpan.FromSeconds(playTimeCasual);
		if (SceneManager.GetActiveScene().name == "TestLevel")
		{
			playTimeCasual = Mathf.Max(0, playTimeCasual += Time.deltaTime);
			PlayerPrefs.SetFloat("PlaytimeCasual", playTimeCasual);
		}

		var timeSpanTotal = System.TimeSpan.FromSeconds(playTimeTotal);
		playTimeTotal = Mathf.Max(0, playTimeTotal += Time.deltaTime);
		PlayerPrefs.SetFloat("PlaytimeTotal", playTimeTotal);

		if (SceneManager.GetActiveScene().name == "Main Menu" && playTimeCasualText == null && playTimeTotalText == null)
		{
			playTimeTotalText = GameObject.FindGameObjectWithTag("Total").GetComponent<Text>();
			playTimeTotalText.text = timeSpanTotal.Hours + "H " + timeSpanTotal.Minutes + "M";
		}
	}

}