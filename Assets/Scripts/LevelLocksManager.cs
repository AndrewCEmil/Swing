using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLocksManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static List<int> GetLevelLocks() {
		string levelLocksJson = PlayerPrefs.GetString ("LevelLocks");
		if (levelLocksJson.Equals ("") || levelLocksJson.Equals("{}")) {
			InitLevelLocks ();
			levelLocksJson = PlayerPrefs.GetString ("LevelLocks");
		}
		LevelLocks levelLocks = JsonUtility.FromJson<LevelLocks>(levelLocksJson);
		return levelLocks.levelLocks;
	}

	public static string InitLevelLocks() {
		List<int> levelLocks = new List<int> ();
		levelLocks.Add (1);
		SetLevelLocks (levelLocks);
		return JsonUtility.ToJson (levelLocks);
	}

	public static void SetLevelLocks(List<int> levelLocks) {
		LevelLocks lls = new LevelLocks ();
		lls.levelLocks = levelLocks;
		string levelLocksJson = JsonUtility.ToJson (lls);
		PlayerPrefs.SetString ("LevelLocks", levelLocksJson);
		PlayerPrefs.Save ();
	}

	public static void OpenLevelLock (int level) {
		List<int> levelLocks = GetLevelLocks ();
		if (!levelLocks.Contains (level)) {
			levelLocks.Add (level);
		}
		SetLevelLocks (levelLocks);
	}

	public static bool IsLevelLocked(int level) {
		return !GetLevelLocks ().Contains (level);
	}
}
