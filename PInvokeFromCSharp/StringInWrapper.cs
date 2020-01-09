﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    internal static class NativeStringInFunctions
    {
        private const string DllFile = Program.DllFile;

        [DllImport(DllFile, EntryPoint = "StringIn_CountChar", CharSet = CharSet.Unicode)]
        internal extern static int CountChar(
            [MarshalAs(UnmanagedType.LPUTF8Str), In]string s);

        [DllImport(DllFile, EntryPoint = "StringIn_CountStdString", CharSet = CharSet.Unicode)]
        internal extern static int CountStdString(
            [MarshalAs(UnmanagedType.LPUTF8Str), In]string s);

        [DllImport(DllFile, EntryPoint = "StringIn_CountWChar", CharSet = CharSet.Unicode)]
        internal extern static int CountWChar(string s);
    }

    internal class StringInWrapper : INativeWrapper
    {
        public void DoTest()
        {
            var str_en0 = "aBcD";
            var str_en1 = "#$%&(";
            var str_jp0 = "あ井ウゑヲ";
            var str_jp1 = "◆～♪┗¶〇";

            // char*
            {
                var e0 = NativeStringInFunctions.CountChar(str_en0);
                Debug.Assert(e0 == str_en0.Length);

                var e1 = NativeStringInFunctions.CountChar(str_en1);
                Debug.Assert(e1 == str_en1.Length);

                // 日本語は数えられない
                var j0 = NativeStringInFunctions.CountChar(str_jp0);
                //Debug.Assert(j0 == str_jp0.Length);    
            }

            // std::string
            {
                var se0 = NativeStringInFunctions.CountStdString(str_en0);
                Debug.Assert(se0 == str_en0.Length);

                var se1 = NativeStringInFunctions.CountStdString(str_en1);
                Debug.Assert(se1 == str_en1.Length);

                // 日本語は数えられない
                var sj0 = NativeStringInFunctions.CountStdString(str_jp0);
                //Debug.Assert(sj0 == str_jp0.Length);    
            }

            // wchar*
            {
                var we0 = NativeStringInFunctions.CountWChar(str_en0);
                Debug.Assert(we0 == str_en0.Length);

                var we1 = NativeStringInFunctions.CountWChar(str_en1);
                Debug.Assert(we1 == str_en1.Length);

                var wj0 = NativeStringInFunctions.CountWChar(str_jp0);
                Debug.Assert(wj0 == str_jp0.Length);

                var wj1 = NativeStringInFunctions.CountWChar(str_jp1);
                Debug.Assert(wj1 == str_jp1.Length);
            }
        }
    }

}
