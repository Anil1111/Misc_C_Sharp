using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp
{
    namespace TestInterfaces
    {
        public static class InterfaceExtensions
        {
            public static void TestB(this ITestA ItestA)
            {
                Console.WriteLine("Test B");
            }
        }
    }
}
