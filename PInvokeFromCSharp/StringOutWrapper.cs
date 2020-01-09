using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PInvokeFromCSharp
{
    internal static class NativeStringOutFunctions
    {
        private const string DllFile = Program.DllFile;

        // ◆unsafeなしで何とかならないのか…
        [DllImport(DllFile, EntryPoint = "StringOut_GetMessageEn", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern unsafe static bool GetMessageEn(
            [MarshalAs(UnmanagedType.LPUTF8Str), Out] StringBuilder str,
            int length);

        [DllImport(DllFile, EntryPoint = "StringOut_GetMessageJp", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern unsafe static bool GetMessageJp(
            [MarshalAs(UnmanagedType.LPUTF8Str), Out] StringBuilder str,
            int length);

        [DllImport(DllFile, EntryPoint = "StringOut_ToUpper", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern unsafe static bool ToUpper(
            [MarshalAs(UnmanagedType.LPUTF8Str), In] string inText,
            [MarshalAs(UnmanagedType.LPUTF8Str), Out] StringBuilder outText,
            int outLength);


        /// <summary>ライブラリからの文字列取得</summary>
        internal static string GetString1(Func<StringBuilder, int, bool> func, int capacity = 256)
        {
            var buff = new StringBuilder(capacity);

            if (func.Invoke(buff, buff.Capacity))
            {
                throw new ExternalException($"String buffer is short. ({buff.ToString()})");
            }
            return buff.ToString();
        }


        /// <summary>ライブラリとの文字列入出力</summary>
        internal static string GetString2(Func<string, StringBuilder, int, bool> func, string src, int capacity = 256)
        {
            var buff = new StringBuilder(capacity);

            if (func.Invoke(src, buff, buff.Capacity))
            {
                throw new ExternalException($"String buffer is short. ({buff.ToString()})");
            }
            return buff.ToString();
        }
    }

    internal class StringOutWrapper : INativeWrapper
    {
        public void DoTest()
        {
            // From Library
            var str_en0 = NativeStringOutFunctions.GetString1(NativeStringOutFunctions.GetMessageEn);
            var str_jp0 = NativeStringOutFunctions.GetString1(NativeStringOutFunctions.GetMessageJp);

            // To/From Library
            {
                var lower = str_en0;
                var upper = NativeStringOutFunctions.GetString2(NativeStringOutFunctions.ToUpper, lower);
                Debug.Assert(upper == lower.ToUpper());
            }
        }
    }

}
