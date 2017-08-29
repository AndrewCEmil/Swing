using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

	// Use this for initialization
	private Orchestrator orchestrator;
	void Start () {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger with Floor");
		if (other.name == "Player") {
			orchestrator.TargetDied ();
		}
	}
}
