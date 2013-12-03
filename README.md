OpenCVSharp for Unity
===================
Unity 版 Mono で動作するように変更した [OpenCVSharp](https://code.google.com/p/opencvsharp/) です。
(2013/12/01現在) OpenCV 2.4.5 に対応しています。

# 使い方
## 準備
Windows と Mac で分けて説明します

### Windows 版
 - [OepnCV 2.4.5](http://opencv.org/downloads.html) のコンパイル/インストール (コンパイル時は 32bit 版 DLL を作成)
 - 上記のDLLディレクトリへのPATH環境変数の設定

### Mac 版
 - MacPorts から opencv 2.4.x を "+univeral" オプション付きでインストール
 - config ファイルにDLLのパスを追加(場所:/Applications/Unity/Unity.app/Contents/Frameworks/Mono/etc/mono/config)

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

## ビルド
ビルドせずにサンプルプロジェクトに入っている結果を利用するのが簡単。ビルドする場合は Visual Studio または Mono で [OpenCVSharp プロジェクト](OpencvSharp/OpenCvSharp) のビルドをする。

## 使い方
以下のファイルを Unity プロジェクトに追加する。

 - [OpenCVSharp.dll](Example/Assets/Plugins/)

動かない場合は以下を確認する

 - OpenCV の DLL に PATH が通っているか (Windows)
 - config の DLL target 名は正しいか (Mac)
