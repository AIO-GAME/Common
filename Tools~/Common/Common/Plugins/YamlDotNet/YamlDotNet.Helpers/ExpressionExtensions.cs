using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace AIO.YamlDotNet.Helpers
{
	internal static class ExpressionExtensions
	{
		public static PropertyInfo AsProperty(this LambdaExpression propertyAccessor)
		{
			PropertyInfo propertyInfo = TryGetMemberExpression<PropertyInfo>(propertyAccessor);
			if (propertyInfo == null)
			{
				throw new ArgumentException("Expected a lambda expression in the form: x => x.SomeProperty", "propertyAccessor");
			}
			return propertyInfo;
		}

		[return: MaybeNull]
		private static TMemberInfo TryGetMemberExpression<TMemberInfo>(LambdaExpression lambdaExpression) where TMemberInfo : MemberInfo
		{
			if (lambdaExpression.Parameters.Count != 1)
			{
				return null;
			}
			Expression expression = lambdaExpression.Body;
			if (expression is UnaryExpression unaryExpression)
			{
				if (unaryExpression.NodeType != ExpressionType.Convert)
				{
					return null;
				}
				expression = unaryExpression.Operand;
			}
			if (expression is MemberExpression memberExpression)
			{
				if (memberExpression.Expression != lambdaExpression.Parameters[0])
				{
					return null;
				}
				return memberExpression.Member as TMemberInfo;
			}
			return null;
		}
	}
}
