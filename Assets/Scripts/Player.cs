using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool reSpawn = false , debugMode = false;
	public Helicopter helicopter;
	public GameObject landingAreaPrefab, projectilePrefab, currentTarget;
	public float fistDamage = 50f , fullStamina = 100f , staminaRecoveryRate = 10f, punchCost = 15f, runningCost = 3f, fatalFallingHeight = 30f;

	private float currentStamina;
	private UIPlayerStatusBar uiPlayerStatusBar;
	private CharacterController characterController;
	private float lastPosY, lastFallTime;
	public float fallDistance = 0f;
	private Health health;

	// Respawn related
	private	GameObject playerSpawnPoints;
	private EnemiesSpawn enemiesSpawn;
	private bool lastRespawnToggle = false, isFirstSpawn = true;
	private Transform[] spawnPoints;
	private Vector3 degbugPos;
	private UIManager uiManager;

	// Flare related
	private Vector3 flarePos;
	private GameObject clearArea, lastLandingArea;

	// Weapon related
	public float fire1Rate = 0.5f;
	public int fire1ammo = 99;
	private GameObject firePos, playerProjectiles;	// , uiHelp, uiHelpPrompt;
	private float fire1 = 0;
	private UIAmmo uiAmmo;
	private Animator animator, gunAnimator;


	// Use this for initialization
	void Start () {
		if (GameObject.Find ("PlayerSpawnPoints")) {
			playerSpawnPoints = GameObject.Find ("PlayerSpawnPoints");
		} else {Debug.LogWarning (name + ", PlayerSpawnPoints Missing!");}
		if (GameObject.Find("ClearArea")) {
			clearArea = GameObject.Find ("ClearArea");
		}else {Debug.LogWarning (name + ", missing ClearArea!");}
		if (GameObject.Find ("PlayerProjectiles")) {
			playerProjectiles = GameObject.Find ("PlayerProjectiles");
		}else {Debug.LogWarning (name +", PlayerProjectiles MissingComponentException!");}
		if (GameObject.Find("PlayerGun")) {
			gunAnimator = GameObject.Find ("PlayerGun").GetComponent<Animator> ();
		}else {Debug.LogWarning (name +", missing gunAnimator!");}


		if (GameObject.FindObjectOfType<UIPlayerStatusBar>()) {
			uiPlayerStatusBar = GameObject.FindObjectOfType<UIPlayerStatusBar> ();
		}else {Debug.LogWarning ( name + ", missing UIHealth!");}
		if (GameObject.FindObjectOfType<UIManager>()) {
			uiManager = GameObject.FindObjectOfType<UIManager> ();
		}else {Debug.LogWarning ( name + ", missing UIManager!");}

		if (GameObject.FindObjectOfType<FirePos> ()) {
			firePos = GameObject.FindObjectOfType<FirePos> ().gameObject;
		} else {Debug.LogWarning (name +", FirePos missing!");}	

		if (FindObjectOfType<UIAmmo> ()) {
			uiAmmo = FindObjectOfType<UIAmmo> ();
		}else {Debug.LogWarning (name + ", missing UIAmmo!");}

		if (GameObject.FindObjectOfType<EnemiesSpawn> ()) {
			enemiesSpawn = GameObject.FindObjectOfType<EnemiesSpawn> ();
		}else {Debug.LogWarning (name + ", missing UIAmmo!");}

		if (GetComponent<Animator>()) {
			animator = GetComponent<Animator>();
		}else {Debug.LogWarning (name + ", missing Animator!");}

		if (GetComponent<CharacterController>()) {
			characterController = GetComponent<CharacterController>();
		}else {Debug.LogWarning (name + ", missing CharacterController!");}

		if (GetComponent<Health>()) {
			health = GetComponent<Health>();
		}else {Debug.LogWarning (name + ", missing Health!");}

		spawnPoints = playerSpawnPoints.GetComponentsInChildren<Transform> ();

		currentStamina = fullStamina;
		lastPosY = transform.position.y;

		if (debugMode) {
			degbugPos = transform.position;
		}

		ReSpawn ();
	}
	
	// Update is called once per frame
	void Update () {

		// Respawn
		if (lastRespawnToggle != reSpawn) {
			reSpawn = false;
			ReSpawn ();
			helicopter.ReSpawnReset ();
		}

		// Stamina Recovery.
		if (currentStamina < fullStamina) {
			currentStamina += (staminaRecoveryRate * Time.deltaTime);
			// Update Stamina UI
			UpdateStaminaUI ();
		}

		// Reload counter
		if (fire1 < fire1Rate) {
			fire1 += Time.deltaTime;
		}

		// Fall damage detection
		if (!characterController.isGrounded) {
			fallDistance += (lastPosY - transform.position.y);
			lastPosY = transform.position.y;
			//lastFallTime = Time.time;
			//Debug.Log (fallDistance);
		} else {
			if (fallDistance > fatalFallingHeight/5f) {
				if (fallDistance > fatalFallingHeight) {
					health.TakeDamage (health.fullHealth);
				} else if (fallDistance > fatalFallingHeight/2f) {
					health.TakeDamage (health.fullHealth*0.5f);
				} else if (fallDistance >fatalFallingHeight/4f ) {
					health.TakeDamage (health.fullHealth*0.25f);
				} else {
					health.TakeDamage (health.fullHealth*0.1f);
				}

				Debug.LogWarning (fallDistance + "meters falls. Player takes damage!");
				fallDistance = 0;
			} else {
				fallDistance = 0;
			}
		}


		// Fire !!
		if (Input.GetButton("Fire1")) {
			GameObject bullet;
			if (fire1 >= fire1Rate) {
				fire1 = 0;
				if (fire1ammo <= 0) {
					BroadcastMessage ("Gun1NoAmmo");
					Debug.Log ("Out of ammo");
					if (debugMode) {
						PickUpammo (13);
					}
					return;
				}
				bullet = Instantiate (projectilePrefab, firePos.transform.position, firePos.transform.rotation);
				bullet.GetComponent<Projectile> ().Fire (firePos.transform.forward);
				bullet.transform.SetParent (playerProjectiles.transform);
				BroadcastMessage ("FireGun1");
				fire1ammo -= 1;
			}
		}

		// Aimming crosshair
		if (Input.GetButton ("Fire3")) {
			if (!uiAmmo.crossHairEnabled()) {
				uiAmmo.ShowCrossHair ();
			}

		} else {
			uiAmmo.HideCrossHair ();
		}

		// Punch Attack
		if (currentStamina > punchCost && Input.GetButton ("Fire2")) {
			// Fist animation.
			animator.SetBool ("isPunching", true);		
		} else {
			animator.SetBool ("isPunching", false);	
		}

		// Drain stamina when running.
		if (Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) {
			gunAnimator.SetBool ("isMoving", true);
			if (Input.GetKey (KeyCode.LeftShift)) {
				gunAnimator.speed = 2;
				if (currentStamina > 0) {
					currentStamina -= Time.deltaTime * runningCost;
					UpdateStaminaUI ();
				}
			} else {
				gunAnimator.speed = 1;
			}
		} else {
			gunAnimator.SetBool ("isMoving", false);
		}

	}

	// Set target to which player collide.
	void OnCollisionStay(Collision obj){
		GameObject target = obj.gameObject;
		if (target.GetComponent<MobAttack>()) {
			currentTarget = target;
		}
	}

	// Release only if current target left.
	void OnCollisionExit(Collision obj){
		GameObject target = obj.gameObject;
		if (target == currentTarget) {
			currentTarget = null;
		}
	}

	void PunchAttack(){
		BroadcastMessage ("FistPunch");
		currentStamina -= punchCost;
		UpdateStaminaUI ();
		if (currentTarget) {
			currentTarget.GetComponent<Health> ().TakeDamage (fistDamage);
			BroadcastMessage ("FistSplash");
			Debug.Log (name + ", deals " + fistDamage + " damages to " + currentTarget);
		} else {
			Debug.Log ("No target to puch.");
		}
	}

	void UpdateStaminaUI ()
	{
		uiPlayerStatusBar.UpdateStaminaBar (currentStamina / fullStamina);
	}

	void ReSpawn(){
		if (!debugMode) {
			int spawnIndex = Random.Range (1, spawnPoints.Length);
			Debug.Log ("SpawnPoint (" + (spawnIndex - 1) + ").");
			transform.position = spawnPoints [spawnIndex].transform.position;
		} else {
			transform.position = degbugPos;
		}

		lastPosY = transform.position.y;
		fallDistance = 0;
		currentStamina = fullStamina;
		if (fire1ammo < 13) {fire1ammo = 13;}
		enemiesSpawn.ReSpawnReset ();
		Destroy (lastLandingArea);
		if (isFirstSpawn) {
			isFirstSpawn = false;
		} else {
			uiManager.OnBlackOut ();
			BroadcastMessage ("ReSpawnReset");
		}

	}

	// This is a BroadCast method from clear area. 
	void OnFindClearArea(){
		//Debug.Log (name +", receive OnFindClearArea");
		flarePos = transform.position;
		Invoke ("DropFlare", 1);
		// Deploy flare
		// Start spawning zombies
	}

	void DropFlare ()
	{
		// Drop a flare.
		lastLandingArea = Instantiate(landingAreaPrefab,flarePos,Quaternion.identity);
		enemiesSpawn.ActiveLastWave ();
		Debug.LogWarning (name + ", Last Wave Activated!!!");
	}

	void OnDispatchHelicopter(){		// Received from : RadioSystem
		//Debug.Log (name +", receive OnDispatchHelicopter");
		helicopter.DispatchHelicopter (flarePos);
	}


	public int Fireammo (){
		// TODO make multiple ammo type
		return fire1ammo;
	}

	public void PickUpammo (int ammoRound){
		// TODO make multiple ammo type
		fire1ammo += ammoRound;
	}

	public void PickUpMedic (float healAmount){
		health.TakeHeal (healAmount);
		currentStamina = Mathf.Clamp (currentStamina + 50f, 0 , fullStamina);
		UpdateStaminaUI ();
	}

	public void OnRidingCableCar(){
		fallDistance = 0;
		lastPosY = transform.position.y;
	}

	public void OnPlayerUnderWater(){
		BroadcastMessage ("OnPlayerDrowning");
	}

	public void OnPlayerOutOfWater(){
		BroadcastMessage ("OnPlayerNotDrowning");
	}
}
