using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    internal static class NativeBuiltInFunctions
    {
        private const string DllFile = "NativeLibDemo.dll";

        [DllImport(DllFile, EntryPoint = "BuiltIn_GetInt")]
        internal extern static int GetInt();

        [DllImport(DllFile, EntryPoint = "BuiltIn_GetDouble")]
        internal extern static double GetDouble();

        [DllImport(DllFile, EntryPoint = "BuiltIn_AddIntDouble")]
        internal extern static double AddIntDouble(int i, double d);

    }

    internal class BuiltInWrapper : INativeWrapper
    {
        public void DoTest()
        {
            var i0 = NativeBuiltInFunctions.GetInt();
            Debug.Assert(i0 == 1234);

            var d0 = NativeBuiltInFunctions.GetDouble();
            Debug.Assert(d0 == 12.34);

            var d1 = NativeBuiltInFunctions.AddIntDouble(11, 1.34);
            Debug.Assert(d1 == 12.34);
        }
    }

}
