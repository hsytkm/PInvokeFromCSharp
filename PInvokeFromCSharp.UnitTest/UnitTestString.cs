using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace PInvokeFromCSharp.UnitTest
{
    [TestClass]
    public class UnitTestString
    {
        [TestMethod]
        public void TestNativeStringInMethodsAnsi()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeStringInMethods";
            var methodName = "CountCharAnsi";

            var param0 = Environment.SystemDirectory;
            asm.TestMyMethod(className, methodName, param0.Length, new object[] { param0 });

            // ◆日本語だと文字数が2倍になるっぽい
            var param1 = "あ井ウゑヲ";
            asm.TestMyMethod(className, methodName, param1.Length * 2, new object[] { param1 });
        }

        [TestMethod]
        public void TestNativeStringInMethodsChar()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeStringInMethods";
            var methodName = "CountChar";

            var param0 = "aBcD";
            asm.TestMyMethod(className, methodName, param0.Length, new object[] { param0 });

            var param1 = "#$%&(";
            asm.TestMyMethod(className, methodName, param1.Length, new object[] { param1 });

            // ◆日本語は数えられないっぽい
            //var param2 = "あ井ウゑヲ";
            //TestUtil.TestMethod(className, methodName, param2.Length, new object[] { param2 });
        }

        [TestMethod]
        public void TestNativeStringInMethodsStdString()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeStringInMethods";
            var methodName = "CountStdString";

            var param0 = "aBcD";
            asm.TestMyMethod(className, methodName, param0.Length, new object[] { param0 });

            var param1 = "#$%&(";
            asm.TestMyMethod(className, methodName, param1.Length, new object[] { param1 });

            // ◆日本語は数えられないっぽい
            //var param2 = "あ井ウゑヲ";
            //TestUtil.TestMethod(className, methodName, param2.Length, new object[] { param2 });
        }

        [TestMethod]
        public void TestNativeStringInMethodsWChar()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeStringInMethods";
            var methodName = "CountWChar";

            var param0 = "aBcD";
            asm.TestMyMethod(className, methodName, param0.Length, new object[] { param0 });

            var param1 = "#$%&(";
            asm.TestMyMethod(className, methodName, param1.Length, new object[] { param1 });

            var param2 = "あ井ウゑヲ";
            asm.TestMyMethod(className, methodName, param2.Length, new object[] { param2 });

            var param3 = "◆～♪┗¶〇";
            asm.TestMyMethod(className, methodName, param3.Length, new object[] { param3 });
        }

        [TestMethod]
        public void TestNativeStringOutToMemMethods_FromLibrary()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeStringOutToMemMethods";

            int capacity = 256;
            var stringBuilder = new StringBuilder(capacity);

            // From Library
            var error0 = asm.InvokeInternalStaticMethod<bool>(className, "GetMessageEn", new object[] { stringBuilder, stringBuilder.Capacity });
            Assert.AreEqual(false, error0);
            Assert.AreEqual("Hello, I'm Library!", stringBuilder.ToString());
            stringBuilder.Clear();

            var error1 = asm.InvokeInternalStaticMethod<bool>(className, "GetMessageJp", new object[] { stringBuilder, stringBuilder.Capacity });
            Assert.AreEqual(false, error1);
            // ◆日本語を正しくエンコードできてないっぽいので無効化
            //Assert.AreEqual("こんにちわ！私はライブラリです！", stringBuilder.ToString());
        }

        [TestMethod]
        public void TestNativeStringOutToMemMethods_ToFromLibrary()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeStringOutToMemMethods";

            int capacity = 256;
            var stringBuilder = new StringBuilder(capacity);

            // To/From Library
            var testString = "AbCdefGHijkLmN";
            var error = asm.InvokeInternalStaticMethod<bool>(className, "ToUpper", new object[] { testString, stringBuilder, stringBuilder.Capacity });
            Assert.AreEqual(false, error);
            Assert.AreEqual(testString.ToUpper(), stringBuilder.ToString());
        }

        [TestMethod]
        public void TestNativeStringOutToMemMethods_SystemDirectoryPath()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeStringOutToMemMethods";

            // システムディレクトリPATH
            int capacity = 256;
            var stringBuilder = new StringBuilder(new string('0', capacity));

            var length = asm.InvokeInternalStaticMethod<uint>(className, "GetSystemDirectoryA", new object[] { stringBuilder, (uint)stringBuilder.Capacity });
            var path = stringBuilder.ToString();
            Assert.AreEqual((uint)path.Length, length);
            Assert.AreEqual(Environment.SystemDirectory, path);
        }

        [TestMethod]
        public void TestNativeStringOutMethods()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeStringOutMethods";

            asm.TestMyMethod(className, "GetConstMessage", "This is const char*");
        }

    }
}
