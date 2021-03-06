﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    // Align C++ and C#
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal readonly struct MemoryDataToLib
    {
        public readonly IntPtr Ptr;
        public readonly int Size;

        public MemoryDataToLib(IntPtr p, int s) => (Ptr, Size) = (p, s);

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

    /// <summary>構造体用アンマネージメモリの割当と管理</summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal readonly struct MemoryDataToLibContainer : IDisposable
    {
        public readonly MemoryDataToLib Payload;
        private readonly IntPtr UnmanagedPtr;

        public MemoryDataToLibContainer(int size)
        {
            UnmanagedPtr = Marshal.AllocCoTaskMem(size);    // AllocHGlobal()
            Payload = new MemoryDataToLib(UnmanagedPtr, size);
        }

        public void Dispose()
        {
            if (UnmanagedPtr != IntPtr.Zero)
                Marshal.FreeCoTaskMem(UnmanagedPtr);        // FreeHGlobal()
        }
    }

    internal static class NativeMemToLibMethods
    {
        private const string DllFile = DllLocator.DllFileName;

        [DllImport(DllFile, EntryPoint = "MemToLib_GetBufferDataSum")]
        internal extern static int GetBufferDataSum(ref MemoryDataToLib data);

        [DllImport(DllFile, EntryPoint = "MemToLib_SetBufferLast")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool SetBufferLast(ref MemoryDataToLib data, byte val);
    }

    internal class MemToLibWrapper : INativeWrapper
    {
        public void DoTest()
        {
            using var container = new MemoryDataToLibContainer(10_000_000);
            var payload = container.Payload;

            // 先頭だけ値を書いておく
            Marshal.WriteByte(payload.Ptr, 23);

            // Libにメモリ内の合計値を計算させる
            int sum0 = NativeMemToLibMethods.GetBufferDataSum(ref payload);
            Debug.Assert(sum0 == 23);


            // ◆ここの戻り値が false のはずなのに、常に true が返ってくる。
            //   原因が分からなかった… int なら意図通りの値が返ってくる。
            bool err = NativeMemToLibMethods.SetBufferLast(ref payload, 100);
            //Debug.Assert(err == false);   // ◆意図通りに動かないのでチェックしない

            var sum1 = payload.GetByteSum();
            Debug.Assert(sum1 == 123);

        }
    }
}
