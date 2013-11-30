/*
 * (C) 2008-2013 Schima
 * This code is licenced under the LGPL.
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable 1591

namespace OpenCvSharp.MachineLearning
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
    public class CvTrainTestSplit : DisposableCvObject
    {
        /// <summary>
        /// Track whether Dispose has been called
        /// </summary>
        private bool _disposed = false;

        #region Init and Disposal
#if LANG_JP
        /// <summary>
        /// 既定の初期化
        /// </summary>
#else
        /// <summary>
        /// Default constructor
        /// </summary>
#endif
        public CvTrainTestSplit()
            : base()
        {
            _ptr = MLInvoke.CvTrainTestSplit_construct1();
        }
#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="train_sample_count"></param>
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="train_sample_count"></param>
#endif
        public CvTrainTestSplit(int train_sample_count)
            : this(train_sample_count, true)
        {
        }
#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="train_sample_count"></param>
        /// <param name="mix"></param>
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="train_sample_count"></param>
        /// <param name="mix"></param>
#endif
        public CvTrainTestSplit(int train_sample_count, bool mix)
            : base()
        {
            _ptr = MLInvoke.CvTrainTestSplit_construct2(train_sample_count, mix);
        }
#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="train_sample_portion"></param>
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="train_sample_portion"></param>
#endif
        public CvTrainTestSplit(float train_sample_portion)
            : this(train_sample_portion, true)
        {
        }
#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="train_sample_portion"></param>
        /// <param name="mix"></param>
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="train_sample_portion"></param>
        /// <param name="mix"></param>
#endif
        public CvTrainTestSplit(float train_sample_portion, bool mix)
            : base()
        {
            _ptr = MLInvoke.CvTrainTestSplit_construct3(train_sample_portion, mix);
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
                        MLInvoke.CvTrainTestSplit_destruct(_ptr);
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

        #region Properties
        /// <summary>
        /// sizeof(CvTrainTestSplit)
        /// </summary>
        public static readonly int SizeOf = MLInvoke.CvTrainTestSplit_sizeof();

#if LANG_JP
        /// <summary>
        /// 
        /// </summary>
#else
        /// <summary>
        /// 
        /// </summary>
#endif
        public int TrainSamplePart_Count
        {
            get { return MLInvoke.CvTrainTestSplit_train_sample_part_count_get(_ptr); }
            set { MLInvoke.CvTrainTestSplit_train_sample_part_count_set(_ptr, value); }
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
        public float TrainSamplePart_Portion
        {
            get { return MLInvoke.CvTrainTestSplit_train_sample_part_portion_get(_ptr); }
            set { MLInvoke.CvTrainTestSplit_train_sample_part_portion_set(_ptr, value); }
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
        public PartMode TrainSamplePartMode
        {
            get { return (PartMode)MLInvoke.CvTrainTestSplit_train_sample_part_mode_get(_ptr); }
            set { MLInvoke.CvTrainTestSplit_train_sample_part_mode_set(_ptr, (int)value); }
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
        public bool Mix
        {
            get { return MLInvoke.CvTrainTestSplit_mix_get(_ptr); }
            set { MLInvoke.CvTrainTestSplit_mix_set(_ptr, value); }
        }
        #endregion
    }
}
