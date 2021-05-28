using System;
using System.Threading;

namespace AsyncAwait.Task1.CancellationTokens
{
    static class Calculator
    {
        public static long Calculate(int n, CancellationToken token)
        {
            long sum = 0;

            for (int i = 0; i < n; i++)
            {
                token.ThrowIfCancellationRequested();
                //if (token.IsCancellationRequested)
                //{
                //    Console.WriteLine("Cancellation requested in token");
                //    token.ThrowIfCancellationRequested();
                //}
                //else 
                    sum += (i + 1);
                Thread.Sleep(50);
            }

            return sum;
        }
    }
}
