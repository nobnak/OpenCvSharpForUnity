using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;
using System.Threading;

public class OpticalFlowWorker : MonoBehaviour {
	public int featureCount = 10;
	public float featureQuality = 0.01f;
	public float featureMinDist = 0.01f;
	public bool subPixUsage;
	public int subPixWinSize = 10;
	public int opticalFlowPyramid = 3;
	public int opticalFlowWinSize = 10;

	public Vector2 CaptureSize { get; private set; }

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

	private float _prevTime;
	private float _currTime;

	void Awake () {
		_cap = new CvCapture(0);
		using (var image = _cap.QueryFrame()) {
			Debug.Log(string.Format("Capture info : size{0}", image.Size));
			CaptureSize = new Vector2(image.Width, image.Height);
         	_capImage0 = new IplImage(image.Size, BitDepth.U8, 1);
			_capImage1 = new IplImage(image.Size, BitDepth.U8, 1);
			_pyramidImage0 = new IplImage(new CvSize(image.Width + 8, image.Height/3), BitDepth.U8, 1);
			_pyramidImage1 = new IplImage(new CvSize(image.Width + 8, image.Height/3), BitDepth.U8, 1);
			_eigImage = new IplImage(image.Size, BitDepth.F32, 1);
			_tmpImage = new IplImage(image.Size, BitDepth.F32, 1);
		}

		_subPixCrit = new CvTermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, 20, 0.01);
		_subPixWinSize = new CvSize(subPixWinSize, subPixWinSize);
		_subPixZeroZone = new CvSize(-1, -1);
		_opticalFlowWinSize = new CvSize(opticalFlowWinSize, opticalFlowWinSize);
		_opticalFlowCrit = new CvTermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, 20, 0.01);

		_prevTime = _currTime = Time.time;
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
	}

	public AsyncResult CalculateOpticalFlow() {
		_prevTime = _currTime;
		_currTime = Time.time;
		
		var r = new AsyncResult();
		r.prevTime = _prevTime;
		r.currTime = _currTime;

		ThreadPool.QueueUserWorkItem(_CalculateOpticalFlow, r);
		return r;
	}

	void _CalculateOpticalFlow (System.Object result) {
		using (var image = _cap.QueryFrame ()) {
			Cv.ConvertImage(image, _capImage1, 0);
			_nCorners = featureCount;
			Cv.GoodFeaturesToTrack(_capImage0, _eigImage, _tmpImage, out _corners0, ref _nCorners, featureQuality, featureMinDist);
			if (subPixUsage)
				Cv.FindCornerSubPix(_capImage0, _corners0, _nCorners, _subPixWinSize, _subPixZeroZone, _subPixCrit);
			Cv.CalcOpticalFlowPyrLK(_capImage0, _capImage1, _pyramidImage0, _pyramidImage1, _corners0, out _corners1, 
			                        _opticalFlowWinSize, opticalFlowPyramid, out _opticalFlowStatus, out _trackErrors, _opticalFlowCrit, 0);
			_capImage1.Copy(_capImage0);
		}

		var r = (AsyncResult) result;
		r.imageWidth = _capImage0.Width;
		r.imageHeight = _capImage0.Height;
		r.imageData = _capImage0.ToBytes(".png");
		r.corners0 = _corners0;
		r.corners1 = _corners1;
		r.nCorners = _nCorners;
		r.opticalFlowStatus = _opticalFlowStatus;
		r.trackErrors = _trackErrors;

		r.completed = true;
	}

	public class AsyncResult {
		public bool completed = false;

		public int imageWidth;
		public int imageHeight;
		public byte[] imageData;
		public CvPoint2D32f[] corners0;
		public CvPoint2D32f[] corners1;
		public int nCorners;
		public	sbyte[] opticalFlowStatus;
		public float[] trackErrors;
		
		public float prevTime;
		public float currTime;
	}
}
