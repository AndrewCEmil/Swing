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
		case 2: 
			return Race2 ();
		case 3:
			return Race3 ();
		case 4:
			return Race4 ();
		case 5:
			return Race5 ();
		}

		return Race0();
	}

	public static int NumLevels() {
		return 7;
	}

	public static Level Race0() {
		Level level = new Level ();
		level.name = "Race0";
		level.level = 0;
		level.locked = false;
		level.completed = false;
		return level;
	}

	//Distance swing
	public static Level Race1() {
		Level level = new Level ();
		level.name = "Race1";
		level.level = 1;
		level.locked = false;
		level.completed = false;
		return level;
	}

	//Circle
	public static Level Race2() {
		Level level = new Level ();
		level.name = "Race2";
		level.level = 2;
		level.locked = false;
		level.completed = false;
		return level;
	}

	//Drop
	public static Level Race3() {
		Level level = new Level ();
		level.name = "Race3";
		level.level = 3;
		level.locked = false;
		level.completed = false;
		return level;
	}

	//Right Angles
	public static Level Race4() {
		Level level = new Level ();
		level.name = "Race4";
		level.level = 4;
		level.locked = false;
		level.completed = false;
		return level;
	}

	//Fields with targeting
	public static Level Race5() {
		Level level = new Level ();
		level.name = "Race5";
		level.level = 5;
		level.locked = false;
		level.completed = false;
		return level;
	}

	//Intro level 1
	public static Level Race6() {
		Level level = new Level ();
		level.name = "Race6";
		level.level = 6;
		level.locked = false;
		level.completed = false;
		return level;
	}

}