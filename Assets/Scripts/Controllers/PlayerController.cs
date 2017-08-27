using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Orchestrator orchestrator;
	// Use this for initialization
	void Start () {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		//orchestrator.HandleShoot();
	}
}
