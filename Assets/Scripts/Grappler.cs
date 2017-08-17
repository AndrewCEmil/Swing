using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour {

	private GameObject arrow;
	private GameObject player;
	private Vector3 connectedAnchorPosition;

	// Use this for initialization
	void Start () {
		arrow = GameObject.Find ("Arrow");
		arrow.SetActive (false);
		player = GameObject.Find ("Player");
		connectedAnchorPosition = new Vector3 (0, -0.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//TODO shoot should be where the player is LOOKING, but for now its the target they click
	public void Shoot(GameObject anchor) {
		ShootArrow(anchor);
		//2) Set mode to shooting
	}

	public void AttachArrow (GameObject anchor) {
		arrow.AddComponent<HingeJoint> ();
		HingeJoint hinge = arrow.GetComponent<HingeJoint> ();
		hinge.autoConfigureConnectedAnchor = false;
		hinge.connectedAnchor = connectedAnchorPosition;
		hinge.connectedBody = anchor.GetComponent<Rigidbody> ();
		hinge.enableCollision = true;
	}

	void ShootArrow(GameObject anchor) {
		arrow.SetActive (true);
		arrow.transform.position = player.transform.position;
		arrow.transform.LookAt (anchor.transform.position);

		Vector3 theForwardDirection = arrow.transform.TransformDirection (Vector3.forward);
		Rigidbody arrowRb = arrow.GetComponent<Rigidbody> ();
		arrowRb.velocity = theForwardDirection * 100;
	}
}
