using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    // SafeHandle クラス https://docs.microsoft.com/ja-jp/dotnet/api/system.runtime.interopservices.safehandle?view=netframework-4.8
    internal class NativeLibBufferHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public NativeLibBufferHandle() : base(true) { }

        public IntPtr Ptr => handle;

        // if error, return false
        protected override bool ReleaseHandle()
            => (NativeLibBufferMethods.DisposeLibBuffer(handle) == 0);
    }

    internal static class NativeLibBufferMethods
    {
        private const string DllFile = DllLocator.DllFileName;

        [DllImport(DllFile, EntryPoint = "CreateLibBufferClass")]
        internal static extern NativeLibBufferHandle CreateLibBuffer();

        [DllImport(DllFile, EntryPoint = "DisposeLibBufferClass")]
        internal static extern int DisposeLibBuffer(IntPtr ptr);

        [DllImport(DllFile, EntryPoint = "LibBuffer_GetDataSum")]
        internal static extern int GetDataSum(NativeLibBufferHandle ptr);

        [DllImport(DllFile, EntryPoint = "LibBuffer_GetDataSize")]
        internal static extern int GetDataSize(NativeLibBufferHandle ptr);
    }

    internal class ClassDisposeWrapper : INativeWrapper, IDisposable
    {
        private readonly NativeLibBufferHandle _handle;

        public ClassDisposeWrapper()
        {
            _handle = NativeLibBufferMethods.CreateLibBuffer();

            // Determine if file is opened successfully.
            if (_handle.IsInvalid)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public void DoTest()
        {
            var libsum = NativeLibBufferMethods.GetDataSum(_handle);

            var size = NativeLibBufferMethods.GetDataSize(_handle);
            var answer = Enumerable.Range(0, size).Sum();

            Debug.Assert(libsum == answer);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。
                if (_handle != null && !_handle.IsInvalid)
                {
                    _handle.Dispose();
                }

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~ClassDisposeWrapper()
        // {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
