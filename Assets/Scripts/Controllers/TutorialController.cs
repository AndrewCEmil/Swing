using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	private GameObject canvas;
	private Text textField;
	private int tutorialPosition;
	private Grappler grappler;
	private Rigidbody playerRB;
	private GameObject anchor;
	// Use this for initialization
	void Start () {
		InitVariables ();
	}

	private void InitVariables() {
		canvas = GameObject.Find ("TutorialCanvas");
		textField = canvas.GetComponentInChildren<Text> ();
		grappler = GameObject.Find ("Player").GetComponent<Grappler> ();
		playerRB = GameObject.Find ("Player").GetComponent<Rigidbody> ();
		anchor = GameObject.Find ("Anchor0");
		tutorialPosition = 0;
	}

	void Update () {
		
	}

	public void HandleNextClicked() {
		tutorialPosition += 1;
		switch (tutorialPosition) {
		case 0:
			FillPanelText ();
			break;
		case 1:
			FirstAttach ();
			break;
		}
	}

	private void FirstAttach() {
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
