using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Slider musicSlider;
	public Slider sfxSlider;
	private MusicController musicController;
	private SoundEffectController sfxController;
	// Use this for initialization
	void Start () {
		musicController = GameObject.Find ("MusicPlayer").GetComponent<MusicController> ();
		sfxController = GameObject.Find ("SoundEffectController").GetComponent<SoundEffectController> ();
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

	public void LoadRace0() {
		SceneManager.LoadScene ("Race0");
	}

	public void LoadRace1() {
		SceneManager.LoadScene ("Race1");
	}

	public void LoadRace2() {
		SceneManager.LoadScene ("Race2");
	}

	public void LoadRace3() {
		SceneManager.LoadScene ("Race3");
	}

	public void LoadRace4() {
		SceneManager.LoadScene ("Race4");
	}

	public void LoadRace5() {
		SceneManager.LoadScene ("Race5");
	}

	public void LoadRace6() {
		SceneManager.LoadScene ("Race6");
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

	//TODO initialize settings menu
	private void MaybeInitializeSettings() {
		if (SceneManager.GetActiveScene ().name == "SettingsScene") {
			InitializeSettings ();
		}
	}

	private void InitializeSettings() {
		musicSlider.value = musicController.GetMusicVolume ();
		sfxSlider.value = sfxController.GetVolume ();
	}
}