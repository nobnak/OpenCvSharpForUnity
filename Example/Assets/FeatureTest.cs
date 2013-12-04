using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;

public class FeatureTest : MonoBehaviour {
	public GameObject target;
	public Material lineMat;
	public int maxCorners = 10;
	public float qualityLevel = 0.01f;
	public float minDistance = 0.01f;
	public Color lineColor;
	public bool useSubPix;

	private Texture2D _tex;
	private CvCapture _cap;
	private IplImage _capImage;
	private IplImage _eigImage;
	private IplImage _tmpImage;

	private CvPoint2D32f[] _corners;
	private int _nCorners;

	private CvSize _subPixWinSize, _subPixZeroZone;
	private CvTermCriteria _termCrit;

	// Use this for initialization
	void Start () {
		_tex = new Texture2D(0, 0);
		target.renderer.sharedMaterial.mainTexture = _tex;

		_cap = new CvCapture(0);
		using (var image = _cap.QueryFrame()) {
			_capImage = new IplImage(image.Size, BitDepth.U8, 1);
			_eigImage = new IplImage(image.Size, BitDepth.F32, 1);
			_tmpImage = new IplImage(image.Size, BitDepth.F32, 1);
		}

		_termCrit = new CvTermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, 20, 0.03);
		_subPixWinSize = new CvSize(10, 10);
		_subPixZeroZone = new CvSize(-1, -1);
	}

	void Update() {
		using (var image = _cap.QueryFrame()) {
			Cv.ConvertImage(image, _capImage, 0);
			_nCorners = maxCorners;
			Cv.GoodFeaturesToTrack(_capImage, _eigImage, _tmpImage, out _corners, ref _nCorners, qualityLevel, minDistance);
			if (useSubPix)
				Cv.FindCornerSubPix(_capImage, _corners, _nCorners, _subPixWinSize, _subPixZeroZone, _termCrit);
			ShowImage (_capImage);
		}
	}

	void OnPostRender() {
		ShowCrosshair(_corners, _nCorners);
	}

	void OnDestroy() {
		if (_cap != null) { 
			_cap.Dispose();
			_cap = null;
		}
		if (_capImage != null) {
			_capImage.Dispose();
			_capImage = null;
		}
		if (_eigImage != null) {
			_eigImage.Dispose();
			_eigImage = null;
		}
		if (_tmpImage != null) {
			_tmpImage.Dispose();
			_tmpImage = null;
		}

		Destroy(_tex);
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

		var dx = 1f / _capImage.Width;
		var dy = 1f / _capImage.Height;
		var length = Mathf.Max(dx, dy) * 10f;
		var offset = new Vector3(-0.5f, -0.5f, 0f);

		lineMat.SetPass(0);
		GL.Begin(GL.LINES);
		GL.Color(lineColor);
		for (var i = 0; i < nCorners; i++) {
			var c = corners[i];
			var x = c.X;
			var y = _capImage.Height - c.Y;
			var center = new Vector3(x * dx, y * dy, 0) + offset;
			GL.Vertex(center + new Vector3(-length, 0f, 0f));
			GL.Vertex(center + new Vector3( length, 0f, 0f));
			GL.Vertex(center + new Vector3(0f, -length, 0f));
			GL.Vertex(center + new Vector3(0f,  length, 0f));
		}
		GL.End();

		GL.PopMatrix();
	}
}
