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
		SoundEffectManager.SFXInstance.PlayButtonClicked ();
	}

	public void PlayVolumeSound() {
		SoundEffectManager.SFXInstance.StartVolumeSound ();
	}

	public void StopVolumeSound() {
		SoundEffectManager.SFXInstance.StopVolumeSound ();
	}

	public void SetVolume(float volume) {
		SoundEffectManager.SFXInstance.SetVolume (volume);
	}

	public float GetVolume() {
		return SoundEffectManager.SFXInstance.GetVolume ();
	}
}
