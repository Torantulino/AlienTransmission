﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoldierReport : MonoBehaviour {
	public int numRays = 20;
	public float fieldOfView = 45;
	public float viewDistance = 5;
	public float additionalReportChance = 0.5f;
	public float maxReports = 3;

	void Update() {
		if (Random.value < 0.01) {
			Debug.Log("REPORT: ");
			foreach (var report in Report()) {
				Debug.Log(report);
			}
		}
	}

	public List<string> Report() {
		HashSet<Interesting> seen = new HashSet<Interesting>();

		for (int i = 0; i < numRays; i++) {
			float offset = (i - (numRays / 2)) / (float)numRays;
			float angle = offset * fieldOfView * 2;
			Vector3 rayDir = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;

			Ray ray = new Ray(transform.position, rayDir);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, viewDistance)) {
				Debug.DrawLine(transform.position, transform.position + rayDir * viewDistance, Color.red);
				var interesting = hit.collider.GetComponent<Interesting>();
				if (interesting) {
					seen.Add(interesting);
				}
			} else {
				Debug.DrawLine(transform.position, transform.position + rayDir * viewDistance);
			}
		}

		// TODO: Priority for interesting things.
		// Report highest priority.
		// Randomise reporting other things priority order.

		var counts = seen.GroupBy(item => item.Type).Select(group => new {
			Type = group.Key,
			Count = group.Count(),
			Priority = group.ElementAt(0).priority
		});
		counts = counts.OrderByDescending(item => item.Priority);

		var reports = new List<string>();

		foreach (var count in counts) {
			if (reports.Count > 0) {
				if (Random.value < additionalReportChance || reports.Count >= maxReports) {
					break;
				}
			}
			reports.Add(transform.name + " saw " + count.Count + " " + count.Type);
		}

		return reports;
	}
}
