/*
 * (C) 2008-2013 Schima
 * This code is licenced under the LGPL.
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using CvLabel = System.UInt32;
using CvID = System.UInt32;

namespace OpenCvSharp.Blob
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal struct WCvTrack
    {
        /// <summary>
        /// Track identification number.
        /// </summary>
        public CvID id; 

        /// <summary>
        /// Label assigned to the blob related to this track.
        /// </summary>
        public CvLabel label;

        /// <summary>
        /// X min.
        /// </summary>
        public uint minx; 
        /// <summary>
        /// X max.
        /// </summary>
        public uint maxx; 
        /// <summary>
        /// Y min.
        /// </summary>
        public uint miny; 
        /// <summary>
        /// Y max.
        /// </summary>
        public uint maxy;

        /// <summary>
        /// Centroid.
        /// </summary>
        public CvPoint2D64f centroid; 

        /// <summary>
        /// Indicates number of frames that has been missing.
        /// </summary>
        public uint inactive;
    }

    /// <summary>
    /// Struct that contain information about one track.
    /// </summary>
    public class CvTrack : CvObject
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ptr">struct CvTrack*</param>
        public CvTrack(IntPtr ptr)
        {
            _ptr = ptr;
            IsDisposed = false;
        }

        #region Properties
        /// <summary>
        /// sizeof(CvTrack)
        /// </summary>
        public static readonly int SizeOf = Marshal.SizeOf(typeof(WCvTrack));

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
        /// Track identification number.
        /// </summary>
        public CvID ID
        {
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    return ((WCvTrack*)_ptr)->id;
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    ((WCvTrack*)_ptr)->id = value;
                }
            }
        }	
		/// <summary>
        /// Label assigned to the blob
        /// </summary>
		public CvLabel Label
		{
			get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    return ((WCvTrack*)_ptr)->label; 
                }
            }
			set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    ((WCvTrack*)_ptr)->label = value;
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
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    return ((WCvTrack*)_ptr)->minx;
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    ((WCvTrack*)_ptr)->minx = value;
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
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    return ((WCvTrack*)_ptr)->maxx;
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    ((WCvTrack*)_ptr)->maxx = value;
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
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    return ((WCvTrack*)_ptr)->miny;
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    ((WCvTrack*)_ptr)->miny = value;
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
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    return ((WCvTrack*)_ptr)->maxy;
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    ((WCvTrack*)_ptr)->maxy = value;
                }
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
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    return ((WCvTrack*)_ptr)->centroid;
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    ((WCvTrack*)_ptr)->centroid = value;
                }
            }
		}	

		/// <summary>
        /// Indicates number of frames that has been missing.
        /// </summary>
        public uint Inactive
		{
            get
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    return ((WCvTrack*)_ptr)->inactive;
                }
            }
            set
            {
                if (IsDisposed)
                    throw new ObjectDisposedException("CvTrack");
                unsafe
                {
                    ((WCvTrack*)_ptr)->inactive = value;
                }
            }
		}
		#endregion

		#region Methods
#if LANG_JP
        /// <summary>
        /// 領域を解放する (delete track).
        /// 基本的にはユーザは呼び出すべきではない.
        /// </summary>
#else
        /// <summary>
        /// Clean up any resources being used (delete track).
        /// Usually users should not explicitly call this method.
        /// </summary>
#endif
        public void Release()
        {
            if (!IsDisposed)
            {
                CvBlobInvoke.CvTrack_destruct(_ptr);
                IsDisposed = true;
            }
        }
		#endregion
    }
}
