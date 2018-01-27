﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int maxHP = 1;
    public bool isDead;
    int hp;

	void Start() {
		hp = maxHP;
        isDead = false;

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
        isDead = true;

        if (GetComponent<AlienAI>())
        {
            var alienAi = GetComponent<AlienAI>();
            alienAi.canAct = false;
        }
		//TODO: Do something.
	}
}
