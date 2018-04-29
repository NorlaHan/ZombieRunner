using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatusBar : MonoBehaviour {

	//public Player player;

	private Image healthBar, staminaBar;
	// TODO private after done
	//private float healthPercentage, staminaPercentage;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("HealthBar").GetComponent<Image> ()) {
			healthBar = GameObject.Find ("HealthBar").GetComponent<Image> ();
		} else {Debug.LogWarning ("HealthBar missing!");}

		if (GameObject.Find ("StaminaBar").GetComponent<Image> ()) {
			staminaBar = GameObject.Find ("StaminaBar").GetComponent<Image> ();
		} else {Debug.LogWarning ("StaminaBar missing!");}


	}

	public void UpdateHealthBar (float healthPercentage)
	{
		healthBar.rectTransform.localScale = new Vector3 (healthPercentage, 1, 1);
	}

	public void UpdateStaminaBar (float staminaPercentage){
		staminaBar.rectTransform.localScale = new Vector3 (1, staminaPercentage, 1);
	}
}
