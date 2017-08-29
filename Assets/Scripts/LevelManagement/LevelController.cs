﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	private int currentLevel;
	void Start () {
		currentLevel = GetCurrentLevelId ();
	}

	public void HandleLevelWin() {
		MarkLevelCompleted ();
		MaybeUnlockLevels ();
		if (FirstTimeGameWon ()) {
			HandleGameWin ();
			return;
		}
		ReturnToAntechamber ();
	}

	public void HandleLevelLoss() {
		//TODO
		ReturnToAntechamber();
	}

	public void HandleGameWin() {
		//TODO
		ReturnToAntechamber();
	}

	public int GetCurrentLevelId() {
		string sceneName = SceneManager.GetActiveScene ().name;
		return LevelManager.LevelIdForName (sceneName);
	}

	private void MarkLevelCompleted() {
		LevelManager.MarkLevelCompleted (currentLevel);
	}

	//For now just unlocks next level
	void MaybeUnlockLevels() {
		foreach (Level level in LevelManager.GetLevels()) {
			if (level.level == currentLevel + 1 && level.locked) {
				LevelManager.UnlockLevel (level.level);
			}
		}

	}

	bool FirstTimeGameWon() {
		if (AllLevelsCompleted() && PlayerPrefs.GetInt ("GameWon", 0) == 1) {
			return true;
		}
		return false;
	}

	bool AllLevelsCompleted() {
		foreach (Level level in LevelManager.GetLevels()) {
			if (!level.completed) {
				return false;
			}
		}
		return true;
	}

	void ReturnToAntechamber() {
		SceneManager.LoadScene ("AntechamberScene");
	}
}
