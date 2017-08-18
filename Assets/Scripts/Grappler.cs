using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrapplerMode
{
	Off,
	Shooting,
	Attached
}

public class Grappler : MonoBehaviour {

	private GameObject arrow;
	private GameObject player;
	private GameObject baseLink;
	private GameObject plane;
	private Vector3 connectedAnchorPosition;
	private GrapplerMode mode;
	private GameObject lastLink;
	private float linkDistance;

	// Use this for initialization
	void Start () {
		arrow = GameObject.Find ("Arrow");
		arrow.SetActive (false);
		player = GameObject.Find ("Player");
		baseLink = GameObject.Find ("BaseLink");
		plane = GameObject.Find ("Plane");
		connectedAnchorPosition = new Vector3 (0, -0.5f, 0);
		mode = GrapplerMode.Off;
		linkDistance = 1.2f;
	}
	
	// Update is called once per frame
	void Update () {
		switch (mode) {
		case GrapplerMode.Off:
			break; //noop
		case GrapplerMode.Shooting:
			DoShooting ();
			break;
		case GrapplerMode.Attached:
			break; //noop
		}
	}

	//TODO shoot should be where the player is LOOKING, but for now its the target they click
	public void Shoot(GameObject anchor) {
		ShootArrow(anchor);
		lastLink = arrow;
		mode = GrapplerMode.Shooting;
	}

	public void AttachArrow (GameObject anchor) {
		arrow.AddComponent<HingeJoint> ();
		HingeJoint hinge = arrow.GetComponent<HingeJoint> ();
		hinge.autoConfigureConnectedAnchor = false;
		hinge.connectedAnchor = connectedAnchorPosition;
		hinge.connectedBody = anchor.GetComponent<Rigidbody> ();
		//hinge.enableCollision = true;

		mode = GrapplerMode.Attached;
	}

	private void DoShooting() {
		if(Vector3.Distance(player.transform.position, lastLink.transform.position) > linkDistance) {
			AddLink();
		}
	}

	void AddLink() {
		GameObject link = Instantiate (baseLink);
		//TODO this doesnt seem right
		Vector3 direction = (lastLink.transform.position - player.transform.position).normalized;
		Vector3 newLinkPos = lastLink.transform.position - direction;
		link.transform.position = newLinkPos;
		link.AddComponent<HingeJoint> ();

		HingeJoint hinge = link.GetComponent<HingeJoint> ();
		hinge.autoConfigureConnectedAnchor = false;
		hinge.connectedAnchor = connectedAnchorPosition;
		hinge.connectedBody = lastLink.GetComponent<Rigidbody> ();
		//hinge.enableCollision = true;

		lastLink = link;
	}

	void ShootArrow(GameObject anchor) {
		arrow.SetActive (true);
		arrow.transform.LookAt (anchor.transform.position);
		arrow.transform.position = player.transform.position;
		Vector3 direction = (anchor.transform.position - player.transform.position).normalized;

		Rigidbody arrowRb = arrow.GetComponent<Rigidbody> ();
		arrowRb.AddForce(direction * 10000);
		Physics.IgnoreCollision(arrow.GetComponent<Collider>(), player.GetComponent<Collider>());
		Physics.IgnoreCollision(arrow.GetComponent<Collider>(), plane.GetComponent<Collider>());
	}
}
