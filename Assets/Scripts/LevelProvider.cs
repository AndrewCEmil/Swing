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

	static int NumLevels() {
		return 1;
	}

	public static Level Default() {
		Vector3[] positions = new Vector3[4];
		positions [0] = new Vector3 (10, 10, 10);
		positions [1] = new Vector3 (-10, 10, 10);
		positions [2] = new Vector3 (-10, 10, -10);
		positions [3] = new Vector3 (10, 10, -10);
		Vector3[] drifts = new Vector3[5];
		drifts [0] = new Vector3 (5, 5, 5);
		drifts [1] = new Vector3 (15, 15, 15);
		drifts [2] = new Vector3 (-15, 15, 15);
		drifts [3] = new Vector3 (-15, 15, -15);
		drifts [4] = new Vector3 (15, 15, -15);
		Level level = new Level ();
		level.anchors = positions;
		level.name = "Ring";
		level.level = 1;
		level.locked = LevelLocksManager.IsLevelLocked (level.level);
		return level;
	}
}