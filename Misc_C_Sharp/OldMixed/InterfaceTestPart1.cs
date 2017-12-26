using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp
{
    public interface ITestA
    {
        void TestOne();
    }

    public class TestA : ITestA
    {
        void ITestA.TestOne()
        {
            Console.WriteLine("Test A");
        }
    }
}
