using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp
{
    public class DestructorDemo
    {
        ~DestructorDemo()
        {
            Console.WriteLine("Destructor called");
        }
    }
}
