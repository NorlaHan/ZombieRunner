using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHelpPrompt : MonoBehaviour {
	
	public float fadeSpeed = 1f;
	public string[] promptTexts;

	Text text;
	int index = 0;
	bool canChange = true;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		text.text = promptTexts [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (text.IsActive()) {
			float alpha = Mathf.Sin (Time.time * fadeSpeed);
			text.color = new Color (1f, 1f, 1f, alpha);
			if (alpha >= 0.5 && !canChange) {
				canChange = true;
			}
			if (alpha <= 0 && canChange) {
				index++;
				if (index >= promptTexts.Length) {
					index = 0;
				}
				text.text = promptTexts [index];
				canChange = false;
			}

			//Debug.Log ("UI prompt updated");
		}

	}
}
