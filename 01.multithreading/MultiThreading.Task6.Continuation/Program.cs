/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        private static void Main(string[] args)
        {
            var number = 1;

            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");

            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Task.Run(() => OutputException(ref number)).ContinueWith((result) => Output(ref number)).Wait();

            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Task.Run(() => OutputException(ref number)).ContinueWith((result) => Output(ref number), TaskContinuationOptions.OnlyOnFaulted).Wait();

            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            Task.Run(() => OutputException(ref number)).ContinueWith((result) =>
            {
                if (result.IsFaulted)
                {
                    Output(ref number);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext()).Wait();

            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            var token = new CancellationTokenSource();
            Task.Run(() => OutputCancel(ref number, token), token.Token).ContinueWith((result) =>
            {
                if (token.IsCancellationRequested)
                {
                    Output(ref number);
                }
            }, TaskContinuationOptions.LongRunning).Wait();

            Console.WriteLine("Demonstrate the work of the each case with console utility.");

            Console.ReadLine();
        }
        private static void OutputException(ref int taskNumber)
        {
            Console.WriteLine($"Task id #{Task.CurrentId} number #{taskNumber} will end with exception");
            taskNumber++;
            throw new Exception("Error");
        }

        private static void Output(ref int taskNumber)
        {
            Console.WriteLine($"Task id #{Task.CurrentId} number #{taskNumber} executed correctly");
            taskNumber++;
        }

        private static void OutputCancel(ref int taskNumber, CancellationTokenSource ct)
        {
            Console.WriteLine($"Task id #{Task.CurrentId} number #{taskNumber} will be canceled");
            ct.Cancel();
        }
    }
}
