using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Orchestrator : MonoBehaviour {

	private GameObject player;
	private Rigidbody playerRb;
	private Grappler grappler;
	private TimerController timerController;
	private LeaderboardController leaderboardController;
	private SoundEffectController sfxController;
	private GameObject platform;
	private GameObject introCanvas;
	private GameObject tutorialCanvas;
	private IntroFlowController introFlowController;

	float targetHitTime;
	float targetHitDelay;
	void Start () {
		player = GameObject.Find ("Player");
		playerRb = player.GetComponent<Rigidbody> ();
		grappler = player.GetComponent<Grappler> ();
		timerController = GameObject.Find ("Timer").GetComponent<TimerController> ();
		leaderboardController = GameObject.Find ("Leaderboard").GetComponent<LeaderboardController> ();
		sfxController = GameObject.Find ("SoundEffectController").GetComponent<SoundEffectController> ();
		introCanvas = GameObject.Find ("IntroCanvas");
		if (introCanvas != null) {
			introFlowController = introCanvas.GetComponent<IntroFlowController> ();
		}
		targetHitDelay = 1f;
		targetHitTime = -1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (GvrControllerInput.AppButtonUp) {
			HandleAppButton ();
		}

		if (GvrControllerInput.ClickButtonDown) {
			PointerClicked ();
		}

		if (targetHitTime > 0 && Time.time - targetHitTime > targetHitDelay) {
			//Time.timeScale = 1f;
			LevelController.HandleLevelWin ();
		}
	}

	private void PointerClicked() {
		if (introCanvas != null) {
			if (introFlowController.IsHighlighted() || introFlowController.RecentlyClicked ()) {
				return;
			}
		}
		//TODO another sound
		if (!timerController.RaceStarted ()) {
			timerController.StartRace ();
			playerRb.useGravity = true;
		}
		grappler.PointerClicked ();
	}

	private void HandleAppButton() {
		if (LevelController.IsRaceLevel ()) {
			LevelController.HandleLevelLoss();
		}
	}

	public void AnchorPointedAt(GameObject anchor) {
		sfxController.PlayAnchorHighlight ();
		grappler.AnchorPointedAt (anchor);
	}

	public void AnchorPointerExited(GameObject anchor) {
		grappler.AnchorPointerExit (anchor);
	}

	public void TargetHit() {
		//Handle timer stuff
		float raceTime = timerController.FinishRace();
		//Handle leaderboard stuff
		LeaderboardController.RegisterTime(raceTime, LevelController.GetCurrentLevelId());
		//Handle level stuff
		targetHitTime = Time.time;
		sfxController.PlayTargetHit ();
		//Time.timeScale = 0.3f;
	}

	public void TargetDied() {
		float raceTime = timerController.FinishRace ();
		LevelController.HandleLevelLoss ();
	}
}
