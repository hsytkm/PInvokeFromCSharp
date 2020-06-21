using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DelegateCSharp
{
    class BoolDelegate
    {
        private const string DllFileName = "NativeLibDemo.dll";

        public delegate bool GetTrueDelegate();

        // staticに押し込んだ版
        //public static GetTrueDelegate GetTrue { get; } =
        //    (GetTrueDelegate)new PInvokeProcInfo()
        //    {
        //        ProcName = "Bool_GetTrue",
        //        EntryPoint = "Bool_GetTrue",
        //        ModuleFile = DllFileName,
        //        ReturnType = typeof(bool),
        //        ParameterTypes = Array.Empty<Type>(),
        //        CallingConvention = CallingConvention.StdCall,
        //        CharSet = CharSet.Unicode
        //    }
        //    .CreateMethodInfo()
        //    .CreateDelegate(typeof(GetTrueDelegate));

        public bool GetTrue()
        {
            var procInfo = new PInvokeProcInfo()
            {
                ProcName = "Bool_GetTrue",
                EntryPoint = "Bool_GetTrue",
                ModuleFile = DllFileName,
                ReturnType = typeof(bool),
                ParameterTypes = Array.Empty<Type>(),
                CallingConvention = CallingConvention.StdCall,
                CharSet = CharSet.Unicode
            };
            var methodInfo = procInfo.CreateMethodInfo();
            var getTrueDelegate = (GetTrueDelegate)methodInfo.CreateDelegate(typeof(GetTrueDelegate));

            try
            {
                return getTrueDelegate();
            }
            catch (DllNotFoundException)
            {
                // DLLが存在しなければ実行時例外になる。
                // DllImport(P/Invoke)でも同様に実行時例外になると思っており、Qiita記事と合わない…なぜ？
                throw;
            }
        }
    }
}
