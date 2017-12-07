using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TipsController : MonoBehaviour {

	private Text text;
	private List<string> texts;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("TipsPanelDisabled") == 1 || SceneManager.GetActiveScene().name.StartsWith("Intro")) {
			gameObject.SetActive (false);
		} else {
			text = GetComponentInChildren<Text> ();
			texts = new List<string> { 
				"Remember that you can exit a race by looking above you for the back button",
				"Try attaching to anchors as your fly perpendicular to them - this will minimize your speed loss",
				"You can always restart a level simply by pressing the \"-\" button on your Daydream controller",
				"Recallibrate your Daydream setup by holding down the bottom button - it drifts quite easily!",
				"Try using the speed panel (described in the about area) to help improve your play",
				"Have you noticed the mysterious object to the right of the level selection menu???",
				"You can turn off these tips in the settings panel",
				"DON'T. GIVE. UP."
			};
			text.text = GetText ();
		}
	}

	private string GetText() {
		return texts [GetTextIndex()];
	}

	private int GetTextIndex() {
		int textIndex = PlayerPrefs.GetInt ("TextIndex");
		PlayerPrefs.SetInt ("TextIndex", (textIndex + 1) % texts.Count);
		return textIndex;
	}
}
