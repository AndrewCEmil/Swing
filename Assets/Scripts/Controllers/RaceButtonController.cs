using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceButtonController : MonoBehaviour {

	public int raceId;
	private Level level;
	private Button button;
	private Text buttonText;
	// Use this for initialization
	void Start () {
		button = GetComponentInChildren<Button> ();
		buttonText = GetComponentInChildren<Text> ();
		level = LevelProvider.GetLevel (raceId);

		buttonText.text = level.name;
		if (level.locked) {
			LockLevel ();
		}
	}

	private void LockLevel() {
		button.interactable = false;
	}

	public void OnClick() {
		SceneManager.LoadScene (level.name);
	}
}
