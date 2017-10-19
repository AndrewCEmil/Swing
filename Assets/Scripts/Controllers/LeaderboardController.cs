using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RegisterTime(float raceTime, int raceTrack) {
		//TODO
	}

	private string GetLeaderboardIdForRaceId(int raceTrack) {
		if (raceTrack == 1) {
			return "CgkI2LL-oOYDEAIQAA";
		}
		return "CgkI2LL-oOYDEAIQAA";
	}

	public List<LeaderboardEntry> GetEntries(int raceTrack, int pageSize, int startPosition) {
		//TODO
		return null;
	}
}
