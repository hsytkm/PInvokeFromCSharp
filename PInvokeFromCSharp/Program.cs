using System;

namespace PInvokeFromCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var wrappers = new[]
            {
                new BuiltInWrapper(),
            };

            foreach (var wrapper in wrappers)
            {
                wrapper.DoTest();
            }

            Console.WriteLine("Finish!!!");
        }
    }
}
