using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugKeys : MonoBehaviour {

	void Start() {
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.F1)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (Input.GetKeyDown(KeyCode.Pause)) {
			SceneManager.LoadScene("StartView");
		}
	}
}
