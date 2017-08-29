using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimerMode
{
	PreRace,
	Starting,
	Racing,
	Finished
}

public class TimerController : MonoBehaviour {

	private Orchestrator orchestrator;
	private TimerMode mode;
	private float startTriggeredTime;
	private float startedTime;
	private float endedTime;
	private float totalStartDelay;
	// Use this for initialization
	void Start () {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
		mode = TimerMode.PreRace;
		totalStartDelay = 3f;
	}

	void Update() {
		switch(mode) {
		case TimerMode.PreRace:
			break;
		case TimerMode.Starting:
			DoStarting ();
			break;
		case TimerMode.Racing:
			break;
		case TimerMode.Finished:
			break;
		}
	}

	void DoStarting() {
		if (Time.time - startTriggeredTime > totalStartDelay) {
			StartRace ();
		}
	}

	void StartTriggered() {
		startTriggeredTime = Time.time;
		mode = TimerMode.Starting;
	}

	void StartRace() {
		orchestrator.StartRace ();
		startedTime = Time.time;
		mode = TimerMode.Racing;
	}

	//Returns the time to complete the race
	public float FinishRace() {
		endedTime = Time.time;
		mode = TimerMode.Finished;
		return endedTime - startedTime;
	}

	void OnMouseDown(){
		StartTriggered ();
	}
}
