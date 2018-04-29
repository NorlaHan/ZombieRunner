using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WaterFunction : MonoBehaviour {

	public float playerDrownTime = 3;
	public bool isPlayerInWater = false , isSubmerged = false;

	FirstPersonController fpsController;

	float contactTime = 0;

	// Use this for initialization
	void Start () {
		fpsController = GameObject.FindObjectOfType <FirstPersonController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider obj){
		GameObject target = obj.gameObject;
		if (target.GetComponent<Player>()) {
			target.GetComponent<Player> ().OnRidingCableCar ();
			if (!isPlayerInWater) {
				isPlayerInWater = true;
				fpsController.OnPlayerInWater ();
			}
			if (transform.position.y - target.transform.position.y > 1.3f) {
				contactTime += Time.deltaTime;
				if (!isSubmerged) {
					isSubmerged = true;
					target.GetComponent<Player> ().OnPlayerUnderWater ();
				}
				if (contactTime > playerDrownTime) {
					target.GetComponent<Health> ().TakeDamage (10);
					Debug.Log ("Player Drown");
					contactTime = 0;
				}
			} else {
				contactTime = 0;
				isSubmerged = false;
				target.GetComponent<Player>().OnPlayerOutOfWater();
			}
		}
	}

	void OnTriggerExit(Collider obj){
		GameObject target = obj.gameObject;
		if (target.GetComponent<Player>() && isPlayerInWater) {
			isPlayerInWater = false;
			fpsController.OnPlayerOutOfWater(); 
		}
	}
}
