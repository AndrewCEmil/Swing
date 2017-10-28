using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePanelController : MonoBehaviour {
	public GameObject lastTimeTextObj;
	public GameObject bestTimeTextObj;

	private Text lastTimeText;
	private Text bestTimeText;
	private LevelController levelController;
	void Start () {
		lastTimeText = lastTimeTextObj.GetComponent<Text> ();
		bestTimeText = bestTimeTextObj.GetComponent<Text> ();
		levelController = GameObject.Find ("LevelObj").GetComponent<LevelController> ();
		Initialize ();
	}

	private void Initialize() {

		//Get last race time
		float lastTime = GetLastRaceTime();
		if (lastTime < 0) {
			lastTimeText.text = "N/A";
			bestTimeText.text = "N/A";
			return;
		}
		float bestTime = GetBestRaceTime ();
		lastTimeText.text = lastTime.ToString ();
		bestTimeText.text = bestTime.ToString ();
	}

	private float GetLastRaceTime() {
		return LeaderboardController.GetLastRaceTime (levelController.GetCurrentLevelId ());
	}

	private float GetBestRaceTime() {
		return LeaderboardController.GetBestRaceTime (levelController.GetCurrentLevelId ());
	}
}
