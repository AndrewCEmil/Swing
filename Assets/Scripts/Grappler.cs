using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrapplerMode
{
	Off,
	Shooting,
	Retracting,
	Attached
}

public class Grappler : MonoBehaviour {

	private GameObject arrow;
	private ArrowController arrowController;
	private GameObject player;
	private GameObject baseLink;
	private GameObject plane;
	private Vector3 anchorPosition;
	private Vector3 axis;
	private Vector3 secondaryAxis;
	private GrapplerMode mode;
	private GameObject lastLink;
	private float linkDistance;
	private List<GameObject> links;
	private float timePerRetraction;
	private float timeSinceLastRetraction;

	// Use this for initialization
	void Start () {
		arrow = GameObject.Find ("Arrow");
		arrowController = arrow.GetComponent<ArrowController> ();
		arrow.SetActive (false);
		player = GameObject.Find ("Player");
		baseLink = GameObject.Find ("BaseLink");
		plane = GameObject.Find ("Plane");
		anchorPosition = new Vector3 (0, 0, .75f);
		axis = new Vector3 (1f, 0, 0);
		secondaryAxis = new Vector3 (0, 1f, 0);
		mode = GrapplerMode.Off;
		linkDistance = 2f;
		links = new List<GameObject> ();
		timePerRetraction = .1f;
		timeSinceLastRetraction = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch (mode) {
		case GrapplerMode.Off:
			break; //noop
		case GrapplerMode.Shooting:
			DoShooting ();
			break;
		case GrapplerMode.Retracting:
			Retract ();
			break;
		case GrapplerMode.Attached:
			break; //noop
		}
	}

	//TODO shoot should be where the player is LOOKING, but for now its the target they click
	public void Shoot(GameObject anchor) {
		//TODO this section gets removed once we switch to vr
		if (mode == GrapplerMode.Attached) {
			StartRetraction ();
		} else {
			ShootArrow (anchor);
			lastLink = arrow;
			mode = GrapplerMode.Shooting;
		}
	}

	 
	public void StartRetraction() {
		Destroy (arrow.GetComponent<ConfigurableJoint> ());
		arrowController.attached = false;
		arrow.SetActive (false);
		timeSinceLastRetraction = 0f;
		mode = GrapplerMode.Retracting;
	}

	public void AttachArrow (GameObject anchor) {
		arrow.transform.LookAt (anchor.transform.position);
		arrow.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);

		BuildJoint (arrow, anchor);

		FinishLinks ();
		mode = GrapplerMode.Attached;
	}

	private void DoShooting() {
		AddLinks ();
	}

	private void Retract() {
		timeSinceLastRetraction += Time.deltaTime;
		int numToRemove = (int) Mathf.Floor (Mathf.Min (links.Count, timeSinceLastRetraction / timePerRetraction));
		for (int i = 0; i < numToRemove; i++) {
			Destroy (links [i]);
			links.RemoveAt (i);
		}
		timeSinceLastRetraction = timeSinceLastRetraction % timePerRetraction;
		if (links.Count == 0) {
			TriggerRetracted ();
		}
	}

	private void TriggerRetracted() {
		//TODO anything else?
		mode = GrapplerMode.Off;
	}

	void FinishLinks() {
		AddLinks ();
		LinkPlayer ();
	}

	void AddLinks() {
		float distance = Vector3.Distance(player.transform.position, lastLink.transform.position);
		while(distance > linkDistance) {
			AddLink();
			distance = Vector3.Distance(player.transform.position, lastLink.transform.position);
		}
	}

	void AddLink() {
		GameObject link = Instantiate (baseLink);
		Vector3 direction = (lastLink.transform.position - player.transform.position).normalized;
		Vector3 newLinkPos = lastLink.transform.position - direction * 1.5f;
		link.transform.position = newLinkPos;
		link.transform.LookAt (lastLink.transform.position);

		BuildJoint (link, lastLink);
		links.Add (link);

		lastLink = link;
	}

	void LinkPlayer() {
		player.transform.LookAt (lastLink.transform.position);

		BuildJoint (player, lastLink);

		lastLink = player;
	}

	void BuildJoint(GameObject link, GameObject anchor) {
		link.AddComponent<ConfigurableJoint> ();
		ConfigurableJoint joint = link.GetComponent<ConfigurableJoint> ();
		joint.autoConfigureConnectedAnchor = true;
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
		spring.spring = 0;
		spring.damper = 0;
		joint.linearLimitSpring = spring;
		SoftJointLimit limit = new SoftJointLimit ();
		limit.limit = .1f;
		limit.bounciness = 0f;
		limit.contactDistance = .1f;
		joint.linearLimit = limit;
		joint.connectedBody = anchor.GetComponent<Rigidbody> ();
		joint.projectionMode = JointProjectionMode.PositionAndRotation;
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
