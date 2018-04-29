using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinScene : MonoBehaviour {

	GameManager gameManager;
	private Image bgImage;
	float bgAlpha = 1 , textAlpha = 0;
	bool next = false;
	Text context;
	//AudioSource audioSource;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		bgImage = GetComponent<Image> ();
		context = GetComponentInChildren<Text> ();

		bgAlpha = 1;
		textAlpha = -2;
		//audioSource = GetComponent<AudioSource> ();
		//Invoke ("ZombieRoar", 2.4f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!next) {
			if (!next && bgImage.color.a > 0) {
				Debug.Log ("updating BG");
				bgAlpha -= Time.deltaTime;
				bgImage.color = new Color (0, 0, 0, bgAlpha);
			} else if (textAlpha < 1) {
				Debug.Log ("updating contex");
				textAlpha += Time.deltaTime;
				context.color = new Color (1, 1, 1, textAlpha);
			} else if (context.color.a >= 1) {
				next = true;
				bgAlpha = -1;
			}
		} else if (bgAlpha < 1) {
			Debug.Log ("updating BG2");
			bgAlpha += Time.deltaTime;
			bgImage.color = new Color (0, 0, 0, bgAlpha);
		} else if (textAlpha > 0) {
			Debug.Log ("updating contex");
			textAlpha -= Time.deltaTime;
			context.color = new Color (1, 1, 1, textAlpha);
		} else {
			//Debug.Log ("Load Menu");
			//gameManager.NextLevel();
			gameManager.LoadLevel("Menu");
		}
			
	}
}
