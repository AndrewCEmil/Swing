using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {

	GameObject player;
	GameObject baseLink;
	Rope rope;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		baseLink = GameObject.Find ("BaseLink");
		rope = player.GetComponent<Rope> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HandleAnchorClick(GameObject anchor) {
		rope.Construct (anchor, player, baseLink);
	}
}
