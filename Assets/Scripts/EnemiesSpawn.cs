using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour {

	public GameObject Enemy1Prefab;
	public static int enemiesSpawnedCount = 0;

	[Tooltip("How many mobs should every spawn point have.")]
	public int minMobCountPerSP = 1; 

	[Tooltip("How far from player shall no mobs can spawn.")]
	public float minSpawnedDistance = 50f ;

	// Last Wave
	[Tooltip("Multiplies the mobs for each spawn point in last wave")]
	public int lastWaveSpawnCountMultiplier = 3;
	[Tooltip("Multiplies  whith minSpawnedDistance and reverse it to how far in between will spawn.")]
	public float lastWaveDistMultiplier = 2f;
	[Tooltip("How many seconds inbetween next mob spawn")]
	public float lastWaveSpawnRate = 1f;

	// Debug Tool
	public bool lastWaveDebugMode = false;

	private Transform[] spawnPoints;
	private Player player;
	private bool isLastWave = false;
	private float lastWaveTimer = 0;
	private MobAttack[] preSpawnMobs;


	void Awake(){
		preSpawnMobs = GameObject.FindObjectsOfType<MobAttack> ();
	}

	// Use this for initialization
	void Start () {
		if (FindObjectOfType<Player>()) {
			player = FindObjectOfType<Player>();
		}else{Debug.LogWarning(name + ", missing Player");}


		// this array includes the transform of the parent in [0].
		spawnPoints = transform.GetComponentsInChildren<Transform> ();
		//Debug.Log (spawnPoints.Length);
		enemiesSpawnedCount = 0;

		if (!lastWaveDebugMode) {
			isLastWave = false;
		} else {
			isLastWave = true;
		}

//		foreach (Transform point in spawnPoints) {
//			Debug.Log (point.position);
//		}
		//SpawnedFromPoints ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLastWave) {
			SpawnedFromPoints ();
		} else {
			SpawnedFromePointsLastWave ();
		}

	}

	void SpawnedFromPoints ()
	{
		if (enemiesSpawnedCount < spawnPoints.Length-1) {
			for (int i = 1; i < spawnPoints.Length; i++) {
				float distance = (spawnPoints[i].transform.position - player.transform.position).magnitude;
				int childCount = spawnPoints[i].transform.childCount;
				if (distance > minSpawnedDistance && childCount < minMobCountPerSP ){
					GameObject spawnedEnemy;
					Vector3 spawnPos = spawnPoints [i].position;

					spawnedEnemy = Instantiate (Enemy1Prefab);
					spawnedEnemy.transform.SetParent (spawnPoints [i].transform);
					UnityEngine.AI.NavMeshAgent agent = spawnedEnemy.GetComponent<UnityEngine.AI.NavMeshAgent> ();
					agent.Warp (spawnPos);
					enemiesSpawnedCount++;
					//Debug.Log (name + ", enemiesSpawnedCount is " + enemiesSpawnedCount);
				}	
			}
		}
	}

	void SpawnedFromePointsLastWave(){
		lastWaveTimer += Time.deltaTime;
		for (int i = 1; i < spawnPoints.Length; i++) {
			float distance = (spawnPoints[i].transform.position - player.transform.position).magnitude;
			int childCount = spawnPoints[i].transform.childCount;
			if (distance < minSpawnedDistance * lastWaveDistMultiplier && childCount < minMobCountPerSP*lastWaveSpawnCountMultiplier && lastWaveTimer >= lastWaveSpawnRate ){
				lastWaveTimer = 0;
				GameObject spawnedEnemy;
				Vector3 spawnPos = spawnPoints [i].position;
				spawnedEnemy = Instantiate (Enemy1Prefab);
				spawnedEnemy.transform.SetParent (spawnPoints [i].transform);

				UnityEngine.AI.NavMeshAgent agent = spawnedEnemy.GetComponent<UnityEngine.AI.NavMeshAgent> ();
				agent.Warp (spawnPos);

				MobAttack mobAttack = spawnedEnemy.GetComponent<MobAttack> ();
				mobAttack.LastWave ();

//				UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
//				ai = spawnedEnemy.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ();
//				ai.SetTarget (player.transform);

				enemiesSpawnedCount++;
				if (lastWaveDebugMode) {
					Debug.Log (enemiesSpawnedCount);
				}
				//Debug.Log (name + ", enemiesSpawnedCount is " + enemiesSpawnedCount);
			}	
		}
	}

	public void ActiveLastWave(){
		isLastWave = true;
		MobAttack[] mobAttacks = GameObject.FindObjectsOfType<MobAttack> ();	// GetComponentsInChildren<MobAttack> ();
		foreach (MobAttack mob in mobAttacks) {
			Vector3 playerPos = GameObject.FindObjectOfType<Player> ().transform.position;
			if ((mob.transform.position-playerPos).magnitude < minSpawnedDistance * lastWaveDistMultiplier) {
				mob.LastWave ();
			}

		}
	}

	public void ReSpawnReset(){
		isLastWave = false;
		MobAttack[] mobs = GetComponentsInChildren<MobAttack> ();
		foreach (MobAttack mob in mobs) {
			Destroy (mob.gameObject);
		}
		foreach (MobAttack mob in preSpawnMobs) {
			if (mob) {
				mob.MobNotAggro();
			} 
//			else {
//				Debug.Log (mob.name + ", is already killed.");
//			}
		}
		enemiesSpawnedCount = 0;
		//Destroy (GetComponentInChildren<MobAttack> ().gameObject);
	}
}
