using System.Collections;
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
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.PlayButtonClicked ();
		}
	}

	public void PlayVolumeSound() {
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.StartVolumeSound ();
		}
	}

	public void StopVolumeSound() {
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.StopVolumeSound ();
		}
	}

	public void PlayAttach() {
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.PlayAttach ();
		}
	}

	public void PlayDetach() {
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.PlayDetach ();
		}
	}

	public void PlayUnlock() {
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.PlayLevelUnlock ();
		}
	}

	public void PlayTargetHit() {
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.PlayTargetHit ();
		}
	}

	public void StartAttached() {
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.StartAttachedSound ();
		}
	}

	public void StopAttached() {
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.StopAttachedSound ();
		}
	}

	public void SetVolume(float volume) {
		if (SoundEffectManager.SFXInstance != null) {
			SoundEffectManager.SFXInstance.SetVolume (volume);
		}
	}

	public float GetVolume() {
		if (SoundEffectManager.SFXInstance != null) {
			return SoundEffectManager.SFXInstance.GetVolume ();
		}
		return 0f;
	}
}
