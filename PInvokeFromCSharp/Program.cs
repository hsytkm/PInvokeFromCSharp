using System;

namespace PInvokeFromCSharp
{
    // 方針 P/Invoke を unsafe で使う
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
                new StringOutWrapper(),
                new StringOutToMemWrapper(),
                new MemFromLibWrapper(),
                new MemToLibWrapper(),
                new ClassDisposeWrapper(),
            };

            foreach (var wrapper in wrappers)
            {
                wrapper.DoTest();

                if (wrapper is IDisposable d)
                    d.Dispose();
            }

            Console.WriteLine("Success!!");
        }
    }
}
