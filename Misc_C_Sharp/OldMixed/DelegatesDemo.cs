using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Misc_C_Sharp
{
    public class DelegatesDemo
    {
        public DelegatesDemo()
        {
            //DoubleOperationsDemo demo1 = new DoubleOperationsDemo();
            //BubbleSorterDemo demo2 = new BubbleSorterDemo(); 
            //AsyncConverterDemo demo3 = new AsyncConverterDemo();
            HandlingExceptionDemo demo4 = new HandlingExceptionDemo();
        }
    }

    
    public class DoubleOperationsDemo
    {
        public DoubleOperationsDemo()
        {
            Func<double,double>[] operations = { MathOperations.MultiplyByTwo, MathOperations.Square };
            for (int i = 0; i < operations.Length; i++)
            {
                Console.WriteLine("Using operations[{0}]", i);
                ProcessAndDisplayNumber(operations[i], 3.8);
                ProcessAndDisplayNumber(operations[i], 7.9);
                Console.WriteLine();
            }
        }

        public void ProcessAndDisplayNumber(Func<double, double> action, double val)
        {
            double result = action(val);
            Console.WriteLine("Value is {0}, result is {1}", val, result);
        }

        private class MathOperations
        {
            public static double MultiplyByTwo(double val) { return val * 2; }
            public static double Square(double val) { return val * val; }
        }
    }
    
    public class BubbleSorterDemo
    {
        public BubbleSorterDemo()
        {
            int[] array = new int[] { 3, 40, 4, 2, 1, 0, 4 };

            Console.WriteLine("Bofore Sorting....");
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }            
            BubbleSorter.Sort(array, (num1, num2) => num1 > num2);
            Console.WriteLine("After Sorting....");
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
        }

        private class BubbleSorter
        {
            public static void Sort<T>(IList<T> list, Func<T, T, bool> compareAction)
            {
                bool swapped = true;
                do
                {
                    swapped = false;
                    for (int i = 0; i < list.Count() - 1; i++)
                    {
                        if (compareAction(list[i], list[i + 1]))
                        {
                            swapped = true;
                            T temp = list[i];
                            list[i] = list[i + 1];
                            list[i + 1] = temp;
                        }
                    }
                } while (swapped);

            }
        }
    }

    public class AsyncConverterDemo
    {
        public AsyncConverterDemo()
        {
            CallOldAsyncPattern();
            Console.ReadLine();
        }
        public static async void CallOldAsyncPattern()
        {
            string result = await Task<string>.Factory.FromAsync<string>(OldAsyncPattern.BeginGreeting,
                OldAsyncPattern.EndGreeting, "Angela", null);
            Console.WriteLine(result);
        }

        private class OldAsyncPattern
        {
            static string Greeting(string name)
            {
                Thread.Sleep(3000);
                return string.Format("Hello, {0}", name);
            }
            private static Func<string, string> greetingsInvoker = Greeting;
            public static IAsyncResult BeginGreeting(string name, AsyncCallback callback, object state)
            {
                return greetingsInvoker.BeginInvoke(name, callback, state);
            }

            public static string EndGreeting(IAsyncResult ar)
            {
                return greetingsInvoker.EndInvoke(ar);
            }
        }
    }

    public class HandlingExceptionDemo
    {
        static async Task ThrowAfter(int millisecond, string message)
        {
            await Task.Delay(millisecond);
            throw new Exception(message);
        }
        public HandlingExceptionDemo()
        {
            //DontHandle();
            //HandleOneError();
            //StartTwoTasks();
            Task t = StartTwoTasksAggregateException();
            t.Wait();
        }

        static void DontHandle()
        {
            try
            {
                ThrowAfter(200, "First Exception Message");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async void HandleOneError()
        {
            try
            {
                await ThrowAfter(200, "HandleOneError Thrown exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async void StartTwoTasks()
        {
            try
            {
                await ThrowAfter(2000, "Frist Task exception");
                await ThrowAfter(1000, "Second Task Exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task StartTwoTasksAggregateException()
        {
            Task TaskResult = null;
            try
            {
                Task t1 = ThrowAfter(2000, "Frist Task exception");
                Task t2 = ThrowAfter(1000, "Second Task Exception");
                await (TaskResult = Task.WhenAll(t1, t2));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var innerException in TaskResult.Exception.InnerExceptions)
                {
                    Console.WriteLine(innerException.Message);
                }
            }
        }
    }
}
