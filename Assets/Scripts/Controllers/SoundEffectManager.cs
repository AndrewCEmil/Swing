using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

	public AudioClip buttonClickClip;

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
		defaultSoundEffectVolume = 1f;
		soundEffectVolume = defaultSoundEffectVolume;
	}

	public void PlayButtonClicked() {
		audioSource.PlayOneShot (buttonClickClip, soundEffectVolume);
	}
}
