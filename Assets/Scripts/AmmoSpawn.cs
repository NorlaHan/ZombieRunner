using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawn : MonoBehaviour {

	public GameObject ammo1Prefab;
	public static int ammoSpawnedCount = 0;
	public int minAmmoCountPerSP = 1;
	public float minSpawnedDistance = 100f;

	private Transform[] spawnPoints;
	private Player player;

	// Use this for initialization
	void Start () {
		if (FindObjectOfType<Player>()) {
			player = FindObjectOfType<Player>();
		}else{Debug.LogWarning(name + ", missing Player");}
		// this array includes the transform of the parent in [0].
		spawnPoints = transform.GetComponentsInChildren<Transform> ();
		ammoSpawnedCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		SpawnedFromPoints ();
	}

	void SpawnedFromPoints ()
	{
		if (ammoSpawnedCount < spawnPoints.Length-1) {
			for (int i = 1; i < spawnPoints.Length; i++) {
				float distance = (spawnPoints[i].transform.position - player.transform.position).magnitude;
				int childCount = spawnPoints[i].transform.childCount;
				if (distance > minSpawnedDistance && childCount < minAmmoCountPerSP ){
					GameObject spawnedAmmo;
					Vector3 spawnPos = spawnPoints [i].position;
					spawnedAmmo = Instantiate (ammo1Prefab,spawnPos,Quaternion.identity);
					spawnedAmmo.transform.SetParent (spawnPoints [i].transform);
					
					ammoSpawnedCount++;
					//Debug.Log (name + ", ammoSpawnedCount is " + ammoSpawnedCount);
				}	
			}
		}
	}
}
