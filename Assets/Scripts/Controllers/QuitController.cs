using UnityEngine;

// Ensures correct app and scene setup.
public class QuitController : MonoBehaviour {
	void Start() {
		Input.backButtonLeavesApp = true;
	}

	void Update() {
		// Exit when (X) is tapped.
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}

