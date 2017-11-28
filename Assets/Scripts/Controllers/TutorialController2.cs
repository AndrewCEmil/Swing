using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController2 : MonoBehaviour {

	private GameObject player;
	private GameObject canvas;
	private Button button;
	private Text buttonText;
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
		if (tutorialPosition == 3 && player.transform.position.x > 0 && player.transform.position.y > -45f) {
			Detach ();
		}
		if (targetHitTime > 0 && Time.time - targetHitTime > targetHitDelay) {
			ResetPanel ();
			targetHitTime = -1f;
		}
	}

	public void TargetHit() {
		targetHitTime = Time.time;
	}

	public void HandleNextClicked() {
		tutorialPosition += 1;
		if (tutorialPosition < 3) {
			FillPanelText ();
		} else if (tutorialPosition == 3) {
			Attach ();
		} else if (tutorialPosition > 3 && tutorialPosition < 6) {
			FillPanelText ();
		} else if (tutorialPosition == 6) {
			LevelManager.MarkLevelCompleted (19);
			SceneManager.LoadScene ("AntechamberScene");
		}
	}

	private void Detach() {
		grappler.BreakLink ();
	}

	private void Attach() {
		//attach player to anchor0
		playerRB.useGravity = true;
		grappler.Attach(anchor);
		canvas.SetActive (false);
	}

	private void ResetPanel() {
		tutorialPosition += 1;
		canvas.SetActive (true);
		FillPanelText ();
	}

	private void FillPanelText() {
		StringPair panelText = GetPanelText ();
		textField.text = panelText.a;
		buttonText.text = panelText.b;
	}

	private StringPair GetPanelText() {
		switch (tutorialPosition) {
		case 0:
			return new StringPair ("Pretty simple right?  There is one more thing you need to understand: detaching", "Okay");
		case 1:
			return new StringPair ("You can detach from the anchor you are connected to at any time.  By detaching at the right moment you can use your momentum and fly through the level.", "Got it");
		case 2:
			return new StringPair ("When you click \"play\", the player will attach to the anchor and swing forwad.  Once the player has momentum, they will detach from the anchor and fly forward", "play");
		case 4:
			return new StringPair ("Okay well thats it for this tutorial.  You can now go play the intro levels which will give you a feel for how this all works in practice.", "Thanks!");
		case 5:
			return new StringPair ("If you want to read more details on how to play, click the About button in the start scene", "Okay");
		default:
			return new StringPair (tutorialPosition.ToString (), "");
		}
	}
}
