using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsController : MonoBehaviour {

	private Text text;
	private List<string> texts;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("TipsPanelDisabled") == 1) {
			gameObject.SetActive (false);
		} else {
			text = GetComponentInChildren<Text> ();
			texts = new List<string> { "a", "b", "c" };
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
