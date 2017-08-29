using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimerMode
{
	PreRace,
	Starting,
	Racing
}

public class TimerController : MonoBehaviour {

	private Orchestrator orchestrator;
	private TimerMode mode;
	private float startTriggeredTime;
	private float startedTime;
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

	void OnMouseDown(){
		StartTriggered ();
	}
}
