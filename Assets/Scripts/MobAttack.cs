
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobAttack : MonoBehaviour {

	public float meleeDamage = 10f , meleeRate = 1f, meleeRange = 1.2f;
	public bool autoSetTarget = true;
	public AudioClip[] audioClips;


	private GameObject currentTarget;
	private UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
	private AudioSource audioSource;
	private float meleeCount = 0f;
	private bool isLastWave = false, isGrowling = false;
	private Animator animator;

	// Use this for initialization
	void Start () {
		ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ();
		if (GetComponent<AudioSource> ()) {
			audioSource = GetComponent<AudioSource> ();
		} else {Debug.LogWarning (name + "missing AudioSource!");}

		if (autoSetTarget && !ai.target) {
			Debug.LogWarning ("Target not set, auto set.");
			currentTarget = FindObjectOfType<Player> ().gameObject;
			Invoke ("AutoGetPlayer", 2);
		}
		if (GetComponent<Animator> ()) {
			animator = GetComponent<Animator> ();
		} else {Debug.LogWarning (name + "missing Animator!");}

	}

	void AutoGetPlayer ()
	{
		ai.SetTarget (currentTarget.transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (meleeCount < meleeRate) {
			meleeCount += Time.deltaTime;
		}
		if (currentTarget && (currentTarget.transform.position-transform.position).magnitude <= meleeRange) {
			if (meleeCount >= meleeRate) {
				//Debug.Log ("Trigger attack!!!");
				meleeCount = 0;
				MeleeAttack ();
				//animator.SetBool ("isMelee",false);
			}
		}
	}

	// Attack player if collision
//	void OnCollisionEnter(Collision collision){
//		if (collision.gameObject.GetComponent<Player>()) {
//			currentTarget = collision.gameObject;
//			MeleeAttack ();
//		}
//
//	}

	// Target and chase player if trigger
	void OnTriggerStay(Collider obj){
		if (!ai) {ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ();}
		GameObject target = obj.gameObject;
		if (target.GetComponent<Player>()) {
			currentTarget = target.gameObject;
			ai.SetTarget(currentTarget.transform);
			if (!isGrowling) {
				MobGrowl ();
			}
			//ai.StartChasing ();
			//Debug.Log (name + " player in sight!");
		}
	}

	// Back to passive after player leaves
	void OnTriggerExit(Collider obj){
		GameObject target = obj.gameObject;
		if (target == currentTarget && !isLastWave) {
			MobNotAggro ();
			//Debug.Log (name + " player out of sight!");
		}
	}

	void MeleeAttack ()
	{	
		//Debug.Log (name + ", attacks "+ currentTarget);
		//Debug.Log ((currentTarget.transform.position-gameObject.transform.position).magnitude);
		//animator.SetBool ("isMelee",true);
		animator.SetTrigger("MeleeTrigger");
		try {
			currentTarget.GetComponent<Health> ().TakeDamage (meleeDamage);	
		} catch {
			Debug.LogWarning (name + ", can't find target.");	
		}

	}

	public void LastWave(){
		isLastWave = true;
		MobAggro ();
	}

	public void MobAggro(){
		if (!ai) {ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ();}
		currentTarget = GameObject.FindObjectOfType<Player> ().gameObject;
		ai.SetTarget (currentTarget.transform);
		//Debug.Log (name + ", Aggro!");
	}

	public void MobNotAggro ()
	{
		if (!ai) {ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ();}
		ai.SetTarget (null);
		isGrowling = false;
	}

	void MobGrowl(){
		isGrowling = true;
		audioSource = GetComponent<AudioSource> ();
		int index = Random.Range (0, audioClips.Length);
		audioSource.clip = audioClips [index];
		audioSource.Play ();
		Debug.Log (name + " Growl");
		if (isGrowling) {
			Invoke ("MobGrowl", audioClips [index].length);
		}
	}

	public void MobDies(){
		MonoBehaviour[] comps = GetComponents<MonoBehaviour> ();
		foreach (MonoBehaviour comp in comps) {
			comp.enabled = false;
		}
		Rigidbody rigidBody = GetComponent<Rigidbody> ();
		GetComponent<Animator> ().enabled = false;
		GetComponent<NavMeshAgent> ().enabled = false;
		GetComponent<SphereCollider> ().enabled = false;
		GetComponent<AudioSource> ().enabled = false;
		rigidBody.velocity = Vector3.zero;
		rigidBody.constraints = RigidbodyConstraints.None;
		float xRot = Random.Range (-1f, 1f);
		float yRot = Random.Range (-1f, 1f);
		float zRot = Random.Range (-1f, 1f);
		rigidBody.angularVelocity = new Vector3 (xRot, yRot, zRot);

		GetComponent<CapsuleCollider>().enabled = true;
		GetComponent<CapsuleCollider> ().material.dynamicFriction = 0f;
		GetComponent<CapsuleCollider> ().material.dynamicFriction = 1f;
		MobDestroy ();
	}

	void MobDestroy (){
		Destroy (gameObject, 5);
	}
}
