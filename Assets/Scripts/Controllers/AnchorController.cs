﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorController : MonoBehaviour {

	private Orchestrator orchestrator;
	private bool isPointedAt;
	private bool isLinked;
	private Renderer myRenderer;
	private ParticleSystem ps;
	void Start() {
		gameObject.tag = "Anchor";
		orchestrator = Utils.GetOrchestrator ();
		isPointedAt = false;
		isLinked = false;
		myRenderer = GetComponent<Renderer> ();
		UpdateColor ();
		ps = GetComponentInChildren<ParticleSystem> ();
	}

	public void PointerEnter() {
		orchestrator.AnchorPointedAt (gameObject);
		SetPointedAt (true);
	}

	public void PointerExit() {
		orchestrator.AnchorPointerExited (gameObject);
	}

	public void Link() {
		isLinked = true;
		UpdateColor ();
		if (ps != null) {
			ps.Emit (1000);
		}
	}

	public void UnLink() {
		isLinked = false;
		UpdateColor ();
	}

	public void SetPointedAt(bool pointedAt) {
		isPointedAt = pointedAt;
		UpdateColor ();
	}

	private void UpdateColor() {
		Material material = getMaterial();
		if (isLinked) {
			material.SetColor ("_EmissionColor", new Color (.951f, .647f, 1f));
		} else if (isPointedAt) {
			material.SetColor ("_EmissionColor", new Color (.096f, .485f, .195f));
		} else {
			material.SetColor ("_EmissionColor", new Color (.250f, .208f, .382f));
		}
	}

	private Material getMaterial() {
		return getRenderer().material;
	}

	private Renderer getRenderer() {
		if (myRenderer == null) {
			myRenderer = GetComponent<Renderer> ();
		}
		return myRenderer;
	}
}
