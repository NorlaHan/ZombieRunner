using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UIQuit : MonoBehaviour {

	private GameManager gameManager;

	void Start(){
		if (GameObject.FindObjectOfType<GameManager>()) {
			gameManager = GameObject.FindObjectOfType<GameManager>();
		}else {Debug.LogWarning (name +", missing GameManager!");}
	}

	void OnPlayerCallQuit (){
		Time.timeScale = 0;
		GameObject.FindObjectOfType<FirstPersonController> ().enabled = false;
		GameObject.FindObjectOfType<Player> ().enabled = false;
	}

	void OnPlayerCancelQuit (){
		Time.timeScale = 1;
		GameObject.FindObjectOfType<FirstPersonController> ().enabled = true;
		GameObject.FindObjectOfType<Player> ().enabled = true;
	}

	void OnPlayerExeQuit (){
		Application.Quit ();
	}

	void OnPlayerQuitReturn(){
		Time.timeScale = 1;
		gameManager.LoadLevel("Menu");
	}
}
