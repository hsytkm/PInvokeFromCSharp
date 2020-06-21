using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DelegateCSharp
{
    // C# - DLLImportを使わずDLLを動的に呼び出す
    //    https://qiita.com/radian-jp/items/2f3bdba833b27c79895c
    // 『P/Invokeと違って実行時にDLLが無くても起動できる』とあるが、
    //  P/Invokeでも実行時にDLLが無くても起動できる ような気がする…
    class Program
    {
        private delegate int MessageBoxDelegate(IntPtr hWnd, string text, string caption, int buttonType);

        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            // 自作DLLの実行
            var ret = new BoolDelegate().GetTrue();

            var invInfo = new PInvokeProcInfo()
            {
                ProcName = "MessageBox",
                EntryPoint = "MessageBoxW",
                ModuleFile = "User32.dll",
                ReturnType = typeof(Int32),
                ParameterTypes = new Type[] { typeof(IntPtr), typeof(string), typeof(string), typeof(Int32) },
                CallingConvention = CallingConvention.StdCall,
                CharSet = CharSet.Unicode
            };

            // Invokeで実行
            MethodInfo method = invInfo.CreateMethodInfo();
            method.Invoke(null, new object[] { IntPtr.Zero, "Run Invoke", "test1", 0 });

            // Delegateで実行
            var messageBox = (MessageBoxDelegate)method.CreateDelegate(typeof(MessageBoxDelegate));
            messageBox(IntPtr.Zero, $"ret = {ret}", "Window title", 0);
        }
    }
}
