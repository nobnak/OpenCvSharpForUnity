using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityEditor.Callbacks;

public static class PostBuildScript {
	public const string CONFIG_FILE_SRC = "Assets/Editor/PostBuild/config";
	public const string CONFIG_FILE_DST = "Contents/Data/Managed/etc/mono/config";

	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
		//Debug.Log(pathToBuiltProject);
		System.IO.File.Copy(CONFIG_FILE_SRC, Path.Combine(pathToBuiltProject, CONFIG_FILE_DST), true);
	}
}
