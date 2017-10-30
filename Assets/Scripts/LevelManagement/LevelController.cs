using System.Collections;
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
		string sceneName = SceneManager.GetActiveScene ().name;
		LoadScene (sceneName);
	}

	public void HandleGameWin() {
		ReturnToAntechamber();
	}

	public bool IsRaceLevel() {
		string sceneName = SceneManager.GetActiveScene ().name;
		List<Level> levels = LevelManager.GetLevels ();
		foreach (Level level in levels) {
			if (level.name == sceneName) {
				return true;
			}
		}
		return false;
	}



	public int GetCurrentLevelId() {
		string sceneName = SceneManager.GetActiveScene ().name;
		return LevelManager.LevelIdForName (sceneName);
	}

	private void MarkLevelCompleted() {
		LevelManager.MarkLevelCompleted (currentLevel);
	}

	void MaybeUnlockLevels() {
		List<Level> levels = LevelManager.GetLevels ();
		foreach (Level level in levels) {
			if (level.locked) {
				bool unlocked = true;
				foreach (int preReq in level.preReqs) {
					if (!levels [preReq].completed) {
						unlocked = false;
						break;
					}
				}

				if (unlocked) {
					LevelManager.UnlockLevel (level.level);
				}
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
		List<Level> levels = LevelManager.GetLevels ();
		foreach (Level level in levels) {
			if (!level.completed) {
				return false;
			}
		}
		return true;
	}

	void ReturnToAntechamber() {
		SceneManager.LoadScene ("AntechamberScene");
	}

	void LoadScene(string sceneName) {
		SceneManager.LoadScene (sceneName);
	}
}
