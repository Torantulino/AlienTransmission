using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

	void Start() {
	}

	public void Begin() {
		SceneManager.LoadScene("MainView");
	}
}
