using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    // Align C++ and C#
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal readonly struct MemoryDataFromLib
    {
        public readonly IntPtr Ptr;
        public readonly int Size;

        public int GetByteSum()
        {
            int sum = 0;
            for (var i = 0; i < Size; i++)
            {
                checked
                {
                    sum += Marshal.ReadByte(Ptr, i);
                }
            }
            return sum;
        }
    }

    internal static class NativeMemFromLibMethods
    {
        private const string DllFile = Program.DllFile;

        [DllImport(DllFile, EntryPoint = "MemFromLib_GetBufferData")]
        internal extern static MemoryDataFromLib GetBufferDataValue();

        [DllImport(DllFile, EntryPoint = "MemFromLib_GetBufferDataRef")]
        internal extern static ref MemoryDataFromLib GetBufferDataRef();
    }

    internal class MemFromLibWrapper : INativeWrapper
    {
        public void DoTest()
        {
            // 各メソッド呼ぶと、byte のSumが 123 になる巨大メモリが返ってくる

            // 値戻り
            var data0 = NativeMemFromLibMethods.GetBufferDataValue();
            var sum0 = data0.GetByteSum();
            Debug.Assert(sum0 == 123);

            // 参照戻り： data1の型(var)に ref 付いてない理由が謎。値戻しの方が良さそう
            var data1 = NativeMemFromLibMethods.GetBufferDataRef();
            var sum1 = data1.GetByteSum();
            Debug.Assert(sum1 == 123);
        }
    }
}
