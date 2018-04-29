using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour {
	[Tooltip("How many times faster than real world.")]
	//public float timeScale = 60;
	public bool	isTimeChange = true;

	private UITime uiTime;
	private float degreePerSecond,timeScale, sunRotX , sunRotY, SunRotZ;
	private bool isCalabrating;

	// Use this for initialization
	void Start () {
		uiTime = GameObject.FindObjectOfType<UITime>();
		sunRotY = transform.eulerAngles.y;
		SunRotZ = transform.eulerAngles.z;
		MatchSunWithTime ();
	}
	
	// Update is called once per frame
	void Update () {
//		// EulerAngle is accurate in this range, thus trigger calabrate.
//		if (transform.eulerAngles.x > 30 && transform.eulerAngles.x < 45) {isCalabrating = true;}
//		if (isCalabrating) {
//			MatchSunWithTime ();
//			isCalabrating = false;
//		}
//		if (isTimeChange) {
//			transform.RotateAround(transform.position, Vector3.forward, degreePerSecond * Time.deltaTime);
//			//transform.eulerAngles += new Vector3(degreePerSecond*Time.deltaTime,0,0); 
//		}
		MatchSunWithTime ();
	}

	void MatchSunWithTime ()
	{
		timeScale = uiTime.timeScale;
		sunRotX = 270 + (uiTime.TimeInSeconds () / 240);
		//if (sunRotX > 360) {sunRotX -= 360;}
		transform.eulerAngles = new Vector3 (sunRotX, sunRotY, SunRotZ);
		//degreePerSecond = 360f / 24f / 3600f * timeScale;
		//Debug.Log ("degreePerSecond "+ degreePerSecond);
	}
}
