/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        private static Random _randNum;
        static void Main(string[] args)
        {
            _randNum = new Random();

            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();


            Task.Run(Task1)
                .ContinueWith(array => Task2(array.Result))
                .ContinueWith(array => Task3(array.Result))
                .ContinueWith(array => Task4(array.Result));

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static void PrintRes(int[] array, int task)
        {
            Console.Write("Result: ");
            foreach (int value in array)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine($"Task {task} finished.");
        }

        private static int[] Task1()
        {
            int[] array = new int[10];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = _randNum.Next(1, 10);
                Console.WriteLine($"Task 1: Created value in array #{i} – {array[i]}");
            }
            PrintRes(array, 1);
            return array;
        }

        private static int[] Task2(int[] array)
        {
            int numberRandom = _randNum.Next(1, 10);
            Console.WriteLine($"Random number: {numberRandom}.");

            for (int i = 0; i < array.Length; i++)
            {
                var before = array[i];
                array[i] *= numberRandom;
                Console.WriteLine($"Task 2: multiplies value in array #{i} – {before} – result {array[i]}");
            }
            PrintRes(array, 2);
            return array;
        }

        private static int[] Task3(int[] array)
        {
            Array.Sort<int>(array, new Comparison<int>(
                  (i1, i2) =>
                  {
                      Console.WriteLine($"Task 3: sorts value in array compare {i1} – {i2}");
                      return i1.CompareTo(i2);
                  }));
            PrintRes(array, 3);
            return array;
        }

        private static int[] Task4(int[] array)
        {
            int result = 0;
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i];
                Console.WriteLine($"Task 4: Find average in array #{i} – {array[i]}");
            }
            Console.WriteLine($"Task 4. Average  {result / array.Length}");
            PrintRes(array, 4);
            return array;
        }
    }
}
