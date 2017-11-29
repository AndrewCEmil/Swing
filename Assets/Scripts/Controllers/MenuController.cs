using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Slider musicSlider;
	public Slider sfxSlider;
	public Toggle speedPanelToggle;
	public Toggle tipsPanelToggle;
	private MusicController musicController;
	private SoundEffectController sfxController;
	// Use this for initialization
	void Start () {
		musicController = Utils.GetMusicController ();
		sfxController = Utils.GetSFXController ();
		MaybeInitializeSettings ();
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

	public void Quit() {
		PlayerPrefs.Save ();
		Application.Quit ();
	}

	public void SetMusicVolume() {
		musicController.SetMusicVolume (musicSlider.value);
	}

	public void SetSoundEffectVolume() {
		sfxController.SetVolume (sfxSlider.value);
	}

	public void SfxVolumeSliderClicked() {
		sfxController.PlayVolumeSound ();
	}

	public void SfxVolumeSliderReleased() {
		sfxController.StopVolumeSound ();
	}

	public void ClearUserData() {
		PlayerPrefs.DeleteAll ();
	}

	public void TipsPanelEnabledChganged() {
		if (tipsPanelToggle.isOn) {
			PlayerPrefs.SetInt ("TipsPanelDisabled", 0);
		} else {
			PlayerPrefs.SetInt ("TipsPanelDisabled", 1);
		}
	}

	public void SpeedPanelEnabledChanged() {
		if (speedPanelToggle.isOn) {
			PlayerPrefs.SetInt ("SpeedPanelEnabled", 1);
		} else {
			PlayerPrefs.SetInt ("SpeedPanelEnabled", 0);
		}
	}

	//TODO initialize settings menu
	private void MaybeInitializeSettings() {
		if (SceneManager.GetActiveScene ().name == "SettingsScene") {
			InitializeSettings ();
		}
	}

	private void InitializeSettings() {
		musicSlider.value = musicController.GetMusicVolume ();
		sfxSlider.value = sfxController.GetVolume ();
		speedPanelToggle.isOn = isSpeedPanelEnabled ();
		tipsPanelToggle.isOn = isSpeedPanelEnabled ();
	}

	private bool isSpeedPanelEnabled() {
		if (PlayerPrefs.GetInt ("SpeedPanelEnabled") == 0) {
			return false;
		}
		return true;
	}

	private bool isTipsPanelEnabled() {
		if (PlayerPrefs.GetInt ("TipsPanelDisabled") == 1) {
			return false;
		}
		return true;
	}
}