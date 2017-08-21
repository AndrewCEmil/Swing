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
	private Vector3 anchorPosition;
	private Vector3 axis;
	private Vector3 secondaryAxis;
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
		connectedAnchorPosition = new Vector3 (0, 0, -.75f);
		anchorPosition = new Vector3 (0, 0, .75f);
		axis = new Vector3 (1f, 0, 0);
		secondaryAxis = new Vector3 (0, 1f, 0);
		mode = GrapplerMode.Off;
		linkDistance = 2f;
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
		arrow.transform.LookAt (anchor.transform.position);
		arrow.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
		arrow.AddComponent<ConfigurableJoint> ();
		ConfigurableJoint joint = arrow.GetComponent<ConfigurableJoint> ();
		joint.autoConfigureConnectedAnchor = false;
		joint.axis = axis;
		joint.secondaryAxis = secondaryAxis;
		joint.anchor = anchorPosition;
		joint.connectedAnchor = connectedAnchorPosition;
		joint.xMotion = ConfigurableJointMotion.Limited;
		joint.yMotion = ConfigurableJointMotion.Limited;
		joint.zMotion = ConfigurableJointMotion.Locked;
		joint.angularXMotion = ConfigurableJointMotion.Free;
		joint.angularYMotion = ConfigurableJointMotion.Free;
		joint.angularZMotion = ConfigurableJointMotion.Free;
		joint.connectedBody = anchor.GetComponent<Rigidbody> ();

		mode = GrapplerMode.Attached;

		FinishLinks ();
	}

	private void DoShooting() {
	}


	void FinishLinks() {
		AddLinks ();
		LinkPlayer ();
		mode = GrapplerMode.Attached;
	}

	void AddLinks() {
		while(Vector3.Distance(player.transform.position, lastLink.transform.position) > linkDistance) {
			AddLink();
		}
	}

	void AddLink() {
		GameObject link = Instantiate (baseLink);
		Vector3 direction = (lastLink.transform.position - player.transform.position).normalized;
		Vector3 newLinkPos = lastLink.transform.position - direction * 1.5f;
		link.transform.position = newLinkPos;
		link.transform.LookAt (lastLink.transform.position);

		link.AddComponent<ConfigurableJoint> ();
		ConfigurableJoint joint = link.GetComponent<ConfigurableJoint> ();
		joint.autoConfigureConnectedAnchor = false;
		joint.axis = axis;
		joint.secondaryAxis = secondaryAxis;
		joint.anchor = anchorPosition;
		joint.connectedAnchor = connectedAnchorPosition;
		joint.xMotion = ConfigurableJointMotion.Limited;
		joint.yMotion = ConfigurableJointMotion.Limited;
		joint.zMotion = ConfigurableJointMotion.Locked;
		joint.angularXMotion = ConfigurableJointMotion.Free;
		joint.angularYMotion = ConfigurableJointMotion.Free;
		joint.angularZMotion = ConfigurableJointMotion.Free;
		joint.connectedBody = lastLink.GetComponent<Rigidbody> ();

		lastLink = link;
	}

	void LinkPlayer() {
		player.transform.LookAt (lastLink.transform.position);

		player.AddComponent<ConfigurableJoint> ();
		ConfigurableJoint joint = player.GetComponent<ConfigurableJoint> ();
		joint.autoConfigureConnectedAnchor = false;
		joint.axis = axis;
		joint.secondaryAxis = secondaryAxis;
		joint.anchor = anchorPosition;
		joint.connectedAnchor = connectedAnchorPosition;
		joint.xMotion = ConfigurableJointMotion.Limited;
		joint.yMotion = ConfigurableJointMotion.Limited;
		joint.zMotion = ConfigurableJointMotion.Locked;
		joint.angularXMotion = ConfigurableJointMotion.Free;
		joint.angularYMotion = ConfigurableJointMotion.Free;
		joint.angularZMotion = ConfigurableJointMotion.Free;
		joint.connectedBody = lastLink.GetComponent<Rigidbody> ();

		lastLink = player;
	}

	void ShootArrow(GameObject anchor) {
		arrow.SetActive (true);
		arrow.transform.LookAt (anchor.transform.position);
		arrow.transform.position = player.transform.position;
		arrow.transform.LookAt (anchor.transform.position);
		Vector3 direction = (anchor.transform.position - player.transform.position).normalized;

		Rigidbody arrowRb = arrow.GetComponent<Rigidbody> ();
		arrowRb.AddForce(direction * 10000);
		Physics.IgnoreCollision(arrow.GetComponent<Collider>(), player.GetComponent<Collider>());
		Physics.IgnoreCollision(arrow.GetComponent<Collider>(), plane.GetComponent<Collider>());
	}
}
