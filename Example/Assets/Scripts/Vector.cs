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
	private OpticalFlowWorker.AsyncResult _result;
	private bool _firstTimeUpdate = true;

	void Start () {
		_worker = GetComponent<OpticalFlowWorker>();
		_tex = new Texture2D(0, 0, TextureFormat.RGB24, false);
		target.renderer.sharedMaterial.mainTexture = _tex;

		_result = _worker.CalculateOpticalFlow();
	}

	void Update() {
		if (!_result.completed)
			return;

		if (_firstTimeUpdate) {
			_firstTimeUpdate = false;
			UpdateAspectRatio(_result.imageWidth, _result.imageHeight);
			flowLine.mesh = GenerateFlowMesh(_result);
		}

		ShowImage(_result);
		UpdateFlowMesh(_result, flowLine.mesh);

		_result = _worker.CalculateOpticalFlow();
	}

	void UpdateAspectRatio(int width, int height) {
		var s = target.transform.localScale;
		s.x = s.y * width / height;
		target.transform.localScale = s;
	}

	void OnDestroy() {
		Destroy(_tex);
		Destroy(flowLine.mesh);
	}

	public void ShowImage(OpticalFlowWorker.AsyncResult r) {
		if (_tex.width != r.imageWidth || _tex.height != r.imageHeight)
			_tex.Resize(r.imageWidth, r.imageHeight);
		_tex.LoadRawTextureData(r.imageData);
		_tex.Apply();
	}
	public void UpdateFlowMesh(OpticalFlowWorker.AsyncResult r, Mesh mesh) {
		var vertices = mesh.vertices;
		var colors = mesh.colors;
		var rTexelSize = new Vector2(1f / r.imageWidth, 1f / r.imageHeight);
		var limitSqrVelocity = limitVelocity * limitVelocity;
		var c1s = r.corners1;
		for (var i = 0; i < r.nCorners; i++) {
			var baseIndex = 2 * i;
			var c1 = c1s[i];
			var v0 = vertices[baseIndex];
			var v1 = new Vector3(c1.X * rTexelSize.x - 0.5f, -(c1.Y * rTexelSize.y - 0.5f), 0f);
			var v = v1 - v0;
			var rad = Mathf.Atan2(v.y, v.x);
			if (rad < 0)
				rad += 2 * Mathf.PI;
			var color = HSBColor.ToColor(new HSBColor(rad * R_TWO_PI, 1f, 1f));

			if (limitSqrVelocity < v.sqrMagnitude)
				v = Vector3.zero;
			vertices[baseIndex + 1] = v0 + v;
			colors[baseIndex] = color;
			colors[baseIndex + 1] = color;
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
	}

	public Mesh GenerateFlowMesh(OpticalFlowWorker.AsyncResult r) {
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
}
