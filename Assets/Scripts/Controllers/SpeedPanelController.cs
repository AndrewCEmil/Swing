using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedPanelController : MonoBehaviour {

	private GameObject speedTextObj;
	private GameObject lossTextObj;
	private GameObject totalLossTextObj;

	private GameObject player;
	private Rigidbody playerRB;
	private Text speedText;
	private Text lossText;
	private Text totalLossText;
	private LevelController levelController;
	private Vector3 offset;
	private float beforeAttachSpeed;
	private float attachedTriggeredFrameNum;
	private float totalLoss;
	void Start () {
		speedTextObj = GameObject.Find ("SpeedText");
		lossTextObj = GameObject.Find ("LossText");
		totalLossTextObj = GameObject.Find ("TotalLossText");
		speedText = speedTextObj.GetComponent<Text> ();
		lossText = lossTextObj.GetComponent<Text> ();
		totalLossText = totalLossTextObj.GetComponent<Text> ();
		player = GameObject.Find ("Player");
		playerRB = player.GetComponent<Rigidbody> ();
		offset = new Vector3 (20f, 6.5f, 20f);
		attachedTriggeredFrameNum = -10;
		beforeAttachSpeed = 0;
		totalLoss = 0;

		if (PlayerPrefs.GetInt ("SpeedPanelEnabled") == 0) {
			gameObject.SetActive (false);
		}
	}

	public void Reset() {
		lossText.text = "";
		totalLoss = 0;
		totalLossText.text = "";
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

		totalLoss += loss;
		totalLossText.text = totalLoss.ToString ();
	}

	public void AttachTriggered() {
		beforeAttachSpeed = playerRB.velocity.magnitude;
		attachedTriggeredFrameNum = Time.frameCount;
	}
}
