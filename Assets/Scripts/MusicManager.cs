using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip[] audioClips;

	private AudioSource audioSource;
	//private GameManager gameManager;
	private int LevelIndex;


	void Awake(){
		audioSource = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		//gameManager = GameObject.FindObjectOfType<GameManager> ();

		//currentLevelIndex = gameManager.CurrentLevel ();
		//ChangeBGM ();

	}
	
	public void ChangeBGM(int currentLevelIndex){
		LevelIndex = currentLevelIndex;
		int i;
		if (currentLevelIndex == 1) {
			i = 0;
			audioSource.clip = audioClips[i];
		}else if (currentLevelIndex == 2) {
			i = Random.Range(1,4);
			audioSource.clip = audioClips[i];
			Invoke ("LoopChangeBGM", audioClips [i].length);
		}else if (currentLevelIndex == 3) {
			i = 4;
			audioSource.clip = audioClips[i];
		}else if(currentLevelIndex == 0){
			audioSource.clip = null;
			return;
		}
		audioSource.Play ();
	}

	void LoopChangeBGM(){
		ChangeBGM (LevelIndex);
	}
}
