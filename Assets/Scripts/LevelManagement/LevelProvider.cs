﻿using UnityEngine;
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
			return IntroIV ();
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
		case 13:
			return LittleAdventure ();
		case 14:
			return BigDrop ();
		case 15:
			return MoreDistance ();
		case 16:
			return Walkway ();
		case 17:
			return Playground ();
		case 18:
			return BigAdventure ();
		case 19:
			return Tutorial ();
		case 20:
			return Final ();
		}

		return IntroI();
	}

	public static int NumLevels() {
		return 21;
	}

	public static Level IntroI() {
		Level level = new Level ();
		level.name = "Intro I";
		level.level = 0;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { Tutorial().level };
		return level;
	}

	public static Level IntroII() {
		Level level = new Level ();
		level.name = "Intro II";
		level.level = 1;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { IntroI().level };
		return level;
	}

	public static Level IntroIII() {
		Level level = new Level ();
		level.name = "Intro III";
		level.level = 2;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { IntroII().level };
		return level;
	}

	public static Level Adventure() {
		Level level = new Level ();
		level.name = "Adventure";
		level.level = 6;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { FieldTrip().level, Spiral().level };
		return level;
	}

	public static Level PureDistance() {
		Level level = new Level ();
		level.name = "Pure Distance";
		level.level = 7;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { BigDrop ().level, BigAdventure ().level };
		return level;
	}

	//Circle
	public static Level Spiral() {
		Level level = new Level ();
		level.name = "Spiral";
		level.level = 8;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { Playground ().level };
		return level;
	}

	//Drop
	public static Level BigDrop() {
		Level level = new Level ();
		level.name = "Big Drop";
		level.level = 14;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { Runway().level };
		return level;
	}

	public static Level Runway() {
		Level level = new Level ();
		level.name = "Runway";
		level.level = 4;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { Drop().level };
		return level;
	}

	public static Level Longshot() {
		Level level = new Level ();
		level.name = "Longshot";
		level.level = 5;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { 9 };
		return level;
	}

	public static Level FieldTrip() {
		Level level = new Level ();
		level.name = "Field Trip";
		level.level = 9;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { Playground().level };
		return level;
	}

	public static Level Distance() {
		Level level = new Level ();
		level.name = "Distance";
		level.level = 10;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { IntroIV().level };
		return level;
	}

	public static Level LeftRight() {
		Level level = new Level ();
		level.name = "LeftRight";
		level.level = 11;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { IntroIV().level };
		return level;
	}

	public static Level Drop() {
		Level level = new Level ();
		level.name = "Drop";
		level.level = 12;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { Playground().level };
		return level;
	}

	public static Level LittleAdventure() {
		Level level = new Level ();
		level.name = "Little Adventure";
		level.level = 13;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { Distance().level, LeftRight().level };
		return level;
	}

	public static Level IntroIV() {
		Level level = new Level ();
		level.name = "Intro IV";
		level.level = 3;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { IntroIII().level };
		return level;
	}

	public static Level MoreDistance() {
		Level level = new Level ();
		level.name = "More Distance";
		level.level = 15;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { LittleAdventure().level };
		return level;
	}

	public static Level Walkway() {
		Level level = new Level ();
		level.name = "Walkway";
		level.level = 16;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { LittleAdventure().level };
		return level;
	}

	public static Level Playground() {
		Level level = new Level ();
		level.name = "Playground";
		level.level = 17;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { MoreDistance ().level, Walkway ().level };
		return level;
	}

	public static Level BigAdventure() {
		Level level = new Level ();
		level.name = "Big Adventure";
		level.level = 18;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { Adventure ().level };
		return level;
	}

	public static Level Tutorial() {
		Level level = new Level ();
		level.name = "Tutorial";
		level.level = 19;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { };
		return level;
	}

	public static Level Final() {
		Level level = new Level ();
		level.name = "";
		level.level = 20;
		level.locked = true;
		level.completed = false;
		level.preReqs = new int[] { PureDistance().level };
		return level;
	}
}