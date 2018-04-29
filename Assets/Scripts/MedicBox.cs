using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicBox : MonoBehaviour {

	public float medicHeal = 33f;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider obj){
		//Debug.Log (name + ", Trigger");
		Player player;
		if (obj.gameObject.GetComponent<Player> ()) {
			//Debug.Log (name + ", trigger with player");
			player = obj.gameObject.GetComponent<Player> ();
			healRandom ();
			Debug.Log ("Medic box heals " + medicHeal);
			player.PickUpMedic (medicHeal);
			audioSource.Play ();
			Invoke ("DestroyAmmoBox", audioSource.clip.length);
		}
	}

	void DestroyAmmoBox ()
	{
		Destroy (gameObject);
		AmmoSpawn.ammoSpawnedCount--;
	}

	void healRandom(){
		medicHeal = Random.Range (medicHeal/2, medicHeal);
	}
}
