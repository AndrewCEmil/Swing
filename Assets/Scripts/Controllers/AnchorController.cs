using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorController : MonoBehaviour {

	private Orchestrator orchestrator;
	void Start() {
		gameObject.tag = "Anchor";
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
	}

	void OnMouseDown() {
		orchestrator.HandleShoot (gameObject);
	}
}
