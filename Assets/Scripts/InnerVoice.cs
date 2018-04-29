using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerVoice : MonoBehaviour {

	public AudioClip[] audioClips;

	private AudioSource audioSource;
	private int audioIndex;
	//private float countDown = 0;
	private bool isPaused = false;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		InnerVoiceSpeaks (0);
	}

	void Update(){
		if (isPaused && !audioSource.isPlaying) {
			Time.timeScale = 1;
			isPaused = false;
		}
	}

	// Freeze and play narration.
	void ReSpawnReset(){
		Time.timeScale = 0;
		InnerVoiceSpeaks (2);
		isPaused = true;
	}


	// This is a BroadCast method from clear area. 
	void OnFindClearArea(){
		//Debug.Log (name + ", receive OnFindClearArea");
		InnerVoiceSpeaks (1);
		Invoke ("CallHeli",audioClips[audioIndex].length + 2f);
	}

	public void InnerVoiceSpeaks(int Index){
		//Debug.Log (name + " InnerVoiceSpeaks");
		audioIndex = Index;
		audioSource.clip = audioClips[audioIndex];
		audioSource.loop = false;
		audioSource.Play ();
	}

	void CallHeli(){
		SendMessageUpwards ("OnMakeInitialHeliCall");	// Receive : RadioSystem
		//Debug.Log (name + ", send OnMakeInitialHeliCall");
	}

	void OnPlayerDrowning(){
		InnerVoiceSpeaks (3);
		audioSource.Play ();
		audioSource.loop = true;
	}

	void OnPlayerNotDrowning(){
		audioSource.Stop ();
		audioSource.loop = false;
	}
}
