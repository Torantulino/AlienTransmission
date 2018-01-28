using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int maxHP = 1;
    public bool isDead;
    int hp;
	public AudioClip deathSound;
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
	}

    public void Revive()
    {
        hp = maxHP;
        isDead = false;
    }
}
