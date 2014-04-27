OpenCVSharp for Unity
===================
Altered version of [OpenCVSharp](https://code.google.com/p/opencvsharp/) for Unity.

## History
 - (2013/12/01) Support OpenCV 2.4.5

# Installation
## Install OpenCV
#### Windows
 - Compile 32 bit version of [OepnCV 2.4.5](http://opencv.org/downloads.html)
 - Add the DLL path to PATH enviroment variable.

#### Mac
 - Install 32 bit version of opencv 2.4.x
 - Add the DLL path in Unity config file(/Applications/Unity/Unity.app/Contents/Frameworks/Mono/etc/mono/config)

```
<dllmap os="osx" dll="opencv_calib3d245" target="/opt/local/lib/libopencv_calib3d.dylib" />
<dllmap os="osx" dll="opencv_contrib245" target="/opt/local/lib/libopencv_contrib.dylib" />
<dllmap os="osx" dll="opencv_core245" target="/opt/local/lib/libopencv_core.dylib" />
<dllmap os="osx" dll="opencv_features2d245" target="/opt/local/lib/libopencv_features2d.dylib" />
<dllmap os="osx" dll="opencv_flann245" target="/opt/local/lib/libopencv_flann.dylib" /> 
<!--  <dllmap os="osx" dll="opencv_gpu240.dll" target="/opt/local/lib/libopencv_gpu.dylib" /> -->
<dllmap os="osx" dll="opencv_highgui245" target="/opt/local/lib/libopencv_highgui.dylib" />
<dllmap os="osx" dll="opencv_imgproc245" target="/opt/local/lib/libopencv_imgproc.dylib" />
<dllmap os="osx" dll="opencv_legacy245" target="/opt/local/lib/libopencv_legacy.dylib" />
<!--  <dllmap os="osx" dll="opencv_ml240.dll" target="/opt/local/lib/libopencv_ml.dylib" /> -->
<dllmap os="osx" dll="opencv_objdetect245" target="/opt/local/lib/libopencv_objdetect.dylib" />
<dllmap os="osx" dll="opencv_video245" target="/opt/local/lib/libopencv_video.dylib" />
```

## Install OpenCVSharp
Add the following dll to Unity projects.

 - [OpenCVSharp.dll](Example/Assets/Plugins/)

## Check if this doesn't work

 - Check the PATH environment variable (Windows)
 - CHeck the target name in the Unity config (Mac)
