using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmo : MonoBehaviour {

	private Player player;

	private Text[] ammoTexts; 
	private Image crossHair;

	// Use this for initialization
	void Start () {
		if (FindObjectOfType<Player> ()) {
			player = FindObjectOfType<Player> ();
		} else {Debug.Log (name + ", missing Player");}
		if (GetComponentInChildren<Image> ()) {
			crossHair = GetComponentInChildren<Image> ();
		}else {Debug.Log (name + ", missing CrossHair");}
		crossHair.enabled = false;

		ammoTexts = GetComponentsInChildren<Text> ();

//		foreach (Text item in ammoTexts) {
//			Debug.Log (item.text);
//		}
	}
	
	// Update is called once per frame
	void Update () {
//		ammoTexts [0]; // The name of the ammo.
		ammoTexts [1].text = player.Fireammo().ToString();
	}

	public bool crossHairEnabled(){
		return crossHair.enabled;
	}

	public void ShowCrossHair(){
		//Debug.Log ("Fire3 pressed!");
		crossHair.enabled = true;
	}

	public void HideCrossHair(){
		crossHair.enabled = false;
	}
}
