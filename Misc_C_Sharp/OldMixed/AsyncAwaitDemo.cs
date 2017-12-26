using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Misc_C_Sharp
{
    public class AsyncAwaitDemo
    {
        public AsyncAwaitDemo()
        {
            //Console.WriteLine(Greeting("Ali"));
            Task<string> t1 = GreetingAsync("Zaib");
            t1.ContinueWith(t => 
            {
                Console.WriteLine(t.Result);
            });
            Console.WriteLine("Press Any key to end");
            Console.ReadKey();
        }

        public static Task<string> GreetingAsync(string name)
        {
            return Task<string>.Run(() => { return Greeting(name); });

        }
        public static string Greeting(string name)
        {
            Thread.Sleep(6000);
            return string.Format($"Hello {name}");
        }
    }
    
}
