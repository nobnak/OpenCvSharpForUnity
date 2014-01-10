using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;

public class Vector : MonoBehaviour {
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
		//ShowCrosshair(_prevResult);
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
			if (limitSqrVelocity < v.sqrMagnitude)
				v = Vector3.zero;
			vertices[baseIndex] = v0;
			vertices[baseIndex + 1] = v0 + v;
			lines[baseIndex] = baseIndex;
			lines[baseIndex + 1] = baseIndex + 1;
		}
		mesh.vertices = vertices;
		mesh.SetIndices(lines, MeshTopology.Lines, 0);
		mesh.bounds = new Bounds(Vector3.zero, float.MaxValue * Vector3.one);
	}
}
