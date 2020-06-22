using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PInvokeFromCSharp.UnitTest
{
    [TestClass]
    public class UnitTestBuiltIn
    {
        [TestMethod]
        public void TestNativeBoolInMethods()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeBoolInMethods";

            asm.TestMyMethod(className, "GetTrue", true);

            asm.TestMyMethod(className, "Not", false, new object[] { true });
            asm.TestMyMethod(className, "Not", true, new object[] { false });

            asm.TestMyMethod(className, "And", true, new object[] { true, true });
            asm.TestMyMethod(className, "And", false, new object[] { true, false });
            asm.TestMyMethod(className, "And", false, new object[] { false, true });
            asm.TestMyMethod(className, "And", false, new object[] { false, false });
        }

        [TestMethod]
        public void TestNativeBuiltInMethods()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeBuiltInMethods";

            // int
            asm.TestMyMethod(className, "GetInt", 1234);
            asm.TestMyMethod(className, "GetIntMin", int.MinValue);
            asm.TestMyMethod(className, "GetIntMax", int.MaxValue);

            // byte
            asm.TestMyMethod(className, "GetByteMax", byte.MaxValue);

            // ulong
            asm.TestMyMethod(className, "GetUInt64Max", ulong.MaxValue);

            // double
            asm.TestMyMethod(className, "GetDouble", 12.34);

            // Add
            asm.TestMyMethod(className, "AddIntDouble", 12.34, new object[] { 11, 1.34 });
            asm.TestMyMethod(className, "AddIntDouble", 0.0, new object[] { 0, 0.0 });
            asm.TestMyMethod(className, "AddIntDouble", 0.0, new object[] { -10, 10.0 });
            asm.TestMyMethod(className, "AddIntDouble", -10.0, new object[] { -10, 0.0 });
        }
    }
}
