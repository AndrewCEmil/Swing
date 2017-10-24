using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedPanelController : MonoBehaviour {

	public GameObject speedTextObj;
	public GameObject lossTextObj;

	private GameObject player;
	private Rigidbody playerRB;
	private Text speedText;
	private Text lossText;
	private LevelController levelController;
	private Vector3 offset;
	private float beforeAttachSpeed;
	private float attachedTriggeredFrameNum;
	void Start () {
		speedText = speedTextObj.GetComponent<Text> ();
		lossText = lossTextObj.GetComponent<Text> ();
		player = GameObject.Find ("Player");
		playerRB = player.GetComponent<Rigidbody> ();
		offset = new Vector3 (5, 0, 10);
		attachedTriggeredFrameNum = -10;
		beforeAttachSpeed = 0;
	}

	// Update is called once per frame
	void Update () {
		UpdateSpeed ();
		MaybeUpdateLoss ();
		UpdatePosition ();
	}

	private void UpdateSpeed() {
		float speed = playerRB.velocity.magnitude;
		speedText.text = speed.ToString ();
	}

	private void MaybeUpdateLoss() {
		if (Time.frameCount == attachedTriggeredFrameNum + 1) {
			UpdateLoss ();
		}
	}

	private void UpdatePosition() {
		transform.position = player.transform.position + offset;
		transform.LookAt(2 * transform.position - player.transform.position);
	}

	private void UpdateLoss() {
		float loss = beforeAttachSpeed - playerRB.velocity.magnitude;
		lossText.text = loss.ToString ();
		attachedTriggeredFrameNum = -10;
	}

	public void AttachTriggered() {
		beforeAttachSpeed = playerRB.velocity.magnitude;
		attachedTriggeredFrameNum = Time.frameCount;
	}
}
