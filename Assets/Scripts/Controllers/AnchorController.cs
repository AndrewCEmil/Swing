using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorController : MonoBehaviour {

	private Orchestrator orchestrator;
	private bool isPointedAt;
	private float emissionIntensity;
	private Renderer myRenderer;
	void Start() {
		gameObject.tag = "Anchor";
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
		isPointedAt = false;
		emissionIntensity = 1f;
		myRenderer = GetComponent<Renderer> ();
		UpdateColor ();
	}

	public void PointerEnter() {
		orchestrator.AnchorPointedAt (gameObject);
		SetPointedAt (true);
	}

	public void PointerExit() {
		SetPointedAt (false);
	}

	private void SetPointedAt(bool pointedAt) {
		isPointedAt = pointedAt;
		UpdateColor ();
	}

	private void UpdateColor() {
		Material material = getMaterial();
		if (isPointedAt) {
			material.SetColor ("_EmissionColor", new Color (.096f, .485f, .195f));
		} else {
			material.SetColor ("_EmissionColor", new Color (.250f, .208f, .382f));
		}
	}

	private Material getMaterial() {
		return myRenderer.material;
	}
}
