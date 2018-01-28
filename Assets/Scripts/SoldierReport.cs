using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoldierReport : MonoBehaviour {
	public int numRays = 20;
	public float fieldOfView = 45;
	public float viewDistance = 5;
	public float additionalReportChance = 0.5f;
	public float maxReports = 3;

	UIIOMan uiioMan;
	GridScript grid;
	Health health;

	void Start() {
		uiioMan = GameObject.FindObjectOfType<UIIOMan>();
		grid = GameObject.FindObjectOfType<GridScript>();
		health = GetComponent<Health>();
	}

	void Update() {
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
                    var health = hit.collider.GetComponent<Health>();
                    if (health != null)
                    {
                        seen.Add(interesting);
                    }
				}
			} else {
				Debug.DrawLine(transform.position, transform.position + rayDir * viewDistance);
			}
		}

		var counts = seen.GroupBy(item => item.Type).Select(group => new {
			Type = group.Key,
			Count = group.Count(),
			Priority = group.ElementAt(0).priority,
			FirstPos = group.ElementAt(0).transform.position
		});
		counts = counts.OrderByDescending(item => item.Priority);

		var reports = new List<string>();

        foreach (var count in counts) {
			if (reports.Count > 0) {
				if (Random.value < additionalReportChance || reports.Count >= maxReports) {
					break;
				}
			}

			if (health.isDead) {
				reports.Add(Garble(Garble(Garble(Garble(Garble(Garble("AAARARRRRRRGGGGHHHHH!!!!!!!!!")))))));
			} else {
				string report;
				if (count.Count == 1) {
					string gridPos = grid.CoordsToGridString(grid.WorldCoordsToGrid(count.FirstPos));
					report = transform.name + " saw a " + count.Type + " at " + gridPos;
				} else {
					report = transform.name + " saw " + count.Count + " " + count.Type;
				}

				reports.Add(Garble(report));
			}
		}

		uiioMan.Report(reports, GetComponent<SoldierInfo>().name);

		return reports;
	}

	string Garble(string original) {
		int nlen = 0;
		int pos;

		string result = original;
		// Add randomised distortion to a string.
		string[] sn1 = { "&", "$", "?", "#", "!", "@" };
		string[] sn3 = { "???", "#$1", "***" };
		string[] sn5 = { "<static>", "*HISS*", "<crackle>" };
		string noise = "";
		int ierr;
		ierr = Random.Range(-1, result.Length * 2 / 3);
		while (ierr > 0)
		{
			ierr -= 1;
			nlen = 1;
			noise = sn1[Random.Range(0, 6)];
			pos = Random.Range(0, result.Length - nlen);
			result = result.Substring(0, pos) + noise + result.Substring(pos + nlen, result.Length - pos - nlen);

		}
		ierr = Random.Range(-1, result.Length / 12);
		while (ierr > 0)
		{
			ierr -= 1;
			nlen = 3;
			noise = sn3[Random.Range(0, 3)];
			pos = Random.Range(0, result.Length - nlen);
			result = result.Substring(0, pos) + noise + result.Substring(pos + nlen, result.Length - pos - nlen);

		}
		ierr = Random.Range(-1, result.Length / 20);
		while (ierr > 0)
		{
			if (Random.value < 0.5) break;
			ierr -= 1;
			nlen = 6;
			noise = sn5[Random.Range(0, 3)];
			pos = Random.Range(0, result.Length - nlen);
			result = result.Substring(0, pos) + noise + result.Substring(pos + nlen, result.Length - pos - nlen);
		}

		return result;
	}
}
