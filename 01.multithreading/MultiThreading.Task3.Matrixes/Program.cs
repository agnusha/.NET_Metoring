/*
 * 3. Write a program, which multiplies two matrices and uses class Parallel.
 * a. Implement logic of MatricesMultiplierParallel.cs
 *    Make sure that all the tests within MultiThreading.Task3.MatrixMultiplier.Tests.csproj run successfully.
 * b. Create a test inside MultiThreading.Task3.MatrixMultiplier.Tests.csproj to check which multiplier runs faster.
 *    Find out the size which makes parallel multiplication more effective than the regular one.
 */

using System;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier
{
    public static class Program
    {
        private static Random _randNum;

        static void Main(string[] args)
        {
            Console.WriteLine("3.	Write a program, which multiplies two matrices and uses class Parallel. ");
            Console.WriteLine();

            const byte matrixSize = 7; // todo: use any number you like or enter from console
            CreateAndProcessMatrices(matrixSize);
            Console.ReadLine();
        }

        private static void CreateAndProcessMatrices(byte sizeOfMatrix)
        {
            Console.WriteLine("Multiplying...");
            _randNum = new Random();
            var firstMatrix = CreateRandomMatrix(sizeOfMatrix, sizeOfMatrix, _randNum);
            var secondMatrix = CreateRandomMatrix(sizeOfMatrix, sizeOfMatrix, _randNum);

            IMatrix resultMatrix = new MatricesMultiplierParalel().Multiply(firstMatrix, secondMatrix);

            Console.WriteLine("firstMatrix:");
            firstMatrix.Print();
            Console.WriteLine("secondMatrix:");
            secondMatrix.Print();
            Console.WriteLine("resultMatrix:");
            resultMatrix.Print();
        }

        public static Matrix CreateRandomMatrix(int row, int col, Random _randNum)
        {

            var m1 = new Matrix(row, col);

            for (long i = 0; i < m1.RowCount; i++)
            {
                for (byte j = 0; j < m1.ColCount; j++)
                {
                    m1.SetElement(i, j, _randNum.Next(0, 10));
                }
            }
            return m1;
        }
    }
}
