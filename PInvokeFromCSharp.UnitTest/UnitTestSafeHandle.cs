using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace PInvokeFromCSharp.UnitTest
{
    [TestClass]
    public class UnitTestSafeHandle
    {
        [TestMethod]
        public void TestNativeLibBufferMethods()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeLibBufferMethods";

            var handle = asm.InvokeInternalStaticMethod<object>(className, "CreateLibBuffer");  //NativeLibBufferHandle

            var sum = asm.InvokeInternalStaticMethod<int>(className, "GetDataSum", new object[] { handle });

            var size = asm.InvokeInternalStaticMethod<int>(className, "GetDataSize", new object[] { handle });
            var answer = Enumerable.Range(0, size).Sum();

            Assert.AreEqual(sum, answer);
        }

    }
}
