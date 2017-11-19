using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().AddTorque (Random.onUnitSphere * 100f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
