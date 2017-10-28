using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelProvider : MonoBehaviour {

	public static List<Level> GetLevels() {
		List<Level> levels = new List<Level>();
		for (int i = 0; i < NumLevels (); i++) {
			levels.Add (GetNewLevel (i));
		}
		return levels;
	}

	private static Level GetNewLevel(int level) {
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
		case 6:
			return Race6 ();
		case 7:
			return Race7 ();
		case 8:
			return Race8 ();
		}

		return Race0();
	}

	public static int NumLevels() {
		return 9;
	}

	public static Level Race0() {
		Level level = new Level ();
		level.name = "Race0";
		level.level = 0;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { 8 };
		return level;
	}

	//Distance swing
	public static Level Race1() {
		Level level = new Level ();
		level.name = "Race1";
		level.level = 1;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { 8 };
		return level;
	}

	//Circle
	public static Level Race2() {
		Level level = new Level ();
		level.name = "Race2";
		level.level = 2;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { 8 };
		return level;
	}

	//Drop
	public static Level Race3() {
		Level level = new Level ();
		level.name = "Race3";
		level.level = 3;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { 8 };
		return level;
	}

	//Right Angles
	public static Level Race4() {
		Level level = new Level ();
		level.name = "Race4";
		level.level = 4;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { 8 };
		return level;
	}

	//Fields with targeting
	public static Level Race5() {
		Level level = new Level ();
		level.name = "Race5";
		level.level = 5;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { 8 };
		return level;
	}

	//Intro level 1
	public static Level Race6() {
		Level level = new Level ();
		level.name = "Race6";
		level.level = 6;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { };
		return level;
	}

	//Intro level 2
	public static Level Race7() {
		Level level = new Level ();
		level.name = "Race7";
		level.level = 7;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { 6 };
		return level;
	}

	public static Level Race8() {
		Level level = new Level ();
		level.name = "Race8";
		level.level = 8;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { 7 };
		return level;
	}
}