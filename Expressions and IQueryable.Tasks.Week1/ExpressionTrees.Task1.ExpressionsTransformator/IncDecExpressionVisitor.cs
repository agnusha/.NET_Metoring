using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        Dictionary<string, int> pairs;
        bool isReplaced;

        public Expression Modify(Expression expression, Dictionary<string, int> pairs)
        {
            isReplaced = true;
            this.pairs = pairs;
            return base.Visit(expression);
        }

        public Expression Modify(Expression expression)
        {
            return base.Visit(expression);
        }

        //Замена выражений вида <переменная> + 1 / <переменная> - 1 на операции инкремента и декремента
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add && node.Right.NodeType == ExpressionType.Constant && node.Right.ToString() == "1")
                return Expression.Increment(node.Left);
            else if (node.NodeType == ExpressionType.Subtract && node.Right.NodeType == ExpressionType.Constant && node.Right.ToString() == "1")
                return Expression.Decrement(node.Left);

            return base.VisitBinary(node);
        }

        //Замена параметров, входящих в lambda-выражение, на константы
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (isReplaced && pairs.ContainsKey(node.Name))
            {
                return Expression.Constant(pairs[node.Name]);
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (isReplaced)
            {
                return Expression.Lambda(Visit(node.Body), node.Parameters);
            }
            return base.VisitLambda(node);
        }
    }
}
