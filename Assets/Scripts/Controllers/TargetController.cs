using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	private Orchestrator orchestrator;
	void Start () {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger with Target");
		if (other.name == "Player") {
			orchestrator.TargetHit ();
		}
	}

}
