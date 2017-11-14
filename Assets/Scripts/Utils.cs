﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

	public static Orchestrator GetOrchestrator() {
		GameObject obj = GameObject.Find ("Orchestrator");
		if (obj != null) {
			return obj.GetComponent<Orchestrator> ();
		}
		return null;
	}

	public static MusicController GetMusicController() {
		GameObject obj = GameObject.Find ("MusicPlayer");
		if (obj != null) {
			return obj.GetComponent<MusicController> ();
		}
		return null;
	}

	public static SoundEffectController GetSFXController() {
		GameObject obj = GameObject.Find ("SoundEffectController");
		if (obj != null) {
			return obj.GetComponent<SoundEffectController> ();
		}
		return null;
	}
}
