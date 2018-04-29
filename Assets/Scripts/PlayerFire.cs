using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

	public AudioClip[] audioClips;

	private AudioSource audioSource;
	private ParticleSystem[] particleSystems;

	// Use this for initialization
	void Start () {
		if (GetComponent<AudioSource> ()) {
			audioSource = GetComponent<AudioSource> ();
		} else {Debug.LogWarning (name + ", missing AudioSource");}

		particleSystems = GetComponentsInChildren<ParticleSystem> ();
	}


	void FireGun1(){
		foreach (ParticleSystem particle in particleSystems) {
			particle.Play ();
		}
		audioSource.clip = audioClips [0];
		audioSource.Play ();
	}

	void Gun1NoAmmo(){
		audioSource.clip = audioClips [1];
		audioSource.Play ();
	}
}
