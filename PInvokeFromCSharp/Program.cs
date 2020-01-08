using System;

namespace PInvokeFromCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var wrappers = new INativeWrapper[]
            {
                new BuiltInWrapper(),
                new BoolWrapper(),

            };

            foreach (var wrapper in wrappers)
            {
                wrapper.DoTest();
            }

            Console.WriteLine("Success!!");
        }
    }
}
