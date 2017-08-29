using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelProvider : MonoBehaviour {

	public static List<Level> GetLevels() {
		List<Level> levels = new List<Level>();
		for (int i = 1; i <= NumLevels (); i++) {
			levels.Add (GetLevel (i));
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
		Level level = new Level ();
		level.name = "Straight";
		level.level = 1;
		level.locked = false;
		level.completed = false;
		return level;
	}


}