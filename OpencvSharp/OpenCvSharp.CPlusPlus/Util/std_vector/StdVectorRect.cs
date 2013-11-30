using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using OpenCvSharp.Utilities;

namespace OpenCvSharp.CPlusPlus
{
    /// <summary>
    /// 
    /// </summary>
    public class StdVectorRect : DisposableCvObject, IStdVector
    {
        /// <summary>
        /// Track whether Dispose has been called
        /// </summary>
        private bool _disposed = false;

        #region Init and Dispose
        /// <summary>
        /// 
        /// </summary>
        public StdVectorRect()
        {
            this._ptr = CppInvoke.vector_cvRect_new1();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public StdVectorRect(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException("size");
            this._ptr = CppInvoke.vector_cvRect_new2(new IntPtr(size));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public StdVectorRect(CvRect[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            this._ptr = CppInvoke.vector_cvRect_new3(data, new IntPtr(data.Length));
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged resources can be disposed.
        /// If false, the method has been called by the runtime from inside the finalizer and you should not reference other objects. Only unmanaged resources can be disposed.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                try
                {
                    if (IsEnabledDispose)
                    {
                        CppInvoke.vector_cvRect_delete(_ptr);
                    }
                    this._disposed = true;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// vector.size()
        /// </summary>
        public int Size
        {
            get { return CppInvoke.vector_cvRect_getSize(_ptr).ToInt32(); }
        }
        /// <summary>
        /// &amp;vector[0]
        /// </summary>
        public IntPtr ElemPtr
        {
            get { return CppInvoke.vector_cvRect_getPointer(_ptr); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Converts std::vector to managed array
        /// </summary>
        /// <returns></returns>
        public CvRect[] ToArray()
        {
            int size = Size;
            if (size == 0)
            {
                return new CvRect[0];
            }
            else
            {
                CvRect[] dst = new CvRect[size];
                using (ArrayAddress1<CvRect> dstPtr = new ArrayAddress1<CvRect>(dst))
                {
                    Util.CopyMemory(dstPtr, ElemPtr, CvRect.SizeOf * dst.Length);
                }
                return dst;
            }
        }
        #endregion
    }
}
