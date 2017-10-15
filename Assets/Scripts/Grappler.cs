using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrapplerMode
{
	Off,
	Attached
}

public class Grappler : MonoBehaviour {

	private GameObject player;
	private GameObject plane;
	private Vector3 anchorPosition;
	private Vector3 axis;
	private Vector3 secondaryAxis;
	private GrapplerMode mode;
	private LineRenderer lineRenderer;
	private float maxRayDistance;
	private bool isRetracting;
	private float springRetractionScale;
	private bool clickOccured;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		anchorPosition = new Vector3 (0, 0, 0);
		axis = new Vector3 (1f, 0, 0);
		secondaryAxis = new Vector3 (0, 1f, 0);
		mode = GrapplerMode.Off;
		maxRayDistance = 100f;
		isRetracting = false;
		InitializeLine ();
		springRetractionScale = 10f;
		clickOccured = false;
	}

	private void InitializeLine() {
		lineRenderer = GameObject.Find("Line").GetComponent<LineRenderer> ();
		lineRenderer.positionCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		clickOccured = false;
		//HandleRetraction ();
		switch (mode) {
		case GrapplerMode.Off:
			break; //noop
		case GrapplerMode.Attached:
			DoLine ();
			break; //noop
		}
		HandleBreakline ();
	}

	void HandleBreakline() {
		if (Input.GetMouseButtonUp (1)) {
			BreakLink ();
		}
	}

	/*
	void HandleRetraction() {
		UpdateRetractionMode ();
		if (isRetracting) {
			Retract ();
		}
	}

	void UpdateRetractionMode() {
		//first maybe update retraction
		if (Input.GetMouseButtonDown (0) && mode == GrapplerMode.Attached) {
			isRetracting = true;
		} else if (Input.GetMouseButtonUp (0)) {
			isRetracting = false;
		} else if (mode != GrapplerMode.Attached) {
			isRetracting = false;
		}
	}

	void Retract() {
		ConfigurableJoint joint = player.GetComponent<ConfigurableJoint> ();
		SoftJointLimitSpring spring = joint.linearLimitSpring;
		spring.spring = spring.spring + Time.deltaTime * springRetractionScale;
	}
*/

	//TODO shoot should be where the player is LOOKING, but for now its the target they click
	public void Shoot(GameObject anchor) {
		//TODO this section gets removed once we switch to vr
		if (mode == GrapplerMode.Attached) {
			BreakLink ();
		} else if (mode == GrapplerMode.Off) {
			Attach (anchor);
		} else {
			//Aready shooting or retracting, do nothing
		}
		clickOccured = true;
	}

	public void PointerClicked() {
		if (!clickOccured && mode == GrapplerMode.Attached) {
			BreakLink ();
		}
	}
	 
	public void BreakLink() {
		FixedJoint joint = player.GetComponent<FixedJoint> ();
		if(joint != null) {
			Destroy (joint);
		}
		mode = GrapplerMode.Off;
		lineRenderer.positionCount = 0;
	}

	//TODO need to attach where it _hits_
	public void Attach (GameObject anchor) {
		BuildJoint (player, anchor);
		CreateLine (anchor);

		anchorPosition = anchor.transform.position;
		mode = GrapplerMode.Attached;
	}

	private void CreateLine(GameObject anchor) {
		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition (0, player.transform.position);
		lineRenderer.SetPosition (1, anchor.transform.position);
	}

	private void DoLine() {
		if (lineRenderer.positionCount > 0) {
			lineRenderer.SetPosition (0, player.transform.position);
			lineRenderer.SetPosition (1, anchorPosition);
		}
	}

	void BuildJoint(GameObject link, GameObject anchor) {
		link.AddComponent<FixedJoint> ();
		FixedJoint joint = link.GetComponent<FixedJoint> ();
		joint.connectedBody = anchor.GetComponent<Rigidbody> ();
	}
}
