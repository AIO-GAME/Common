#region

using System;
using System.Collections.Generic;

#endregion

// ReSharper disable NonReadonlyMemberInGetHashCode

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
namespace AIO
{
    /// <summary>
    /// HashCode is designed to quickly compute hash codes of arbitrary objects.
    /// </summary>
    public class HashCode
    {
        private const int num = 23;
        private       int _dummyPrimitive;

        public void Add<T>(T value)
        {
            _dummyPrimitive += EqualityComparer<T>.Default.GetHashCode(value) * 23;
        }

        public void Add<T>(T value, IEqualityComparer<T> comparer)
        {
            _dummyPrimitive += comparer.GetHashCode(value) * 23;
        }

        public static int Combine<T1>(T1 value1)
        {
            return EqualityComparer<T1>.Default.GetHashCode(value1) * num;
        }

        public static int Combine<T1, T2>(T1 value1, T2 value2)
        {
            return EqualityComparer<T1>.Default.GetHashCode(value1) * num +
                   EqualityComparer<T2>.Default.GetHashCode(value2);
        }

        public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            return EqualityComparer<T1>.Default.GetHashCode(value1) * num +
                   EqualityComparer<T2>.Default.GetHashCode(value2) * num +
                   EqualityComparer<T3>.Default.GetHashCode(value3);
        }

        public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            return EqualityComparer<T1>.Default.GetHashCode(value1) * num +
                   EqualityComparer<T2>.Default.GetHashCode(value2) * num +
                   EqualityComparer<T3>.Default.GetHashCode(value3) * num +
                   EqualityComparer<T4>.Default.GetHashCode(value4);
        }

        public static int Combine<T1, T2, T3, T4, T5>(
            T1 value1,
            T2 value2,
            T3 value3,
            T4 value4,
            T5 value5)
        {
            return EqualityComparer<T1>.Default.GetHashCode(value1) * num +
                   EqualityComparer<T2>.Default.GetHashCode(value2) * num +
                   EqualityComparer<T3>.Default.GetHashCode(value3) * num +
                   EqualityComparer<T4>.Default.GetHashCode(value4) * num +
                   EqualityComparer<T5>.Default.GetHashCode(value5);
        }

        public static int Combine<T1, T2, T3, T4, T5, T6>(
            T1 value1,
            T2 value2,
            T3 value3,
            T4 value4,
            T5 value5,
            T6 value6)
        {
            return EqualityComparer<T1>.Default.GetHashCode(value1) * num +
                   EqualityComparer<T2>.Default.GetHashCode(value2) * num +
                   EqualityComparer<T3>.Default.GetHashCode(value3) * num +
                   EqualityComparer<T4>.Default.GetHashCode(value4) * num +
                   EqualityComparer<T5>.Default.GetHashCode(value5) * num +
                   EqualityComparer<T6>.Default.GetHashCode(value6);
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(
            T1 value1,
            T2 value2,
            T3 value3,
            T4 value4,
            T5 value5,
            T6 value6,
            T7 value7)
        {
            return EqualityComparer<T1>.Default.GetHashCode(value1) * num +
                   EqualityComparer<T2>.Default.GetHashCode(value2) * num +
                   EqualityComparer<T3>.Default.GetHashCode(value3) * num +
                   EqualityComparer<T4>.Default.GetHashCode(value4) * num +
                   EqualityComparer<T5>.Default.GetHashCode(value5) * num +
                   EqualityComparer<T6>.Default.GetHashCode(value6) * num +
                   EqualityComparer<T7>.Default.GetHashCode(value7);
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(
            T1 value1,
            T2 value2,
            T3 value3,
            T4 value4,
            T5 value5,
            T6 value6,
            T7 value7,
            T8 value8)
        {
            return EqualityComparer<T1>.Default.GetHashCode(value1) * num +
                   EqualityComparer<T2>.Default.GetHashCode(value2) * num +
                   EqualityComparer<T3>.Default.GetHashCode(value3) * num +
                   EqualityComparer<T4>.Default.GetHashCode(value4) * num +
                   EqualityComparer<T5>.Default.GetHashCode(value5) * num +
                   EqualityComparer<T6>.Default.GetHashCode(value6) * num +
                   EqualityComparer<T7>.Default.GetHashCode(value7) * num +
                   EqualityComparer<T8>.Default.GetHashCode(value8);
        }

        [Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes.", true)]
        public sealed override bool Equals(object obj)
        {
            return obj is HashCode other && Equals(other);
        }

        [Obsolete(
            "HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.",
            true)]
        public sealed override int GetHashCode()
        {
            return _dummyPrimitive;
        }

        public int ToHashCode()
        {
            return _dummyPrimitive;
        }
    }
}