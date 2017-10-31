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
		level = LevelManager.GetLevel (raceId);

		buttonText.text = level.name;
		if (level.locked) {
			bool newlyUnlocked = LevelController.MaybeUnlockLevel (raceId);
			if (newlyUnlocked) {
				NewlyUnlocked ();
			} else {
				LockLevel ();
			}
		} else {
			SetColor (level.completed);
		}
	}

	private void LockLevel() {
		button.interactable = false;
	}

	//TODO maybe do this on a delay?
	private void NewlyUnlocked() {
		ColorBlock colorBlock = button.colors;
		colorBlock.normalColor = new Color(1f,0f, 0f);
		button.colors = colorBlock;
		GameObject.Find ("SoundEffectController").GetComponent<SoundEffectController> ().PlayUnlock ();
	}

	private void SetColor(bool completed) {
		ColorBlock colorBlock = button.colors;
		if (completed) {
			colorBlock.normalColor = new Color (0f, 1f, 0f);
		} else {
			colorBlock.normalColor = new Color (0f, 0f, 1f);
		}
		button.colors = colorBlock;
	}

	public void OnClick() {
		SceneManager.LoadScene (level.name);
	}
}
