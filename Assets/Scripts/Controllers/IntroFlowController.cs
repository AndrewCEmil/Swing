using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum IntroMode
{
	Basic,
	Intermediate,
	Advanced,
	Final
}

public class IntroFlowController : MonoBehaviour {

	// Use this for initialization
	public Text textField;
	public Button button;
	private Text buttonText;
	private IntroMode mode;
	private int panelIndex;
	private int clickFrameCountdown;
	private bool isHighlighted;
	void Start () {
		clickFrameCountdown = 0;
		buttonText = button.GetComponentInChildren<Text>();
		string sceneName = SceneManager.GetActiveScene ().name;
		if (sceneName == "Intro I") {
			mode = IntroMode.Basic;
		} else if (sceneName == "Intro II") {
			mode = IntroMode.Intermediate;
		} else if (sceneName == "Intro III") {
			mode = IntroMode.Advanced;
		} else {
			mode = IntroMode.Final;
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
		StringPair panelText = GetPanelText ();
		textField.text = panelText.a;
		if (panelText.b == "") {
			Destroy (button);
		} else {
			buttonText.text = panelText.b;
		}
	}

	private StringPair GetPanelText() {
		switch (mode) {
		case IntroMode.Basic:
			return GetBasicPanelText ();
		case IntroMode.Intermediate:
			return GetIntermediatePanelText ();
		case IntroMode.Advanced:
			return GetAdvancedPanelText ();
		case IntroMode.Final:
			return GetFinalPanelText ();
		}
		return new StringPair ();
	}

	private StringPair GetBasicPanelText() {
		switch(panelIndex) {
		case 0:
			return new StringPair ("Welcome!  First off, note the menu directly above you.  If you ever want exit play, just click Back up there.", "Got it");
		case 1:
			return new StringPair ("As you learned in the tutorial, the goal of each level is to hit the target", "Yep");
		case 2:
			return new StringPair ("To beat this level you need to connect to the anchor in front of you and swing into the target", "Right");
		case 3:
			return new StringPair ("Alright, so connect to the Diamond by pointing your controller at it and clicking the touchpad", "");
		default: 
			return new StringPair ("Alright, so connect to the Diamond by pointing your controller at it and clicking the touchpad", "");
		}
	}


	private StringPair GetIntermediatePanelText() {
		switch (panelIndex) {
		case 0:
			return new StringPair ("Way to go!  On the previous level you swung into the target successfully.", "Thanks");
		case 1:
			return new StringPair ("You can detach from your current anchor by pointing away from all anchors and clicking", "Okay");
		case 2:
			return new StringPair ("To beat this level, attach to the anchor in front of you, swing foward, and detach.", "");
		default:
			return new StringPair ("To beat this level, attach to the anchor in front of you, swing foward, and detach.", "");
		}
	}

	private StringPair GetAdvancedPanelText() {
		switch (panelIndex) {
		case 0:
			return new StringPair ("Nice flying!  Now we will try moving from anchor to anchor.", "Sweet");
		case 1:
			return new StringPair ("On this level there are two anchors.  Attach to the first, swing forward, and then attach to the second.  Once you are attached the to second, swing forward and release to fly into the target", "");
		default:
			return new StringPair ("On this level there are two anchors.  Attach to the first, swing forward, and then attach to the second.  Once you are attached the to second, swing forward and release to fly into the target", "");
		}
	}

	private StringPair GetFinalPanelText() {
		switch (panelIndex) {
		case 0:
			return new StringPair ("Awesome flying on that last level! At this point you have learned everything you need to know to win!", "Great");
		case 1:
			return new StringPair ("This is your final test.  Attach to the first anchor, swing forward, release, and fly.  Then attach to the second anchor (on your right) and notice how you rotate around.", "Whoa");
		case 2:
			return new StringPair ("If you time it right, you can fly off the second anchor and into the target.  If you can hit this target, I know you can hit every other target in this game.  Good luck!", "");
		default:
			return new StringPair ("If you time it right, you can fly off the second anchor and into the target.  If you can hit this target, I know you can hit every other target in this game.  Good luck!", "");
		}
	}



	public void NextButtonClicked() {
		panelIndex += 1;
		clickFrameCountdown = 4;
		FillPanelText ();
	}
}
