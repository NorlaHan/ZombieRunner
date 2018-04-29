using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSpace : MonoBehaviour {
	public Color gizmosColor;

	private Vector3 gizmosSize;

	void OnDrawGizmos(){
		Gizmos.color = gizmosColor;
		gizmosSize = GetComponent<BoxCollider> ().size;
		Gizmos.DrawWireCube (transform.position, gizmosSize);
	}
}
