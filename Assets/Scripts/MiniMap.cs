using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

	private Player player;
	private GameObject playerIndicator;
	private RectTransform pIRectTransform , mapRectTransform;
	private Rect mapRect;
	// Use this for initialization
	void Start () {
		if (player = GameObject.FindObjectOfType<Player> ()) {
			player = GameObject.FindObjectOfType<Player> ();
		} else {Debug.LogWarning (name + ", Missing player!");}
		if (GameObject.Find ("PlayerIndicator")) {
			playerIndicator = GameObject.Find ("PlayerIndicator");
			pIRectTransform = playerIndicator.GetComponent<RectTransform> ();
		} else {Debug.LogWarning (name + ", missing PlayerIndicator");}

		mapRect = GetComponent<RectTransform> ().rect; 

		//Debug.Log (pIRectTransform.anchoredPosition + "pIPos.anchoredPosition, Rect is "+mapRect);
	}
	
	// Update is called once per frame
	void Update () {
		pIRectTransform.anchoredPosition = new Vector2 (player.transform.position.x / 800 * mapRect.width, player.transform.position.z / 800 * mapRect.height);
		//Debug.Log ("pIPos.anchoredPosition is " +pIRectTransform.anchoredPosition );
	}
}
