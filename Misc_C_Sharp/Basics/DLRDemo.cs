using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp.Basics
{
    public class DLRDemo
    {
        public DLRDemo()
        {
            dynamic dyn;
            dyn = 100;
            Display(dyn);
            dyn = "Hello String World";
            Display(dyn);
            dyn = new PersonDLR { FirstName = "Ali", LastName = "Zaib" };
            Display(dyn);
        }
        public void Display(dynamic d)
        {
            Console.WriteLine(d.GetType());
            Console.WriteLine(d);
        }
    }
    public class PersonDLR
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return $"{FirstName}, {LastName}";
        }
    }
}
