using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController1 : MonoBehaviour {

	private GameObject player;
	private GameObject canvas;
	private Text textField;
	private Button button;
	private Text buttonText;
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
		button = canvas.GetComponentInChildren<Button> ();
		buttonText = button.GetComponentInChildren<Text> ();
		player = GameObject.Find ("Player");
		grappler = player.GetComponent<Grappler> ();
		playerRB = player.GetComponent<Rigidbody> ();
		anchor = GameObject.Find ("Anchor0");
		tutorialPosition = 0;
		FillPanelText ();
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
		if (tutorialPosition < 3) {
			FillPanelText ();
		} else {
			Attach ();
		}
	}

	private void Attach() {
		//attach player to anchor0
		playerRB.useGravity = true;
		grappler.Attach(anchor);
		canvas.SetActive (false);
	}

	private void FillPanelText() {
		StringPair panelText = GetPanelText ();
		textField.text = panelText.a;
		buttonText.text = panelText.b;
	}

	private StringPair GetPanelText() {
		switch (tutorialPosition) {
		case 0:
			return new StringPair("WELCOME!  This tutorial will provide a 3rd person perspective on gameplay.  Keep in mind that when you start playing you will be seeing everything from the perspective of the ball", "Cool");
		case 1:
			return new StringPair ("The point of the game is to swing through levels and crash into the portal on the right.  You swing through levels by connecting to anchors.  The diamond you see in front of you is an anchor", "Got it");
		case 2:
			return new StringPair("Now, lets demonstate how this works.  When you click Okay, the \"player\" will attach to an anchor and swing into the target", "Okay");
		default:
			return null;
		}
	}
}
