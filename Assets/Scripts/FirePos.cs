using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePos : MonoBehaviour {

	public Vector3 gizmosSize = new Vector3 (0.1f, 0.1f, 0.1f);

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (transform.position, gizmosSize);
	}


}
