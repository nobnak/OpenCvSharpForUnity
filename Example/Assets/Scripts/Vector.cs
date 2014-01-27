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

	private Mesh _mesh;
	private Texture2D _tex;
	private OpticalFlowWorker _of;

	private OpticalFlowWorker.AsyncResult _result;
	private bool _firstTimeUpdate = true;
	private CvPoint2D32f[] _velocities;

	private OpenCvSharp.CvPoint2D32f[] _corners0;

	void Start () {
		_of = GetComponent<OpticalFlowWorker>();
		_tex = new Texture2D(0, 0, TextureFormat.RGB24, false);
		var mf = flow.GetComponent<MeshFilter>();
		_mesh = mf.mesh = new Mesh();
		background.renderer.sharedMaterial.mainTexture = _tex;

		_corners0 = FlowUtil.GenGridCorners(_of.width, _of.height, 50f);
		_result = _of.CalculateOpticalFlow(_corners0);
	}

	void Update() {
		if (!_result.completed)
			return;

		if (_firstTimeUpdate) {
			_firstTimeUpdate = false;
			UpdateAspectRatio(_result.imageWidth, _result.imageHeight);
			_velocities = new CvPoint2D32f[_mesh.vertexCount / 2];
		}

		ShowImage(_result);
		FlowUtil.CalculateFlowVelocities(_result, ref _velocities);
		FlowUtil.UpdateLineMesh(_result, _mesh, _velocities, limitVelocity);

		_result = _of.CalculateOpticalFlow(_corners0);
	}

	void UpdateAspectRatio(int width, int height) {
		var s = background.transform.localScale;
		s.x = s.y * width / height;
		background.transform.localScale = s;
	}

	void OnDestroy() {
		Destroy(_tex);
		Destroy(_mesh);
	}

	public void ShowImage(OpticalFlowWorker.AsyncResult r) {
		if (_tex.width != r.imageWidth || _tex.height != r.imageHeight)
			_tex.Resize(r.imageWidth, r.imageHeight);
		_tex.LoadRawTextureData(r.imageData);
		_tex.Apply();
	}


}
