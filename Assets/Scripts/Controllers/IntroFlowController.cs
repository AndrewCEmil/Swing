using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum IntroMode
{
	Basic,
	Intermediate,
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
		} else if (sceneName == "Race7") {
			mode = IntroMode.Intermediate;
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
		switch (mode) {
		case IntroMode.Basic:
			return GetBasicPanelText ();
		case IntroMode.Intermediate:
			return GetIntermediatePanelText ();
		case IntroMode.Advanced:
			return GetAdvancedPanelText ();
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
		case 0:
			return "Way to go!  On the previous level you swung into the target successfully.";
		case 1:
			return "You can detach from your current anchor by pointing away from all anchors and clicking";
		case 2:
			return "To beat this level, attach to the anchor in front of you, swing foward, and detach.  You need to fly forward, unattached, in order to hit the target";
		default:
			return "To beat this level, attach to the anchor in front of you, swing foward, and detach.  You need to fly forward, unattached, in order to hit the target";
		}
	}

	private string GetAdvancedPanelText() {
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
