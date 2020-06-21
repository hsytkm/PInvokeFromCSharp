using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PInvokeFromCSharp
{
    internal static class NativeStringOutToMemMethods
    {
        private const string DllFile = Program.DllFile;

        // ◆unsafeなしで何とかならないのか…
        [DllImport(DllFile, EntryPoint = "StringOutToMem_GetMessageEn", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool GetMessageEn(
            [MarshalAs(UnmanagedType.LPUTF8Str), Out] StringBuilder str,
            int length);

        // ◆Unicodeを受け取れていない。文字化してる。
        [DllImport(DllFile, EntryPoint = "StringOutToMem_GetMessageJp", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool GetMessageJp(
            [MarshalAs(UnmanagedType.LPUTF8Str), Out] StringBuilder str,
            int length);

        [DllImport(DllFile, EntryPoint = "StringOutToMem_ToUpper", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool ToUpper(
            [MarshalAs(UnmanagedType.LPUTF8Str), In] string inText,
            [MarshalAs(UnmanagedType.LPUTF8Str), Out] StringBuilder outText,
            int outLength);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern uint GetSystemDirectoryA(
                [MarshalAs(UnmanagedType.LPStr)]StringBuilder lpBuffer, uint uSize);

        /// <summary>ライブラリからの文字列取得</summary>
        internal static string GetString1(Func<StringBuilder, int, bool> func, int capacity = 256)
        {
            var buff = new StringBuilder(capacity);

            if (func.Invoke(buff, buff.Capacity))
            {
                throw new ExternalException($"String buffer is short. ({buff})");
            }
            return buff.ToString();
        }

        /// <summary>ライブラリとの文字列入出力</summary>
        internal static string GetString2(Func<string, StringBuilder, int, bool> func, string src, int capacity = 256)
        {
            var buff = new StringBuilder(capacity);

            if (func.Invoke(src, buff, buff.Capacity))
            {
                throw new ExternalException($"String buffer is short. ({buff})");
            }
            return buff.ToString();
        }
    }

    internal class StringOutToMemWrapper : INativeWrapper
    {
        public void DoTest()
        {
            // From Library
            var str_en0 = NativeStringOutToMemMethods.GetString1(NativeStringOutToMemMethods.GetMessageEn);
            var str_jp0 = NativeStringOutToMemMethods.GetString1(NativeStringOutToMemMethods.GetMessageJp);

            // To/From Library
            {
                var lower = str_en0;
                var upper = NativeStringOutToMemMethods.GetString2(NativeStringOutToMemMethods.ToUpper, lower);
                Debug.Assert(upper == lower.ToUpper());
            }

            // システムディレクトリPATH
            var sb = new StringBuilder(new string('0', 256));
            var ret = NativeStringOutToMemMethods.GetSystemDirectoryA(sb, (uint)sb.Capacity);
            var strlib = sb.ToString();
            Debug.Assert(strlib == Environment.SystemDirectory);
        }
    }
}
