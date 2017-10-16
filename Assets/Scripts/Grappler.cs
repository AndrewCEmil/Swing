﻿using System.Collections;
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
	private GameObject currentPointedAt;
	private float pointedAtSetTime;
	private float pointedAtCooldown;

	// Use this for initialization
	void Start () {
		pointedAtCooldown = .5f;
		player = GameObject.Find ("Player");
		anchorPosition = new Vector3 (0, 0, 0);
		axis = new Vector3 (1f, 0, 0);
		secondaryAxis = new Vector3 (0, 1f, 0);
		mode = GrapplerMode.Off;
		maxRayDistance = 100f;
		InitializeLine ();
	}

	private void InitializeLine() {
		lineRenderer = GameObject.Find("Line").GetComponent<LineRenderer> ();
		lineRenderer.positionCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePointedAt ();
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

	void UpdatePointedAt() {
		if (currentPointedAt != null) {
			if (Time.time - pointedAtSetTime > pointedAtCooldown) {
				currentPointedAt.GetComponent<AnchorController> ().PointerExit ();
				currentPointedAt = null;
			}
		}
	}

	public void AnchorPointedAt(GameObject anchor) {
		currentPointedAt = anchor;
		pointedAtSetTime = Time.time;
	}

	public void PointerClicked() {
		if (mode == GrapplerMode.Attached) {
			BreakLink ();
		}
		if (currentPointedAt != null) {
			Attach (currentPointedAt);
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
