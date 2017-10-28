using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum IntroMode
{
	Basic,
	Advanced
}

public class IntroFlowController : MonoBehaviour {

	// Use this for initialization
	public Text textField;
	private IntroMode mode;
	private int panelIndex;
	private int clickFrameCountdown;
	private bool isHighlighted;
	void Start () {
		clickFrameCountdown = 0;
		string sceneName = SceneManager.GetActiveScene ().name;
		if (sceneName == "Race6") {
			mode = IntroMode.Basic;
		} else {
			mode = IntroMode.Advanced;
		}
		FillPanelText ();
		isHighlighted = false;
	}

	void Update() {
		if (clickFrameCountdown > 0) {
			clickFrameCountdown--;
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

	private void FillPanelText() {
		string panelText = GetPanelText ();
		textField.text = panelText;
	}

	private string GetPanelText() {
		if (mode == IntroMode.Basic) {
			return GetBasicPanelText();
		}
		return "";
	}

	private string GetBasicPanelText() {
		switch(panelIndex) {
		case 0:
			return "Welcome!  First off, note the menu directly above you.  If you ever want exit play, just click Back up there.";
		case 2:
			return "The goal of each level is to hit the target.  The target is the Red Square in front of you.";
		case 3:
			return "To hit the target you will have to swing through the level by connecting to anchors.  The diamond in front of you is an anchor.";
		case 4:
			return "Connect to the Diamond by pointing your controller at it and clicking the touchpad.";
		default: 
			return "Connect to the Diamond by pointing your controller at it and clicking the touchpad.";
		}
	}

	private string GetIntermediatePanelText() {
		switch (panelIndex) {
		default:
			return "";

		}
	}

	public void NextButtonClicked() {
		panelIndex += 1;
		clickFrameCountdown = 4;
		FillPanelText ();
	}
}
