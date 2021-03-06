﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Health : MonoBehaviour {

	public int maxHP = 1;
    public bool isDead;
    int hp;
	public AudioClip deathSound;
    public Animator soldierAnimator;
    AudioSource source;

	void Start() {
		hp = maxHP;
        isDead = false;
		source = GetComponent<AudioSource>();
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
		if (isDead) {
			return;
		}
        isDead = true;

		if (deathSound) {
			source.PlayOneShot(deathSound);
		}

	    if (GetComponent<AlienAI>())
	    {
	        var alienAi = GetComponent<AlienAI>();
	        alienAi.canAct = false;

	        var interesting = GetComponent<Interesting>();
	        interesting.Type = "dead " + interesting.Type;
	        interesting.priority -= 10;
	    }
	    else
	    {
            Debug.Log("Soldier DED!");
            soldierAnimator.SetBool("isMIA", true);
	        this.gameObject.GetComponentInChildren<LineRenderer>().enabled = false;
        }

        
	}

    public void Revive()
    {
        hp = maxHP;
        isDead = false;
        soldierAnimator.SetBool("isDowned", false);
        this.gameObject.GetComponentInChildren<LineRenderer>().enabled = true;
    }
}
