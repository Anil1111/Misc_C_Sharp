using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misc_C_Sharp.TestInterfaces;
using System.Collections.Specialized;

namespace Misc_C_Sharp
{
    public class Program
    {
        enum Styles { Plaid = 0, Striped = 23, Tartan = 65, Corduroy = 78 };
        static void Main(string[] args)
        {
            //DelegatesDemo d = new DelegatesDemo();
            //ParallelClassDemo d = new ParallelClassDemo();
            //TaskDemo d = new TaskDemo();

            //ITestA obj = new TestA();
            //obj.TestB();
            //LINQDemo demo = new LINQDemo();

            //var tartan = "Tartan";
            //int value = (int)Enum.Parse(typeof(Styles), tartan);

            //var formFields = new NameValueCollection();
            //formFields.Add("username", "ali");
            //Console.WriteLine(FileUploadString.UploadFilesToRemoteUrl("http://localhost", new string[] { "D:\\Personal Data\\alizaib.pdf" }, formFields));

            //var convertersDemo = new ConvertersDemo();
            var securityDemo = new SecurityDemo();

        }

        
    }

    
}
