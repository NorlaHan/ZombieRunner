using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour {

	public int hour, minute;
	public float timeScale;

	private GameObject dayLight;
	private Text timeDisplay;
	private float  timeInSeconds;
	//private Vector3 sunRot;

	// Use this for initialization
	void Awake(){
		timeInSeconds = ((minute * 60) + (hour * 3600));
	}

	void Start () {
		if (GameObject.FindObjectOfType<DayCycle> ()) {
			dayLight = GameObject.FindObjectOfType<DayCycle> ().gameObject;
		} else {
			Debug.LogWarning ("DayLight missing!");
		}

		timeDisplay = GetComponent<Text> ();
		//timeScale = dayLight.GetComponent<DayCycle> ().timeScale;
		//sunRot = dayLight.transform.localEulerAngles;
		//GetTimeFromSun ();
		DisplayTime ();
	}
	
	// Update is called once per frame
	void Update () {
		timeInSeconds += Time.deltaTime * timeScale;
		if (timeInSeconds >= 86400) { timeInSeconds = 0;}
		DisplayTime ();
//		if (dayLight.transform.localEulerAngles.x >30 && dayLight.transform.localEulerAngles.x < 45) {
//			sunRot.x = dayLight.transform.localEulerAngles.x;
//		} else {
//			if (sunRot.x >= 360) {sunRot.x -= 360;}
//			sunRot.x = sunRot.x + (15 / 3600 * timeScale* Time.deltaTime);
//			Debug.Log ("sunRot is " +sunRot+ ", timeScale is " + timeScale);
//		}
//
//		//GetTimeFromSun ();
//		Debug.Log ("update");
	}

//	void GetTimeFromSun ()
//	{
//		string hourS, minuteS;
//
//		//Debug.Log ("sunRot is " +sunRot);
//		if (sunRot.x > 0) {
//			hour = Mathf.FloorToInt (sunRot.x / 15f + 6f);
//			minute = Mathf.FloorToInt ((sunRot.x % 15f) * 4f);
//		}
//		else
////			if (sunRot.x > 90) {
////				hour = 24 - Mathf.FloorToInt (sunRot.x / 15f + 6f);
////				minute = Mathf.FloorToInt ((sunRot.x % 15f) * 4f);
////			}
//		if (sunRot.x < 0) {
//			hour = Mathf.FloorToInt ((sunRot.x + 360f) / 15f + 6f);
//			minute = Mathf.FloorToInt (((sunRot.x + 360f) % 15f) * 4f);
//		}
//		// keep 24 hour due to 0 degree is 6 am.
//		if (hour > 24) {hour -= 24;}
//
//		// keep hour and minute at 2 digit.
//		if (hour.ToString ().Length < 2) {
//			hourS = "0" + hour.ToString ();
//		} else {hourS =hour.ToString ();}
//		if (minute.ToString ().Length < 2) {
//			minuteS = "0" + minute.ToString ();
//		} else {minuteS = minute.ToString ();}
//
//		timeDisplay.text = hourS + " : " + minuteS;
//	}

	void DisplayTime(){
		string hourS, minuteS;
		hour = Mathf.FloorToInt(timeInSeconds / 3600);
		minute = Mathf.FloorToInt((timeInSeconds % 3600)/60);

		if (hour.ToString ().Length < 2) {
			hourS = "0" + hour.ToString ();
		} else {hourS =hour.ToString ();}
		if (minute.ToString ().Length < 2) {
			minuteS = "0" + minute.ToString ();
		} else {minuteS = minute.ToString ();}

		timeDisplay.text = hourS + " : " + minuteS;
	}

	public float TimeInSeconds(){
		return timeInSeconds;
	}
}
