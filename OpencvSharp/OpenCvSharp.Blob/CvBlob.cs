/*
 * (C) 2008-2013 Schima
 * This code is licenced under the LGPL.
 */

using System;
using System.Collections.Generic;
using System.Text;

using CvLabel = System.UInt32;

namespace OpenCvSharp.Blob
{
    /// <summary>
    /// Struct that contain information about one blob.
    /// </summary>
    public class CvBlob : CvObject
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ptr">struct CvBlob*</param>
        public CvBlob(IntPtr ptr)
            : base(ptr)
        {
            IsDisposed = false;
        }

        #region Properties
        /// <summary>
        /// sizeof(CvBlob)
        /// </summary>
        public static readonly int SizeOf = CvBlobInvoke.CvBlob_sizeof();

#if LANG_JP
        /// <summary>
        /// リソースが解放済みかどうかを取得する
        /// </summary>
#else
        /// <summary>
        /// Gets a value indicating whether this instance has been disposed.
        /// </summary>
#endif
        public bool IsDisposed { get; private set; }

		/// <summary>
        /// Label assigned to the blob
        /// </summary>
		public CvLabel Label
		{
			get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_label(_ptr)); 
                }
            }
			set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_label(_ptr)) = value;
                }
            }
		}    
		
		/// <summary>
        /// Area (moment 00)
        /// </summary>
		public uint Area
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_area(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_area(_ptr)) = value; 
                }
            }
		}		
		/// <summary>
        /// Area (moment 00)
        /// </summary>
		public uint M00
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_m00(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_m00(_ptr))  = value;
                }
            }
		}	

		/// <summary>
        /// X min
        /// </summary>
		public uint MinX
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_minx(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_minx(_ptr))  = value;
                }
            }
		}	
		/// <summary>
        /// X max
        /// </summary>
		public uint MaxX
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_maxx(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_maxx(_ptr)) = value;
                }
            }
		}	
		/// <summary>
        /// Y min
        /// </summary>
		public uint MinY
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_miny(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_miny(_ptr)) = value;
                }
            }
		}	
		/// <summary>
        /// Y max
        /// </summary>
		public uint MaxY
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_maxy(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_maxy(_ptr)) = value;
                }
            }
		}

        /// <summary>
        /// CvRect(MinX, MinY, MaxX - MinX, MaxY - MinY)
        /// </summary>
        public CvRect Rect
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                return new CvRect((int)MinX, (int)MinY, (int)(MaxX - MinX), (int)(MaxY - MinY));
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                MinX = (uint)value.Left;
                MinY = (uint)value.Top;
                MaxX = (uint)value.Right;
                MaxY = (uint)value.Bottom;
            }
        }

		/// <summary>
        /// Centroid
        /// </summary>
		public CvPoint2D64f Centroid
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_centroid(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_centroid(_ptr)) = value;
                }
            }
		}	

		/// <summary>
        /// Moment 10
        /// </summary>
		public double M10
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_m10(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_m10(_ptr)) = value;
                }
            }
		}
		/// <summary>
        /// Moment 01
        /// </summary>
		public double M01
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_m01(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_m01(_ptr)) = value;
                }
            }
		}
		/// <summary>
        /// Moment 11
        /// </summary>
		public double M11
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_m11(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_m11(_ptr)) = value;
                }
            }
		}
		/// <summary>
        /// Moment 20
        /// </summary>
		public double M20
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_m20(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_m20(_ptr)) = value;
                }
            }
		}
		/// <summary>
        /// Moment 02
        /// </summary>
		public double M02
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_m02(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_m02(_ptr)) = value;
                }
            }
		}
	    
		/// <summary>
        /// True if central moments are being calculated
        /// </summary>
		public bool CentralMoments
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_centralMoments(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_centralMoments(_ptr)) = value;
                }
            }
		}
		/// <summary>
        /// Central moment 11
        /// </summary>
		public double U11
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_u11(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_u11(_ptr)) = value;
                }
            }
		}
		/// <summary>
        /// Central moment 20
        /// </summary>
		public double U20
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_u20(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_u20(_ptr)) = value;
                }
            }
		}
		/// <summary>
        /// Central moment 02
        /// </summary>
        public double U02
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    return *(CvBlobInvoke.CvBlob_u02(_ptr));
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                unsafe
                {
                    *(CvBlobInvoke.CvBlob_u02(_ptr)) = value;
                }
            }
        }

        /// <summary>
        /// Contour
        /// </summary>
        public CvContourChainCode Contour
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                IntPtr ptr = CvBlobInvoke.CvBlob_contour(_ptr);
                return new CvContourChainCode(ptr);
            }
        }
        /// <summary>
        /// Internal contours
        /// </summary>
        public CvContoursChainCode InternalContours
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvBlob");
                IntPtr ptr = CvBlobInvoke.CvBlob_internalContours(_ptr);
                return new CvContoursChainCode(ptr);
            }
        }
		#endregion

		#region Methods
        #region CalcCentroid
        /// <summary>
		/// Calculates centroid.
		/// Centroid will be returned and stored in the blob structure. (cvCentroid)
		/// </summary>
		/// <returns>Centroid.</returns>
		public CvPoint2D64f CalcCentroid()
		{
            if (IsDisposed)
                throw new ObjectDisposedException("CvBlob");

            return CvBlobLib.Centroid(this);
        }
        #endregion
        #region CalcCentralMoments
        /// <summary>
		/// Calculates central moment for a blob.
		/// Central moments will be stored in blob structure. (cvCentralMoments)
		/// </summary>
		/// <param name="img">Label image (depth=IPL_DEPTH_LABEL and num. channels=1).</param>
		public void CalcCentralMoments(IplImage img)
		{
            if (IsDisposed)
                throw new ObjectDisposedException("CvBlob");
			if(img == null)
				throw new ArgumentNullException("img");

		    CvBlobLib.CentralMoments(this, img);
        }
        #endregion
        #region CalcAngle
        /// <summary>
		/// Calculates angle orientation of a blob.
		/// This function uses central moments so cvCentralMoments should have been called before for this blob. (cvAngle)
		/// </summary>
		/// <returns>Angle orientation in radians.</returns>
		public double CalcAngle()
		{
            if (IsDisposed)
                throw new ObjectDisposedException("CvBlob");

            return CvBlobLib.Angle(this);
        }
        #endregion
        /*
        #region GetContour
        /// <summary>
        /// Get the contour of a blob.
        /// Uses Theo Pavlidis' algorithm (see http://www.imageprocessingplace.com/downloads_V3/root_downloads/tutorials/contour_tracing_Abeer_George_Ghuneim/theo.html ).
        /// </summary>
        /// <param name="img">Label image.</param>
        /// <returns>Chain code contour.</returns>
        public CvContourChainCode GetContour(IplImage img)
        {
            return CvBlobLib.GetContour(this, img);
        }
        #endregion
        //*/
        #region Release
#if LANG_JP
        /// <summary>
        /// 領域を解放 (cvReleaseBlob).
        /// 基本的にはユーザは呼び出すべきではない.
        /// </summary>
#else
        /// <summary>
        /// Clean up any resources being used (cvReleaseBlob).
        /// Usually users should not explicitly call this method.
        /// </summary>
#endif
        public void Release()
        {
            if (!IsDisposed)
            {
                CvBlobInvoke.cvb_cvReleaseBlob(_ptr);
                IsDisposed = true;
            }
        }
        #endregion
        #region SetImageROItoBlob
        /// <summary>
        /// Set the ROI of an image to the bounding box of a blob.
        /// </summary>
        /// <param name="img">Image.</param>
        public void SetImageROItoBlob(IplImage img)
        {
            if (IsDisposed)
                throw new ObjectDisposedException("CvBlob");

            CvBlobLib.SetImageROItoBlob(img, this);
        }
        #endregion
		#endregion
    }
}
