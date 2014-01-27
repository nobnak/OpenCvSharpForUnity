using UnityEngine;
using System.Collections;
using nobnak.OpenCV;
using OpenCvSharp;

public static class FlowUtil {
	public const float R_TWO_PI = 1f / (2f * Mathf.PI);
	
	public static void CalculateFlowVelocities(OpticalFlowWorker.AsyncResult r, ref CvPoint2D32f[] velocities) {
		if (velocities == null || velocities.Length != r.nCorners)
			velocities = new CvPoint2D32f[r.nCorners];

		var c0s = r.corners0;
		var c1s = r.corners1;
		for (var i = 0; i < r.nCorners; i++) {
			var c0 = c0s[i];
			var c1 = c1s[i];
			var cv = c1 - c0;
			velocities[i] = cv;
		}
	}
	public static void UpdateLineMesh(OpticalFlowWorker.AsyncResult r, Mesh mesh, CvPoint2D32f[] velocities, float limitVelocity) {
		var vertices = new Vector3[r.nCorners * 2];
		var colors = new Color[vertices.Length];
		var indices = new int[vertices.Length];
		var limitSqrVelocity = limitVelocity * limitVelocity;
		var c0s = r.corners0;
		var rTexelSize = new Vector2(1f / r.imageWidth, 1f / r.imageHeight);
		for (var i = 0; i < r.nCorners; i++) {
			var vertexIndex = 2 * i;
			var c0 = c0s[i];
			var v0 = new Vector3(c0.X * rTexelSize.x - 0.5f,  -(c0.Y * rTexelSize.y - 0.5f), 0f);

			var cv = velocities[i];
			var v = new Vector3(cv.X * rTexelSize.x, cv.Y * rTexelSize.y, 0f);
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
			indices[vertexIndex] = vertexIndex;
			indices[vertexIndex + 1] = vertexIndex + 1;
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.SetIndices(indices, MeshTopology.Lines, 0);
		mesh.RecalculateBounds();
	}

	
	public static OpenCvSharp.CvPoint2D32f[] GenGridCorners(int width, int height, float gridSize) {
		var nx = (int)(width / gridSize);
		var ny = (int)(height / gridSize);
		var corners = new CvPoint2D32f[nx * ny];
		var offset = gridSize * 0.5f;
		for (var y = 0; y < ny; y++) {
			for (var x = 0; x < nx; x++) {
				var index = x + y * nx;
				corners[index] = new CvPoint2D32f(offset + x * gridSize, offset + y * gridSize);
			}
		}
		return corners;
	}
}
