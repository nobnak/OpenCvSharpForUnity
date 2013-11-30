using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable 1591

namespace OpenCvSharp.CPlusPlus
{
    /// <summary>
    /// Base class for high-level OpenCV algorithms
    /// </summary>
    public class Algorithm : DisposableCvObject
    {
        /// <summary>
        /// sizeof(BackgroundSubtractor)
        /// </summary>
        public static readonly int SizeOf = CppInvoke.cv_Algorithm_sizeof();
        /// <summary>
        /// 
        /// </summary>
        protected bool _disposed = false;

        /// <summary>
        /// 
        /// </summary>
        public Algorithm()
        {
            _ptr = CppInvoke.cv_Algorithm_new();
            if (_ptr == IntPtr.Zero)
                throw new OpenCvSharpException();
        }
        /// <summary>
        /// 
        /// </summary>
        public Algorithm(IntPtr ptr)
        {
            _ptr = ptr;
            IsEnabledDispose = false;
        }
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
                        CppInvoke.cv_Algorithm_delete(_ptr);
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

        public string Name
        {
            get
            {
                StringBuilder buf = new StringBuilder(1024);
                CppInvoke.cv_Algorithm_name(_ptr, buf);
                return buf.ToString();
            }
        }

        #region Get
        public int GetInt(string name)
        {
            return CppInvoke.cv_Algorithm_getInt(_ptr, name);            
        }
        public double GetDouble(string name)
        {
            return CppInvoke.cv_Algorithm_getDouble(_ptr, name);
        }
        public bool GetBool(string name)
        {
            return CppInvoke.cv_Algorithm_getBool(_ptr, name);
        }
        public string GetString(string name)
        {
            StringBuilder buf = new StringBuilder(1024);
            CppInvoke.cv_Algorithm_getString(_ptr, name, buf);
            return buf.ToString();
        }
        public Mat GetMat(string name)
        {
            throw new NotImplementedException();
        }
        public Mat[] GetMatVector(string name)
        {
            throw new NotImplementedException();
        }
        public Algorithm GetAlgorithm(string name)
        {
            IntPtr ptr = CppInvoke.cv_Algorithm_getAlgorithm(_ptr, name);
            return new Algorithm(ptr);
        }
        #endregion

        #region Set
        public void SetInt(string name, int value)
        {
            CppInvoke.cv_Algorithm_setInt(_ptr, name, value);
        }
        public void SetDouble(string name, double value)
        {
            CppInvoke.cv_Algorithm_setDouble(_ptr, name, value);
        }
        public void SetBool(string name, bool value)
        {
            CppInvoke.cv_Algorithm_setBool(_ptr, name, value);
        }
        public void SetString(string name, string value)
        {
            CppInvoke.cv_Algorithm_setString(_ptr, name, value);
        }
        public void SetMat(string name, Mat value)
        {
            throw new NotImplementedException();
        }
        public void SetMatVector(string name, Mat[] value)
        {
            throw new NotImplementedException();
        }
        public void SetAlgorithm(string name, Algorithm value)
        {
            CppInvoke.cv_Algorithm_setAlgorithm(_ptr, name, value.CvPtr);
        }
        #endregion
    }
}
