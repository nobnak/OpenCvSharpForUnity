using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;

public class CannyTest : MonoBehaviour {
	public GameObject target;

	private Texture2D _tex;
	private CvCapture _cap;

	// Use this for initialization
	void Start () {
		_tex = new Texture2D(0, 0);
		target.renderer.sharedMaterial.mainTexture = _tex;
		_cap = new CvCapture(0);
	}

	void Update() {
		Test01b();
	}

	void OnDestroy() {
		if (_cap != null) { 
			_cap.Dispose();
			_cap = null;
		}

		Destroy(_tex);
	}
	void Test01b() {
		using (var src = _cap.QueryFrame())
		using (var dst = new IplImage(src.Size, BitDepth.U8, 1)) {
			src.Canny(dst, 50, 200);
			ShowImage(dst);
		}
	}

	public void ShowImage(IplImage image) {
		var pngBytes = image.ToBytes(".png");
		_tex.LoadImage(pngBytes);
	}
}
