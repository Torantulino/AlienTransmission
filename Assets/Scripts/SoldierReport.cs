using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierReport : MonoBehaviour {
	int numRays = 20;
	public float fieldOfView = 45;
	public float viewDistance = 5;


	void Update() {
		HashSet<GameObject> seen = new HashSet<GameObject>();

		for (int i = 0; i < numRays; i++) {
			float offset = (i - (numRays / 2)) / (float)numRays;
			float angle = offset * fieldOfView * 2;
			Vector3 rayDir = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
			Debug.Log(rayDir);

			Ray ray = new Ray(transform.position, rayDir);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, viewDistance)) {
				Debug.DrawLine(transform.position, transform.position + rayDir * viewDistance, Color.red);
			} else {
				Debug.DrawLine(transform.position, transform.position + rayDir * viewDistance);
			}
		}
	}
}
