using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {
	private Orchestrator orchestrator;
	private void Awake () {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger with Floor");
		if (other.name == "Player") {
			orchestrator.TargetDied ();
		}
	}
}
