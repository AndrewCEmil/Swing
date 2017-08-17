using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	private float elementSize;
	private List<GameObject> links;
	// Use this for initialization
	void Start () {
		elementSize = 2f;
		links = new List<GameObject> ();
	}

	// Update is called once per frame
	void Update () {
	}

	public void Construct(GameObject anchor, GameObject payload, GameObject baseLink) {
		//1 get distance
		float numEles = NumberOfElements(anchor, payload);
		Vector3 directionStep = DirectionStep (anchor, payload);
		GameObject previous = payload;
		for (int i = 0; i < numEles; i++) {
			GameObject link = Instantiate (baseLink);
			link.transform.position = directionStep * (i + 1) + payload.transform.position;
			link.SetActive (true);

			HingeJoint hinge = previous.GetComponent<HingeJoint> ();
			hinge.connectedBody = link.GetComponent<Rigidbody> ();
			previous.transform.LookAt (link.transform.position);

			links.Add (link);
			previous = link;
		}
		HingeJoint finalHinge = previous.GetComponent<HingeJoint> ();
		finalHinge.connectedBody = anchor.GetComponent<Rigidbody> ();
		previous.transform.LookAt (anchor.transform.position);
	}

	public void Destruct() {
		foreach(GameObject link in links) {
			Destroy (link);
		}
		links.Clear();
	}

	private float NumberOfElements(GameObject anchor, GameObject payload) {
		return Mathf.Floor(Vector3.Distance (anchor.transform.position, payload.transform.position) / elementSize);
	}

	private Vector3 DirectionStep(GameObject anchor, GameObject payload) {
		float numEles = NumberOfElements (anchor, payload);
		Vector3 direction = anchor.transform.position - payload.transform.position;
		direction.x = direction.x / numEles;
		direction.y = direction.y / numEles;
		direction.z = direction.z / numEles;
		return direction;
	}
}