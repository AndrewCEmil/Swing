using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColliderController : MonoBehaviour {

	// Use this for initialization
	private Orchestrator orchestrator;
	void Start () {
		orchestrator = Utils.GetOrchestrator ();
	}
	
	// Update is called once per frame
		
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger with Floor");
		if (other.name == "Player") {
			orchestrator.TargetDied ();
		}
	}
}
