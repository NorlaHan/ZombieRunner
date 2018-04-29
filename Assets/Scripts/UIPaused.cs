using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UIPaused : MonoBehaviour {



	void OnPlayerPaused(){
		Time.timeScale = 0;
		GameObject.FindObjectOfType<FirstPersonController> ().enabled = false;
		GameObject.FindObjectOfType<Player> ().enabled = false;
	}

	void OnPlayerResume(){
		Time.timeScale = 1;
		GameObject.FindObjectOfType<FirstPersonController> ().enabled = true;
		GameObject.FindObjectOfType<Player> ().enabled = true;
	}
}
