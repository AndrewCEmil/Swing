using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalHolderController : MonoBehaviour {


	private GameObject canvas;
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("FinalCanvas");
		canvas.SetActive (false);

		Level level = LevelManager.GetLevel (20);
		bool locked = level.locked;
		if (locked) {
			locked = LevelController.MaybeUnlockLevel (20);
		}
		//if(!locked) {
			Unlock();
		//}
	}

	public void HandleClick() {
		SceneManager.LoadScene ("FINAL");
	}

	void Unlock() {
		StartCoroutine ("UnlockIterator");
	}

	IEnumerator UnlockIterator() {
		for (int i = 10; i >= 0; i--) {
			yield return new WaitForSeconds (.3f);
			GameObject.Find ("Cube" + i).SetActive (false);
		}
		canvas.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
