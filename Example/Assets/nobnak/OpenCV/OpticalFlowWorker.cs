using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using nobnak.Util;

namespace nobnak.OpenCV {

	public class OpticalFlowWorker : MonoBehaviour {
		public int opticalFlowPyramid = 3;
		public int opticalFlowWinSize = 10;
		public float featureSpace = 50f;
		public int ofCritIterations = 20;
		public float ofCritError = 0.01f;

		public int width, height;

		private CvCapture _cap;
		private IplImage _capImage, _capRgbImage;
		private IplImage _capGrayImage0, _capGrayImage1;
		private IplImage _pyramidImage0, _pyramidImage1;
		private IplImage _eigImage, _tmpImage;

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

			_capImage = _cap.QueryFrame();
			_capRgbImage = new IplImage(_capImage.Width, _capImage.Height, BitDepth.U8, 3);
			Debug.Log(string.Format("Capture info : size{0}", _capImage.Size));
	       	_capGrayImage0 = new IplImage(_capImage.Size, BitDepth.U8, 1);
			_capGrayImage1 = new IplImage(_capImage.Size, BitDepth.U8, 1);
			_pyramidImage0 = new IplImage(new CvSize(_capImage.Width + 8, _capImage.Height/3), BitDepth.U8, 1);
			_pyramidImage1 = new IplImage(new CvSize(_capImage.Width + 8, _capImage.Height/3), BitDepth.U8, 1);
			_eigImage = new IplImage(_capImage.Size, BitDepth.F32, 1);
			_tmpImage = new IplImage(_capImage.Size, BitDepth.F32, 1);
			Cv.ConvertImage(_capImage, _capGrayImage0, 0);
			width = _capImage.Width;
			height = _capImage.Height;

			_opticalFlowWinSize = new CvSize(opticalFlowWinSize, opticalFlowWinSize);
			_opticalFlowCrit = new CvTermCriteria(CriteriaType.Iteration | CriteriaType.Epsilon, ofCritIterations, ofCritError);

			_prevTime = _currTime = UnityEngine.Time.time;
		}

		void OnDestroy() {
			if (_cap != null) _cap.Dispose();
			if (_capRgbImage != null) _capRgbImage.Dispose();
			if (_capGrayImage0 != null) _capGrayImage0.Dispose();
			if (_capGrayImage1 != null) _capGrayImage1.Dispose();
			if (_pyramidImage0 != null) _pyramidImage0.Dispose();
			if (_pyramidImage1 != null) _pyramidImage1.Dispose();
			if (_eigImage != null) _eigImage.Dispose();
			if (_tmpImage != null) _tmpImage.Dispose();
		}

		public AsyncResult CalculateOpticalFlow(CvPoint2D32f[] corners0) {
			var r = new AsyncResult();
			r.prevTime = _prevTime = _currTime;
			r.currTime = _currTime = Time.time;
			r.corners0 = corners0;
			r.nCorners = corners0.Length;

			ThreadPool.QueueUserWorkItem(_CalculateOpticalFlow, r);
			return r;
		}

		void _CalculateOpticalFlow (System.Object result) {
			var r = (AsyncResult) result;

			var startTime = HighResTime.UtcNow;
			_capImage = _cap.QueryFrame ();
			Cv.ConvertImage(_capImage, _capGrayImage1, 0);
			CvPoint2D32f[] corners1;
			Cv.CalcOpticalFlowPyrLK(_capGrayImage0, _capGrayImage1, _pyramidImage0, _pyramidImage1, r.corners0, out corners1, 
			                        _opticalFlowWinSize, opticalFlowPyramid, out _opticalFlowStatus, out _trackErrors, _opticalFlowCrit, 0);
			_capGrayImage1.Copy(_capGrayImage0);

			Cv.CvtColor(_capImage, _capRgbImage, ColorConversion.BgrToRgb);
			var raw = new byte[3 * _capImage.Width * _capImage.Height];
			System.IntPtr rawPtr;
			_capRgbImage.GetRawData(out rawPtr);
			Marshal.Copy(rawPtr, raw, 0, raw.Length);

			r.imageWidth = _capGrayImage0.Width;
			r.imageHeight = _capGrayImage0.Height;		
			r.imageData = raw;
			r.corners1 = corners1;
			r.opticalFlowStatus = _opticalFlowStatus;
			r.trackErrors = _trackErrors;
			r.elapsedMs = (float)((HighResTime.UtcNow - startTime).TotalMilliseconds);

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
			public float elapsedMs;
		}
	}

}
