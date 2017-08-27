using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {

	private GameObject player;
	private Rigidbody playerRb;
	private GameObject baseLink;
	private Grappler grappler;
	private Vector3 startForce;
	private LevelController levelController;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerRb = player.GetComponent<Rigidbody> ();
		baseLink = GameObject.Find ("BaseLink");
		grappler = player.GetComponent<Grappler> ();
		startForce = new Vector3 (1000, 0, 0);
		levelController = GameObject.Find ("LevelObj").GetComponent<LevelController> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void HandleShoot(GameObject anchor) {
		grappler.Shoot (anchor);
	}

	public void StartLevel() {
		LoadLevel ();
		//Just kick the player
		playerRb.AddForce(startForce);
	}

	public void LoadLevel() {
		levelController.LoadLevel ();
	}
}
