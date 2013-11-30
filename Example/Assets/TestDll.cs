using UnityEngine;
using System.Collections;
using OpenCvSharp;
using System.IO;

public class TestDll : MonoBehaviour {
	public string srcImagePath;
	public string dstImagePath;

	// Use this for initialization
	void Start () {
		using (var src = new IplImage(BuildPath(srcImagePath), LoadMode.GrayScale))
		using (var dst = new IplImage(src.Size, BitDepth.U8, 1)) {
			src.Canny(dst, 50, 200);
			dst.SaveImage(BuildPath(dstImagePath));
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public string BuildPath(string filename) {
		return Path.Combine(Application.streamingAssetsPath, filename);
	}
}
