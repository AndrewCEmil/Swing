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
	//private SpeedPanelController speedPanelController;
	private AnchorController currentAttachedController;
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
		//speedPanelController = GameObject.Find ("SpeedCanvas").GetComponent<SpeedPanelController> ();
		anchorPosition = new Vector3 (0, 0, 0);
		axis = new Vector3 (1f, 0, 0);
		secondaryAxis = new Vector3 (0, 1f, 0);
		mode = GrapplerMode.Off;
		maxRayDistance = 100f;
		InitializeLine ();
		currentAttachedController = null;
	}

	private void InitializeLine() {
		lineRenderer = GameObject.Find("Line").GetComponent<LineRenderer> ();
		lineRenderer.positionCount = 0;
	}
	
	void Update () {
		UpdatePointedAt ();
		switch (mode) {
		case GrapplerMode.Off:
			break;
		case GrapplerMode.Attached:
			DoLine ();
			break;
		}
	}

	void UpdatePointedAt() {
		if (currentPointedAt != null && pointedAtSetTime > 0) {
			if (Time.time - pointedAtSetTime > pointedAtCooldown) {
				RemoveCurrentPointedAt ();
			}
		}
	}

	private void RemoveCurrentPointedAt() {
		currentPointedAt.GetComponent<AnchorController> ().SetPointedAt (false);
		currentPointedAt = null;
	}

	public void AnchorPointedAt(GameObject anchor) {
		if (currentPointedAt != null) {
			RemoveCurrentPointedAt ();
		}
		currentPointedAt = anchor;
		pointedAtSetTime = -1f;
	}

	public void AnchorPointerExit(GameObject anchor) {
		if (currentPointedAt.name == anchor.name) {
			pointedAtSetTime = Time.time;
		}
	}

	public void PointerClicked() {
		if (currentPointedAt != null) {
			Attach (currentPointedAt);
		} else {
			BreakLink ();
		}
	}
	 
	public void BreakLink() {
		currentAttachedController.UnLink ();
		DestroyJoint ();
		mode = GrapplerMode.Off;
		lineRenderer.positionCount = 0;
	}

	private void DestroyJoint() {
		FixedJoint joint = player.GetComponent<FixedJoint> ();
		if(joint != null) {
			Destroy (joint);
		}
	}

	//TODO need to attach where it _hits_
	public void Attach (GameObject anchor) {
		BuildJoint (player, anchor);
		CreateLine (anchor);
		currentAttachedController = anchor.GetComponent<AnchorController> ();
		currentAttachedController.Link ();

		//speedPanelController.AttachTriggered ();

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
		FixedJoint joint = link.GetComponent<FixedJoint> ();
		if (joint == null) {
			link.AddComponent<FixedJoint> ();
			joint = link.GetComponent<FixedJoint> ();
		}
		joint.connectedBody = anchor.GetComponent<Rigidbody> ();
	}
}
