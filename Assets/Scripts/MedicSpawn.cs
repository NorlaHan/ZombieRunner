using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicSpawn : MonoBehaviour {

	public GameObject medicPrefab;
	public static int medicSpawnedCount = 0;
	public int minMedicCountPerSP = 1;
	public float minSpawnedDistance = 75f;

	private Transform[] spawnPoints;
	private Player player;

	// Use this for initialization
	void Start () {
		if (FindObjectOfType<Player>()) {
			player = FindObjectOfType<Player>();
		}else{Debug.LogWarning(name + ", missing Player");}
		// this array includes the transform of the parent in [0].
		spawnPoints = transform.GetComponentsInChildren<Transform> ();
		medicSpawnedCount = 0;
	}

	// Update is called once per frame
	void Update () {
		SpawnedFromPoints ();
	}

	void SpawnedFromPoints ()
	{
		if (medicSpawnedCount < spawnPoints.Length-1) {
			for (int i = 1; i < spawnPoints.Length; i++) {
				float distance = (spawnPoints[i].transform.position - player.transform.position).magnitude;
				int childCount = spawnPoints[i].transform.childCount;
				if (distance > minSpawnedDistance && childCount < minMedicCountPerSP ){
					GameObject spawnedMedic;
					Vector3 spawnPos = spawnPoints [i].position;
					spawnedMedic = Instantiate (medicPrefab,spawnPos,Quaternion.identity);
					spawnedMedic.transform.SetParent (spawnPoints [i].transform);

					medicSpawnedCount++;
					//Debug.Log (name + ", medicSpawnedCount is " + medicSpawnedCount);
				}	
			}
		}
	}
}
