using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Misc_C_Sharp
{
    public class TaskDemo
    {
        private object testMethodLock = new object();

        public TaskDemo()
        {
            //TaskCreationDemo();
            //CancelParallelForDemo();
            CancelTaskDemo();
            Console.ReadKey();
        }

        private void CancelTaskDemo()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("*** Task was cancelled"));
            cts.CancelAfter(500);
            Task t1 = Task.Run(() => {
                Console.WriteLine("In Task");                
                for(int i=0; i<20; i++)
                {
                    Thread.Sleep(100);
                    var token = cts.Token;
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation was requested within the task");
                        token.ThrowIfCancellationRequested();
                        break;
                    }
                    Console.WriteLine("In loop...");
                }
                Console.WriteLine("loop completed without cancellation");
            }, cts.Token);

            try
            {
                t1.Wait();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("Exception: {0}, {1}", ae.GetType().Name, ae.Message);
                foreach (var innerException in ae.InnerExceptions)
                {
                    Console.WriteLine("**** Exception: {0}, {1}", innerException.GetType().Name, innerException.Message);
                }
            }
        }

        private void CancelParallelForDemo()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => { Console.WriteLine("*** Token Cancelled"); });
            cts.CancelAfter(500);
            try
            {
                Parallel.For(0, 100, new ParallelOptions() { CancellationToken = cts.Token }, 
                    i => {
                        Console.WriteLine("loop {0} started", i);
                        int sum = 0;
                        for (int x = 0; x < 100; x++)
                        {
                            Thread.Sleep(2);
                            sum += x;
                        }
                        Console.WriteLine("loop {0} finished", i);
                    });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void TaskCreationDemo()
        {
            //// Differen Task Creation Method
            //var tf = new TaskFactory();
            //tf.StartNew(TaskMethod, "from taskfactory start new");
            //Task.Factory.StartNew(TaskMethod, "from Task.Factory.StartNew");
            //var task = new Task(TaskMethod, "from Task Constructor");
            //task.Start();
            //Task.Run(() => TaskMethod("from Task.Run"));

            //// Synchronous Task
            //TaskMethod("Main Thread");
            //var task = new Task(TaskMethod, "Run Syn");
            //task.RunSynchronously();

            //// Task Created with TaskCreationOptions.LongRunning, a thread from the thread pool is not used.
            //// When a thread is take from thread pool, task scheduler can decide to wait for an already running task to finish 
            //// an use that thread instead of creating new one. This is not the case when LongRunningOption is used.
            //var task = new Task(TaskMethod, "long running", TaskCreationOptions.LongRunning);
            //task.Start();

            // Task with Result
            var task = new Task<Tuple<int, int>>(TaskWithResult, Tuple.Create<int, int>(8, 3));
            task.Start();
            Console.WriteLine(task.Result);
            Console.WriteLine("Result from task: {0} {1}", task.Result.Item1, task.Result.Item2);
        }

        private void TaskMethod(object title)
        {
            lock (testMethodLock)
            {
                Console.WriteLine(title);
                Console.WriteLine("Task: {0} Thread: {1}", 
                    Task.CurrentId == null ? "No Task": Task.CurrentId.ToString(), Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("IsPooled Thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
                Console.WriteLine("IsBackground Thread: {0}", Thread.CurrentThread.IsBackground);
                Console.WriteLine();
            }
        }
        private Tuple<int, int> TaskWithResult(object division)
        {
            var div = (Tuple<int, int>)division;
            int result = div.Item1 / div.Item2;
            int remainder = div.Item1 % div.Item2;
            Console.WriteLine("Task creates result...");
            //return new Tuple<int, int>(result, remainder);
            return Tuple.Create<int, int>(result, remainder);
        }
    }
}
