using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFist : MonoBehaviour {

	public AudioClip[] audioClips;

	private AudioSource audioSource;
	private ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
		if (GetComponent<AudioSource> ()) {
			audioSource = GetComponent<AudioSource> ();
		} else {Debug.LogWarning (name + ", missing AudioSource");}

		if (GetComponentInChildren<ParticleSystem>()) {
			particleSystem = GetComponentInChildren<ParticleSystem> ();
		}else {Debug.LogWarning (name + ", missing ParticleSystem in children!");}
	}

	void FistPunch(){
		int i = Random.Range (0, 3);
		audioSource.clip = audioClips [i];
		audioSource.Play ();
	}

	public void FistSplash(){
		audioSource.clip = audioClips [3];
		audioSource.Play ();
		particleSystem.Play ();
	}
}
