﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Slider musicSlider;
	private SoundController soundController;
	// Use this for initialization
	void Start () {
		MaybeInitializeSettings ();
		soundController = GameObject.Find ("MusicPlayer").GetComponent<SoundController> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadGame() {
		SceneManager.LoadScene ("DemoScene");
	}

	public void LoadAnteChamber() {
		SceneManager.LoadScene ("AntechamberScene");
	}

	public void LoadSettings() {
		SceneManager.LoadScene ("SettingsScene");
	}

	public void LoadStart() {
		SceneManager.LoadScene ("StartScene");
	}

	public void LoadAboutScene() {
		SceneManager.LoadScene ("AboutScene");
	}

	public void LoadTestRace() {
		SceneManager.LoadScene ("TestRace");
	}

	public void Quit() {
		PlayerPrefs.Save ();
		Application.Quit ();
	}

	public void SetMusicVolume() {
		soundController.SetMusicVolume (musicSlider.value);
	}

	//TODO initialize settings menu
	private void MaybeInitializeSettings() {
		if (SceneManager.GetActiveScene ().name == "SettingsScene") {
			InitializeSettings ();
		}
	}

	public void ClearPreferences() {
		PlayerPrefs.DeleteAll ();
	}

	private void InitializeSettings() {
		musicSlider.value = soundController.GetMusicVolume ();
	}
}