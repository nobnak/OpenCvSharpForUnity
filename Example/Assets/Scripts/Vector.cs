using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;
using nobnak.OpenCV;

public class Vector : MonoBehaviour {
	public GameObject background;
	public GameObject flow;
	
	public float errorThreshold = 1;
	public float limitVelocity = 50;

	private Texture2D _tex;
	private OpticalFlowWorker _worker;
	private MeshFilter _meshFilter;

	private OpticalFlowWorker.AsyncResult _result;
	private bool _firstTimeUpdate = true;
	private Vector3[] _velocities;

	void Start () {
		_worker = GetComponent<OpticalFlowWorker>();
		_tex = new Texture2D(0, 0, TextureFormat.RGB24, false);
		_meshFilter = flow.GetComponent<MeshFilter>();
		background.renderer.sharedMaterial.mainTexture = _tex;

		_result = _worker.CalculateOpticalFlow();
	}

	void Update() {
		if (!_result.completed)
			return;

		if (_firstTimeUpdate) {
			_firstTimeUpdate = false;
			UpdateAspectRatio(_result.imageWidth, _result.imageHeight);
			_meshFilter.mesh = FlowUtil.GenerateFlowMesh(_result);
			_velocities = new Vector3[_meshFilter.mesh.vertexCount / 2];
		}

		ShowImage(_result);
		FlowUtil.CalculateFlowVelocities(_result, _meshFilter.mesh, ref _velocities);
		FlowUtil.UpdateMeshPositions(_result, _meshFilter.mesh, _velocities, limitVelocity);

		_result = _worker.CalculateOpticalFlow();
	}

	void UpdateAspectRatio(int width, int height) {
		var s = background.transform.localScale;
		s.x = s.y * width / height;
		background.transform.localScale = s;
	}

	void OnDestroy() {
		Destroy(_tex);
		Destroy(_meshFilter.mesh);
	}

	public void ShowImage(OpticalFlowWorker.AsyncResult r) {
		if (_tex.width != r.imageWidth || _tex.height != r.imageHeight)
			_tex.Resize(r.imageWidth, r.imageHeight);
		_tex.LoadRawTextureData(r.imageData);
		_tex.Apply();
	}


}
