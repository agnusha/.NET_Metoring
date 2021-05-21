/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    static class Program
    {
        private static List<int> _listValues;

        static void Main(string[] args)
        {
            _listValues = new List<int>() { 1, 2, 3, 4, 5 };

            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            Console.WriteLine("Join");
            var tasks = new List<Thread>() {new Thread(Add), new Thread(PrintRes)};
            tasks.ForEach(t =>
            {
                t.Start();
                t.Join();
            });

            Console.WriteLine();

            Console.WriteLine("Lock");
            var tasksL = new List<Thread>() { new Thread(() => LockMethod(Add)), new Thread(() => LockMethod(PrintRes)) };
            tasksL.ForEach(t => t.Start());

            Console.ReadLine();
        }

        private static void LockMethod(Action method)
        {
            lock (_listValues)
            {
                method();
            }
        }

        private static void Add()
        {
            foreach (var i in Enumerable.Range(1, 10))
            {
                _listValues.Add(i);
            }
        }
        
        private static void PrintRes()
        {
            Console.Write("Result: ");
            foreach (var value in _listValues)
            {
                Console.Write(value + " ");
            }
        }
    }
}
