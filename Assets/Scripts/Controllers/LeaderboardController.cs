using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour {

	public static void RegisterTime(float raceTime, int raceTrack) {
		SaveLastRaceTime(raceTime, raceTrack);
		MaybeUpdateBestRaceTime (raceTime, raceTrack);
		GooglePlayController.PostScore(raceTime, GetLeaderboardIdForRaceId(raceTrack));
	}

	private static void SaveLastRaceTime(float raceTime, int raceTrack) {
		PlayerPrefs.SetFloat ("last_time" + raceTrack, raceTime);
	}

	private static void MaybeUpdateBestRaceTime(float raceTime, int raceTrack) {
		float bestTime = GetBestRaceTime (raceTrack);
		if (raceTime < bestTime) {
			SetBestRaceTime (raceTime, raceTrack);
		}
	}

	public static float GetLastRaceTime(int raceTrack) {
		return PlayerPrefs.GetFloat ("last_time" + raceTrack, -1);
	}

	public static float GetBestRaceTime(int raceTrack) {
		return PlayerPrefs.GetFloat ("best_time" + raceTrack, float.MaxValue);
	}

	private static void SetBestRaceTime(float raceTime, int raceTrack) {
		PlayerPrefs.SetFloat ("best_time" + raceTrack, raceTime);
	}


	private static string GetLeaderboardIdForRaceId(int raceTrack) {
		if (raceTrack == 1) {
			return "CgkI2LL-oOYDEAIQAA";
		}
		return "CgkI2LL-oOYDEAIQAA";
	}

	public static List<LeaderboardEntry> GetEntries(int raceTrack, int pageSize, int startPosition) {
		//TODO
		return null;
	}
}
