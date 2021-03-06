﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

	public AudioClip buttonClickClip;
	public AudioClip volumeClip;
	public AudioClip hitTargetClip;
	public AudioClip attachClip;
	public AudioClip detachClip;
	public AudioClip attachedClip;
	public AudioClip unlockClip;
	public AudioClip shotClip;
	public AudioClip buttonHighlightClip;
	public AudioClip anchorHighlightClip;

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
		playOneShot (buttonClickClip);
	}

	public void PlayAttach() {
		playOneShot (attachClip);
	}

	public void PlayDetach() {
		playOneShot (detachClip);
	}

	public void PlayLevelUnlock() {
		playOneShot (unlockClip);
	}

	public void PlayTargetHit() {
		playOneShot (hitTargetClip);
	}

	public void PlayShot() {
		playOneShot (shotClip);
	}

	public void PlayButtonHightlight() {
		playOneShotVolumeOffset (buttonHighlightClip, .3f);
	}

	public void PlayAnchorHighlight() {
		playOneShot (anchorHighlightClip);
	}

	private void playOneShot(AudioClip audioClip) {
		playOneShotVolumeOffset (audioClip, 0);
	}

	private void playOneShotVolumeOffset(AudioClip audioClip, float volumeOffset) {
		playOneShotVolume (audioClip, soundEffectVolume - volumeOffset);
	}

	private void playOneShotVolume(AudioClip audioClip, float volume) {
		audioSource.PlayOneShot (audioClip, volume);
	}

	public void StartAttachedSound() {
		audioSource.clip = attachedClip;
		audioSource.Play ();
	}

	public void StopAttachedSound() {
		audioSource.Stop ();
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
