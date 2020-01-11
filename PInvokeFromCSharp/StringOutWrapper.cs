﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PInvokeFromCSharp
{
    internal static class NativeStringOutFunctions
    {
        private const string DllFile = Program.DllFile;

        [DllImport(DllFile, EntryPoint = "StringOut_GetConstMessagePtr")]
        internal extern static IntPtr GetConstMessagePtr();

    }

    internal class StringOutWrapper : INativeWrapper
    {
        public void DoTest()
        {
            // From Library (const message)
            var ptr0 = NativeStringOutFunctions.GetConstMessagePtr();
            var msg0 = Marshal.PtrToStringAnsi(ptr0);
            Debug.Assert(msg0 == "This is const char*");

        }
    }

}