using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = new Vector3 (0, 0, -50);
	}

	void LateUpdate () {
		//transform.position = player.transform.position + offset;
		//transform.LookAt (player.transform.position);
	}
}