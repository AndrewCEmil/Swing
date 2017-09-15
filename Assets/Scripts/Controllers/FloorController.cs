using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FloorController : MonoBehaviour {

	public int xSize;
	public int ySize;
	public int scale;
	private Vector3[] verticies;
	private List<GameObject> anchors;
	private Orchestrator orchestrator;
	private float maxHeight;
	private float maxDistance;
	private Vector3 middle;
	private Vector3 storedVector;
	private void Awake () {
		InitVariables ();
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
		Generate();
	}

	private void InitVariables() {
		maxHeight = 100f;
		maxDistance = Mathf.Sqrt ((xSize * xSize + ySize * ySize) * scale) / 2f;
		anchors = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Anchor"));
		middle = new Vector3 (xSize * scale / 2f, 0, ySize * scale / 2f);
	}

	private void Generate() {
		Mesh mesh = new Mesh ();
		mesh.name = "MovingMesh";
		GetComponent<MeshFilter> ().mesh = mesh;
		verticies = new Vector3[(xSize + 1) * (ySize + 1)];
		Vector2[] uv = new Vector2[verticies.Length];
		float timez = 3.23f;
		for (int i = 0, y = 0; y <= ySize * scale; y += scale) {
			for (int x = 0; x <= xSize * scale; x += scale, i++) {
				float noise = Mathf.PerlinNoise (timez + (x / scale), timez + (y / scale));
				verticies [i] = new Vector3 (x, noise, y) - middle;
				verticies [i].y = verticies [i].y * GetScale (verticies [i]);
				uv [i] = new Vector2 ((float)x / xSize * scale, (float)y / ySize * scale);
			}
		}

		mesh.vertices = verticies;
		mesh.uv = uv;

		int[] triangles = new int[xSize * ySize * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
			for (int x = 0; x < xSize; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
				triangles[ti + 5] = vi + xSize + 2;
			}
		}
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
		mesh.MarkDynamic ();
	}

	float GetScale(Vector3 position) {
		float scale = maxHeight * MinDist (position) / maxDistance;
		return scale;
	}

	float MinDist(Vector3 position) {
		float minDist = float.MaxValue;
		foreach (GameObject anchor in anchors) {
			minDist = Mathf.Min (minDist, Vector3.Distance (position, anchor.transform.position));
		}
		return minDist;
	}
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger with Floor");
		if (other.name == "Player") {
			orchestrator.TargetDied ();
		}
	}
}
