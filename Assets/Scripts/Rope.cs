using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	private float elementSize;
	private List<GameObject> links;
	private Vector3 anchorOffset;
	private Vector3 connectedAnchorPosition;
	// Use this for initialization
	void Start () {
		elementSize = 1.25f;
		links = new List<GameObject> ();
		anchorOffset = new Vector3 (-.5f, 0, 0);
		connectedAnchorPosition = new Vector3 (0, -0.5f, 0);
	}

	// Update is called once per frame
	void Update () {
	}

	public void Construct(GameObject anchor, GameObject payload, GameObject baseLink) {
		//1 get distance
		float numEles = NumberOfElements(anchor, payload);
		Vector3 directionStep = DirectionStep (anchor, payload);
		GameObject previous = payload;
		payload.AddComponent<HingeJoint> ();
		for (float i = 0; i < numEles; i++) {
			GameObject link = Instantiate (baseLink);
			link.transform.position = directionStep * (i + .5f) + payload.transform.position;
			link.AddComponent<HingeJoint> ();
			link.SetActive (true);

			HingeJoint hinge = previous.GetComponent<HingeJoint> ();
			hinge.autoConfigureConnectedAnchor = false;
			hinge.connectedAnchor = connectedAnchorPosition;
			hinge.connectedBody = link.GetComponent<Rigidbody> ();
			hinge.enableCollision = true;

			links.Add (link);
			previous = link;
		}
		HingeJoint finalHinge = previous.GetComponent<HingeJoint> ();
		finalHinge.connectedBody = anchor.GetComponent<Rigidbody> ();
		previous.transform.LookAt (anchor.transform.position + anchorOffset);
	}

	public void Destruct() {
		foreach(GameObject link in links) {
			Destroy (link);
		}
		links.Clear();
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