using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void Begin() {
		SceneManager.LoadScene("MainView");
	}
	public void BeginRandom() {
		SceneManager.LoadScene("MainView RNG edition");
	}
}
