using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    static class DllLocator
    {
        internal const string DllFileName = "NativeLibDemo.dll";

        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        static extern bool SetDllDirectory(string lpPathName);

        /// <summary>
        /// dllの探索PATHを追加する
        /// </summary>
        /// <param name="directoryName">実行.exeディレクトリ内のディレクトリ名</param>
        /// <returns>実行結果(true=Success)</returns>
        public static bool AddDllLocationDirectory(string directoryName)
        {
            // https://blog.shibayan.jp/entry/20190909/1568007641
            var exePath = Process.GetCurrentProcess().MainModule.FileName;
            var exeDir = Path.GetDirectoryName(exePath);
            if (string.IsNullOrEmpty(exeDir)) throw new DirectoryNotFoundException(exeDir);

            var dllDirectoryPath = Path.Combine(exeDir, directoryName);
            if (Directory.Exists(dllDirectoryPath))
            {
                SetDllDirectory(dllDirectoryPath);
                return true;
            }
            return false;
        }
    }
}
