using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private float maxX;
	private float maxZ;
	private List<Vector3> worlyPoints;
	void Start() {
		deformed = false;
		count = 0;
		middlePosition = new Vector3 (500, 0, 500);
		storedVector = new Vector3 (0, 0, 0);
		maxX = 1000f;
		maxZ = 1000f;
		InitWorlyPoints ();
	}

	private void InitWorlyPoints() {
		float numRandoms = 10;
		worlyPoints = new List<Vector3> ();
		for (int i = 0; i < numRandoms; i++) {
			worlyPoints.Add (new Vector3 (Random.Range (0, maxX), 0, Random.Range (0, maxZ)));
		}
	}

	void FBMDeform() {
		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter> ();
		Mesh mesh = meshFilter.mesh;
		Vector3[] baseVerticies = mesh.vertices;
		Vector3[] vertices = new Vector3[baseVerticies.Length];

		for (int i = 0; i < baseVerticies.Length; i++) {
			Vector3 vertex = baseVerticies [i];
			float scale = GetScale (vertex);
			float noise = FBMNoise (vertex);
			vertex.y = noise * scale;
			vertices[i] = vertex;
		}

		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

	//Actually very smooth lol
	void RidgeDeform() {
		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter> ();
		Mesh mesh = meshFilter.mesh;
		Vector3[] baseVerticies = mesh.vertices;
		Vector3[] vertices = new Vector3[baseVerticies.Length];

		for (int i = 0; i < baseVerticies.Length; i++) {
			Vector3 vertex = baseVerticies [i];
			float scale = GetScale (vertex);
			float noise = RidgeNoise (vertex);
			vertex.y = noise * scale / 100f - 20f;
			vertices[i] = vertex;
		}

		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}


	private float FBMNoise(Vector3 vertex) {
		vertex.x = vertex.x / maxX;
		vertex.z = vertex.z / maxZ;
		float result = 0f;
		float amplitud = .5f;
		int octaves = 6;
		for (int i = 0; i < octaves; i++) {
			float noise = Mathf.PerlinNoise (vertex.x + Time.time /5f, vertex.z + Time.time/5f);
			result += amplitud * noise;
			vertex = vertex * 4f;
			amplitud = amplitud * .8f;
		}
		return result;
	}

	private float RidgeNoise(Vector3 vertex) {
		vertex.x = vertex.x / maxX;
		vertex.z = vertex.z / maxZ;
		float result = 0f;
		float amplitud = .5f;
		float lacunarity = 1f;
		float gain = .8f;
		int octaves = 8;
		float prev = 0f;;
		for (int i = 0; i < octaves; i++) {
			float noise = Mathf.Abs (Mathf.Sin (Mathf.PerlinNoise (vertex.x + Time.time / 5f, vertex.z + Time.time / 5f)));
			noise = 1 - noise;
			noise = noise * noise;


			result += amplitud * noise;
			result += result + noise * amplitud * prev;
			prev = result;
			vertex = vertex * lacunarity;
			amplitud = amplitud * gain;
		}
		return result;
	}

	void WorlyDeform() {
		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter> ();
		Mesh mesh = meshFilter.mesh;
		Vector3[] baseVerticies = mesh.vertices;
		Vector3[] vertices = new Vector3[baseVerticies.Length];

		UpdateWorlyPoints();

		for (int i = 0; i < baseVerticies.Length; i++) {
			Vector3 vertex = baseVerticies[i];
			float noise = WorlyNoise (vertex);
			vertex.y = noise * GetScale (vertex) - 10f;
			vertices[i] = vertex;
		}

		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

	private void UpdateWorlyPoints() {
		Vector3 curPoint;
		Vector3 up = new Vector3 (0, 1, 0);
		for (int i = 0; i < worlyPoints.Count; i++) {
			curPoint = worlyPoints [i] - middlePosition;
			worlyPoints [i] = Quaternion.AngleAxis (Time.deltaTime * 10f, up) * curPoint + middlePosition;
		}
	}

	private float WorlyNoise (Vector3 point) {
		return MinDistance (point, worlyPoints) / maxDistance;
	}

	private float MinDistance(Vector3 point, List<Vector3> points) {
		point.y = 0f;
		float minDist = float.MaxValue;
		foreach (Vector3 p in points) {
			minDist = Mathf.Min (minDist, Vector3.Distance (point, p));
		}
		return minDist;
	}
}
*/