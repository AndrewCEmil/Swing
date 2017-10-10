using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorController : MonoBehaviour {

	private Orchestrator orchestrator;
	private bool isPointedAt;
	void Start() {
		gameObject.tag = "Anchor";
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
		isPointedAt = false;
		UpdateColor ();
	}

	void OnMouseDown() {
		//ShotAt ();
	}

	public void PointerEnter() {
		SetPointedAt (true);
	}

	public void PointerExit() {
		SetPointedAt (false);
	}

	public void PointerClicked() {
		ShotAt ();
	}

	void ShotAt() {
		orchestrator.HandleShoot (gameObject);
	}

	private void SetPointedAt(bool isPointedAt) {
		isPointedAt = true;
		UpdateColor ();
	}

	private void UpdateColor() {
		if (isPointedAt) {
		} else {

		}
	}
}
