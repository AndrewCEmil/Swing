using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Orchestrator : MonoBehaviour {

	private GameObject player;
	private Rigidbody playerRb;
	private Grappler grappler;
	private Vector3 startForce;
	private LevelController levelController;
	private TimerController timerController;
	private LeaderboardController leaderboardController;
	private SoundEffectController sfxController;
	private GameObject platform;
	void Start () {
		player = GameObject.Find ("Player");
		playerRb = player.GetComponent<Rigidbody> ();
		grappler = player.GetComponent<Grappler> ();
		startForce = new Vector3 (2000, 0, 0);
		levelController = GameObject.Find ("LevelObj").GetComponent<LevelController> ();
		timerController = GameObject.Find ("Timer").GetComponent<TimerController> ();
		leaderboardController = GameObject.Find ("Leaderboard").GetComponent<LeaderboardController> ();
		sfxController = GameObject.Find ("SoundEffectController").GetComponent<SoundEffectController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GvrController.AppButtonUp) {
			HandleAppButton ();
		}
	}

	public void PointerClicked() {
		//TODO another sound
		sfxController.PlayButtonClicked ();
		grappler.PointerClicked ();
	}

	private void HandleAppButton() {
		if (levelController.IsRaceLevel ()) {
			levelController.HandleLevelLoss();
		}
	}

	public void AnchorPointedAt(GameObject anchor) {
		grappler.AnchorPointedAt (anchor);
	}

	public void AnchorPointerExited(GameObject anchor) {
		grappler.AnchorPointerExit (anchor);
	}

	public void TargetHit() {
		//Handle timer stuff
		float raceTime = timerController.FinishRace();
		//Handle leaderboard stuff
		LeaderboardController.RegisterTime(raceTime, levelController.GetCurrentLevelId());
		//Handle level stuff
		levelController.HandleLevelWin ();
	}

	public void TargetDied() {
		float raceTime = timerController.FinishRace ();
		levelController.HandleLevelLoss ();
	}

	public void StartRace() {
		player.GetComponent<Rigidbody> ().useGravity = true;
	}
}
