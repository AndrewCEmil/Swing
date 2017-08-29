using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {

	private GameObject player;
	private Rigidbody playerRb;
	private GameObject baseLink;
	private Grappler grappler;
	private Vector3 startForce;
	private LevelController levelController;
	private TimerController timerController;
	private LeaderboardController leaderboardController;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerRb = player.GetComponent<Rigidbody> ();
		baseLink = GameObject.Find ("BaseLink");
		grappler = player.GetComponent<Grappler> ();
		startForce = new Vector3 (2000, 0, 0);
		levelController = GameObject.Find ("LevelObj").GetComponent<LevelController> ();
		timerController = GameObject.Find ("Start").GetComponent<TimerController> ();
		leaderboardController = GameObject.Find ("Leaderboard").GetComponent<LeaderboardController> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void HandleShoot(GameObject anchor) {
		if (timerController.CanShoot ()) {
			grappler.Shoot (anchor);
		}
	}

	public void TargetHit() {
		//Handle timer stuff
		float raceTime = timerController.FinishRace();
		//Handle leaderboard stuff
		leaderboardController.RegisterTime(raceTime, levelController.GetCurrentLevelId());
		//Handle level stuff
		levelController.HandleLevelWin ();
	}

	public void TargetDied() {
		float raceTime = timerController.FinishRace ();
		levelController.HandleLevelLoss ();
	}

	public void StartRace() {
		//Just kick player
		playerRb.AddForce(startForce);
	}
}
