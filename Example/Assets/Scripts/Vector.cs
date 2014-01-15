using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;

public class Vector : MonoBehaviour {
	public const float R_TWO_PI = 1f / (2f * Mathf.PI);

	public GameObject target;
	public MeshFilter flowLine;
	
	public float errorThreshold = 1;
	public float limitVelocity = 50;

	private Texture2D _tex;
	private OpticalFlowWorker _worker;
	private OpticalFlowWorker.AsyncResult _result, _prevResult;
	private bool _firstTimeUpdate = true;

	void Start () {
		_worker = GetComponent<OpticalFlowWorker>();
		_tex = new Texture2D(0, 0, TextureFormat.RGB24, false);
		target.renderer.sharedMaterial.mainTexture = _tex;

		_prevResult = _result = _worker.CalculateOpticalFlow();
	}

	void Update() {
		if (!_result.completed)
			return;

		if (_firstTimeUpdate) {
			_firstTimeUpdate = false;
			UpdateAspectRatio(_result.imageWidth, _result.imageHeight);
		}

		ShowImage(_result);

		_prevResult = _result;
		_result = _worker.CalculateOpticalFlow();
	}

	void UpdateAspectRatio(int width, int height) {
		var s = target.transform.localScale;
		s.x = s.y * width / height;
		target.transform.localScale = s;
	}

	void OnPostRender() {
		ShowOpticalFlow(_prevResult);
	}

	void OnDestroy() {
		Destroy(_tex);
	}

	public void ShowImage(OpticalFlowWorker.AsyncResult r) {
		if (_tex.width != r.imageWidth || _tex.height != r.imageHeight)
			_tex.Resize(r.imageWidth, r.imageHeight);
		_tex.LoadRawTextureData(r.imageData);
		_tex.Apply();
	}
	public void ShowOpticalFlow(OpticalFlowWorker.AsyncResult r) {
		var mesh = (flowLine.mesh == null) ? flowLine.mesh = new Mesh() : flowLine.mesh;
		mesh.Clear();
		var vertices = new Vector3[2 * r.nCorners];
		var colors = new Color[vertices.Length];
		var lines = new int[vertices.Length];
		var rTexelSize = new Vector2(1f / r.imageWidth, 1f / r.imageHeight);
		var limitSqrVelocity = limitVelocity * limitVelocity;
		for (var i = 0; i < r.nCorners; i++) {
			var baseIndex = 2 * i;
			var c0s = r.corners0;
			var c1s = r.corners1;
			var c0 = c0s[i];
			var c1 = c1s[i];
			var v0 = new Vector3(c0.X * rTexelSize.x - 0.5f,  -(c0.Y * rTexelSize.y - 0.5f), 0f);
			var v1 = new Vector3(c1.X * rTexelSize.x - 0.5f, -(c1.Y * rTexelSize.y - 0.5f), 0f);
			var v = v1 - v0;
			var rad = Mathf.Atan2(v.y, v.x);
			if (rad < 0)
				rad += 2 * Mathf.PI;
			var color = HSBColor.ToColor(new HSBColor(rad * R_TWO_PI, 1f, 1f));

			if (limitSqrVelocity < v.sqrMagnitude)
				v = Vector3.zero;
			vertices[baseIndex] = v0;
			vertices[baseIndex + 1] = v0 + v;
			colors[baseIndex] = color;
			colors[baseIndex + 1] = color;
			lines[baseIndex] = baseIndex;
			lines[baseIndex + 1] = baseIndex + 1;
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.SetIndices(lines, MeshTopology.Lines, 0);
		mesh.bounds = new Bounds(Vector3.zero, float.MaxValue * Vector3.one);
	}

	public void GenerateLattice(int width, int height, float space, out CvPoint2D32f[] corners, out Vector3[] vertices, out int[] triangles) {
		var nx = (int)(width / space);
		var ny = (int)(height / space);
		var offset = space * 0.5f;
		var rTexelSize = new Vector2(1f / width, 1f / height);

		corners = new CvPoint2D32f[nx * ny];
		vertices = new Vector3[corners.Length];
		triangles = new int[6 * (nx - 1) * (ny - 1)];

		for (var y = 0; y < ny; y++) {
			for (var x = 0; x < nx; x++) {
				var index = x + y * nx;
				var c = new CvPoint2D32f(offset + x * space, offset + y * space);
				var v = new Vector3(c.X * rTexelSize.x - 0.5f,  -(c.Y * rTexelSize.y - 0.5f), 0f);
				corners[index] = c;
				vertices[index] = v;
			}
		}

		var iTriangle = 0;
		for (var y = 0; y < (ny - 1); y++) {
			for (var x = 0; x < (nx - 1); x++) {
				var index = x + y * nx;
				triangles[iTriangle++] = index;
				triangles[iTriangle++] = index + 1;
				triangles[iTriangle++] = index + 1 + nx;
				triangles[iTriangle++] = index;
				triangles[iTriangle++] = index + 1 + nx;
				triangles[iTriangle++] = index + nx;
			}
		}
	}
}
