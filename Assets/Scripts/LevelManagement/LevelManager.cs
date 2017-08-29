using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public static void UnlockLevel (int levelId) {
		List<Level> levels = GetLevels ();
		foreach (Level level in levels) {
			if (level.level == levelId) {
				level.locked = false;
			}
		}
		SetLevels (levels);
	}

	public static void MarkLevelCompleted (int levelId) {
		List<Level> levels = GetLevels ();
		foreach (Level level in levels) {
			if (level.level == levelId) {
				level.completed = true;
			}
		}
		SetLevels (levels);
	}

	//Defaults to true
	public static bool IsLevelLocked(int levelId) {
		List<Level> levels = GetLevels ();
		foreach (Level level in levels) {
			if (level.level == levelId) {
				return level.locked;
			}
		}
		return true;
	}

	public static bool IsLevelCompleted(int levelId) {
		List<Level> levels = GetLevels ();
		foreach (Level level in levels) {
			if (level.level == levelId) {
				return level.completed;
			}
		}
		return false;
	}

	public static List<Level> GetLevels() {
		string levelsJson = PlayerPrefs.GetString ("Levels");
		if (levelsJson.Equals ("") || levelsJson.Equals("{}")) {
			InitLevels ();
			levelsJson = PlayerPrefs.GetString ("Levels");
		}
		Levels levels = JsonUtility.FromJson<Levels>(levelsJson);
		return levels.levels;
	}

	public static int LevelIdForName(string levelName) {
		foreach (Level level in GetLevels()) {
			if (level.name == levelName) {
				return level.level;
			}
		}
		return -1;
	}

	private static void InitLevels() {
		SetLevels (LevelProvider.GetLevels ());
	}

	private static void SetLevels(List<Level> levels) {
		Levels levelsObj = new Levels ();
		levelsObj.levels = levels;
		string levelsJson = JsonUtility.ToJson (levelsObj);
		PlayerPrefs.SetString ("Levels", levelsJson);
		PlayerPrefs.Save ();
	}
}
