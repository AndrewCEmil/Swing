using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 torque = Random.onUnitSphere * 10f;
		GetComponent<Rigidbody> ().AddTorque (torque);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
