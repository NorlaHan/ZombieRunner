using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableCarButton : MonoBehaviour {
	public GameObject cableCar;

	public bool isGoingDown = false, isButtonPressed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider obj){
		Debug.Log ("Collision!");
		GameObject starter = obj.gameObject;
		if (starter.tag == "Player") {
			Debug.Log (starter.name + ", Collision!");
			SendMessageUpwards ("SeatStopFollowPlayer");
			starter.transform.parent.transform.parent.SetParent (cableCar.transform);
			if (!isButtonPressed) {
				isButtonPressed = true;
				if (!isGoingDown) {
					SendMessageUpwards ("CableCarGoDown");
					isGoingDown = true;	
				}else {
					SendMessageUpwards ("CableCarGoUp");
					isGoingDown = false;
				}
			}

		}
	}

	void ButtonActionComplete(){
		isButtonPressed = false;
	}
}
