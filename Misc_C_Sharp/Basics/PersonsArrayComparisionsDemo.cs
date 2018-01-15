using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp.Basics
{
    public class PersonsArrayComparisionsDemo
    {
        public PersonsArrayComparisionsDemo()
        {
            var janet = new Person { FirstName = "Janet", LastName = "Jackson" };
            Person[] persons1 = {
                    new Person
                    {
                        FirstName = "Michael",
                        LastName = "Jackson"
                    },
                    janet
                };
            Person[] persons2 = {
                    new Person
                    {
                        FirstName = "Michael",
                        LastName = "Jackson"
                    },
                    janet
                };
            if (persons1 != persons2)
                Console.WriteLine("not the same reference");
            if ((persons1 as IStructuralEquatable).Equals(persons2, EqualityComparer<Person>.Default))
                Console.WriteLine("arrays are the same");
        }
    }
    public class Person: IEquatable<Person>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return string.Format($"{FirstName}, {LastName}");
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return base.Equals(obj);
            return Equals(obj as Person);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Person other)
        {
            if (other == null)
                return base.Equals(other);
            return this.Id == other.Id
                && this.FirstName == other.FirstName
                && this.LastName == other.LastName;
        }
    }
}
