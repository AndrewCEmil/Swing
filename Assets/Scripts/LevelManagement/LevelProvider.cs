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
			return Race0 ();
		case 1:
			return Race1 ();
		}

		return Race0();
	}

	public static int NumLevels() {
		return 2;
	}

	public static Level Race0() {
		Level level = new Level ();
		level.name = "Race0";
		level.level = 0;
		level.locked = false;
		level.completed = false;
		return level;
	}

	public static Level Race1() {
		Level level = new Level ();
		level.name = "Race1";
		level.level = 0;
		level.locked = false;
		level.completed = false;
		return level;
	}


}