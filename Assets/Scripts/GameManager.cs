using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject musicManagerPrefab;
	public bool autoNext = false;
	public float autoTimer = 6f;

	private int sceneIndex, screenW, screenH;
	private CursorLockMode cursorLockMode;
	private MusicManager musicManager;


	void Awake(){
		
		if (!GameObject.FindObjectOfType<MusicManager> ()) {
			Instantiate (musicManagerPrefab, Vector3.zero,Quaternion.identity);
		}
	}

	// Use this for initialization
	void Start () {
		if (autoNext) {
			Invoke ("NextLevel", autoTimer);
		}

		//Debug.Log (Cursor.lockState);

		if (Cursor.lockState != CursorLockMode.None ) {
			Cursor.lockState = CursorLockMode.None;
		}

		screenW = Screen.width;
		screenH = Screen.height;

		if ((screenW / screenH) != (16 / 9)) {
			Screen.SetResolution (screenW, (screenW/16*9),Screen.fullScreen,60);
			Debug.Log ("Screen.resolutions is not 16:9, full screen and change to " + screenW + ", "+(screenW/16*9) );
		}


//		if (Screen.fullScreen == true) {
//			Screen.SetResolution (1600, 900, true, 60);
//			Debug.Log ("fullScreen, change resolution to 1600 900");
//		}

		sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		musicManager =  GameObject.FindObjectOfType<MusicManager> ();
		Debug.Log ("Find MusicManager");
		musicManager.ChangeBGM (sceneIndex);
	}

	void Update(){
		if (Input.GetButtonDown("MouseCursor")) {
			Debug.Log (Cursor.lockState);
			Debug.Log ("Toggle mouse cursor");

			if (Cursor.lockState != CursorLockMode.None) {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			} else {
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}


	public void NextLevel(){
		int totalScene = SceneManager.sceneCountInBuildSettings;
		sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		if (sceneIndex + 1 >= totalScene) {
			SceneManager.LoadScene (0);
		} else {
			SceneManager.LoadScene (sceneIndex + 1);
		}
	}

	public void LoadLevel(string levelName){
		//SceneManager.GetSceneByName (levelName);
		SceneManager.LoadScene (levelName);
		Debug.Log (SceneManager.GetSceneByName (levelName));
	}

	public int CurrentLevel (){
		return SceneManager.GetActiveScene ().buildIndex;
	}
}
