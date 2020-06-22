using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;

namespace PInvokeFromCSharp.UnitTest
{
    [TestClass]
    public class UnitTestMemory
    {
        [TestMethod]
        public void TestNativeMemFromLibMethods()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeMemFromLibMethods";

            // �e���\�b�h�ĂԂƁAbyte ��Sum�� 123 �ɂȂ鋐�僁�������Ԃ��Ă���

            // �l�߂�
            var data0 = asm.InvokeInternalStaticMethod<object>(className, "GetBufferDataValue");
            var method0 = data0.GetType().GetMethod("GetByteSum");
            var sum0 = method0.Invoke(data0, null);
            Assert.AreEqual(123, sum0);

            // �Q�Ɩ߂�F data1�̌^(var)�� ref �t���ĂȂ����R����B�l�߂��̕����ǂ�����
            var data1 = asm.InvokeInternalStaticMethod<object>(className, "GetBufferDataRef");
            var method1 = data1.GetType().GetMethod("GetByteSum");
            var sum1 = method1.Invoke(data1, null);
            Assert.AreEqual(123, sum1);
        }

        [TestMethod]
        public void TestNativeMemToLibMethods()
        {
            var asm = TestUtils.MyAssembly;
            var className = $"{asm.AssemblyName}.NativeMemToLibMethods";

            // ��ref �Q�ƂƂ�����₱���߂��ė͐s�����B�����ł��񂩂����c

#if false
            var type = asm.GetType("MemoryDataToLibContainer");
            object container = Activator.CreateInstance(type, new object[] { 10_000_000 });

            //using var container = new MemoryDataToLibContainer(10_000_000);
            var payload = container.Payload;

            // �擪�����l�������Ă���
            Marshal.WriteByte(payload.Ptr, 23);

            // Lib�Ƀ��������̍��v�l���v�Z������
            var sum0 = asm.InvokeInternalStaticMethod<int>(className, "GetBufferDataSum", new object[] { payload });
            //int sum0 = NativeMemToLibMethods.GetBufferDataSum(ref payload);
            //Assert.AreEqual(23, sum0);


            // �������̖߂�l�� false �̂͂��Ȃ̂ɁA��� true ���Ԃ��Ă���B
            //   ������������Ȃ������c int �Ȃ�Ӑ}�ʂ�̒l���Ԃ��Ă���B
            var err = asm.InvokeInternalStaticMethod<int>(className, "SetBufferLast", new object[] { payload, 100 });
            //bool err = NativeMemToLibMethods.SetBufferLast(ref payload, 100);
            //Debug.Assert(err == false);   // ���Ӑ}�ʂ�ɓ����Ȃ��̂Ń`�F�b�N���Ȃ�

            var sum1 = payload.GetByteSum();
            Assert.AreEqual(123, sum1);
#endif

        }
    }
}