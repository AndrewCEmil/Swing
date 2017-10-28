using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimerMode
{
	PreRace,
	Racing,
	Finished
}

public class TimerController : MonoBehaviour {

	private Orchestrator orchestrator;
	private TimerMode mode;
	private float startedTime;
	private float endedTime;
	// Use this for initialization
	void Start () {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
		mode = TimerMode.PreRace;
	}

	void Update() {
		switch(mode) {
		case TimerMode.PreRace:
			break;
		case TimerMode.Racing:
			break;
		case TimerMode.Finished:
			break;
		}
	}

	public bool RaceStarted () {
		return mode != TimerMode.PreRace;
	}

	public void StartRace() {
		startedTime = Time.time;
		mode = TimerMode.Racing;
	}

	//Returns the time to complete the race
	public float FinishRace() {
		endedTime = Time.time;
		mode = TimerMode.Finished;
		return endedTime - startedTime;
	}
}
