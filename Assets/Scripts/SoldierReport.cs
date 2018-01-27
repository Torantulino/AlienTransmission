﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierReport : MonoBehaviour {
	public int numRays = 20;
	public float fieldOfView = 45;
	public float viewDistance = 5;


	void Update() {
		Report();
	}

	public void Report() {
		HashSet<GameObject> seen = new HashSet<GameObject>();

		for (int i = 0; i < numRays; i++) {
			float offset = (i - (numRays / 2)) / (float)numRays;
			float angle = offset * fieldOfView * 2;
			Vector3 rayDir = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;

			Ray ray = new Ray(transform.position, rayDir);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, viewDistance)) {
				Debug.DrawLine(transform.position, transform.position + rayDir * viewDistance, Color.red);
				if (hit.collider.GetComponent<Interesting>()) {
					seen.Add(hit.collider.gameObject);
				}
			} else {
				Debug.DrawLine(transform.position, transform.position + rayDir * viewDistance);
			}
		}

		// TODO: Priority for interesting things.
		// Report highest priority.
		// Randomise reporting other things priority order.

		foreach(var thing in seen) {
			Debug.Log(transform.name + " saw " + thing.GetComponent<Interesting>().name);
		}
	}
}