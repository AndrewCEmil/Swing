using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedPanelController : MonoBehaviour {

	public GameObject speedTextObj;

	private GameObject player;
	private Rigidbody playerRB;
	private Text speedText;
	private LevelController levelController;
	private Vector3 offset;
	void Start () {
		speedText = speedTextObj.GetComponent<Text> ();
		player = GameObject.Find ("Player");
		playerRB = player.GetComponent<Rigidbody> ();
		offset = new Vector3 (5, 0, 10);
	}

	// Update is called once per frame
	void Update () {
		float speed = playerRB.velocity.magnitude;
		speedText.text = speed.ToString ();
		transform.position = player.transform.position + offset;
		transform.LookAt(2 * transform.position - player.transform.position);
	}
}
