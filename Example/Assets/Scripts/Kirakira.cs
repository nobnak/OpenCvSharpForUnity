using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;
using nobnak.OpenCV;

public class Kirakira : MonoBehaviour {
	public GameObject target;
	public ParticleSystem particleSys;
	public Material lineMat;
	public Color crossHairColor;
	public Color flowColor;
	public bool drawLines;
	public float particelRadius = 1f;
	
	public float opticalFlowErrorThreshold = 1;
	public float particleVelocityThreshold = 1f;

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
		ShowParticles(_result);

		_prevResult = _result;
		_result = _worker.CalculateOpticalFlow();
	}

	void UpdateAspectRatio(int width, int height) {
		var s = target.transform.localScale;
		s.x = s.y * width / height;
		target.transform.localScale = s;
	}

	void OnPostRender() {
		if (!drawLines)
			return;

		ShowCrosshair(_prevResult);
		ShowOpticalFlow(_prevResult);
	}

	void OnDestroy() {
		Destroy(_tex);
	}

	void ShowParticles(OpticalFlowWorker.AsyncResult r) {
		var dt = r.currTime - r.prevTime;

		var height = r.imageHeight;
		var dx = 1f / r.imageWidth;
		var dy = 1f / height;
		var offset = new Vector3(-0.5f, -0.5f, -0.1f);
		for (var i = 0; i < r.nCorners; i++) {
			if (r.opticalFlowStatus[i] != 1 || r.trackErrors[i] > opticalFlowErrorThreshold)
				continue;
			var c0 = r.corners0[i];
			var c1 = r.corners1[i];
			var pos0 = new Vector3(c0.X * dx, (height - c0.Y) * dy, 0) + offset;
			var pos1 = new Vector3(c1.X * dx, (height - c1.Y) * dy, 0) + offset;
			pos0 = target.transform.TransformPoint(pos0);
			pos1 = target.transform.TransformPoint(pos1);
			var velocity = (pos1 - pos0) / (dt + 1e-9f);
			if (velocity.sqrMagnitude < (particleVelocityThreshold * particleVelocityThreshold))
			    continue;
			particleSys.Emit(pos1 + (Vector3)Random.insideUnitCircle * particelRadius, velocity, 
			                 particleSys.startSize, particleSys.startLifetime, particleSys.startColor);
		}
	}

	public void ShowImage(OpticalFlowWorker.AsyncResult r) {
		if (_tex.width != r.imageWidth || _tex.height != r.imageHeight)
			_tex.Resize(r.imageWidth, r.imageHeight);
		_tex.LoadRawTextureData(r.imageData);
		_tex.Apply();
	}
	public void ShowCrosshair(OpticalFlowWorker.AsyncResult r) {
		GL.PushMatrix();
		var m = camera.worldToCameraMatrix * target.transform.localToWorldMatrix;
		GL.LoadIdentity();
		GL.MultMatrix(m);

		var height = r.imageHeight;
		var dx = 1f / r.imageWidth;
		var dy = 1f / height;
		var length = Mathf.Max(dx, dy) * 10f;
		var offset = new Vector3(-0.5f, -0.5f, 0f);

		lineMat.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(crossHairColor);
		for (var i = 0; i < r.nCorners; i++) {
			var c = r.corners0[i];
			var x = c.X;
			var y = height - c.Y;
			var center = new Vector3(x * dx, y * dy, 0) + offset;
			GL.Vertex(center + new Vector3(-length, 0f, 0f));
			GL.Vertex(center + new Vector3( length, 0f, 0f));
			GL.Vertex(center + new Vector3(0f, -length, 0f));
			GL.Vertex(center + new Vector3(0f,  length, 0f));
		}
		GL.End();

		GL.PopMatrix();
	}
	public void ShowOpticalFlow(OpticalFlowWorker.AsyncResult r) {
		GL.PushMatrix();
		var m = camera.worldToCameraMatrix * target.transform.localToWorldMatrix;
		GL.LoadIdentity();
		GL.MultMatrix(m);

		var height = r.imageHeight;
		var dx = 1f / r.imageWidth;
		var dy = 1f / height;
		var offset = new Vector3(-0.5f, -0.5f, 0f);

		lineMat.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(flowColor);
		for (var i = 0; i < r.nCorners; i++) {
			if (r.opticalFlowStatus[i] != 1)
				continue;
			if (r.trackErrors[i] > opticalFlowErrorThreshold)
				continue;
			var c0 = r.corners0[i];
			var c1 = r.corners1[i];
			var p0 = new Vector3(c0.X * dx, (height - c0.Y) * dy, 0);
			var p1 = new Vector3(c1.X * dx, (height - c1.Y) * dy, 0);
			GL.Vertex(offset + p0);
			GL.Vertex(offset + p1);
		}
		GL.End();

		GL.PopMatrix();
	}
}
