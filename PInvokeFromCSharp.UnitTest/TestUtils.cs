using System;
using System.Reflection;

namespace PInvokeFromCSharp.UnitTest
{
    static class TestUtils
    {
        //公開クラスからアセンブリを特定  
        private static readonly Assembly _targetAssembly = Assembly.GetAssembly(typeof(PInvokeFromCSharp.DllLocator));

        public static MyAssembly MyAssembly { get; } = new MyAssembly(_targetAssembly);
    }
}
