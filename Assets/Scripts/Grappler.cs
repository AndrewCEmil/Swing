using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrapplerMode
{
	Off,
	Shooting,
	Attached
}

public class Grappler : MonoBehaviour {

	private GameObject player;
	private GameObject bullet;
	private GameObject startCube;
	private SpeedPanelController speedPanelController;
	private AnchorController currentAttachedController;
	private Vector3 anchorPosition;
	private GrapplerMode mode;
	private LineRenderer lineRenderer;
	private ParticleSystem lineParticleSystem;
	private GameObject currentPointedAt;
	private float pointedAtSetTime;
	private float pointedAtCooldown;
	private SoundEffectController sfxController;
	private GameObject shotAtTarget;

	// Use this for initialization
	void Start () {
		pointedAtCooldown = .5f;
		player = GameObject.Find ("Player");
		bullet = GameObject.Find ("Bullet");
		startCube = GameObject.Find ("StartCube");
		sfxController = GameObject.Find ("SoundEffectController").GetComponent<SoundEffectController> ();
		lineParticleSystem = GameObject.Find ("LineParticleSystem").GetComponent<ParticleSystem> ();
		speedPanelController = Utils.GetSpeedPanelController ();
		anchorPosition = new Vector3 (0, 0, 0);
		mode = GrapplerMode.Off;
		currentAttachedController = null;
		InitializeLine ();
		InitializeStartCube ();
	}

	private void InitializeLine() {
		GameObject line = GameObject.Find ("Line");
		if (line != null) {
			lineRenderer = line.GetComponent<LineRenderer> ();
			lineRenderer.positionCount = 0;
		}
	}

	private void InitializeStartCube() {
		if(startCube != null) {
			Attach (startCube);
		}
	}
	
	void Update () {
		UpdatePointedAt ();
		switch (mode) {
		case GrapplerMode.Off:
			break;
		case GrapplerMode.Shooting:
			DoShooting ();
			break;
		case GrapplerMode.Attached:
			DoLine ();
			DoParticleLine ();
			break;
		}
	}

	private void DoShooting() {
		if (Vector3.Distance (player.transform.position, shotAtTarget.transform.position) < Vector3.Distance (player.transform.position, bullet.transform.position)) {
			Attach (shotAtTarget);
		}
	}

	void UpdatePointedAt() {
		if (currentPointedAt != null && pointedAtSetTime > 0) {
			if (Time.time - pointedAtSetTime > pointedAtCooldown) {
				RemoveCurrentPointedAt ();
			}
		}
	}

	private void RemoveCurrentPointedAt() {
		currentPointedAt.GetComponent<AnchorController> ().SetPointedAt (false);
		currentPointedAt = null;
	}

	public void AnchorPointedAt(GameObject anchor) {
		if (currentPointedAt != null) {
			RemoveCurrentPointedAt ();
		}
		currentPointedAt = anchor;
		pointedAtSetTime = -1f;
	}

	public void AnchorPointerExit(GameObject anchor) {
		if (currentPointedAt.name == anchor.name) {
			pointedAtSetTime = Time.time;
		}
	}

	public void PointerClicked() {
		if (currentPointedAt != null) {
			shotAtTarget = currentPointedAt;
			Shoot ();
		} else {
			BreakLink ();
		}
	}
	 
	public void BreakLink() {
		currentAttachedController.UnLink ();
		DestroyJoint ();
		if (mode == GrapplerMode.Attached) {
			//sfxController.StopAttached ();
			sfxController.PlayDetach ();
		}
		mode = GrapplerMode.Off;
		lineRenderer.positionCount = 0;
		lineParticleSystem.Stop ();
	}

	private void DestroyJoint() {
		FixedJoint joint = player.GetComponent<FixedJoint> ();
		if(joint != null) {
			Destroy (joint);
		}
	}

	private void Shoot() {
		//Shoot bullet, set state
		bullet.SetActive(true);
		Rigidbody rb = bullet.GetComponent<Rigidbody> ();
		rb.MovePosition (player.transform.position);
		rb.velocity = new Vector3 (0f, 0f, 0f);
		Vector3 direction = (shotAtTarget.transform.position - player.transform.position).normalized;
		rb.AddForce(direction * 6000);
		Physics.IgnoreCollision(bullet.GetComponent<Collider>(), player.GetComponent<Collider>());

		sfxController.PlayShot ();
		mode = GrapplerMode.Shooting;
	}

	public void Attach (GameObject anchor) {
		bullet.SetActive (false);
		if (currentAttachedController != null) {
			currentAttachedController.UnLink ();
		}
		BuildJoint (player, anchor);
		CreateLine (anchor);
		currentAttachedController = anchor.GetComponent<AnchorController> ();
		currentAttachedController.Link ();

		if (speedPanelController != null) {
			speedPanelController.AttachTriggered ();
		}

		anchorPosition = anchor.transform.position;
		mode = GrapplerMode.Attached;
		if (anchor.name == "StartCube") {
			sfxController.PlayShot ();
		} else {
			sfxController.PlayAttach ();
		}
		//sfxController.StartAttached ();
	}

	private void CreateLine(GameObject anchor) {
		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition (0, player.transform.position);
		lineRenderer.SetPosition (1, anchor.transform.position);
	}

	private void DoParticleLine() {
		if (lineParticleSystem.isStopped) {
			lineParticleSystem.Play ();
		}
		float distance = Vector3.Distance (transform.position, anchorPosition) - 1f;
		lineParticleSystem.startLifetime = distance / lineParticleSystem.startSpeed;
		lineParticleSystem.transform.LookAt (anchorPosition);
	}

	private void DoLine() {
		if (lineRenderer.positionCount > 0) {
			lineRenderer.SetPosition (0, player.transform.position);
			lineRenderer.SetPosition (1, anchorPosition);
		}
	}

	void BuildJoint(GameObject link, GameObject anchor) {
		FixedJoint joint = link.GetComponent<FixedJoint> ();
		if (joint == null) {
			link.AddComponent<FixedJoint> ();
			joint = link.GetComponent<FixedJoint> ();
		}
		joint.connectedBody = anchor.GetComponent<Rigidbody> ();
	}
}
