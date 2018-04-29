using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Intro : MonoBehaviour {


	public float fadeSpeed = 2f;
	public string[] Hints;
	public bool[] pages;

	private GameManager gameManager;
	private Image bgImage;
	private Text context;
	public int i = 0 , maxIndex;
	public float textAlpha = 1;
	private bool fade;



	//AudioSource audioSource;
	void Awake(){
		
	}

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		bgImage = GetComponent<Image> ();
		context = GetComponentInChildren<Text> ();

		Cursor.visible = true;

		maxIndex = Hints.Length - 1;
		pages [0] = true;

		fade = false;
		//i = 0;
		textAlpha = 0;
		//Debug.Log ("Start");
	}
	
	// Update is called once per frame
	void Update () {
		if (fade) {
			TextFadeOut ();
			if (textAlpha<=0) {
				fade = false;
				pages [i] = true;	// start the next after this one is completely done.
			}
		}
		if (pages [i]) {
			if (textAlpha <= 0) {	// switch the text while invisible.
				context.text = Hints [i];
				TextFadeIn ();		// get out of this stage.
			} else {
				TextFadeIn ();

				if (textAlpha>=1) {
					pages [i] = false;	// stop the fade.
					//Debug.Log ("Display text.");
				}
			}
		}

		if (Input.GetButtonDown("Horizontal")) {
			if (Input.GetAxisRaw("Horizontal") > 0 ) {
				NextPage ();
			} else{
				PreviousPage ();
			}
		}

		if (Input.GetButtonDown ("Vertical")) {
			if (Input.GetAxisRaw ("Vertical") > 0) {
				gameManager.NextLevel ();
			}
		}
	}

	void TextFadeIn(){
		//Debug.Log ("Fade in.");
		if (textAlpha<1) {
			textAlpha += Time.deltaTime*fadeSpeed;
			//Debug.Log ("Fade in. add");
			//Debug.Log (textAlpha);
			context.color = new Color (1, 1, 1, textAlpha);
		}
	}

	void TextFadeOut(){
		//Debug.Log ("Fade out.");
		if (textAlpha>0) {
			textAlpha -= Time.deltaTime*fadeSpeed;
			//Debug.Log (textAlpha);
			context.color = new Color (1, 1, 1, textAlpha);
		}
	}

	public void NextPage(){
		//Debug.Log ("NextPage.");
		if (i<maxIndex && textAlpha >= 1) {
			i++;
			fade = true;
			//Debug.Log ("i = " + i);
		}else if (i >= maxIndex) {
			gameManager.NextLevel ();
		}
	}

	public void PreviousPage(){
		if (i>0 && textAlpha >= 1) {
			i--;
			fade = true;
			//Debug.Log ("i = " + i);
		}
	}


}
