using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelProvider : MonoBehaviour {


	public static Level[] GetLevels() {
		Level[] levels = new Level[NumLevels ()];
		for (int i = 1; i <= NumLevels (); i++) {
			levels [i - 1] = GetLevel (i);
		}
		return levels;
	}

	public static Level GetLevel(int level) {
		switch (level) {
		case 0:
			return Default();
		}

		return Default();
	}

	public static int NumLevels() {
		return 1;
	}

	public static Level Default() {
		Vector3[] positions = new Vector3[1];
		positions [0] = new Vector3 (20, 15, 0);
		Vector3 target = new Vector3 (32, 00, 0);
		Vector3 targetLookAt = new Vector3 (60, 20, 0);
		Level level = new Level ();
		level.anchors = positions;
		level.name = "Straight";
		level.level = 1;
		level.locked = LevelLocksManager.IsLevelLocked (level.level);
		level.target = target;
		level.targetLookAt = targetLookAt;
		return level;
	}
}