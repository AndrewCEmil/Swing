using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayController : MonoBehaviour {

	public static void SignIn() {
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if(success) {
				Debug.Log("Success authenticating user with google play");
			} else {
				Debug.Log("FAILURE authenticating user with google play");
			}
		});
	}

	public static void PostScore(float score, string leaderboardId) {
		// post score 12345 to leaderboard ID "Cfji293fjsie_QA")
		Social.ReportScore((long) score, leaderboardId, (bool success) => {
			// handle succes			if(success) {
			if(success) {
				Debug.Log("Success posting score to leaderboard");
			} else {
				Debug.Log("Failuring posting score to leaderboard");
			}
		});
	}
}




