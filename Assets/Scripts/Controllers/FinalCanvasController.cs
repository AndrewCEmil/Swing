using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalCanvasController : MonoBehaviour {

	int currentPosition;
	Text text;
	GameObject button;
	Text buttonText;
	bool isHighlighted;
	int clickFrameCountdown;

	// Use this for initialization
	void Start () {
		currentPosition = 0;
		isHighlighted = false;
		text = GetComponentInChildren<Text> ();
		button = GameObject.Find ("FinalCanvasButton");
		buttonText = button.GetComponentInChildren<Text> ();
		FillText ();
	}

	public void HandleButtonClicked() {
		currentPosition += 1;
		FillText ();
	}

	void FillText() {
		StringPair sp = GetNextText ();
		text.text = sp.a;
		if (sp.b == "") {
			button.SetActive (false);
		} else {
			buttonText.text = sp.b;
		}
	}

	StringPair GetNextText() {
		switch (currentPosition) {
		case 0:
			return new StringPair ("text", "button");
		default:
			return new StringPair ("", "");
		}
	}


	public void PointerEnter() {
		isHighlighted = true;
	}

	public void PointerExit() {
		isHighlighted = false;
	}

	public bool RecentlyClicked() {
		return clickFrameCountdown > 0;
	}

	public bool IsHighlighted() {
		return isHighlighted;
	}

	void Update() {
		if (clickFrameCountdown > 0) {
			clickFrameCountdown--;
		}

		if (!button.activeSelf) {
			isHighlighted = false;
		}
	}
}
