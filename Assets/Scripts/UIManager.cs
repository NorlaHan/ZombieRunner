using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public	bool uiHelpShow = false, uiHelpPromptShow = true , isQuitUIShowen = false,uiBlackOutShow = false, uiPausedShow = false, uiQuitShow = false;

	private GameObject uiHelp, uiHelpPrompt, uiBlackOut, uiPaused, uiQuit;

//	private bool isFadingBlackOut = false;
//	private float blackOutAlpha = 1;
	private Image blackOutImage;

	// Use this for initialization
	void Start () {
		if (GameObject.Find("UIHelp")) {
			uiHelp = GameObject.Find ("UIHelp");
		}else {Debug.LogWarning (name +", missing UIHelp!");}
		if (GameObject.Find("UIHelpPrompt")) {
			uiHelpPrompt = GameObject.Find ("UIHelpPrompt");
		}else {Debug.LogWarning (name +", missing UIHelp!");}
		if (GameObject.FindObjectOfType<UIBlackOut>()) {
			uiBlackOut = GameObject.FindObjectOfType<UIBlackOut>().gameObject;
		}else {Debug.LogWarning (name +", missing UIBlackOut!");}
		if (GameObject.FindObjectOfType<UIPaused>()) {
			uiPaused = GameObject.FindObjectOfType<UIPaused>().gameObject;
		}else {Debug.LogWarning (name +", missing UIPaused!");}
		if (GameObject.FindObjectOfType<UIQuit>()) {
			uiQuit = GameObject.FindObjectOfType<UIQuit>().gameObject;
		}else {Debug.LogWarning (name +", missing UIQuit!");}


		uiHelp.SetActive(uiHelpShow);
		uiHelpPrompt.SetActive (uiHelpPromptShow);
		uiBlackOut.SetActive (uiBlackOutShow);
		uiPaused.SetActive (uiPausedShow);
		uiQuit.SetActive (uiQuitShow);

	}
	
	// Update is called once per frame
	void Update () {
		// Toggle help UI layer.
		if (Input.GetButtonDown ("Help")) {
			if (uiHelp.activeSelf == true) {
				uiHelp.SetActive(false);
				uiHelpPrompt.SetActive (true);
			} else {
				uiHelp.SetActive(true);
				uiHelpPrompt.SetActive (false);
			}
		}
		if (Input.GetButtonDown("Paused") && uiQuit.activeSelf == false) {
			if (uiPaused.activeSelf == false) {
				uiPaused.SetActive (true);
				BroadcastMessage ("OnPlayerPaused");
			} else {
				BroadcastMessage ("OnPlayerResume");
				uiPaused.SetActive (false);
			}
		}

		if (Input.GetButtonDown ("Cancel") && uiPaused.activeSelf == false) {
			if (uiQuit.activeSelf == false) {
				uiQuit.SetActive (true);
				BroadcastMessage ("OnPlayerCallQuit");
			}else{
				BroadcastMessage ("OnPlayerCancelQuit");
				uiQuit.SetActive (false);
			}
		}

		if (uiQuit.activeSelf == true) {
			if (Input.GetKeyDown(KeyCode.End)) {
				Debug.Log ("Shut down!!");
				BroadcastMessage ("OnPlayerExeQuit");
			}else if (Input.GetKeyDown(KeyCode.Home)) {
				Debug.Log ("Return to start menu.");
				BroadcastMessage ("OnPlayerQuitReturn");
			}

		}
	}



	public void OnBlackOut(){
		uiBlackOut.SetActive (true);
		BroadcastMessage ("BlackOut");
	}
}
