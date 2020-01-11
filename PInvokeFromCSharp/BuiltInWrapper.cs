using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    internal static class NativeBuiltInFunctions
    {
        private const string DllFile = Program.DllFile;

        [DllImport(DllFile, EntryPoint = "BuiltIn_GetInt")]
        internal extern static int GetInt();

        [DllImport(DllFile, EntryPoint = "BuiltIn_GetIntMin")]
        internal extern static int GetIntMin();

        [DllImport(DllFile, EntryPoint = "BuiltIn_GetIntMax")]
        internal extern static int GetIntMax();

        [DllImport(DllFile, EntryPoint = "BuiltIn_GetByteMax")]
        internal extern static byte GetByteMax();

        [DllImport(DllFile, EntryPoint = "BuiltIn_GetUInt64Max")]
        internal extern static ulong GetUInt64Max();

        [DllImport(DllFile, EntryPoint = "BuiltIn_GetDouble")]
        internal extern static double GetDouble();

        [DllImport(DllFile, EntryPoint = "BuiltIn_AddIntDouble")]
        internal extern static double AddIntDouble(int i, double d);

    }

    internal class BuiltInWrapper : INativeWrapper
    {
        public void DoTest()
        {
            // int
            var i0 = NativeBuiltInFunctions.GetInt();
            Debug.Assert(i0 == 1234);
            Debug.Assert(int.MinValue == NativeBuiltInFunctions.GetIntMin());
            Debug.Assert(int.MaxValue == NativeBuiltInFunctions.GetIntMax());

            // byte
            Debug.Assert(byte.MaxValue == NativeBuiltInFunctions.GetByteMax());

            // ulong
            Debug.Assert(ulong.MaxValue == NativeBuiltInFunctions.GetUInt64Max());

            // double
            var d0 = NativeBuiltInFunctions.GetDouble();
            Debug.Assert(d0 == 12.34);

            // Add
            var d1 = NativeBuiltInFunctions.AddIntDouble(11, 1.34);
            Debug.Assert(d1 == 12.34);
        }
    }

}
