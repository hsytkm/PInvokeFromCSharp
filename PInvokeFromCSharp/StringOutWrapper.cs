using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    internal static class NativeStringOutMethods
    {
        private const string DllFile = DllLocator.DllFileName;

        [DllImport(DllFile, EntryPoint = "StringOut_GetConstMessagePtr")]
        internal extern static IntPtr GetConstMessagePtr();
    }

    internal class StringOutWrapper : INativeWrapper
    {
        public void DoTest()
        {
            // From Library (const message)
            var ptr0 = NativeStringOutMethods.GetConstMessagePtr();
            var msg0 = Marshal.PtrToStringAnsi(ptr0);
            Debug.Assert(msg0 == "This is const char*");
        }
    }
}
