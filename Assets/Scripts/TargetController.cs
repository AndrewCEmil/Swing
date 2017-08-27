using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	private Orchestrator orchestrator;
	void Start () {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.name == "Player") {
			orchestrator.TargetHit ();
		}
	}
}
