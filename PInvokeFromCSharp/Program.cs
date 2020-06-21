using System;
using System.Diagnostics;

namespace PInvokeFromCSharp
{
    // 方針 P/Invoke を unsafe で使う
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            if (!DllLocator.AddDllLocationDirectory("bin"))
                Debug.WriteLine($"AddDllLocationDirectory() Failed...");

            var wrapperTypes = new []
            {
                typeof(BuiltInWrapper),
                typeof(BoolWrapper),
                typeof(StringInWrapper),
                typeof(StringOutWrapper),
                typeof(StringOutToMemWrapper),
                typeof(MemFromLibWrapper),
                typeof(MemToLibWrapper),
                typeof(ClassDisposeWrapper),
            };

            foreach (var type in wrapperTypes)
            {
                if (Activator.CreateInstance(type) is INativeWrapper wrapper)
                {
                    wrapper.DoTest();

                    if (wrapper is IDisposable d)
                        d.Dispose();
                }
            }

            Console.WriteLine("Success! (If there are errors, Assertion will be displayed.)");
        }
    }
}
