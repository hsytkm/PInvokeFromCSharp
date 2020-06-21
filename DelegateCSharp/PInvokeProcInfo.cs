using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace DelegateCSharp
{
    /// <summary>
    /// PInvoke関数情報
    /// </summary>
    public class PInvokeProcInfo
    {
        /// <summary>
        /// 関数名
        /// </summary>
        public string ProcName { get; set; }

        /// <summary>
        /// DLLファイル
        /// </summary>
        public string ModuleFile { get; set; }

        /// <summary>
        /// エントリポイント
        /// </summary>
        public string EntryPoint { get; set; }

        /// <summary>
        /// 戻り値の型（戻り値無しはSystem.Void）
        /// </summary>
        public Type ReturnType { get; set; } = typeof(void);

        /// <summary>
        /// 関数のパラメータの型
        /// </summary>
        public Type[] ParameterTypes { get; set; }

        /// <summary>
        /// 呼び出し規約
        /// </summary>
        public CallingConvention CallingConvention { get; set; } = CallingConvention.StdCall;

        /// <summary>
        /// メソッドのキャラクターセット
        /// </summary>
        public CharSet CharSet { get; set; } = CharSet.Auto;

        /// <summary>
        /// PInvoke関数情報から、メソッドのメタデータを作成する。
        /// </summary>
        /// <param name="invInfo">PInvoke関数情報</param>
        /// <returns>PInvoke関数メタデータ</returns>
        public MethodInfo CreateMethodInfo()
        {
            string moduleName = Path.GetFileNameWithoutExtension(this.ModuleFile).ToUpper();

            var asmBld = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName("Asm" + moduleName), AssemblyBuilderAccess.Run);

            var modBld = asmBld.DefineDynamicModule(
                "Mod" + moduleName);

            var typBld = modBld.DefineType(
                "Class" + moduleName,
                TypeAttributes.Public | TypeAttributes.Class);

            var methodBuilder = typBld.DefinePInvokeMethod(
                this.ProcName,
                this.ModuleFile,
                this.EntryPoint,
                MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl | MethodAttributes.HideBySig,
                CallingConventions.Standard,
                this.ReturnType,
                this.ParameterTypes.ToArray(),
                this.CallingConvention,
                this.CharSet);

            methodBuilder.SetImplementationFlags(MethodImplAttributes.PreserveSig);

            return typBld.CreateType().GetMethod(this.ProcName);
        }

    }
}
