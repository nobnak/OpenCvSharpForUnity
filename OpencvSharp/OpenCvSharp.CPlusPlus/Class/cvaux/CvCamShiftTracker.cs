using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCvSharp.CPlusPlus
{
    /// <summary>
    /// 
    /// </summary>
    public class CvCamShiftTracker : DisposableCvObject
    {
        #region Fields
        /// <summary>
        /// sizeof(CvCamShiftTracker) 
        /// </summary>
        public static readonly int SizeOf = CppInvoke.CvCamShiftTracker_sizeof();
        /// <summary>
        /// Track whether Dispose has been called
        /// </summary>
        private bool _disposed = false;
        #endregion

        #region Init and Disposal
        /// <summary>
        /// 
        /// </summary>
        public CvCamShiftTracker()
        {
            _ptr = CppInvoke.CvCamShiftTracker_new();
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
                        CppInvoke.CvCamShiftTracker_delete(_ptr);
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

        #region Methods
        /**** Characteristics of the object that are calculated by track_object method *****/

        /// <summary>
        /// orientation of the object in degrees
        /// </summary>
        /// <returns></returns>
        public float GetOrientation()
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_get_orientation(_ptr);         
        }
        /// <summary>
        /// the larger linear size of the object
        /// </summary>
        /// <returns></returns>
        public float GetLength()
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_get_length(_ptr);
        }
        /// <summary>
        /// the smaller linear size of the object
        /// </summary>
        /// <returns></returns>
        public float GetWidth()
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_get_width(_ptr); 
        }
        /// <summary>
        /// center of the object
        /// </summary>
        /// <returns></returns>
        public CvPoint2D32f GetCenter()
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_get_center(_ptr);
        }
        /// <summary>
        /// bounding rectangle for the object
        /// </summary>
        /// <returns></returns>
        public CvRect GetWindow()
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_get_window(_ptr);
        }

        /*********************** Tracking parameters ************************/

        /// <summary>
        /// thresholding value that applied to back project
        /// </summary>
        /// <returns></returns>
        public int GetThreshold()
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_get_threshold(_ptr); 
        }
        /// <summary>
        /// returns number of histogram dimensions and sets
        /// </summary>
        /// <param name="dims"></param>
        /// <returns></returns>
        public int GetHistDims(params int[] dims)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_get_hist_dims(_ptr, dims); 
        }
        /// <summary>
        /// get the minimum allowed value of the specified channel
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public int GetMinChVal(int channel)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_get_min_ch_val(_ptr, channel);
        }
        /// <summary>
        /// get the maximum allowed value of the specified channel
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public int GetMaxChVal(int channel)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_get_max_ch_val(_ptr, channel); 
        }

        /// <summary>
        /// set initial object rectangle (must be called before initial calculation of the histogram)
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public bool SetWindow(CvRect window)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_set_window(_ptr, window);
        }
        /// <summary>
        /// threshold applied to the histogram bins
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public bool SetThreshold(int threshold)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_set_threshold(_ptr, threshold);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dim"></param>
        /// <param name="min_val"></param>
        /// <param name="max_val"></param>
        /// <returns></returns>
        public bool SetHistBinRange(int dim, int min_val, int max_val)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_set_hist_bin_range(_ptr, dim, min_val, max_val);
        }
        /// <summary>
        /// set the histogram parameters
        /// </summary>
        /// <param name="c_dims"></param>
        /// <param name="dims"></param>
        /// <returns></returns>
        public bool SetHistDims(int c_dims, params int[] dims)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_set_hist_dims(_ptr, c_dims, dims);
        }
        /// <summary>
        /// set the minimum allowed value of the specified channel
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetMinChVal(int channel, int val)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_set_min_ch_val(_ptr, channel, val);
        }
        /// <summary>
        /// set the maximum allowed value of the specified channel
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetMaxChVal(int channel, int val)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_set_max_ch_val(_ptr, channel, val);
        }

        /************************ The processing methods *********************************/
        /// <summary>
        /// update object position
        /// </summary>
        /// <param name="cur_frame"></param>
        /// <returns></returns>
        public virtual bool TrackObject(IplImage cur_frame)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            if (cur_frame == null)
                throw new ArgumentNullException("cur_frame");
            return CppInvoke.CvCamShiftTracker_track_object(_ptr, cur_frame.CvPtr);
        }
        /// <summary>
        /// update object histogram
        /// </summary>
        /// <param name="cur_frame"></param>
        /// <returns></returns>
        public virtual bool UpdateHistogram(IplImage cur_frame)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            if (cur_frame == null)
                throw new ArgumentNullException("cur_frame");
            return CppInvoke.CvCamShiftTracker_update_histogram(_ptr, cur_frame.CvPtr);
        }
        /// <summary>
        /// reset histogram
        /// </summary>
        public virtual void ResetHistogram()
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            CppInvoke.CvCamShiftTracker_reset_histogram(_ptr);
        }

        /************************ Retrieving internal data *******************************/

        /// <summary>
        /// get back project image
        /// </summary>
        /// <returns></returns>
        public virtual IplImage GetBackProject()
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            IntPtr p = CppInvoke.CvCamShiftTracker_get_back_project(_ptr);
            if (p == IntPtr.Zero)
                return null;
            else
                return new IplImage(p, false); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bin"></param>
        /// <returns></returns>
        public float Query(params int[] bin)
        {
            if (_disposed)
                throw new ObjectDisposedException("CvCamShiftTracker");
            return CppInvoke.CvCamShiftTracker_query(_ptr, bin);
        }
        #endregion
    }
}
