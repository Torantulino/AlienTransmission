﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienAI : MonoBehaviour {
	public bool canAct;
	public float detectionRange = 10;

	NavMeshAgent agent;
	Transform target;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update() {
		agent.enabled = canAct;
		if (canAct) {
			if (target) {
				agent.destination = target.position;
				if (Vector3.Distance(target.position, transform.position) > detectionRange) {
					// Target is too far away, lose track of them but continue going to previous position.
					target = null;
				}
			} else {
				// No target, find a new one
				foreach (var collider in Physics.OverlapSphere(transform.position, detectionRange)) {
					var soldier = collider.GetComponent<SoldierReport>();
					if (soldier) {
						target = soldier.transform;
						break;
					}
				}
			}
		}

	}
}