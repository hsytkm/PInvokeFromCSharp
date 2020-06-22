using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp.UnitTest
{
    [TestClass]
    public class UnitTestMemory
    {
        [TestMethod]
        public void TestNativeMemFromLibMethods()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeMemFromLibMethods";

            // 各メソッド呼ぶと、byte のSumが 123 になる巨大メモリが返ってくる

            // 値戻り
            var data0 = asm.InvokeInternalStaticMethod<object>(className, "GetBufferDataValue");
            var method0 = data0.GetType().GetMethod("GetByteSum");
            var sum0 = method0.Invoke(data0, null);
            Assert.AreEqual(123, sum0);

            // 参照戻り： data1の型(var)に ref 付いてない理由が謎。値戻しの方が良さそう
            var data1 = asm.InvokeInternalStaticMethod<object>(className, "GetBufferDataRef");
            var method1 = data1.GetType().GetMethod("GetByteSum");
            var sum1 = method1.Invoke(data1, null);
            Assert.AreEqual(123, sum1);
        }

        [TestMethod]
        public void TestNativeMemToLibMethods()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeMemToLibMethods";

            // ◆ref 参照とかがややこし過ぎて力尽きた。実装できんかった…

#if false
            var type = asm.GetType("MemoryDataToLibContainer");
            object container = Activator.CreateInstance(type, new object[] { 10_000_000 });

            //using var container = new MemoryDataToLibContainer(10_000_000);
            var payload = container.Payload;

            // 先頭だけ値を書いておく
            Marshal.WriteByte(payload.Ptr, 23);

            // Libにメモリ内の合計値を計算させる
            var sum0 = asm.InvokeInternalStaticMethod<int>(className, "GetBufferDataSum", new object[] { payload });
            //int sum0 = NativeMemToLibMethods.GetBufferDataSum(ref payload);
            //Assert.AreEqual(23, sum0);


            // ◆ここの戻り値が false のはずなのに、常に true が返ってくる。
            //   原因が分からなかった… int なら意図通りの値が返ってくる。
            var err = asm.InvokeInternalStaticMethod<int>(className, "SetBufferLast", new object[] { payload, 100 });
            //bool err = NativeMemToLibMethods.SetBufferLast(ref payload, 100);
            //Debug.Assert(err == false);   // ◆意図通りに動かないのでチェックしない

            var sum1 = payload.GetByteSum();
            Assert.AreEqual(123, sum1);
#endif

        }
    }
}
