using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int maxHP = 1;
	int hp;

	void Start() {
		hp = maxHP;
	}

	public void Damage() {
		if (hp > 0) {
			hp--;
			if (hp == 0) {
				Debug.Log(gameObject.name + " was killed.");
				Kill();
			}
		}
	}

	void Kill() {
		//TODO: Do something.
	}
}
