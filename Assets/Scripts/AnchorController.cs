using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorController : MonoBehaviour {

	private Orchestrator orchestrator;
	void Start() {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}

	void OnMouseDown(){
		orchestrator.HandleAnchorClick (gameObject);
	}   
}
