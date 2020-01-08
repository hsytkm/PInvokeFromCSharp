using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    internal static class NativeBuiltInFunctions
    {
        private const string DllFile = "NativeLibDemo.dll";

        [DllImport(DllFile, EntryPoint = "GetInt")]
        internal extern static int GetInt();

        [DllImport(DllFile, EntryPoint = "GetDouble")]
        internal extern static double GetDouble();

    }

    internal class BuiltInWrapper : INativeWrapper
    {
        public void DoTest()
        {
            var i = NativeBuiltInFunctions.GetInt();
            Debug.Assert(i == 123);

            var d = NativeBuiltInFunctions.GetDouble();
            Debug.Assert(d == 12.34);


        }
    }

}
