using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	private int currentLevel;
	private LevelCreator levelCreator;
	void Start () {
		currentLevel = GetCurrentLevel ();
		levelCreator = GameObject.Find ("LevelObj").GetComponent<LevelCreator> ();
	}

	public void LoadLevel () {
		LoadCurrentLevel ();
	}

	public void HandleLevelWin() {
		int nextLevel = currentLevel + 1;
		if (nextLevel > LevelProvider.NumLevels ()) {
			HandleGameWin ();
			return;
		}

		SetCurrentLevel (nextLevel);
		LoadCurrentLevel ();
	}


	public void HandleLevelLoss() {
		LoadCurrentLevel ();
	}

	public void HandleGameWin() {
		//TODO
	}

	private void LoadCurrentLevel() {
		levelCreator.CreateLevel (LevelProvider.GetLevel (currentLevel));
	}

	private int GetCurrentLevel() {
		int cur = PlayerPrefs.GetInt ("CurrentLevel", -1);
		if (cur == -1) {
			cur = InitCurrentLevel ();
		}
		return cur;
	}

	private void SetCurrentLevel(int level) {
		PlayerPrefs.SetInt ("CurrentLevel", level);
		LevelLocksManager.OpenLevelLock (level);
		currentLevel = level;
	}

	private int InitCurrentLevel() {
		int defaultLevel = 1;
		SetCurrentLevel (defaultLevel);
		return defaultLevel;
	}
}
