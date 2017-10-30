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
			return IntroI ();
		case 1:
			return IntroII ();
		case 2: 
			return IntroIII ();
		case 3:
			return BigDrop ();
		case 4:
			return Runway ();
		case 5:
			return Longshot ();
		case 6:
			return Adventure ();
		case 7:
			return PureDistance ();
		case 8:
			return Spiral ();
		case 9:
			return FieldTrip ();
		case 10:
			return Distance ();
		case 11:
			return LeftRight ();
		case 12:
			return Drop ();
		}

		return IntroI();
	}

	public static int NumLevels() {
		return 10;
	}

	public static Level IntroI() {
		Level level = new Level ();
		level.name = "Intro I";
		level.level = 0;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { };
		return level;
	}

	public static Level IntroII() {
		Level level = new Level ();
		level.name = "Intro II";
		level.level = 1;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 0 };
		return level;
	}

	public static Level IntroIII() {
		Level level = new Level ();
		level.name = "Intro III";
		level.level = 2;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 1 };
		return level;


	}

	public static Level Adventure() {
		Level level = new Level ();
		level.name = "Adventure";
		level.level = 6;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}

	public static Level PureDistance() {
		Level level = new Level ();
		level.name = "Pure Distance";
		level.level = 7;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}

	//Circle
	public static Level Spiral() {
		Level level = new Level ();
		level.name = "Spiral";
		level.level = 8;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}

	//Drop
	public static Level BigDrop() {
		Level level = new Level ();
		level.name = "Big Drop";
		level.level = 3;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}

	public static Level Runway() {
		Level level = new Level ();
		level.name = "Runway";
		level.level = 4;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}

	public static Level Longshot() {
		Level level = new Level ();
		level.name = "Longshot";
		level.level = 5;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}

	public static Level FieldTrip() {
		Level level = new Level ();
		level.name = "Field Trip";
		level.level = 9;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}

	public static Level Distance() {
		Level level = new Level ();
		level.name = "Distance";
		level.level = 10;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}

	public static Level LeftRight() {
		Level level = new Level ();
		level.name = "LeftRight";
		level.level = 11;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}

	public static Level Drop() {
		Level level = new Level ();
		level.name = "Drop";
		level.level = 12;
		level.locked = false;
		level.completed = false;
		level.preReqs = new int[] { 2 };
		return level;
	}
}