/*
 * (C) 2008-2013 Schima
 * This code is licenced under the LGPL.
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using OpenCvSharp;

namespace OpenCvSharp.Blob
{
	/// <summary>
    /// Polygon based contour.
    /// </summary>
    public class CvContourPolygon : CvObject, ICollection<CvPoint>
    {
        #region Init and Disposal
#if LANG_JP
        /// <summary>
        /// ポインタから初期化
        /// </summary>
        /// <param name="ptr">CvContourPolygon*</param>
#else
        /// <summary>
        /// Initializes from pointer
        /// </summary>
        /// <param name="ptr">CvContourPolygon*</param>
#endif
        public CvContourPolygon(IntPtr ptr)
            : base(ptr)
        {
        }
#if LANG_JP
        /// <summary>
        /// ポインタから初期化
        /// </summary>
        /// <param name="ptr">CvContourPolygon*</param>
#else
        /// <summary>
        /// Initializes from pointer
        /// </summary>
        /// <param name="ptr">CvContourPolygon*</param>
#endif
        public static CvFileNode FromPtr(IntPtr ptr)
        {
            return new CvFileNode(ptr);
        }
        #endregion

        #region Methods
        #region ContourPolygonArea
        /// <summary>
        /// Calculates area of a polygonal contour. 
        /// </summary>
        /// <returns>Area of the contour.</returns>
        public double ContourPolygonArea()
        {
            return CvBlobLib.ContourPolygonArea(this);
        }
        #endregion
        #region ContourPolygonPerimeter
        /// <summary>
        /// Calculates perimeter of a polygonal contour.
        /// </summary>
        /// <returns>Perimeter of the contour.</returns>
        public double ContourPolygonPerimeter()
        {
            return CvBlobLib.ContourPolygonPerimeter(this);
        }
        #endregion
        #region PolygonContourConvexHull
#if LANG_JP
        /// <summary>
        /// Calculates convex hull of a contour.
        /// Uses the Melkman Algorithm. Code based on the version in http://w3.impa.br/~rdcastan/Cgeometry/.
        /// </summary>
        /// <returns>Convex hull.</returns>
#else
        /// <summary>
        /// Calculates convex hull of a contour.
        /// Uses the Melkman Algorithm. Code based on the version in http://w3.impa.br/~rdcastan/Cgeometry/.
        /// </summary>
        /// <returns>Convex hull.</returns>
#endif
        public CvContourPolygon PolygonContourConvexHull()
        {
            IntPtr result = CvBlobInvoke.cvb_cvPolygonContourConvexHull(_ptr);
            if (result == IntPtr.Zero)
                return null;
            else
                return new CvContourPolygon(result);
        }
        #endregion
        #region RenderContourPolygon
        /// <summary>
        /// Draw a polygon.
        /// </summary>
        /// <param name="img">Image to draw on.</param>
        public void RenderContourPolygon(IplImage img)
        {
            CvBlobLib.RenderContourPolygon(this, img);
        }
        /// <summary>
        /// Draw a polygon.
        /// </summary>
        /// <param name="img">Image to draw on.</param>
        /// <param name="color">Color to draw (default, white).</param>
        public void RenderContourPolygon(IplImage img, CvScalar color)
        {
            CvBlobLib.RenderContourPolygon(this, img, color);
        }
        #endregion
        #region SimplifyPolygon
        /// <summary>
        /// Simplify a polygon reducing the number of vertex according the distance "delta". 
        /// Uses a version of the Ramer-Douglas-Peucker algorithm (http://en.wikipedia.org/wiki/Ramer-Douglas-Peucker_algorithm). 
        /// </summary>
        /// <returns>A simplify version of the original polygon.</returns>
        public CvContourPolygon SimplifyPolygon()
        {
            return CvBlobLib.SimplifyPolygon(this);
        }
        /// <summary>
        /// Simplify a polygon reducing the number of vertex according the distance "delta". 
        /// Uses a version of the Ramer-Douglas-Peucker algorithm (http://en.wikipedia.org/wiki/Ramer-Douglas-Peucker_algorithm). 
        /// </summary>
        /// <param name="delta">Minimun distance.</param>
        /// <returns>A simplify version of the original polygon.</returns>
        public CvContourPolygon SimplifyPolygon(double delta)
        {
            return CvBlobLib.SimplifyPolygon(this, delta);
        }
        #endregion
        #region WriteContourPolygonCSV
        /// <summary>
        /// Write a contour to a CSV (Comma-separated values) file.
        /// </summary>
        /// <param name="filename">File name.</param>
        public void WriteContourPolygonCSV(string filename)
        {
            CvBlobLib.WriteContourPolygonCSV(this, filename);
        }
        #endregion
        #region WriteContourPolygonSVG
        /// <summary>
        /// Write a contour to a SVG file.
        /// </summary>
        /// <param name="filename">File name.</param>
        public void WriteContourPolygonSVG(string filename)
        {
            CvBlobLib.WriteContourPolygonSVG(this, filename);
        }
        /// <summary>
        /// Write a contour to a SVG file.
        /// </summary>
        /// <param name="filename">File name.</param>
        /// <param name="stroke">Stroke color (black by default).</param>
        /// <param name="fill">Fill color (white by default).</param>
        public void WriteContourPolygonSVG(string filename, CvScalar stroke, CvScalar fill)
        {
            CvBlobLib.WriteContourPolygonSVG(this, filename, stroke, fill);
        }
        #endregion
        #endregion

        #region ICollection<CvPoint> Members
#if LANG_JP
        /// <summary>
        /// このコレクション に項目を追加します。
        /// </summary>
        /// <param name="item">このコレクション に追加するオブジェクト。</param>
#else
        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item">The object to add to the collection.</param>
#endif
        public void Add(CvPoint item)
        {
            CvBlobInvoke.CvContourPolygon_PushBack(_ptr, item);
        }
#if LANG_JP
        /// <summary>
        /// コレクション からすべての項目を削除します。
        /// </summary>
#else
        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
#endif
        public void Clear()
        {
            CvBlobInvoke.CvContourPolygon_Clear(_ptr);
        }
#if LANG_JP
        /// <summary>
        /// このコレクション に特定の値が格納されているかどうかを判断します。
        /// </summary>
        /// <param name="item">このコレクション 内で検索するオブジェクト。</param>
        /// <returns>item が このコレクション に存在する場合は true。それ以外の場合は false。</returns>
#else
        /// <summary>
        /// Determines whether the collection contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the collection.</param>
        /// <returns> true if item is found in the collection; otherwise, false.</returns>
#endif
        public bool Contains(CvPoint item)
        {
            return CvBlobInvoke.CvContourPolygon_Contains(_ptr, item);
        }
#if LANG_JP
        /// <summary>
        /// このコレクション の要素を Array にコピーします。Array の特定のインデックスからコピーが開始されます。
        /// </summary>
        /// <param name="array">このコレクション から要素がコピーされる 1 次元の Array。Array には、0 から始まるインデックス番号が必要です。</param>
        /// <param name="arrayIndex">コピーの開始位置となる、array の 0 から始まるインデックス番号。</param>
#else
        /// <summary>
        /// Copies the elements of the collection to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from collection. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
#endif
        public void CopyTo(CvPoint[] array, int arrayIndex)
        {
            CvBlobInvoke.CvContourPolygon_CopyTo(_ptr, array, arrayIndex);
        }
#if LANG_JP
        /// <summary>
        /// このコレクション に格納されている要素の数を取得します。
        /// </summary>
#else
        /// <summary>
        /// Gets the number of elements contained in the collection.
        /// </summary>
#endif
        public int Count
        {
            get
            {
                return CvBlobInvoke.CvContourPolygon_Count(_ptr);
            }
        }
#if LANG_JP
        /// <summary>
        /// このコレクション が読み取り専用かどうかを示す値を取得します。
        /// </summary>
#else
        /// <summary>
        /// Gets a value indicating whether the collectio is read-only.
        /// </summary>
#endif
        public bool IsReadOnly
        {
            get { return false; }
        }
#if LANG_JP
        /// <summary>
        /// コレクション 内で最初に見つかった特定のオブジェクトを削除します。
        /// </summary>
        /// <param name="item">コレクションから削除するオブジェクト。</param>
        /// <returns>item がコレクション から正常に削除された場合は true。それ以外の場合は false。</returns>
#else
        /// <summary>
        /// Removes the first occurrence of a specific object from the collection.
        /// </summary>
        /// <param name="item">The object to remove from the collection.</param>
        /// <returns>true if item was successfully removed from the collection; otherwise, false.</returns>
#endif
        public bool Remove(CvPoint item)
        {
            return CvBlobInvoke.CvContourPolygon_Remove(_ptr, item);
        }
        #endregion
        #region IEnumerable<CvPoint> Members
#if LANG_JP
		/// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
#else
        /// <summary>
        /// Returns an enumerator that iterates through a collection. 
        /// </summary>
#endif
        public IEnumerator<CvPoint> GetEnumerator()
        {
            CvPoint[] array = new CvPoint[Count];
            CopyTo(array, 0);

            foreach (CvPoint item in array)
            {
                yield return item;
            }
        }
#if LANG_JP
		/// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
#else
        /// <summary>
        /// Returns an enumerator that iterates through a collection. 
        /// </summary>
#endif
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
