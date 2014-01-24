using UnityEngine;
using System.Collections;
using nobnak.OpenCV;

[RequireComponent(typeof(OpticalFlowWorker))]
public class Flow : MonoBehaviour {
	public GameObject video;
	public GameObject flow;
	public float limitVelocity = 10f;

	private OpticalFlowWorker _of;
	private Texture2D _videoTex;
	private OpticalFlowWorker.AsyncResult _ofResult;
	private Mesh _flowMesh;
	private Vector3[] _velocities;

	// Use this for initialization
	void Start () {
		_of = GetComponent<OpticalFlowWorker>();
		_videoTex = new Texture2D(0, 0, TextureFormat.RGB24, false, false);

		video.renderer.sharedMaterial.mainTexture = _videoTex;

		_ofResult = _of.CalculateOpticalFlow();
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

			Destroy(_flowMesh);
			_flowMesh = FlowUtil.GenerateFlowMesh(_ofResult);
			var mf = flow.GetComponent<MeshFilter>();
			mf.sharedMesh = _flowMesh;
		}
		_videoTex.LoadRawTextureData(_ofResult.imageData);
		_videoTex.Apply();

		FlowUtil.CalculateFlowVelocities(_ofResult, _flowMesh, ref _velocities);
		FlowUtil.UpdateMeshPositions(_ofResult, _flowMesh, _velocities, limitVelocity);

		_of.UpdateInitialCorner(_ofResult.corners1);
		_ofResult = _of.CalculateOpticalFlow();
	}
}
