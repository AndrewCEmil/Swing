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
	private Rigidbody arrowRb;
	private ArrowController arrowController;
	private GameObject player;
	private GameObject plane;
	private Vector3 anchorPosition;
	private Vector3 axis;
	private Vector3 secondaryAxis;
	private GrapplerMode mode;
	private Vector3 jointConnectedAnchor;
	private LineRenderer lineRenderer;
	private float maxArrowDistance;

	// Use this for initialization
	void Start () {
		arrow = GameObject.Find ("Arrow");
		arrowRb = arrow.GetComponent<Rigidbody> ();
		arrowController = arrow.GetComponent<ArrowController> ();
		arrow.SetActive (false);
		player = GameObject.Find ("Player");
		anchorPosition = new Vector3 (0, 0, .75f);
		axis = new Vector3 (1f, 0, 0);
		secondaryAxis = new Vector3 (0, 1f, 0);
		mode = GrapplerMode.Off;
		maxArrowDistance = 50f;
		InitializeLine ();
	}

	private void InitializeLine() {
		lineRenderer = GameObject.Find("Line").GetComponent<LineRenderer> ();
		lineRenderer.positionCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch (mode) {
		case GrapplerMode.Off:
			break; //noop
		case GrapplerMode.Shooting:
			DoShooting ();
			DoLine ();
			break;
		case GrapplerMode.Attached:
			DoLine ();
			break; //noop
		}
	}

	//TODO shoot should be where the player is LOOKING, but for now its the target they click
	public void Shoot(GameObject anchor) {
		//TODO this section gets removed once we switch to vr
		if (mode == GrapplerMode.Attached) {
			BreakLink ();
		} else if (mode == GrapplerMode.Off) {
			ShootArrow (anchor);
			mode = GrapplerMode.Shooting;
		} else {
			//Aready shooting or retracting, do nothing
		}
	}

	 
	public void BreakLink() {
		Destroy (arrow.GetComponent<ConfigurableJoint> ());
		arrowController.attached = false;
		arrow.SetActive (false);
		mode = GrapplerMode.Off;
		lineRenderer.positionCount = 0;
	}

	//TODO need to attach where it _hits_
	public void AttachArrow (GameObject anchor) {
		arrow.transform.LookAt (anchor.transform.position);
		arrow.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
		arrowRb.useGravity = false;
		arrowRb.isKinematic = true;

		//TODO right here
		BuildJoint (player, arrow);

		mode = GrapplerMode.Attached;
	}

	private void DoShooting() {
		if (Vector3.Distance (arrow.transform.position, player.transform.position) > maxArrowDistance) {
			arrow.SetActive (false);
			mode = GrapplerMode.Off;
			lineRenderer.positionCount = 0;
		}
	}

	private void DoLine() {
		if (lineRenderer.positionCount > 0) {
			lineRenderer.SetPosition (0, player.transform.position);
			lineRenderer.SetPosition (1, arrow.transform.position);
		}
	}

	void BuildJoint(GameObject link, GameObject anchor) {
		link.AddComponent<ConfigurableJoint> ();
		ConfigurableJoint joint = link.GetComponent<ConfigurableJoint> ();
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedAnchor = jointConnectedAnchor;
		joint.axis = axis;
		joint.secondaryAxis = secondaryAxis;
		joint.anchor = anchorPosition;
		joint.xMotion = ConfigurableJointMotion.Limited;
		joint.yMotion = ConfigurableJointMotion.Limited;
		joint.zMotion = ConfigurableJointMotion.Locked;
		joint.angularXMotion = ConfigurableJointMotion.Free;
		joint.angularYMotion = ConfigurableJointMotion.Free;
		joint.angularZMotion = ConfigurableJointMotion.Free;
		SoftJointLimitSpring spring = new SoftJointLimitSpring ();
		spring.spring = 50;
		spring.damper = 0;
		joint.linearLimitSpring = spring;
		SoftJointLimit limit = new SoftJointLimit ();
		limit.limit = Vector3.Distance (player.transform.position, anchor.transform.position) + .5f;
		limit.bounciness = 0f;
		limit.contactDistance = .1f;
		joint.linearLimit = limit;
		joint.connectedBody = anchor.GetComponent<Rigidbody> ();
		joint.projectionMode = JointProjectionMode.PositionAndRotation;
	}

	void ShootArrow(GameObject anchor) {
		arrow.SetActive (true);
		arrowRb.useGravity = true;
		arrowRb.isKinematic = false;
		arrowRb.velocity = new Vector3 (0, 0, 0);
		arrow.transform.LookAt (anchor.transform.position);
		arrow.transform.position = player.transform.position;
		arrow.transform.LookAt (anchor.transform.position);
		Vector3 direction = (anchor.transform.position - player.transform.position).normalized;

		arrowRb.AddForce(direction * 3000);
		Physics.IgnoreCollision(arrow.GetComponent<Collider>(), player.GetComponent<Collider>());

		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition (0, player.transform.position);
		lineRenderer.SetPosition (1, arrow.transform.position);
	}
}
