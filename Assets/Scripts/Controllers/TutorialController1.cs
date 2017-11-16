using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController1 : MonoBehaviour {

	private GameObject player;
	private GameObject canvas;
	private GameObject startCube;
	private Text textField;
	private int tutorialPosition;
	private Grappler grappler;
	private Rigidbody playerRB;
	private GameObject anchor;
	private float targetHitDelay;
	private float targetHitTime;
	// Use this for initialization
	void Start () {
		InitVariables ();
	}

	private void InitVariables() {
		targetHitDelay = 1f;
		targetHitTime = -1f;
		canvas = GameObject.Find ("TutorialCanvas");
		textField = canvas.GetComponentInChildren<Text> ();
		player = GameObject.Find ("Player");
		grappler = player.GetComponent<Grappler> ();
		playerRB = player.GetComponent<Rigidbody> ();
		anchor = GameObject.Find ("Anchor0");
		startCube = GameObject.Find ("StartCube");
		tutorialPosition = 0;
	}

	void Update () {
		if (targetHitTime > 0 && Time.time - targetHitTime > targetHitDelay) {
			SceneManager.LoadScene ("Tutorial 2");
		}
	}

	public void TargetHit() {
		targetHitTime = Time.time;
	}

	public void HandleNextClicked() {
		tutorialPosition += 1;
		switch (tutorialPosition) {
		case 0:
			FillPanelText ();
			break;
		case 1:
			Attach ();
			break;
		}
	}

	private void Attach() {
		//attach player to anchor0
		playerRB.useGravity = true;
		grappler.Attach(anchor);
		canvas.SetActive (false);
	}

	private void FillPanelText() {
		string panelText = GetPanelText ();
		textField.text = panelText;
	}

	private string GetPanelText() {
		switch (tutorialPosition) {
		case 0:
			return "TEST";
		default:
			return "FIN";
		}
	}
}
