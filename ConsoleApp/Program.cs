using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static string ThreadId => $"(ThreadId: {Thread.CurrentThread.ManagedThreadId})";

        static List<string> data = new List<string>() { "first", "second", "third" };


        static void Main(string[] args)
        {
            #region UI

            SyncWork();

            //AsyncWorkWithThreads();

            //AsyncWorkWithThreadPool(); 

            #endregion

            #region Processing multiple work items

            //ProcessDataSync(data);

            //ProcessDataAsync(data);

            #endregion

            #region CPU intensive work
            //ProcessIntData();

            //ProcessIntDataParallel();
            #endregion

            #region Tasks

            // AsyncWorkWithTasks()

            #endregion

            Console.ReadLine();
        }


        static void SyncWork()
        {
            DoSomeWork();

            DoMoreWork();
        }

        static void DoSomeWork()
        {
            Console.WriteLine($"Doing some work {ThreadId}");
            Thread.Sleep(5000);
            Console.WriteLine($"Still doing some work {ThreadId}");
            Thread.Sleep(5000);
            Console.WriteLine($"Work is done! {ThreadId}");
        }

        static void DoMoreWork()
        {
            Console.Write($"Waiting for input {ThreadId}: ");
            string input = Console.ReadLine();
            Console.WriteLine($"Here is your input in upper case: {input.ToUpper()} {ThreadId}");
        }


        static void AsyncWorkWithThreads()
        {
            Thread t = new Thread(DoSomeWork);
            t.Start();

            Thread.Sleep(500);
            DoMoreWork();
        }

        static void AsyncWorkWithThreadPool()
        {
            ThreadPool.QueueUserWorkItem(o => DoSomeWork());

            Thread.Sleep(500);
            DoMoreWork();
        }



        static void ProcessDataSync(List<string> data)
        {
            foreach (string item in data)
            {
                Process(item);
            }
        }

        static void Process(object s)
        {
            Console.WriteLine($"Processing of {s} has started {ThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"Processing of {s} has finished {ThreadId}");
        }


        static void ProcessDataAsync(List<string> data)
        {
            foreach (string item in data)
            {
                Thread t = new Thread(Process);
                t.Start(item);
            }

            //Parallel.ForEach(data, x => Process(x));
        }



        static void ProcessIntData()
        {
            // Get a very large array of integers.
            int[] source = Enumerable.Range(1, 100_000_000).ToArray();

            // Find the numbers where num % 3 == 0 is true, returned in descending order.
            int[] modThreeIsZero = source.Where(x => x % 3 == 0).OrderByDescending(x => x).ToArray();

            Console.WriteLine($"Found { modThreeIsZero.Count()} numbers that match query!");
        }

        static void ProcessIntDataParallel()
        {
            // Get a very large array of integers.
            int[] source = Enumerable.Range(1, 100_000_000).ToArray();

            // Find the numbers where num % 3 == 0 is true, returned in descending order.
            int[] modThreeIsZero = source.AsParallel().Where(x => x % 3 == 0).OrderByDescending(x => x).ToArray();

            Console.WriteLine($"Found { modThreeIsZero.Count()} numbers that match query!");
        }



        static void AsyncWorkWithTasks()
        {
            Task t = Task.Factory.StartNew(() => DoSomeWork());

            Thread.Sleep(500);
            DoMoreWork();
        }
    }
}
