using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;

public class OpticalFlowTest : MonoBehaviour {
	public GameObject target;
	public Material lineMat;
	public int featureCount = 10;
	public float featureQuality = 0.01f;
	public float featureMinDist = 0.01f;
	public Color crossHairColor;
	public Color flowColor;
	public bool useSubPix;
	public int opticalFlowPyramid = 3;
	public int opticalFlowWinSize = 3;
	public float opticalFlowErrorThreshold = 1;

	private Texture2D _tex;
	private CvCapture _cap;
	private IplImage _capImage0, _capImage1;
	private IplImage _pyramidImage0, _pyramidImage1;
	private IplImage _eigImage, _tmpImage;

	private CvPoint2D32f[] _corners0;
	private CvPoint2D32f[] _corners1;
	private int _nCorners;

	private CvSize _subPixWinSize, _subPixZeroZone;
	private CvTermCriteria _subPixCrit;
	private CvSize _opticalFlowWinSize;
	private CvTermCriteria _opticalFlowCrit;
	private	sbyte[] _opticalFlowStatus;
	private float[] _trackErrors;

	// Use this for initialization
	void Start () {
		_tex = new Texture2D(0, 0);
		target.renderer.sharedMaterial.mainTexture = _tex;

		_cap = new CvCapture(0);
		using (var image = _cap.QueryFrame()) {
			_capImage0 = new IplImage(image.Size, BitDepth.U8, 1);
			_capImage1 = new IplImage(image.Size, BitDepth.U8, 1);
			_pyramidImage0 = new IplImage(new CvSize(image.Width + 8, image.Height/3), BitDepth.U8, 1);
			_pyramidImage1 = new IplImage(new CvSize(image.Width + 8, image.Height/3), BitDepth.U8, 1);
			_eigImage = new IplImage(image.Size, BitDepth.F32, 1);
			_tmpImage = new IplImage(image.Size, BitDepth.F32, 1);
		}

		_subPixCrit = new CvTermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, 64, 0.01);
		_subPixWinSize = new CvSize(10, 10);
		_subPixZeroZone = new CvSize(-1, -1);
		_opticalFlowWinSize = new CvSize(opticalFlowWinSize, opticalFlowWinSize);
		_opticalFlowCrit = new CvTermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, 20, 0.01);
	}

	void Update() {
		CalculateOpticalFlow();
		ShowImage(_capImage1);
	}

	void OnPostRender() {
		ShowCrosshair(_corners0, _nCorners);
		ShowOpticalFlow(_corners0, _corners1, _nCorners, _opticalFlowStatus);
	}

	void OnDestroy() {
		if (_cap != null)
			_cap.Dispose();
		if (_capImage0 != null)
			_capImage0.Dispose();
		if (_capImage1 != null)
			_capImage1.Dispose();
		if (_pyramidImage0 != null)
			_pyramidImage0.Dispose();
		if (_pyramidImage1 != null)
			_pyramidImage1.Dispose();
		if (_eigImage != null)
			_eigImage.Dispose();
		if (_tmpImage != null)
			_tmpImage.Dispose();

		Destroy(_tex);
	}

	void CalculateOpticalFlow () {
		using (var image = _cap.QueryFrame ()) {
			Cv.ConvertImage(image, _capImage1, 0);
			_nCorners = featureCount;
			Cv.GoodFeaturesToTrack(_capImage0, _eigImage, _tmpImage, out _corners0, ref _nCorners, featureQuality, featureMinDist);
			if (useSubPix)
				Cv.FindCornerSubPix(_capImage0, _corners0, _nCorners, _subPixWinSize, _subPixZeroZone, _subPixCrit);
			Cv.CalcOpticalFlowPyrLK(_capImage0, _capImage1, _pyramidImage0, _pyramidImage1, _corners0, out _corners1, 
			                        _opticalFlowWinSize, opticalFlowPyramid, out _opticalFlowStatus, out _trackErrors, _opticalFlowCrit, 0);
			_capImage1.Copy(_capImage0);
		}
	}

	public void ShowImage(IplImage image) {
		var pngBytes = image.ToBytes(".png");
		_tex.LoadImage(pngBytes);
	}
	public void ShowCrosshair(CvPoint2D32f[] corners, int nCorners) {
		GL.PushMatrix();
		var m = camera.worldToCameraMatrix * target.transform.localToWorldMatrix;
		GL.LoadIdentity();
		GL.MultMatrix(m);

		var height = _capImage0.Height;
		var dx = 1f / _capImage0.Width;
		var dy = 1f / height;
		var length = Mathf.Max(dx, dy) * 10f;
		var offset = new Vector3(-0.5f, -0.5f, 0f);

		lineMat.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(crossHairColor);
		for (var i = 0; i < nCorners; i++) {
			var c = corners[i];
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
	public void ShowOpticalFlow(CvPoint2D32f[] corners0, CvPoint2D32f[] corners1, int nCorners, sbyte[] status) {
		GL.PushMatrix();
		var m = camera.worldToCameraMatrix * target.transform.localToWorldMatrix;
		GL.LoadIdentity();
		GL.MultMatrix(m);

		var height = _capImage0.Height;
		var dx = 1f / _capImage0.Width;
		var dy = 1f / height;
		var offset = new Vector3(-0.5f, -0.5f, 0f);

		lineMat.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(flowColor);
		for (var i = 0; i < nCorners; i++) {
			if (status[i] != 1)
				continue;
			if (_trackErrors[i] > opticalFlowErrorThreshold)
				continue;
			var c0 = corners0[i];
			var c1 = corners1[i];
			var p0 = new Vector3(c0.X * dx, (height - c0.Y) * dy, 0);
			var p1 = new Vector3(c1.X * dx, (height - c1.Y) * dy, 0);
			GL.Vertex(offset + p0);
			GL.Vertex(offset + p1);
		}
		GL.End();

		GL.PopMatrix();
	}
}
