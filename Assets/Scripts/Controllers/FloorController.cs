using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FloorController : MonoBehaviour {
	public int xSize;
	public int ySize;
	public int scale;
	public float maxHeight;
	public float hillDistance;

	private Vector3[] verticies;
	private Vector3 storedVector;
	private Vector3 middlePosition;
	private float maxDistance;
	private void Awake () {
		middlePosition = new Vector3 (xSize * scale / 2f, 0, xSize * scale / 2f);
		maxDistance = Vector3.Distance (middlePosition, new Vector3 (0, 0, 0));
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
				float heightScale = GetScale (vertex);
				float noise = Mathf.PerlinNoise (3.14f + vertex.x, 3.14f + vertex.z);
				vertex.y = noise * heightScale;
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

	float GetScale(Vector3 position) {
		float radius = GetRadius (position);
		if (radius > hillDistance) {
			return maxHeight * ScaleFunction (position);
		} else {
			return 1f;
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
