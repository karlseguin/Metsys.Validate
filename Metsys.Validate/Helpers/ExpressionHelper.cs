using System;
using System.Linq.Expressions;
    
namespace Metsys.Validate
{
    internal static class ExpressionHelper
    {
        public static string GetName(this MemberExpression expression)
        {
            return new ExpressionNameVisitor().Visit(expression);
        }
        public static string GetName<T, TValue>(this Expression<Func<T, TValue>> expression)
        {
            return expression.GetMemberExpression().GetName();
        }

        public static MemberExpression GetMemberExpression<T, TValue>(this Expression<Func<T, TValue>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            if (expression.Body is MemberExpression)
            {
                return (MemberExpression)expression.Body;
            }
            if (expression.Body is UnaryExpression)
            {
                var operand = ((UnaryExpression)expression.Body).Operand;
                if (operand is MemberExpression)
                {
                    return (MemberExpression)operand;
                }
                if (operand is MethodCallExpression)
                {
                    return ((MethodCallExpression)operand).Object as MemberExpression;
                }
            }
            return null;
        }

        private class ExpressionNameVisitor
        {
            public string Visit(Expression expression)
            {
                if (expression is UnaryExpression)
                {
                    expression = ((UnaryExpression)expression).Operand;
                }
                if (expression is MethodCallExpression)
                {
                    return Visit((MethodCallExpression)expression);
                }
                if (expression is MemberExpression)
                {
                    return Visit((MemberExpression)expression);
                }
                if (expression is BinaryExpression && expression.NodeType == ExpressionType.ArrayIndex)
                {
                    return Visit((BinaryExpression)expression);
                }
                return null;
            }

            private string Visit(BinaryExpression expression)
            {
                string result = null;
                if (expression.Left is MemberExpression)
                {
                    result = Visit((MemberExpression)expression.Left);
                }
                var index = Expression.Lambda(expression.Right).Compile().DynamicInvoke();
                return result + string.Format("[{0}]", index);
            }

            private string Visit(MemberExpression expression)
            {
                var name = expression.Member.Name;
                var ancestorName = Visit(expression.Expression);
                if (ancestorName != null)
                {
                    name = ancestorName + "." + name;
                }
                return name;
            }

            private string Visit(MethodCallExpression expression)
            {
                string name = null;
                if (expression.Object is MemberExpression)
                {
                    name = Visit((MemberExpression)expression.Object);
                }

                //TODO: Is there a more certain way to determine if this is an indexed property?
                if (expression.Method.Name == "get_Item" && expression.Arguments.Count == 1)
                {
                    var index = Expression.Lambda(expression.Arguments[0]).Compile().DynamicInvoke();
                    name += string.Format("[{0}]", index);
                }
                return name;
            }
        }
    }        
}