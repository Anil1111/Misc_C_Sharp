using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misc_C_Sharp.TestInterfaces;
using System.Collections.Specialized;
using Misc_C_Sharp.Basics;

namespace Misc_C_Sharp
{
    public class Program
    {        
        static void Main(string[] args)
        {
            //OldDemo();
            //CheckedDemo();
            //UnSafeDemo();
            //int a = -1;
            //uint b = (uint)a;
            //Console.WriteLine(b);
            //var demo = new PersonsArrayComparisionsDemo();
            var demo = new LINQQueries();
        }

        unsafe static void UnSafeDemo()
        {
            byte aByte = 8;
            byte* pByte = &aByte;
            double* pDouble = (double*)pByte;
            Console.WriteLine(*pDouble);
            Console.WriteLine(*pByte);
        }

        private static void CheckedDemo()
        {
            try
            {
                int ten = 10;
                int i2 = checked(2147483647 + ten);
                Console.WriteLine(i2);
            }
            catch (System.OverflowException e)
            {
                Console.WriteLine($"Checked and Caught {e.ToString()}");

            }
        }

        static void OldDemo()
        {
            //DelegatesDemo d = new DelegatesDemo();
            //ParallelClassDemo d = new ParallelClassDemo();
            //TaskDemo d = new TaskDemo();

            //ITestA obj = new TestA();
            //obj.TestB();
            LINQDemo demo = new LINQDemo();

            //var tartan = "Tartan";
            //int value = (int)Enum.Parse(typeof(Styles), tartan);

            //var formFields = new NameValueCollection();
            //formFields.Add("username", "ali");
            //Console.WriteLine(FileUploadString.UploadFilesToRemoteUrl("http://localhost", new string[] { "D:\\Personal Data\\alizaib.pdf" }, formFields));

            //var convertersDemo = new ConvertersDemo();
            //var securityDemo = new SecurityDemo();
            //var asyncAwaitDemo = new AsyncAwaitDemo();
            //{
            //    var dDemo = new DestructorDemo();
            //}
            //Console.WriteLine("Out of scope, By now destructor should have been called");
            //Console.WriteLine("It is not been called yet, and it wouldn't untill the next key press get executed");
            //Console.ReadKey();
            //using (var ido = new IDisposableDemo())
            //{

            //}
        }
        
    }

    
}
