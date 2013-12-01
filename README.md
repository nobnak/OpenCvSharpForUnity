OpenCVSharp for Unity
===================
Unity 版 Mono で動作するように変更した [OpenCVSharp](https://code.google.com/p/opencvsharp/) です。
(2013/12/01現在) OpenCV 2.4.5 に対応しています。

# 使い方
## 準備
このライブラリのコンパイル、Unity上での実行には以下が必要です。
 ― [OepnCV 2.4.5](http://opencv.org/downloads.html) のコンパイル/インストール (コンパイル時は 32bit 版 DLL を作成)
 ― 上記のDLLディレクトリへのPATH環境変数の設定
 - (Windows 以外で DLL 名が) OpenCVSharp.dll.config に記述された dllmap の target DLL名変更

## ビルド
ビルドせずにサンプルプロジェクトに入っている結果を利用してもいいが、ビルドする必要がある場合は Visual Studio または Mono で [OpenCVSharp プロジェクト](OpencvSharp/OpenCvSharp) のビルドをする。

## 使い方
以下のファイルを Unity プロジェクトに追加する。
 - OpenCVSharp.dll
 - OpenCVSharp.dll.config
動かない場合は以下を確認する
 - OpenCV の DLL に　PATH が通っているか
 - OpenCVShapr.dll.config の DLL 名は正しいか
