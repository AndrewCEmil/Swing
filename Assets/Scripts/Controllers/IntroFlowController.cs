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
	private IntroMode mode;
	private int panelIndex;
	private int clickFrameCountdown;
	private bool isHighlighted;
	void Start () {
		clickFrameCountdown = 0;
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
		case IntroMode.Final:
			return GetFinalPanelText ();
		}
		return "";
	}

	private string GetBasicPanelText() {
		switch(panelIndex) {
		case 0:
			return "Welcome!  First off, note the menu directly above you.  If you ever want exit play, just click Back up there.";
		case 1:
			return "The goal of each level is to hit the target.  The target is the Red Square in front of you.";
		case 2:
			return "To hit the target you will have to swing through the level by connecting to anchors.  The diamond in front of you is an anchor.";
		case 3:
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
		case 0:
			return "Nice flying!  Now we will try moving from anchor to anchor.";
		case 1:
			return "On this level there are two anchors.  Attach to the first, swing forward, and then attach to the second.  Once you are attached the to second, swing forward and release to fly into the target";
		default:
			return "On this level there are two anchors.  Attach to the first, swing forward, and then attach to the second.  Once you are attached the to second, swing forward and release to fly into the target";
		}
	}

	private string GetFinalPanelText() {
		switch (panelIndex) {
		case 0:
			return "Awesome flying on that last level! At this point you have learned everything you need to know to win!";
		case 1:
			return "This is your final test.  Attach to the first anchor, swing forward, release, and fly.  Then attach to the second anchor (on your right) and notice how you rotate around.";
		case 2:
			return "If you time it right, you can fly off the second anchor and into the target.  If you can hit this target, I know you can hit every other target in this game.  Good luck!";
		default:
			return "If you time it right, you can fly off the second anchor and into the target.  If you can hit this target, I know you can hit every other target in this game.  Good luck!";
		}
	}



	public void NextButtonClicked() {
		panelIndex += 1;
		clickFrameCountdown = 4;
		FillPanelText ();
	}
}
