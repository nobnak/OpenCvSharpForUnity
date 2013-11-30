/*
 * (C) 2008-2013 Schima
 * This code is licenced under the LGPL.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using OpenCvSharp;
using OpenCvSharp.Utilities;
using OpenCvSharp.Gpu;

namespace OpenCvSharp.CPlusPlus
{
#if LANG_JP
    /// <summary>
    /// 
    /// </summary>
#else
    /// <summary>
    /// 
    /// </summary>
#endif
    public partial class StereoBM_GPU : DisposableCvObject
    {
        #region Fields
        /// <summary>
        /// sizeof(StereoBM_GPU)
        /// </summary>
        public static readonly int SizeOf;
        /// <summary>
        /// Track whether Dispose has been called
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// 
        /// </summary>
        public const int BASIC_PRESET = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int PREFILTER_XSOBEL = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int DEFAULT_NDISP = 64;
        /// <summary>
        /// 
        /// </summary>
        public const int DEFAULT_WINSZ = 19;
        #endregion

        #region Init and Disposal
        /// <summary>
        /// static constructor
        /// </summary>
        static StereoBM_GPU()
        {
            try
            {
                SizeOf = (int)GpuInvoke.StereoBM_GPU_sizeof();
            }
            catch (DllNotFoundException e)
            {
                PInvokeHelper.DllImportError(e);
            }
            catch (BadImageFormatException e)
            {
                PInvokeHelper.DllImportError(e);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #region Constructor
#if LANG_JP
        /// <summary>
        /// デフォルトのパラメータで初期化.
        /// </summary>
#else
        /// <summary>
        /// Default constructor
        /// </summary>
#endif
        public StereoBM_GPU()
        {
            _ptr = GpuInvoke.StereoBM_GPU_new1();
            if (_ptr == IntPtr.Zero)
                throw new OpenCvSharpException();
        }

#if LANG_JP
        /// <summary>
        /// StereoBM_GPU コンストラクタ
        /// </summary>
        /// <param name="preset"></param>
        /// <param name="ndisparities"></param>
        /// <param name="winSize"></param>
#else
        /// <summary>
        /// StereoBM_GPU Constructor
        /// </summary>
        /// <param name="preset"></param>
        /// <param name="ndisparities"></param>
        /// <param name="winSize"></param>
#endif
        public StereoBM_GPU(int preset, int ndisparities = DEFAULT_NDISP, int winSize = DEFAULT_WINSZ)
        {
            _ptr = GpuInvoke.StereoBM_GPU_new2(preset, ndisparities, winSize);
            if (_ptr == IntPtr.Zero)
                throw new OpenCvSharpException();
        }
        #endregion
        #region Dispose
#if LANG_JP
        /// <summary>
        /// リソースの解放
        /// </summary>
#else
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
#endif
        public void Release()
        {
            Dispose(true);
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
            if (!this._disposed)
            {
                // 継承したクラス独自の解放処理
                try
                {
                    if (disposing)
                    {                        
                    }
                    if (IsEnabledDispose)
                    {
                        GpuInvoke.StereoBM_GPU_delete(_ptr);
                    }
                    this._disposed = true;
                }
                finally
                {
                    // 親の解放処理
                    base.Dispose(disposing);
                }
            }
        }
        #endregion
        #endregion

        #region Properties
#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public int Preset
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException("StereoBM_GPU");
                unsafe
                {
                    return *GpuInvoke.StereoBM_GPU_preset(_ptr);
                }
            }
            set
            {
                if (_disposed)
                    throw new ObjectDisposedException("StereoBM_GPU");
                unsafe
                {
                    *GpuInvoke.StereoBM_GPU_preset(_ptr) = value;
                }
            }
        }
#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public int Ndisp
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException("StereoBM_GPU");
                unsafe
                {
                    return *GpuInvoke.StereoBM_GPU_ndisp(_ptr);
                }
            }
            set
            {
                if (_disposed)
                    throw new ObjectDisposedException("StereoBM_GPU");
                unsafe
                {
                    *GpuInvoke.StereoBM_GPU_ndisp(_ptr) = value;
                }
            }
        }
#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public int WinSize
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException("StereoBM_GPU");
                unsafe
                {
                    return *GpuInvoke.StereoBM_GPU_winSize(_ptr);
                }
            }
            set
            {
                if (_disposed)
                    throw new ObjectDisposedException("StereoBM_GPU");
                unsafe
                {
                    *GpuInvoke.StereoBM_GPU_winSize(_ptr) = value;
                }
            }
        }
#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public float AvergeTexThreshold
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException("StereoBM_GPU");
                unsafe
                {
                    return *GpuInvoke.StereoBM_GPU_avergeTexThreshold(_ptr);
                }
            }
            set
            {
                if (_disposed)
                    throw new ObjectDisposedException("StereoBM_GPU");
                unsafe
                {
                    *GpuInvoke.StereoBM_GPU_avergeTexThreshold(_ptr) = value;
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool CheckIfGpuCallReasonable()
        {
            return GpuInvoke.StereoBM_GPU_checkIfGpuCallReasonable() != 0;
        }

#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="disparity"></param>
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="disparity"></param>
#endif
        public void Run(GpuMat left, GpuMat right, GpuMat disparity)
        {
            if (_disposed)
                throw new ObjectDisposedException("StereoBM_GPU");
            if(left == null)
                throw new ArgumentNullException("left");
            if(right == null)
                throw new ArgumentNullException("right");
            if (disparity == null)
                throw new ArgumentNullException("disparity");

            GpuInvoke.StereoBM_GPU_run1(_ptr, left.CvPtr, right.CvPtr, disparity.CvPtr);
        }
        #endregion
    }
}
