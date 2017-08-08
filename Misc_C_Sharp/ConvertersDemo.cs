using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp
{
    public class ConvertersDemo
    {
        public ConvertersDemo()
        {
            Person p = new PersonBuilder();
            Console.WriteLine(p);
        }
        
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }

    public class PersonBuilder
    {
        public Person Build()
        {
            return new Person { FirstName = "Ali", LastName = "Zaib" };
        }
        public static implicit operator Person(PersonBuilder builder)
        {
            return builder.Build();
        }
    }

}
