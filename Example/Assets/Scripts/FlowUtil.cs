using UnityEngine;
using System.Collections;
using nobnak.OpenCV;

public static class FlowUtil {
	public const float R_TWO_PI = 1f / (2f * Mathf.PI);

	public static Mesh GenerateFlowMesh(OpticalFlowWorker.AsyncResult r) {
		var mesh = new Mesh();
		var vertices = new Vector3[2 * r.nCorners];
		var colors = new Color[vertices.Length];
		var lines = new int[vertices.Length];
		var rTexelSize = new Vector2(1f / r.imageWidth, 1f / r.imageHeight);
		var c0s = r.corners0;
		for (var i = 0; i < r.nCorners; i++) {
			var baseIndex = 2 * i;
			var c0 = c0s[i];
			var v0 = new Vector3(c0.X * rTexelSize.x - 0.5f,  -(c0.Y * rTexelSize.y - 0.5f), 0f);
			vertices[baseIndex] = v0;
			vertices[baseIndex + 1] = v0;
			colors[baseIndex] = Color.black;
			colors[baseIndex + 1] = Color.black;
			lines[baseIndex] = baseIndex;
			lines[baseIndex + 1] = baseIndex + 1;
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.SetIndices(lines, MeshTopology.Lines, 0);
		mesh.bounds = new Bounds(Vector3.zero, float.MaxValue * Vector3.one);
		return mesh;
	}
	public static void CalculateFlowVelocities(OpticalFlowWorker.AsyncResult r, Mesh mesh, ref Vector3[] velocities) {
		if (velocities == null || velocities.Length != r.nCorners)
			velocities = new Vector3[r.nCorners];

		var vertices = mesh.vertices;
		var rTexelSize = new Vector2(1f / r.imageWidth, 1f / r.imageHeight);
		var c1s = r.corners1;
		for (var i = 0; i < r.nCorners; i++) {
			var baseIndex = 2 * i;
			var c1 = c1s[i];
			var v0 = vertices[baseIndex];
			var v1 = new Vector3(c1.X * rTexelSize.x - 0.5f, -(c1.Y * rTexelSize.y - 0.5f), 0f);
			var v = v1 - v0;
			velocities[i] = v;
		}
	}
	public static void UpdateMeshPositions(OpticalFlowWorker.AsyncResult r, Mesh mesh, Vector3[] velocities, float limitVelocity) {
		var vertices = mesh.vertices;
		var colors = mesh.colors;
		var limitSqrVelocity = limitVelocity * limitVelocity;
		var c0s = r.corners0;
		var rTexelSize = new Vector2(1f / r.imageWidth, 1f / r.imageHeight);
		for (var i = 0; i < r.nCorners; i++) {
			var vertexIndex = 2 * i;
			var c0 = c0s[i];
			var v0 = new Vector3(c0.X * rTexelSize.x - 0.5f,  -(c0.Y * rTexelSize.y - 0.5f), 0f);

			var v = velocities[i];
			var rad = Mathf.Atan2(v.y, v.x);
			if (rad < 0)
				rad += 2 * Mathf.PI;
			var color = HSBColor.ToColor(new HSBColor(rad * R_TWO_PI, 1f, 1f));
			
			if (limitSqrVelocity < v.sqrMagnitude)
				v = Vector3.zero;
			vertices[vertexIndex] = v0;
			vertices[vertexIndex + 1] = v0 + v;
			colors[vertexIndex] = color;
			colors[vertexIndex + 1] = color;
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
	}
}
