using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {

	GameObject player;
	GameObject baseLink;
	Grappler grappler;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		baseLink = GameObject.Find ("BaseLink");
		grappler = player.GetComponent<Grappler> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HandleShoot(GameObject anchor) {
		grappler.Shoot (anchor);
	}
}
