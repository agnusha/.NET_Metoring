using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }

            if (node.Method.DeclaringType == typeof(String))
            {
                switch (node.Method.Name)
                {
                    case "Equals":
                        VisitTwoNodes(node.Object, node.Arguments[0]);
                        return node;

                    case "StartsWith":
                        VisitTwoNodes(node.Object, node.Arguments[0], false, true);
                        return node;

                    case "EndsWith":
                        VisitTwoNodes(node.Object, node.Arguments[0], true);
                        return node;

                    case "Contains":
                        VisitTwoNodes(node.Object, node.Arguments[0], true, true);
                        return node;
                }

            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {

            switch (node.NodeType)
            {
                case ExpressionType.Equal:

                    if (node.Left.NodeType == ExpressionType.MemberAccess && (node.Right.NodeType == ExpressionType.Constant))
                        VisitTwoNodes(node.Left, node.Right);

                    else if (node.Left.NodeType == ExpressionType.Constant && (node.Right.NodeType == ExpressionType.MemberAccess))
                        VisitTwoNodes(node.Right, node.Left);

                    break;
                case ExpressionType.AndAlso:
                    _resultStringBuilder.Append("'statements': [");
                    _resultStringBuilder.Append("{ 'query':'");
                    Visit(node.Left);
                    _resultStringBuilder.Append("'}, { 'query':'");
                    Visit(node.Right);
                    _resultStringBuilder.Append("'}]");
                    break;
                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            }
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name);

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        private void VisitTwoNodes(Expression first, Expression second, bool isAppendBefore = false, bool isAppendAfter = false)
        {
            Visit(first);
            _resultStringBuilder.Append(":");
            _resultStringBuilder.Append("(");
            if (isAppendBefore) _resultStringBuilder.Append("*");
            Visit(second);
            if (isAppendAfter) _resultStringBuilder.Append("*");
            _resultStringBuilder.Append(")");
        }

        #endregion
    }
}
