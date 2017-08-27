using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {

	// Use this for initialization
	private GameObject baseAnchor;
	private GameObject player;
	private Rigidbody playerRb;
	private Vector3 playerStartPosition;
	void Start () {
		baseAnchor = GameObject.Find ("Anchor");
		player = GameObject.Find ("Player");
		playerRb = player.GetComponent<Rigidbody> ();
		playerStartPosition = new Vector3 (0, 0, 0);
	}

	public void CreateLevel(Level level) {
		CleanOldLevel ();

		foreach (Vector3 position in level.anchors) {
			PlaceAnchor (position);
		}

		SetPlayer ();
	}

	private void CleanOldLevel() {
		GameObject[] oldAnchors = GameObject.FindGameObjectsWithTag ("Anchor");
		foreach (GameObject oldAnchor in oldAnchors) {
			Destroy (oldAnchor);
		}
	}

	private void PlaceAnchor(Vector3 position) {
		GameObject newAnchor = Instantiate (baseAnchor);
		newAnchor.tag = "Anchor";
		newAnchor.transform.position = position;
	}

	private void SetPlayer() {
		playerRb.velocity = new Vector3 (0, 0, 0);
		playerRb.MovePosition (playerStartPosition);
	}
}
