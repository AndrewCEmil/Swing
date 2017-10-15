using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtter : MonoBehaviour {

	public GameObject target;
	void Start () {
		transform.LookAt(2 * transform.position - target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
