using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	private GameObject player;
	private Grappler grappler;
	public bool attached;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		grappler = player.GetComponent<Grappler> ();
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
			Debug.Log ("collided with anchor");
		}
	}

	void AttachToAnchor(GameObject anchor) {
		grappler.AttachArrow (anchor);
	}
}
