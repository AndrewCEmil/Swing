using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePanelController : MonoBehaviour {
	public GameObject lastTimeTextObj;
	public GameObject bestTimeTextObj;

	private LevelController levelController;
	void Start () {

		levelController = GameObject.Find ("LevelObj").GetComponent<LevelController> ();
		Initialize ();
	}

	private void Initialize() {

		//Get last race time
		float lastTime = GetLastRaceTime();
		if (lastTime < 0) {
			TurnOffPanel ();
			return;
		}
		float bestTime = GetBestRaceTime ();
		lastTimeTextObj.GetComponent<GUIText>().text = lastTime.ToString ();
		bestTimeTextObj.GetComponent<GUIText>().text = bestTime.ToString ();
	}

	private void TurnOffPanel() {
		gameObject.SetActive (false);
	}

	private float GetLastRaceTime() {
		return LeaderboardController.GetLastRaceTime (levelController.GetCurrentLevelId ());
	}

	private float GetBestRaceTime() {
		return LeaderboardController.GetBestRaceTime (levelController.GetCurrentLevelId ());
	}
}
