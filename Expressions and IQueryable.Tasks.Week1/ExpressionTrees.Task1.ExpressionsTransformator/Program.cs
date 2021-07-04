/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression Visitor for increment/decrement.");
            Console.WriteLine();

            var visitor = new IncDecExpressionVisitor();

            Expression<Func<int, int>> expressionInc = variable => variable + 1;
            Expression<Func<int, int>> expressionDec = variable => variable - 1;

            Visit(visitor, expressionInc);
            Visit(visitor, expressionDec);

            Console.WriteLine("Expression Visitor for change params.");
            Expression<Func<int, int, int>> changedExpression = (v1, v2) => v1 + v2 - 100;
            VisitTask2(visitor, changedExpression);

            Console.ReadLine();
        }

        private static void Visit(IncDecExpressionVisitor visitor, Expression<Func<Int32, Int32>> expression)
        {
            Console.WriteLine(expression);
            var res = visitor.Modify(expression);
            PrintResult(res);
        }

        private static void VisitTask2(IncDecExpressionVisitor visitor, Expression<Func<int, int, int>> expression)
        {
            Console.WriteLine(expression);
            var pairs = new Dictionary<string, int>() { { "v1", 100 }, { "v2", 200 } };

            var res = visitor.Modify(expression, pairs);
            PrintResult(res);
        }

        private static void PrintResult(Expression res)
        {
            Console.WriteLine("Result");
            Console.WriteLine(res);
            Console.WriteLine();
        }
    }
}
