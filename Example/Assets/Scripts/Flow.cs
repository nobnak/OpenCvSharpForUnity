using UnityEngine;
using System.Collections;
using nobnak.OpenCV;
using OpenCvSharp;

[RequireComponent(typeof(OpticalFlowWorker))]
public class Flow : MonoBehaviour {
	public GameObject video;
	public GameObject flow;
	public float limitVelocity = 10f;

	private OpticalFlowWorker _of;
	private Texture2D _videoTex;
	private OpticalFlowWorker.AsyncResult _ofResult;
	private Mesh _flowMesh;
	private CvPoint2D32f[] _velocities;
	
	private float[] _cornerBirthTimes;
	private CvPoint2D32f[] _corners0;

	// Use this for initialization
	void Start () {
		_of = GetComponent<OpticalFlowWorker>();
		_videoTex = new Texture2D(0, 0, TextureFormat.RGB24, false, false);
		_flowMesh = new Mesh();

		var mf = flow.GetComponent<MeshFilter>();
		mf.sharedMesh = _flowMesh;
		video.renderer.sharedMaterial.mainTexture = _videoTex;

		_corners0 = FlowUtil.GenGridCorners(_of.width, _of.height, 50f);
		_cornerBirthTimes = new float[_corners0.Length];
		var t = Time.timeSinceLevelLoad;
		for (var i = 0; i < _cornerBirthTimes.Length; i++)
			_cornerBirthTimes[i] = t;
		_ofResult = _of.CalculateOpticalFlow(_corners0);
	}
	
	// Update is called once per frame
	void Update () {
		if (!_ofResult.completed)
			return;

		//Debug.Log(string.Format("Optical Flow elapsed time {0:f}(ms)", _ofResult.elapsedMs));
		if (_ofResult.imageWidth != _videoTex.width || _ofResult.imageHeight != _videoTex.height) {
			_videoTex.Resize(_ofResult.imageWidth, _ofResult.imageHeight);
			var s = video.transform.localScale;
			s.x = s.y * _ofResult.imageWidth / _ofResult.imageHeight;
			video.transform.localScale = s;

		}
		_videoTex.LoadRawTextureData(_ofResult.imageData);
		_videoTex.Apply();

		FlowUtil.CalculateFlowVelocities(_ofResult, ref _velocities);
		FlowUtil.UpdateLineMesh(_ofResult, _flowMesh, _velocities, limitVelocity);

		_ofResult = _of.CalculateOpticalFlow(_ofResult.corners1);
	}
}
