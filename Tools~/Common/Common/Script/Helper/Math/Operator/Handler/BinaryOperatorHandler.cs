﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    /// <summary>
    /// 二进制运算符 处理程序
    /// </summary>
    public abstract class BinaryOperatorHandler : OperatorHandler
    {
        /// <inheritdoc />
        protected BinaryOperatorHandler(
            in string name,
            in string verb,
            in string symbol,
            in string customMethodName
        ) : base(name, verb, symbol, customMethodName)
        {
        }

        private readonly Dictionary<OperatorQuery, Func<object, object, object>> handlers
            = new Dictionary<OperatorQuery, Func<object, object, object>>();

        private readonly Dictionary<OperatorQuery, IOptimizedInvoker> userDefinedOperators
            = new Dictionary<OperatorQuery, IOptimizedInvoker>();

        private readonly Dictionary<OperatorQuery, OperatorQuery> userDefinedOperandTypes
            = new Dictionary<OperatorQuery, OperatorQuery>();

        public virtual object Operate(object leftOperand, object rightOperand)
        {
            OperatorQuery query;

            var leftType = leftOperand?.GetType();
            var rightType = rightOperand?.GetType();

            if (leftType != null && rightType != null)
            {
                query = new OperatorQuery(leftType, rightType);
            }
            else if (leftType != null && leftType.IsNullable())
            {
                query = new OperatorQuery(leftType, leftType);
            }
            else if (rightType != null && rightType.IsNullable())
            {
                query = new OperatorQuery(rightType, rightType);
            }
            else if (leftType == null && rightType == null)
            {
                return BothNullHandling();
            }
            else
            {
                return SingleNullHandling();
            }

            if (handlers.ContainsKey(query))
            {
                return handlers[query](leftOperand, rightOperand);
            }

            if (CustomMethodName != null)
            {
                if (!userDefinedOperators.ContainsKey(query))
                {
                    var leftMethod = query.leftType.GetMethod(CustomMethodName,
                        BindingFlags.Public | BindingFlags.Static, null, new[] { query.leftType, query.rightType },
                        null);

                    if (query.leftType != query.rightType)
                    {
                        var rightMethod = query.rightType.GetMethod(CustomMethodName,
                            BindingFlags.Public | BindingFlags.Static, null, new[] { query.leftType, query.rightType },
                            null);

                        if (leftMethod != null && rightMethod != null)
                        {
                            throw new AmbiguousOperatorException(Symbol, query.leftType, query.rightType);
                        }

                        var method = (leftMethod ?? rightMethod);

                        if (method != null)
                        {
                            userDefinedOperandTypes.Add(query, ResolveUserDefinedOperandTypes(method));
                        }

                        userDefinedOperators.Add(query, method?.Prewarm());
                    }
                    else
                    {
                        if (leftMethod != null)
                        {
                            userDefinedOperandTypes.Add(query, ResolveUserDefinedOperandTypes(leftMethod));
                        }

                        userDefinedOperators.Add(query, leftMethod?.Prewarm());
                    }
                }

                if (userDefinedOperators[query] != null)
                {
                    leftOperand = Utils.Conversion.Convert(leftOperand, userDefinedOperandTypes[query].leftType);
                    rightOperand = Utils.Conversion.Convert(rightOperand, userDefinedOperandTypes[query].rightType);
                    return userDefinedOperators[query].Invoke(null, leftOperand, rightOperand);
                }
            }

            return CustomHandling(leftOperand, rightOperand);
        }

        protected virtual object CustomHandling(object leftOperand, object rightOperand)
        {
            throw new InvalidOperatorException(Symbol, leftOperand?.GetType(), rightOperand?.GetType());
        }

        protected virtual object BothNullHandling()
        {
            throw new InvalidOperatorException(Symbol, null, null);
        }

        protected virtual object SingleNullHandling()
        {
            throw new InvalidOperatorException(Symbol, null, null);
        }

        protected void Handle<TLeft, TRight>(Func<TLeft, TRight, object> handler, bool reverse = false)
        {
            var query = new OperatorQuery(typeof(TLeft), typeof(TRight));

            if (handlers.ContainsKey(query))
            {
                throw new ArgumentException(
                    $"A handler is already registered for '{typeof(TLeft)} {Symbol} {typeof(TRight)}'.");
            }

            handlers.Add(query, (left, right) => handler((TLeft)left, (TRight)right));

            if (reverse && typeof(TLeft) != typeof(TRight))
            {
                var reverseQuery = new OperatorQuery(typeof(TRight), typeof(TLeft));

                if (!handlers.ContainsKey(reverseQuery))
                {
                    handlers.Add(reverseQuery, (left, right) => handler((TLeft)left, (TRight)right));
                }
            }
        }

        private static OperatorQuery ResolveUserDefinedOperandTypes(MethodInfo userDefinedOperator)
        {
            // We will need to convert the operands to the argument types,
            // because .NET is actually permissive of implicit conversion
            // in its GetMethod calls. For example, an operator requiring
            // a float operand will accept an int. However, our optimized
            // reflection code is more strict, and will only accept the
            // exact type, hence why we need to manually store the expected
            // parameter types here to convert them later.
            var parameters = userDefinedOperator.GetParameters();
            return new OperatorQuery(parameters[0].ParameterType, parameters[1].ParameterType);
        }

        private readonly struct OperatorQuery : IEquatable<OperatorQuery>
        {
            public readonly Type leftType;
            public readonly Type rightType;

            public OperatorQuery(Type leftType, Type rightType)
            {
                this.leftType = leftType;
                this.rightType = rightType;
            }

            public bool Equals(OperatorQuery other)
            {
                return
                    leftType == other.leftType &&
                    rightType == other.rightType;
            }

            public override bool Equals(object obj)
            {
                if (obj is OperatorQuery query)
                    return Equals(query);
                return false;
            }

            public override int GetHashCode()
            {
                return Utils.Hash.GetHashCode(leftType, rightType);
            }
        }
    }
}