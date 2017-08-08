using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Misc_C_Sharp
{
    public class ParallelClassDemo
    {
        public ParallelClassDemo()
        {
            //ParallelForDemo();
            //ParallelForEachDemo();
            ParallelInvoke();
        }

        private void ParallelInvoke()
        {
            Parallel.Invoke(Foo, Bar);
        }
        private void Foo()
        {
            Console.WriteLine("foo");
        }
        private void Bar()
        {
            Console.WriteLine("bar");
        }

        private void ParallelForEachDemo()
        {
            string[] data = {"zero", "one", "two", "three", "four", "five",
                "six", "seven", "eight", "nine", "ten", "eleven", "twelve"};
            Parallel.ForEach(data, (s, pls, i) => {
                Console.WriteLine("{0} {1}", i, s);
                Thread.Sleep(10);
            });
            Console.ReadKey();
        }

        private void ParallelForDemo()
        {
            //// simple parallel for loop
            //ParallelLoopResult result = Parallel.For(0, 10, i => {
            //    Console.WriteLine("{0}: Task: {1}, Thread: {2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            //    Thread.Sleep(10);
            //});
            //Console.WriteLine("IsCompleted: {0}", result.IsCompleted);

            //// async for loop
            //ParallelLoopResult result = Parallel.For(0, 10, async i => {
            //    Console.WriteLine("{0}: Task: {1}, Thread: {2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            //    await Task.Delay(10);
            //    Console.WriteLine("{0}: Task: {1}, Thread: {2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            //});
            //Console.WriteLine("IsCompleted: {0}", result.IsCompleted);

            ////breaking early
            //ParallelLoopResult result = Parallel.For(10, 40, async (i, pls) => {
            //    Console.WriteLine("{0}: Task: {1}, Thread: {2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            //    await Task.Delay(1);
            //    if (i > 15)
            //        pls.Break();
            //});

            //Console.WriteLine("IsCompleted: {0}", result.IsCompleted);
            //Console.WriteLine("Lowest break iteration: {0}", result.LowestBreakIteration);

            //using init and end method
            Parallel.For<string>(0, 10, () =>
            {
                Console.WriteLine("Starting  Thread: {0} Task: {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                return string.Format("t{0}", Thread.CurrentThread.ManagedThreadId);
            },
            (i, pls, str) =>
            {
                Console.WriteLine("body i: {0} str: {1} thread: {2} task: {3}", i, str, Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                Thread.Sleep(10);
                return string.Format("i {0}", i);
            },
            (str) => {
                Console.WriteLine("finally {0}", str);
            });

            Console.ReadKey();
        }
    }
}
