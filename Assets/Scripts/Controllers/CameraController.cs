using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	private Vector3 holder;
	// Use this for initialization
	void Start () {
		holder = new Vector3 (0, 0, 0);
		offset = new Vector3 (1, 1, 1);
	}

	void LateUpdate () {
		holder = player.transform.position + offset;
		transform.position = holder;
	}
}