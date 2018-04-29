using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour {

	public float clearAreaCheckTime = 5f;

	public float timeSinceLastTrigger = -1;
	public bool foundClearArea = false, isOpenSpace = false; //isAreaClear = true

	// Update is called once per frame
	void Update () {
		if (!foundClearArea && isOpenSpace == true) {
			timeSinceLastTrigger += Time.deltaTime;
		}
		if (timeSinceLastTrigger >clearAreaCheckTime && !foundClearArea && isOpenSpace == true) {
			//Debug.Log (name + " SendMessageUpwards = OnFindClearArea");
			SendMessageUpwards ("OnFindClearArea"); // Receive : Player , InnerVoice
			foundClearArea = true;
		}
	}

	void OnTriggerStay(Collider collider){
		if (collider.GetComponent<OpenSpace>() && !isOpenSpace) {
			isOpenSpace = true;
		}else if (collider.tag != "Player" && collider.tag != "Mob") {
			timeSinceLastTrigger = 0;
			//Debug.Log (collider.name + ", Area not clear");
		}
//		else{
//			Debug.Log (collider.name);
//		}
	}

	void OnTriggerExit(Collider collider){
		if (collider.GetComponent<OpenSpace>()) {
			isOpenSpace = false;
		}
	}

	void ReSpawnReset(){
		//Debug.Log (name + ", receive message ReSpawnReset");
		foundClearArea = false;
	}

}
