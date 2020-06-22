using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    internal static class NativeStringOutMethods
    {
        private const string DllFile = DllLocator.DllFileName;

        [DllImport(DllFile, EntryPoint = "StringOut_GetConstMessagePtr")]
        private extern static IntPtr GetConstMessagePtr();
        internal static string GetConstMessage() => Marshal.PtrToStringAnsi(GetConstMessagePtr());

    }

    internal class StringOutWrapper : INativeWrapper
    {
        public void DoTest()
        {
            // From Library (const message)
            var msg0 = NativeStringOutMethods.GetConstMessage();
            Debug.Assert(msg0 == "This is const char*");
        }
    }
}
