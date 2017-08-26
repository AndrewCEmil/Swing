using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour {

	private Orchestrator orchestrator;
	// Use this for initialization
	void Start () {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}

	void OnMouseDown(){
		orchestrator.StartPlayer ();
	}
}
