using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Orchestrator : MonoBehaviour {

	private GameObject player;
	private ParticleSystem targetHitParticleSystem;
	private Rigidbody playerRb;
	private Grappler grappler;
	private TimerController timerController;
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
		targetHitParticleSystem = GameObject.Find ("TargetHitParticleSystem").GetComponent<ParticleSystem> ();
		grappler = player.GetComponent<Grappler> ();
		timerController = GameObject.Find ("Timer").GetComponent<TimerController> ();
		sfxController = GameObject.Find ("SoundEffectController").GetComponent<SoundEffectController> ();
		introCanvas = GameObject.Find ("IntroCanvas");
		if (introCanvas != null) {
			introFlowController = introCanvas.GetComponent<IntroFlowController> ();
		}
		targetHitDelay = 2.2f;
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
		targetHitParticleSystem.Emit (10000);
	}

	public void TargetDied() {
		if (targetHitTime > 0) {
			return;
		}
		float raceTime = timerController.FinishRace ();
		LevelController.HandleLevelLoss ();
	}
}
