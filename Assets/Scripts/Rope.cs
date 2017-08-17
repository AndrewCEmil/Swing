using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	private float elementSize;
	private List<GameObject> links;
	private Vector3 anchorOffset;
	private Vector3 connectedAnchorPosition;
	private bool constructed;
	// Use this for initialization
	void Start () {
		elementSize = 1.25f;
		links = new List<GameObject> ();
		anchorOffset = new Vector3 (-.5f, 0, 0);
		connectedAnchorPosition = new Vector3 (0, -0.5f, 0);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Destruct ();
		}
	}

	public void Construct(GameObject anchor, GameObject payload, GameObject baseLink) {
		//1 get distance
		float numEles = NumberOfElements(anchor, payload);
		Vector3 directionStep = DirectionStep (anchor, payload);
		GameObject previous = payload;
		payload.AddComponent<HingeJoint> ();
		for (float i = 0; i < numEles; i++) {
			GameObject link = CreateLink (baseLink, directionStep, payload.transform.position, i);

			HingeJoint hinge = previous.GetComponent<HingeJoint> ();
			ConfigureHinge (hinge, link);
			previous.transform.LookAt (link.transform.position);

			links.Add (link);
			previous = link;
		}
		HingeJoint finalHinge = previous.GetComponent<HingeJoint> ();
		ConfigureFinalHinge (finalHinge, anchor);
		previous.transform.LookAt (anchor.transform.position + anchorOffset);

		constructed = true;
	}

	private GameObject CreateLink(GameObject baseLink, Vector3 directionStep, Vector3 startPosition, float hingeNum) {
		GameObject link = Instantiate (baseLink);
		link.transform.position = directionStep * (hingeNum + .5f) + startPosition;
		link.AddComponent<HingeJoint> ();
		return link;
	}

	private void ConfigureHinge(HingeJoint hinge, GameObject link) {
		hinge.autoConfigureConnectedAnchor = false;
		hinge.connectedAnchor = connectedAnchorPosition;
		hinge.connectedBody = link.GetComponent<Rigidbody> ();
		hinge.enableCollision = true;
	}

	private void ConfigureFinalHinge(HingeJoint hinge, GameObject anchor) {
		hinge.autoConfigureConnectedAnchor = false;
		hinge.connectedAnchor = connectedAnchorPosition;
		hinge.connectedBody = anchor.GetComponent<Rigidbody> ();
		hinge.enableCollision = true;
	}

	public void Destruct() {
		if (constructed) {
			foreach (GameObject link in links) {
				Destroy (link);
			}
			links.Clear ();
		}
		constructed = false;
	}

	private float NumberOfElements(GameObject anchor, GameObject payload) {
		return Mathf.Ceil(Vector3.Distance (anchor.transform.position + anchorOffset, payload.transform.position) / elementSize);
	}

	private Vector3 DirectionStep(GameObject anchor, GameObject payload) {
		float numEles = NumberOfElements (anchor, payload);
		Vector3 direction = anchor.transform.position + anchorOffset - payload.transform.position;
		direction.x = direction.x / numEles;
		direction.y = direction.y / numEles;
		direction.z = direction.z / numEles;
		return direction;
	}
}