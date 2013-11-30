/*
 * (C) 2008-2013 Schima
 * This code is licenced under the LGPL.
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using OpenCvSharp.Utilities;

namespace OpenCvSharp
{
#if LANG_JP
    /// <summary>
    /// フォント
    /// </summary>
#else
    /// <summary>
    /// Font structure
    /// </summary>
#endif
    public class CvFont : DisposableCvObject
    {
        #region Initialization and Disposal
        /// <summary>
        /// 
        /// </summary>
        protected CvFont()
        {
        }
#if LANG_JP
        /// <summary>
        /// 文字描画関数に渡されるフォント構造体を初期化する
        /// </summary>
        /// <param name="font_face">フォント名の識別子</param>
        /// <param name="hscale">幅の比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の幅で表示される． 0.5fにした場合, 文字は元々の半分の幅で表示される．</param>
        /// <param name="vscale">高さの比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の高さで表示される． 0.5fにした場合, 文字は元々の半分の高さで表示される．</param>
#else
        /// <summary>
        /// Initializes font structure
        /// </summary>
        /// <param name="font_face">Font name identifier. Only a subset of Hershey fonts are supported now.</param>
        /// <param name="hscale">Horizontal scale. If equal to 1.0f, the characters have the original width depending on the font type. If equal to 0.5f, the characters are of half the original width. </param>
        /// <param name="vscale">Vertical scale. If equal to 1.0f, the characters have the original height depending on the font type. If equal to 0.5f, the characters are of half the original height. </param>
#endif
        public CvFont(FontFace font_face, double hscale, double vscale)
            : this(font_face, hscale, vscale, 0, 1, LineType.Link8)
        {
        }
#if LANG_JP
        /// <summary>
        /// 文字描画関数に渡されるフォント構造体を初期化する
        /// </summary>
        /// <param name="font_face">フォント名の識別子</param>
        /// <param name="hscale">幅の比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の幅で表示される． 0.5fにした場合, 文字は元々の半分の幅で表示される．</param>
        /// <param name="vscale">高さの比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の高さで表示される． 0.5fにした場合, 文字は元々の半分の高さで表示される．</param>
        /// <param name="shear">垂直線からの文字の相対的な角度．ゼロの場合は非イタリックフォントで，例えば，1.0fは≈45°を意味する．</param>
#else
        /// <summary>
        /// Initializes font structure
        /// </summary>
        /// <param name="font_face">Font name identifier. Only a subset of Hershey fonts are supported now.</param>
        /// <param name="hscale">Horizontal scale. If equal to 1.0f, the characters have the original width depending on the font type. If equal to 0.5f, the characters are of half the original width. </param>
        /// <param name="vscale">Vertical scale. If equal to 1.0f, the characters have the original height depending on the font type. If equal to 0.5f, the characters are of half the original height. </param>
        /// <param name="shear">Approximate tangent of the character slope relative to the vertical line. Zero value means a non-italic font, 1.0f means ≈45° slope, etc. thickness Thickness of lines composing letters outlines. The function cvLine is used for drawing letters. </param>
#endif
        public CvFont(FontFace font_face, double hscale, double vscale, Double shear)
            : this(font_face, hscale, vscale, shear, 1, LineType.Link8)
        {
        }
#if LANG_JP
        /// <summary>
        /// 文字描画関数に渡されるフォント構造体を初期化する
        /// </summary>
        /// <param name="font_face">フォント名の識別子</param>
        /// <param name="hscale">幅の比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の幅で表示される． 0.5fにした場合, 文字は元々の半分の幅で表示される．</param>
        /// <param name="vscale">高さの比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の高さで表示される． 0.5fにした場合, 文字は元々の半分の高さで表示される．</param>
        /// <param name="shear">垂直線からの文字の相対的な角度．ゼロの場合は非イタリックフォントで，例えば，1.0fは≈45°を意味する．</param>
        /// <param name="thickness">文字の太さ．</param>
#else
        /// <summary>
        /// Initializes font structure
        /// </summary>
        /// <param name="font_face">Font name identifier. Only a subset of Hershey fonts are supported now.</param>
        /// <param name="hscale">Horizontal scale. If equal to 1.0f, the characters have the original width depending on the font type. If equal to 0.5f, the characters are of half the original width. </param>
        /// <param name="vscale">Vertical scale. If equal to 1.0f, the characters have the original height depending on the font type. If equal to 0.5f, the characters are of half the original height. </param>
        /// <param name="shear">Approximate tangent of the character slope relative to the vertical line. Zero value means a non-italic font, 1.0f means ≈45° slope, etc. thickness Thickness of lines composing letters outlines. The function cvLine is used for drawing letters. </param>
        /// <param name="thickness">Thickness of the text strokes. </param>
#endif
        public CvFont(FontFace font_face, double hscale, double vscale, double shear, int thickness)
            : this(font_face, hscale, vscale, shear, thickness, LineType.Link8)
        {
        }
#if LANG_JP
        /// <summary>
        /// 文字描画関数に渡されるフォント構造体を初期化する
        /// </summary>
        /// <param name="font_face">フォント名の識別子</param>
        /// <param name="hscale">幅の比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の幅で表示される． 0.5fにした場合, 文字は元々の半分の幅で表示される．</param>
        /// <param name="vscale">高さの比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の高さで表示される． 0.5fにした場合, 文字は元々の半分の高さで表示される．</param>
        /// <param name="shear">垂直線からの文字の相対的な角度．ゼロの場合は非イタリックフォントで，例えば，1.0fは≈45°を意味する．</param>
        /// <param name="thickness">文字の太さ．</param>
        /// <param name="line_type">線の種類．</param>
#else
        /// <summary>
        /// Initializes font structure
        /// </summary>
        /// <param name="font_face">Font name identifier. Only a subset of Hershey fonts are supported now.</param>
        /// <param name="hscale">Horizontal scale. If equal to 1.0f, the characters have the original width depending on the font type. If equal to 0.5f, the characters are of half the original width. </param>
        /// <param name="vscale">Vertical scale. If equal to 1.0f, the characters have the original height depending on the font type. If equal to 0.5f, the characters are of half the original height. </param>
        /// <param name="shear">Approximate tangent of the character slope relative to the vertical line. Zero value means a non-italic font, 1.0f means ≈45° slope, etc. thickness Thickness of lines composing letters outlines. The function cvLine is used for drawing letters. </param>
        /// <param name="thickness">Thickness of the text strokes. </param>
        /// <param name="line_type">Type of the strokes, see cvLine description. </param>
#endif
        public CvFont(FontFace font_face, double hscale, double vscale, double shear, int thickness, LineType line_type)
        {
            this._ptr = base.AllocMemory(SizeOf);
            CvInvoke.cvInitFont(this._ptr, font_face, hscale, vscale, shear, thickness, line_type);
        }

#if LANG_JP
        /// <summary>
        /// リソースの解放
        /// </summary>
        /// <param name="disposing">
        /// trueの場合は、このメソッドがユーザコードから直接が呼ばれたことを示す。マネージ・アンマネージ双方のリソースが解放される。
        /// falseの場合は、このメソッドはランタイムからファイナライザによって呼ばれ、もうほかのオブジェクトから参照されていないことを示す。アンマネージリソースのみ解放される。
        ///</param>
#else
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged resources can be disposed.
        /// If false, the method has been called by the runtime from inside the finalizer and you should not reference other objects. Only unmanaged resources can be disposed.
        /// </param>
#endif
        protected override void Dispose(bool disposing)
        {
            // 親の解放処理
            base.Dispose(disposing);
        }
        #endregion

        #region Properties
        /// <summary>
        /// sizeof(CvFont) 
        /// </summary>
        public static readonly int SizeOf = Marshal.SizeOf(typeof(WCvFont));

#if LANG_JP
        /// <summary>
        /// ColorFont -> cvScalar(blue_component, green_component, red\_component[, alpha_component])
        /// </summary>
#else
        /// <summary>
        /// ColorFont -> cvScalar(blue_component, green_component, red\_component[, alpha_component])
        /// </summary>
#endif
        public string NameFont
        {
            get
            {
                unsafe
                {
                    sbyte* p = ((WCvFont*)_ptr)->nameFont;
                    return new string(p);
                }
            }
            set
            {
                throw new NotImplementedException();
            }
        }
#if LANG_JP
        /// <summary>
        /// ColorFont -> cvScalar(blue_component, green_component, red\_component[, alpha_component])
        /// </summary>
#else
        /// <summary>
        /// ColorFont -> cvScalar(blue_component, green_component, red\_component[, alpha_component])
        /// </summary>
#endif
        public CvScalar Color
        {
            get
            {
                unsafe
                {
                    return ((WCvFont*)_ptr)->color;
                }
            }
            set
            {
                unsafe
                {
                    ((WCvFont*)_ptr)->color = value;
                }
            }
        }
#if LANG_JP
        /// <summary>
        /// フォント名の識別子
        /// </summary>
#else
        /// <summary>
        /// Font name identifier
        /// </summary>
#endif
        public FontFace FontFace
        {
            get
            {
                unsafe
                {
                    return (FontFace)(((WCvFont*)_ptr)->font_face);
                }
            }
            set
            {
                unsafe
                {
                    ((WCvFont*)_ptr)->font_face = (int)value;
                }
            }
        }
        /// <summary>
        /// font data and metrics
        /// </summary>
        public IntPtr Ascii
        {
            get
            {
                unsafe
                {
                    return new IntPtr(((WCvFont*)_ptr)->ascii);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IntPtr Greek
        {
            get
            {
                unsafe
                {
                    return new IntPtr(((WCvFont*)_ptr)->greek);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IntPtr Cyrillic
        {
            get
            {
                unsafe
                {
                    return new IntPtr(((WCvFont*)_ptr)->cyrillic);
                }
            }
        }
#if LANG_JP
        /// <summary>
        /// 幅の比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の幅で表示される． 0.5fにした場合, 文字は元々の半分の幅で表示される．
        /// </summary>
#else
        /// <summary>
        /// Horizontal scale. If equal to 1.0f, the characters have the original width depending on the font type. If equal to 0.5f, the characters are of half the original width. 
        /// </summary>
#endif
        public float HScale
        {
            get
            {
                unsafe
                {
                    return ((WCvFont*)_ptr)->hscale;
                }
            }
            set
            {
                unsafe
                {
                    ((WCvFont*)_ptr)->hscale = value;
                }
            }
        }
#if LANG_JP
        /// <summary>
        /// 高さの比率．1.0fにした場合，文字はそれぞれのフォントに依存する元々の高さで表示される． 0.5fにした場合, 文字は元々の半分の高さで表示される．
        /// </summary>
#else
        /// <summary>
        /// Vertical scale. If equal to 1.0f, the characters have the original height depending on the font type. If equal to 0.5f, the characters are of half the original height. 
        /// </summary>
#endif
        public float VScale
        {
            get
            {
                unsafe
                {
                    return ((WCvFont*)_ptr)->vscale;
                }
            }
            set
            {
                unsafe
                {
                    ((WCvFont*)_ptr)->vscale = value;
                }
            }
        }
#if LANG_JP
        /// <summary>
        /// 垂直線からの文字の相対的な角度．ゼロの場合は非イタリックフォントで，例えば，1.0fは≈45°を意味する．
        /// </summary>
#else
        /// <summary>
        /// slope coefficient: 0 - normal, >0 - italic
        /// </summary>
#endif
        public float Shear
        {
            get
            {
                unsafe
                {
                    return ((WCvFont*)_ptr)->shear;
                }
            }
            set
            {
                unsafe
                {
                    ((WCvFont*)_ptr)->shear = value;
                }
            }
        }
#if LANG_JP
        /// <summary>
        /// 文字の太さ
        /// </summary>
#else
        /// <summary>
        /// letters thickness
        /// </summary>
#endif
        public int Thickness
        {
            get
            {
                unsafe
                {
                    return ((WCvFont*)_ptr)->thickness;
                }
            }
            set
            {
                unsafe
                {
                    ((WCvFont*)_ptr)->thickness = value;
                }
            }
        }
        /// <summary>
        /// horizontal interval between letters
        /// </summary>
        public float Dx
        {
            get
            {
                unsafe
                {
                    return ((WCvFont*)_ptr)->dx;
                }
            }
            set
            {
                unsafe
                {
                    ((WCvFont*)_ptr)->dx = value;
                }
            }
        }
#if LANG_JP
        /// <summary>
        /// 線の種類
        /// </summary>
#else
        /// <summary>
        /// Type of the strokes
        /// </summary>
#endif
        public LineType LineType
        {
            get
            {
                unsafe
                {
                    return (LineType)(((WCvFont*)_ptr)->line_type);
                }
            }
            set
            {
                unsafe
                {
                    ((WCvFont*)_ptr)->line_type = (int)value;
                }
            }
        }
        #endregion

        #region Methods
        #region GetTextSize
#if LANG_JP
        /// <summary>
        /// 文字列の幅と高さを取得する
        /// </summary>
        /// <param name="text_string">入力文字列</param>
#else
        /// <summary>
        /// Retrieves width and height of text string
        /// </summary>
        /// <param name="text_string">Input string. </param>
#endif
        public CvSize GetTextSize(string text_string)
        {
            int baseline;
            return GetTextSize(text_string, out baseline);
        }
#if LANG_JP
        /// <summary>
        /// 文字列の幅と高さを取得する
        /// </summary>
        /// <param name="text_string">入力文字列</param>
        /// <param name="baseline">文字の最下点から見たベースラインのy座標</param>
#else
        /// <summary>
        /// Retrieves width and height of text string
        /// </summary>
        /// <param name="text_string">Input string. </param>
        /// <param name="baseline">y-coordinate of the baseline relatively to the bottom-most text point. </param>
#endif
        public CvSize GetTextSize(string text_string, out int baseline)
        {
            CvSize text_size;
            Cv.GetTextSize(text_string, this, out text_size, out baseline);
            return text_size;
        }
        #endregion
        #endregion
    }
}
