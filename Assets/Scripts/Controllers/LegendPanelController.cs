using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendPanelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Level level = LevelManager.GetLevel (20);	
		if (!level.completed) {
			gameObject.SetActive (false);
		}
	}
}
