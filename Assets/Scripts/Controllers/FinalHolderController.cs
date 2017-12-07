using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalHolderController : MonoBehaviour {


	private GameObject canvas;
	private SoundEffectController sfxController;
	// Use this for initialization
	void Start () {
		sfxController = Utils.GetSFXController ();
		canvas = GameObject.Find ("FinalCanvas");
		canvas.SetActive (false);

		Level level = LevelManager.GetLevel (20);
		bool locked = level.locked;
		if (locked) {
			locked = !LevelController.MaybeUnlockLevel (20);
		}
		if(!locked) {
			Unlock();
		}
		SetPosition ();
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
			if(i != 0) {
				sfxController.PlayDetach ();
			}
		}
		sfxController.PlayUnlock ();
		canvas.SetActive (true);
		SetColor ();
	}

	private void SetColor() {
		Level level = LevelManager.GetLevel (20);
		Button button = GetComponentInChildren<Button> ();
		ColorBlock colorBlock = button.colors;
		if (level.completed) {
			colorBlock.normalColor = new Color (0f/255f, 116f/255f, 39f/255f, 0.66f);
			colorBlock.highlightedColor = new Color (0f / 255f, 116f / 255f, 39f / 255f, 1f);
		} else {
			colorBlock.normalColor = new Color (234f/255f, 141f/255f, 219f/255f, .66f);
			colorBlock.highlightedColor = new Color (234f / 255f, 141f / 255f, 219f / 255f, 1f);
		}
		button.colors = colorBlock;
	}
	private void SetPosition() {
		Vector3 unitVector = transform.position.normalized;
		transform.position = unitVector * 36.66f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
