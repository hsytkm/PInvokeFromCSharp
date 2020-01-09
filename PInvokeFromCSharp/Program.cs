using System;

namespace PInvokeFromCSharp
{
    class Program
    {
        public const string DllFile = "NativeLibDemo.dll";

        static void Main(string[] args)
        {
            var wrappers = new INativeWrapper[]
            {
                new BuiltInWrapper(),
                new BoolWrapper(),
                new StringInWrapper(),
                new StringOutWrapper(),     // need unsafe
            };

            foreach (var wrapper in wrappers)
            {
                wrapper.DoTest();
            }

            Console.WriteLine("Success!!");
        }
    }
}
