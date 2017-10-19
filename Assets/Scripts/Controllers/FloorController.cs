using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FloorController : MonoBehaviour {
	public int xSize;
	public int ySize;
	public int scale;
	public float maxHeight;
	public float maxDistance;

	private Orchestrator orchestrator;
	private Vector3[] verticies;
	private Vector3 storedVector;
	private Vector3 middlePosition;
	private void Awake () {
		orchestrator = GameObject.Find ("Orchestrator").GetComponent<Orchestrator> ();
		CreateMesh();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger with Floor");
		if (other.name == "Player") {
			orchestrator.TargetDied ();
		}
	}

	private void CreateMesh() {
		Mesh mesh = new Mesh ();
		mesh.name = "Landscape";
		GetComponent<MeshFilter> ().mesh = mesh;
		verticies = new Vector3[(xSize + 1) * (ySize + 1)];
		Vector2[] uv = new Vector2[verticies.Length];
		for (int i = 0, y = 0; y <= ySize * scale; y += scale) {
			for (int x = 0; x <= xSize * scale; x += scale, i++) {
				verticies [i] = new Vector3 (x, 0, y);
				uv [i] = new Vector2 ((float)x / (xSize * scale), (float)y / (ySize * scale));
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
}



/*
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Landscape : MonoBehaviour {

	public int xSize;
	public int ySize;
	public int scale;
	public float maxHeight;
	public float maxDistance;

	private Vector3[] verticies;
	private Vector3 storedVector;
	private Vector3 middlePosition;

	private void Awake () {
		InitVariables ();
		CreateMesh();
	}

	private void InitVariables() {
		middlePosition = new Vector3 (500, 0, 500);
	}

	void Update () {
		Deform ();
	}

	private void Deform() {
		PerlinDeform ();
	}

	void PerlinDeform() {
		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter> ();
		Mesh mesh = meshFilter.mesh;
		Vector3[] baseVerticies = mesh.vertices;
		Vector3[] vertices = new Vector3[baseVerticies.Length];

		float timez = (Time.time + 3f) / 5f;
		for (var i=0; i < baseVerticies.Length; i++) {
			Vector3 vertex = baseVerticies[i];
			float scale = GetScale (vertex);
			float noise = Mathf.PerlinNoise (timez + vertex.x, timez + vertex.z);
			vertex.y = noise * scale;
			vertices[i] = vertex;
		}

		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

	float GetScale(Vector3 position) {
		return maxHeight * ScaleFunction (position);
	}

	float ScaleFunction(Vector3 position) {
		return (GetRadius (position) / maxDistance);
	}

	float GetRadius(Vector3 position) {
		storedVector.x = position.x;
		storedVector.z = position.z;
		return Vector3.Distance (storedVector, middlePosition);
	}

	private void CreateMesh() {
		Mesh mesh = new Mesh ();
		mesh.name = "Landscape";
		GetComponent<MeshFilter> ().mesh = mesh;
		verticies = new Vector3[(xSize + 1) * (ySize + 1)];
		Vector2[] uv = new Vector2[verticies.Length];
		for (int i = 0, y = 0; y <= ySize * scale; y += scale) {
			for (int x = 0; x <= xSize * scale; x += scale, i++) {
				verticies [i] = new Vector3 (x, 0, y);
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
}

*/