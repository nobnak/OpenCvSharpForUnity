using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;

public class TestDll : MonoBehaviour {
	public string srcImagePath;
	public string dstImagePath;

	private Texture2D _tex;
	private CvCapture _cap;
	private IplImage _tmpImage;

	// Use this for initialization
	void Start () {
		_tex = new Texture2D(0, 0);
		renderer.sharedMaterial.mainTexture = _tex;
		//_cap = new CvCapture(0);

		Test01();
	}

	void Update() {
		//Test02 ();
	}

	void OnDestroy() {
		if (_cap != null) { 
			_cap.Dispose();
			_cap = null;
		}
		if (_tmpImage != null) {
			_tmpImage.Dispose();
			_tmpImage = null;
		}

		Destroy(_tex);
	}

	void Test01() {
		using (var src = new IplImage(BuildPath(srcImagePath), LoadMode.GrayScale))
		using (var dst = new IplImage(src.Size, BitDepth.U8, 1)) {
			src.Canny(dst, 50, 200);
			ShowImage(dst);
		}
	}

	void Test02() {
		using (var image = _cap.QueryFrame()) {
			if (_tmpImage == null)
				_tmpImage = new IplImage(image.Size, image.Depth, 1);
			image.Canny(_tmpImage, 50, 200);
			ShowImage(_tmpImage);
		}
	}

	public void ShowImage(IplImage image) {
		var pngBytes = image.ToBytes(".png");
		_tex.LoadImage(pngBytes);
	}

	public string BuildPath(string filename) {
		return Path.Combine(Application.streamingAssetsPath, filename);
	}
}
