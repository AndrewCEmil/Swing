using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	//TODO everything for sound effects

	private static MusicController instance = null;
	private AudioSource audioSource;
	private float defaultMusicVolume;
	public static MusicController Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		audioSource = GetComponent<AudioSource> ();
		defaultMusicVolume = .5f;

		InitializeSound ();
	}

	private void InitializeSound() {
		audioSource.volume = GetSavedMusicVolume ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void SetMusicVolume(float volume) {
		audioSource.volume = volume;
		SaveMusicVolume(volume);
	}

	private void SaveMusicVolume(float volume) {
		PlayerPrefs.SetFloat ("musicvolume", volume);
		PlayerPrefs.Save ();
	}

	private float GetSavedMusicVolume() {
		float volume = PlayerPrefs.GetFloat ("musicvolume", -1);
		if (volume < 0) {
			volume = defaultMusicVolume;
			SaveMusicVolume (volume);
		}
		return volume;
	}

	public float GetMusicVolume() {
		return audioSource.volume;
	}
}