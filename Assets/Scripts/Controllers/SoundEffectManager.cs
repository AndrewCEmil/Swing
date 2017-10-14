using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

	public AudioClip buttonClickClip;
	public AudioClip volumeClip;

	private AudioSource audioSource;
	public float soundEffectVolume;
	private float defaultSoundEffectVolume;
	private static SoundEffectManager instance = null;
	public static SoundEffectManager SFXInstance {
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
		defaultSoundEffectVolume = 0.5f;
		soundEffectVolume = GetSavedSfxVolume ();
		audioSource.volume = soundEffectVolume;
	}

	private void SaveVolume(float volume) {
		PlayerPrefs.SetFloat ("sfxvolume", volume);
		PlayerPrefs.Save ();
	}

	private float GetSavedSfxVolume() {
		float volume = PlayerPrefs.GetFloat ("sfxvolume", -1);
		if (volume < 0) {
			volume = defaultSoundEffectVolume;
			SaveVolume (volume);
		}
		return volume;
	}

	public void PlayButtonClicked() {
		audioSource.PlayOneShot (buttonClickClip, soundEffectVolume);
	}

	public void StartVolumeSound() {
		audioSource.clip = volumeClip;
		audioSource.Play ();
	}

	public void StopVolumeSound() {
		audioSource.Stop ();
	}

	public void SetVolume(float volume) {
		SaveVolume (volume);
		soundEffectVolume = volume;
		audioSource.volume = volume;
	}

	public float GetVolume() {
		return soundEffectVolume;
	}
}
