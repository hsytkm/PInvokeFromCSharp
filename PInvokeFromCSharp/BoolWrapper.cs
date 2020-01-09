﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp
{
    internal static class NativeBoolInFunctions
    {
        private const string DllFile = Program.DllFile;

        [DllImport(DllFile, EntryPoint = "Bool_GetTrue")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool GetTrue();

        [DllImport(DllFile, EntryPoint = "Bool_Not")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool Not(bool b);

        [DllImport(DllFile, EntryPoint = "Bool_And")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool And(
            [MarshalAs(UnmanagedType.Bool)]bool b0,
            [MarshalAs(UnmanagedType.Bool)]bool b1);

    }

    internal class BoolWrapper : INativeWrapper
    {
        public void DoTest()
        {
            // Out
            {
                var b1 = NativeBoolInFunctions.GetTrue();
                Debug.Assert(b1 == true);

                // Not
                var b2 = NativeBoolInFunctions.Not(true);
                Debug.Assert(b2 == false);

                var b3 = NativeBoolInFunctions.Not(false);
                Debug.Assert(b3 == true);
            }

            // In/Out
            {
                // And
                var b10 = NativeBoolInFunctions.And(true, true);
                Debug.Assert(b10 == true);

                var b11 = NativeBoolInFunctions.And(true, false);
                Debug.Assert(b11 == false);

                var b12 = NativeBoolInFunctions.And(false, false);
                Debug.Assert(b12 == false);
            }


        }
    }

}
