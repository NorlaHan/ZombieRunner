using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float projectileDamage, lifeTime = 2f;
	public float projectileSpeed = 2f;

	private float spawnTime = 0;
	// Use this for initialization
	void Start () {
		
		//rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		spawnTime += Time.deltaTime;
		//Debug.Log ("spawnTime = "+spawnTime);
		if (spawnTime >= lifeTime) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		GameObject target = collision.gameObject;
		if (target.tag == "Mob") {
			Health health = target.GetComponent<Health> ();
			health.TakeDamage (projectileDamage);
			Destroy (gameObject);
		} else if (target.tag != "Player") {
			Destroy (gameObject);
		}
		//Debug.Log ("Hit "+ target.name);
	}

	public void Fire(Vector3 forward){
		Rigidbody rigidbody = GetComponent<Rigidbody> ();
		rigidbody.velocity = forward * projectileSpeed;
		//Debug.Log (rigidbody.velocity);
	}
}
