using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public static void HandleLevelWin() {
		MarkLevelCompleted ();
		ReturnToAntechamber ();
	}

	public static void HandleLevelLoss() {
		string sceneName = SceneManager.GetActiveScene ().name;
		LoadScene (sceneName);
	}

	public static void HandleGameWin() {
		ReturnToAntechamber();
	}

	public static bool IsRaceLevel() {
		string sceneName = SceneManager.GetActiveScene ().name;
		List<Level> levels = LevelManager.GetLevels ();
		foreach (Level level in levels) {
			if (level.name == sceneName) {
				return true;
			}
		}
		return false;
	}



	public static int GetCurrentLevelId() {
		string sceneName = SceneManager.GetActiveScene ().name;
		return LevelManager.LevelIdForName (sceneName);
	}

	private static void MarkLevelCompleted() {
		LevelManager.MarkLevelCompleted (GetCurrentLevelId());
	}

	//Only returns true on newly unlocked level
	public static bool MaybeUnlockLevel(int levelId) {
		List<Level> levels = LevelManager.GetLevels ();
		Level level = LevelManager.GetLevel (levelId);
		if (!level.locked) {
			return false;
		}
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
		return unlocked;
	}

	static bool FirstTimeGameWon() {
		if (AllLevelsCompleted() && PlayerPrefs.GetInt ("GameWon", 0) == 1) {
			return true;
		}
		return false;
	}

	static bool AllLevelsCompleted() {
		List<Level> levels = LevelManager.GetLevels ();
		foreach (Level level in levels) {
			if (!level.completed) {
				return false;
			}
		}
		return true;
	}

	static void ReturnToAntechamber() {
		SceneManager.LoadScene ("AntechamberScene");
	}

	static void LoadScene(string sceneName) {
		SceneManager.LoadScene (sceneName);
	}
}
