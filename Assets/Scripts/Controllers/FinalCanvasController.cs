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
			return new StringPair ("This is it, the final level", "I know");
		case 1:
			return new StringPair ("You think you are almost done with the game, but the truth is you are only halfway", "What?");
		case 2:
			return new StringPair ("This level is a combination of most of the other levels", "Snap");
		case 3:
			return new StringPair ("Instead of targets, there will be the big start cubes at the end of each level", "Word");
		case 4:
			return new StringPair ("When you shoot the start cube, you will be teleported to the start position for the next level", "Okay");
		case 5:
			return new StringPair ("Under a start cube, look up at the bottom to see what level you are about to go through", "Alright");
		case 6:
			return new StringPair ("I give you respect for making this far - its not easy.  Don't give up, I know you can do this!", "LETS GO");
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
