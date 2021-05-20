/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private const int TaskAmount = 10;

        private static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            Console.WriteLine("- a) Option");

            var state = 100;
            for (var taskNumber = 0; taskNumber < TaskAmount; taskNumber++)
            {
                var thread = new Thread(() => Output(taskNumber, ref state));
                thread.Start();
                thread.Join();
            }
            
            Console.WriteLine("- b) Option");


            Console.ReadLine();
        }

        private static void Output(int taskNumber, ref int state)
        {
            Console.WriteLine($"Task #{taskNumber} – {state--}");
        }
    }
}
