using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	private Orchestrator orchestrator;
	void Start () {
		orchestrator = Utils.GetOrchestrator ();
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger with Target");
		if (other.name == "Player") {
			if (orchestrator != null) {
				orchestrator.TargetHit ();
			} else {
				TutorialTargetHit ();
			}
		}
	}

	//I know this is horrific, but I don't care I want to move on
	private void TutorialTargetHit() {
		TutorialController1 tc1 = GameObject.Find ("TutorialObject").GetComponent<TutorialController1> ();
		if (tc1 != null) {
			tc1.TargetHit ();
			return;
		}
		TutorialController2 tc2 = GameObject.Find ("TutorialObject").GetComponent<TutorialController2> ();
		if (tc2 != null) {
			tc2.TargetHit ();
			return;
		}
	}
}
