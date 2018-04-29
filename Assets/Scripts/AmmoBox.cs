using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour {

	public int ammoRound = 8;

	public enum AmmoType {fire1, fire2, fire3};
	public AmmoType ammoType;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnCollisionEnter(Collision obj){
//		Debug.Log (name + ", collision");
//		Player player;
//		if (obj.gameObject.tag == "Player") {
//			Debug.Log (name + ", collision with player");
//			player = obj.gameObject.GetComponent<Player> ();
//			player.PickUpammo (ammoRound);
//			Destroy (gameObject);
//		}
//	}

	void OnTriggerEnter(Collider obj){
		//Debug.Log (name + ", Trigger");
		Player player;
		if (obj.gameObject.GetComponent<Player> ()) {
			//Debug.Log (name + ", trigger with player");
			player = obj.gameObject.GetComponent<Player> ();
			ammoRandom ();
			player.PickUpammo (ammoRound);
			audioSource.Play ();
			Invoke ("DestroyAmmoBox", audioSource.clip.length);
		}
	}

	void DestroyAmmoBox ()
	{
		Destroy (gameObject);
		AmmoSpawn.ammoSpawnedCount--;
	}

	void ammoRandom(){
		if (ammoType == AmmoType.fire1) {
			ammoRound = Random.Range (ammoRound/2, ammoRound);
		}
	}
}
