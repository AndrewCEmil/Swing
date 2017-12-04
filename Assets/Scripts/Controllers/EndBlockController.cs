using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlockController : MonoBehaviour {

	GameObject player;
	Rigidbody playerRb;
	Grappler grappler;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerRb = player.GetComponent<Rigidbody> ();
		grappler = player.GetComponent<Grappler> ();
	}

	public void ResetPlayer() {
		player.transform.position = transform.position - new Vector3 (0, 20, 0);
		playerRb.velocity = Vector3.zero;
		playerRb.angularVelocity = Vector3.zero;
		grappler.Attach (gameObject);
	}
}
