using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
	public int numRays = 20;
	public float shotAngle = 90;
	public float range = 10;

	public AudioClip fireSound;
	AudioSource source;

	void Start() {
		source = GetComponent<AudioSource>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.F4)) {
			Fire();
		}
	}

	public void Fire() {
		if (!source.isPlaying) {
			source.PlayOneShot(fireSound);
		}
		var targets = new HashSet<Health>();

		for (int i = 0; i < numRays; i++) {
			float offset = (i - (numRays / 2)) / (float)numRays;
			float angle = offset * shotAngle * 2;
			Vector3 rayDir = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;

			Ray ray = new Ray(transform.position, rayDir);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, range)) {
				Debug.DrawLine(transform.position, transform.position + rayDir * range, Color.red);
				var health = hit.collider.GetComponent<Health>();
				var soldier = hit.collider.GetComponent<SoldierCommands>();
				if (health && !soldier) {
					targets.Add(health);
				}
			} else {
				Debug.DrawLine(transform.position, transform.position + rayDir * range);
			}
		}

		float minDistance = float.PositiveInfinity;
		Health closest = null;
		foreach (var health in targets) {
			float dist = Vector3.Distance(health.transform.position, transform.position);
			if (dist < minDistance) {
				minDistance = dist;
				closest = health;
			}
		}

		if (closest) {
			closest.Damage();
		}
	}
}
