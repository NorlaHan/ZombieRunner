using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSystem : MonoBehaviour {


	public AudioClip[] audioClips;

	private AudioSource audioSource;
	private int audioIndex;

	// Use this for initialization
	void Start () {
		if (gameObject.GetComponent<AudioSource> ()) {
			audioSource = gameObject.GetComponent<AudioSource> ();
		} else {
			Debug.LogWarning ("missing AudioSource!");
		}
		//RadioSpeaks (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// message sent by Player
	void OnMakeInitialHeliCall(){
		//Debug.Log (name + ", receive OnMakeInitialHeliCall");
		RadioSpeaks (0);

		Invoke ("InitialReply", audioClips[audioIndex].length + 1f );
	}

	void InitialReply ()
	{
		RadioSpeaks (1);
		BroadcastMessage ("OnDispatchHelicopter");	// Receive : Player
		//Debug.Log (name + ", send OnMakeInitialHeliCall");
	}

	void RadioSpeaks(int Index){
		audioIndex = Index;
		audioSource.clip = audioClips[audioIndex];
		audioSource.Play ();
	}

}
