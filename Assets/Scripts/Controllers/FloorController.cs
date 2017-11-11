using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FloorController : MonoBehaviour {
	public int deformType;
	public int xSize;
	public int ySize;
	public int scale;
	public float maxHeight;
	public float hillDistance;
	public float seed;

	private Vector3[] verticies;
	private Vector3 storedVector;
	private Vector3 middlePosition;
	private float maxDistance;
	private float maxWorlyDistance;
	private float maxX;
	private float maxY;
	private List<Vector3> worlyPoints;
	private void Awake () {
		middlePosition = new Vector3 (xSize * scale / 2f, 0, ySize * scale / 2f);
		maxDistance = Vector3.Distance (middlePosition, new Vector3 (0, 0, 0));
		maxX = xSize * scale;
		maxY = ySize * scale;
		InitWorlyPoints ();
		CreateMesh();
	}


	private void CreateMesh() {
		Mesh mesh = new Mesh ();
		mesh.name = "Landscape";
		GetComponent<MeshFilter> ().mesh = mesh;
		verticies = new Vector3[(xSize + 1) * (ySize + 1)];
		Vector2[] uv = new Vector2[verticies.Length];
		Vector3 vertex;
		for (int i = 0, y = 0; y <= ySize * scale; y += scale) {
			for (int x = 0; x <= xSize * scale; x += scale, i++) {
				vertex = new Vector3 (x, 0, y);
				uv [i] = new Vector2 ((float)x / (xSize * scale), (float)y / (ySize * scale));

				if (deformType == 0) {
					vertex.y = GetPerlinNoise (vertex);
				} else if (deformType == 1) {
					vertex.y = GetRidgeNoise (vertex);
				} else if (deformType == 2) {
					vertex.y = GetWorlyNoise (vertex);
				}
				verticies[i] = vertex;
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

	private float GetPerlinNoise(Vector3 vertex) {
		float heightScale = GetScale (vertex);
		float noise = Mathf.PerlinNoise (seed + vertex.x, seed + vertex.z);
		return noise * heightScale;
	}

	private float GetRidgeNoise(Vector3 vertex) {
		float scale = GetScale (vertex);
		float noise = RidgeNoise (vertex);
		return noise * scale / 100f;
	}

	private float GetWorlyNoise(Vector3 vertex) {
		float scale = GetWorlyScale (vertex);
		float noise =  MinDistance (vertex, worlyPoints) / maxWorlyDistance;
		return noise * scale;
	}

	private void InitWorlyPoints() {
		Random.InitState((int) seed);
		float numRandoms = 36;
		float sectionSize = maxX / Mathf.Sqrt (numRandoms);
		worlyPoints = new List<Vector3> ();
		for (int x = 0; x < Mathf.Sqrt(numRandoms); x++) {
			for (int y = 0; y < Mathf.Sqrt (numRandoms); y++) {
				Vector3 randomPoint = new Vector3 (Random.Range (0, sectionSize), 0, Random.Range (0, sectionSize));
				worlyPoints.Add (randomPoint + new Vector3 (x * sectionSize, 0, y * sectionSize));
			}
		}
		maxWorlyDistance = Vector3.Distance (new Vector3 (0, 0, 0), new Vector3 (sectionSize, 0, sectionSize));
		//put one at each corner for style
		/*
		worlyPoints.Add(new Vector3(0, 0, 0));
		worlyPoints.Add(new Vector3(maxX, 0, 0));
		worlyPoints.Add(new Vector3(maxX, 0, maxY));
		worlyPoints.Add(new Vector3(0, 0, maxY));
*/
	}

	private float MinDistance(Vector3 point, List<Vector3> points) {
		point.y = 0f;
		float minDist = float.MaxValue;
		foreach (Vector3 p in points) {
			minDist = Mathf.Min (minDist, Vector3.Distance (point, p));
		}
		return minDist;
	}

	private float RidgeNoise(Vector3 vertex) {
		vertex.x = vertex.x / maxX;
		vertex.z = vertex.z / maxY;
		float result = 0f;
		float amplitud = .5f;
		float lacunarity = 1f;
		float gain = .8f;
		int octaves = 8;
		float prev = 0f;;
		for (int i = 0; i < octaves; i++) {
			float noise = Mathf.Abs (Mathf.Sin (Mathf.PerlinNoise (vertex.x + seed / 5f, vertex.z + seed / 5f)));
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

	float GetScale(Vector3 position) {
		float radius = GetRadius (position);
		if (radius > hillDistance) {
			return maxHeight * ScaleFunction (position);
		} else {
			return 0f;
		}
	}

	float GetWorlyScale(Vector3 position) {
		float radius = GetRadius (position);
		if (radius > hillDistance) {
			return maxHeight * ScaleFunction (position);
		} else {
			return 0f;
		}
	}

	float ScaleFunction(Vector3 position) {
		return ((GetRadius (position) - hillDistance) / (maxDistance - hillDistance));
	}

	float GetRadius(Vector3 position) {
		storedVector.x = position.x;
		storedVector.z = position.z;
		return Vector3.Distance (storedVector, middlePosition);
	}
}
