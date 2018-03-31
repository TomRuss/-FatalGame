using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	public Text TimerText;
	public float Minutes;
	public float Seconds;
	//public AudioSource rounddraw;
	public Text rounddrawtext;
	//public Animation rounddrawanim;

	void Start()
	{
		StartCoroutine(Wait());
		//rounddraw = GetComponent<AudioSource> ();
		rounddrawtext.enabled = false;
		//rounddrawanim = GetComponent<Animation> ();
	}
	void Update()
	{
		if(Seconds < 10)
		{
			TimerText.text = (Minutes + ":0" + Seconds);
		}
		if(Seconds > 9)
		{
			TimerText.text = (Minutes + ":" + Seconds);
		}

	}
	public void CountDown()
	{
		if(Seconds <= 0)
		{
			MinusMinute();
			Seconds = 60;
		}
		if(Minutes >= 0)
		{
			MinusSeconds();
		}
		if(Minutes <= 0 && Seconds <= 0)
		{
			Debug.Log("Time Up");
			StopTimer();
		}
		else
		{
			Start ();
		}

	}
	public void MinusMinute()
	{
		Minutes -= 1;
	}
	public void MinusSeconds()
	{
		Seconds -= 1;
	}
	public IEnumerator Wait()
	{
		yield return new WaitForSeconds(1);
		CountDown();
	}
	public void StopTimer()
	{
		Seconds = 0;
		Minutes = 0;
		//rounddraw.Play ();
		rounddrawtext.enabled = true;
		StartCoroutine(restartlevel());
	}

	public IEnumerator restartlevel(){
		yield return new WaitForSeconds (4);
		rounddrawtext.enabled = false;
		//rounddrawanim.Play ();
	}
}