using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

	private Image bgImage;
	float bgAlpha = -5f , textAlpha = 2f;
	Text title;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		bgImage = GetComponent<Image> ();
		title = GetComponentInChildren<Text> ();
		audioSource = GetComponent<AudioSource> ();
		Invoke ("ZombieRoar", 2.4f);
	}
	
	// Update is called once per frame
	void Update () {
		if (bgAlpha<1) {bgAlpha += Time.deltaTime*2f;}
		bgImage.color = new Color (1, 1, 1, bgAlpha);

		if (textAlpha>0) {
			textAlpha -= Time.deltaTime;
		}
		title.color = new Color (1, 0, 0, textAlpha);
		
	}

	void ZombieRoar(){
		audioSource.Play ();
	}
}
