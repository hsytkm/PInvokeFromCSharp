using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace PInvokeFromCSharp.UnitTest
{
    class MyAssembly
    {
        public Assembly TargetAssembly { get; }

        public string AssemblyName { get; }

        public MyAssembly(Assembly assembly)
        {
            TargetAssembly = assembly;
            AssemblyName = assembly.FullName.Split(',').First();
        }

        public Type GetType(string className)
        {
            return TargetAssembly.GetType(className);
        }

        public MethodInfo GetInternalStaticMethodInfo(string className, string methodName)
        {
            //クラス型を取得  
            var type = GetType(className);
            return type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        }

        public T InvokeInternalStaticMethod<T>(string className, string methodName, object[] parameters = null)
        {
            var method = GetInternalStaticMethodInfo(className, methodName);
            return (T)method.Invoke(null, parameters);
        }

        public void TestMyMethod<T>(string className, string methodName, T expected, object[] parameters = null)
        {
            var actual = InvokeInternalStaticMethod<T>(className, methodName, parameters);
            Assert.AreEqual(expected, actual);
        }
    }
}
