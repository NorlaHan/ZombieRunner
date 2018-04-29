using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

	public float heliSpeed = 50f, landingSpeed = 20f, landingSpeed2 = 4f,approachingDist = 300f, slowingDownDist = 20f, landingDist = 2f,rotSpeed = 30f, safeEscapeTime = 3f;
	public	Player player;
	public Vector3 landingFoward = new Vector3 (0f , 2f , 100f), cinamaticVelocity = new Vector3 (10f,0f,0f) ,landingPos;
	public bool isCinamatic = false,  updateHeli = false;

	//public AudioSource audioSource;
	private bool isCalled = false; 
	private Rigidbody rigidBody;
	private Vector3 startPos, startRot;
	private float lerp1 = 0f, touchDownTime = 0f;
	private GameManager gameManager;

	// Use this for initialization
	void Awake(){
		rigidBody = GetComponent<Rigidbody> ();
	}

	void Start () {
		if (GameObject.FindObjectOfType<GameManager> ()) {
			gameManager = GameObject.FindObjectOfType<GameManager> ();
		} else {Debug.LogWarning (name + ", missing GameManager!");}

		if (isCinamatic) {
			rigidBody.velocity = cinamaticVelocity;
		}

		startPos = transform.position;
		startRot = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
//		float step = rotSpeed * Time.deltaTime;
//		Vector3 newDir = Vector3.RotateTowards (transform.forward, landingPos - transform.position, step, 0f);
//		Debug.DrawRay (transform.position, newDir, Color.red);
		if (!isCinamatic && updateHeli) {
			Vector3 landing = landingPos - transform.position;
			transform.rotation = Quaternion.LookRotation (landing + landingFoward);

			if (transform.position.z + approachingDist >= landingPos.z) {
				Debug.Log ("Called area reached, now landing.");
				if(landing.magnitude > slowingDownDist){
					Debug.Log ("Heli approaching.");
					rigidBody.velocity = landing.normalized * landingSpeed;
				}else if (landing.magnitude > landingDist) {
					if (lerp1<1) {lerp1 += Time.deltaTime/2;}
					Debug.Log ("Heli slowing down.");
					rigidBody.velocity = landing.normalized * Mathf.Lerp (landingSpeed, landingSpeed2, lerp1);
				} else if (landing.magnitude <= landingDist) {
					Debug.Log ("Landing complete");
					rigidBody.velocity = Vector3.zero;
					rigidBody.isKinematic = true;
					updateHeli = false;
				}
			} 
//			else {
//					Debug.Log ("landingPos is "+ landingPos +", Rotating");
//					//transform.eulerAngles = Vector3.RotateTowards (transform.forward, landingPos - transform.position, step, 0f);;
//					transform.rotation = Quaternion.LookRotation (landingPos - transform.position);
//			}
		}

	}

	public void ReSpawnReset(){
		isCalled = false;
		updateHeli = false;
		transform.position = startPos;
		transform.eulerAngles = startRot;
		rigidBody.velocity = Vector3.zero;
		rigidBody.isKinematic = false;
	}

	public void DispatchHelicopter(Vector3 landingSpot){	//
		if (!isCalled) {
			Debug.Log ("Helicopter called.");
			landingPos = landingSpot;
			rigidBody.velocity = new Vector3 (0f,0f,heliSpeed);
			updateHeli = true;
			isCalled = true;
		}
	}

	void OnTriggerStay(Collider obj){
		if (obj.gameObject.GetComponent<Player>()) {
			Debug.Log ("TouchDownTime Starts!");
			touchDownTime += Time.deltaTime;
			if (touchDownTime >= safeEscapeTime) {
				Debug.Log ("You Win!!");
				gameManager.NextLevel ();
			}
		}
	}

	void OnTriggerExit(Collider obj){
		if (obj.gameObject.GetComponent<Player>()) {
			touchDownTime = 0;
			Debug.Log ("Contect breaks. TouchDownTime Recount!");
		}
	}
}
