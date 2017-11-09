﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void PlayButtonClicked() {
		SoundEffectManager.SFXInstance.PlayButtonClicked ();
	}

	public void PlayVolumeSound() {
		SoundEffectManager.SFXInstance.StartVolumeSound ();
	}

	public void StopVolumeSound() {
		SoundEffectManager.SFXInstance.StopVolumeSound ();
	}

	public void PlayAttach() {
		SoundEffectManager.SFXInstance.PlayAttach ();
	}

	public void PlayDetach() {
		SoundEffectManager.SFXInstance.PlayDetach ();
	}

	public void PlayUnlock() {
		SoundEffectManager.SFXInstance.PlayLevelUnlock ();
	}

	public void PlayTargetHit() {
		SoundEffectManager.SFXInstance.PlayTargetHit ();
	}

	public void StartAttached() {
		SoundEffectManager.SFXInstance.StartAttachedSound ();
	}

	public void StopAttached() {
		SoundEffectManager.SFXInstance.StopAttachedSound ();
	}

	public void SetVolume(float volume) {
		SoundEffectManager.SFXInstance.SetVolume (volume);
	}

	public float GetVolume() {
		return SoundEffectManager.SFXInstance.GetVolume ();
	}
}
