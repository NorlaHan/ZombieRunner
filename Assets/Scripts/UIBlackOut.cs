using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlackOut : MonoBehaviour {
	public float fadeSpeed = 1;

	private Image blackOutImage;
	private bool isFadingBlackOut = false;
	private float blackOutAlpha = 1;

	// Use this for initialization
	void Start () {
		if (GetComponent<Image>()) {
			blackOutImage = GetComponent<Image>();
		}else {Debug.LogWarning (name +", missing blackOutImage!");}
	}
	
	// Update is called once per frame
	void Update () {
		if (isFadingBlackOut) {
			blackOutAlpha -= Time.deltaTime*fadeSpeed;
			blackOutImage.color =new Color (178f,0f,0f,blackOutAlpha);
			if (blackOutAlpha <= 0) {
				isFadingBlackOut = false;
				gameObject.SetActive (false);
			}
		}
	}

	void BlackOut(){
		blackOutImage.color = new Color (178, 0, 0, 1);
		blackOutAlpha = 1;
		isFadingBlackOut = true;
	}
}
