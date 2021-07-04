using System;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        public Expression Modify(Expression expression)
        {
            return base.Visit(expression);
        }

        //Замена выражений вида <переменная> + 1 / <переменная> - 1 на операции инкремента и декремента
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Console.WriteLine(node);
            if (node.NodeType == ExpressionType.Add && node.Right.NodeType == ExpressionType.Constant && node.Right.ToString() == "1")
                return Expression.Increment(node.Left);
            else if (node.NodeType == ExpressionType.Subtract && node.Right.NodeType == ExpressionType.Constant && node.Right.ToString() == "1")
                return Expression.Decrement(node.Left);

            return base.Visit(node);
        }
    }
}
