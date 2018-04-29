using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableCar : MonoBehaviour {

	public GameObject cableCarButton, radioSystem, seat;
	public bool playerEnter = false, buttonPushed = false;

	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider obj){
		GameObject passenger = obj.gameObject;
		if (passenger.GetComponent<Player>()) {
			playerEnter = true;
			if (!buttonPushed) {
				seat.transform.position = passenger.transform.position;
			}
			//Vector3 distance = passenger.transform.position - transform.position;
			//passenger.transform.position = transform.position + new Vector3 (0, -3f, 0) ;
			passenger.GetComponent<Player> ().OnRidingCableCar ();
			Debug.Log (passenger.name + ", On board!" );
		}
	}

	void OnTriggerExit (Collider obj){
		//Debug.Log (obj.name);
		GameObject passenger = obj.gameObject;
		if (passenger.GetComponent<Player>()) {
			playerEnter = false;
			buttonPushed = false;
			CableCarMoveComplete ();
			//seat.transform.position = passenger.transform.position;
			passenger.transform.SetParent (radioSystem.transform);
			Debug.Log (passenger.name + ", Off board!" );
		}
	}

	void CableCarGoDown(){
		animator.SetBool ("isGoing", true);
	}

	void CableCarGoUp(){
		animator.SetBool ("isGoing", false);
	}

	void CableCarMoveComplete(){
		BroadcastMessage ("ButtonActionComplete");
	}
		
	void SeatStopFollowPlayer(){
		buttonPushed = true;
	}
//	void OnCollisionEnter (Collision obj){
//		Debug.Log ("Collision!");
//		GameObject starter = obj.gameObject;
//		if (starter.tag == "Player") {
//			Debug.Log (starter.name + ", Collision!");
//		}
//	}

}
