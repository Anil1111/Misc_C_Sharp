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
            A a = new B { Name = "name of B as A" };
            B b = new A { Name = "name of A as B" };
            Console.WriteLine(a.Name);
            Console.WriteLine(b.Name);
            //Foo(a, b);
        }

        public void Foo<T, U>(T tObj, U uObj) where T : U
        {
            Console.WriteLine("I'm happy");
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

    public class A
    {
        public string Name { get; set; }
    }
    public class B
    {
        public string Name { get; set; }
        public static implicit operator A(B obj)
        {
            return new A { Name = obj.Name };
        }
        public static implicit operator B(A obj)
        {
            return new B { Name = obj.Name };
        }
    }

}
