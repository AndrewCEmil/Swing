using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineParticleSystemController : MonoBehaviour {

	private GameObject player;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		offset = new Vector3 (0, .3f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + offset;
	}
}
