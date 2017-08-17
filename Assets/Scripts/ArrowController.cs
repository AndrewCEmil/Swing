using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	private Rigidbody rb;
	private Grappler grappler;
	bool attached;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		grappler = GameObject.Find ("Player").GetComponent<Grappler> ();
		attached = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("collided");
		GameObject anchor = collision.collider.gameObject;
		if (!attached && anchor.tag == "Anchor") {
			AttachToAnchor (anchor);
			attached = true;
		}
	}

	void AttachToAnchor(GameObject anchor) {
		rb.velocity = new Vector3 (0, 0, 0);
		grappler.AttachArrow (anchor);
	}
}
