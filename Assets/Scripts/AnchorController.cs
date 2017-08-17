using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorController : MonoBehaviour {

	private Orchestrator orchestrator;
	private bool mouseOn;
	void Start() {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
		mouseOn = false;
	}

	void OnMouseDown(){
		orchestrator.HandleAnchorClick (gameObject);
	}
}
