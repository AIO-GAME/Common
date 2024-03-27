// using System;
// using System.Collections.Generic;
//
// namespace AIO.Collections
// {
//     /// <summary>
//     /// Extension methods for creating pooled collections.
//     /// </summary>
//     public static class PooledExtensions
//     {
//         #region Memory
//
//         #region PooledDictionary
//
//         // /// <summary>
//         // /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="Memory{TSource}"/> according to specified 
//         // /// key selector and comparer.
//         // /// </summary>
//         // public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(this Memory<TSource> source,
//         //     Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
//         // {
//         //     return ToPooledDictionary(source.Span, keySelector, comparer);
//         // }
//
//         // /// <summary>
//         // /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="ReadOnlyMemory{TSource}"/> according to specified 
//         // /// key selector and comparer.
//         // /// </summary>
//         // public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(
//         //     this ReadOnlyMemory<TSource> source,
//         //     Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
//         // {
//         //     return ToPooledDictionary(source.Span, keySelector, comparer);
//         // }
//
//         // /// <summary>
//         // /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a <see cref="ReadOnlyMemory{TSource}"/> according to specified 
//         // /// key selector and element selector functions, as well as a comparer.
//         // /// </summary>
//         // public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(
//         //     this ReadOnlyMemory<TSource> source,
//         //     Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer)
//         // {
//         //     return ToPooledDictionary(source.Span, keySelector, valueSelector, comparer);
//         // }
//
//         // /// <summary>
//         // /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a <see cref="Memory{TSource}"/> according to specified 
//         // /// key selector and element selector functions, as well as a comparer.
//         // /// </summary>
//         // public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(
//         //     this Memory<TSource> source,
//         //     Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer)
//         // {
//         //     return ToPooledDictionary(source.Span, keySelector, valueSelector, comparer);
//         // }
//
//         #endregion
//
//         #region PooledList
//
//         // public static PooledList<T> ToPooledList<T>(this ReadOnlyMemory<T> memory)
//         //     => new PooledList<T>(memory.Span);
//
//         // public static PooledList<T> ToPooledList<T>(this Memory<T> memory)
//         //     => new PooledList<T>(memory.Span);
//
//         #endregion
//
//         #region PooledSet
//
//         // public static PooledSet<T> ToPooledSet<T>(this Memory<T> source, IEqualityComparer<T> comparer = null)
//         //     => new PooledSet<T>(source.Span, comparer);
//         //
//         // public static PooledSet<T> ToPooledSet<T>(this ReadOnlyMemory<T> source, IEqualityComparer<T> comparer = null)
//         //     => new PooledSet<T>(source.Span, comparer);
//
//         #endregion
//
//         #region PooledStack
//
//         // /// <summary>
//         // /// Creates an instance of PooledStack from the given items.
//         // /// </summary>
//         // public static PooledStack<T> ToPooledStack<T>(this ReadOnlyMemory<T> memory)
//         //     => new PooledStack<T>(memory.Span);
//         //
//         // /// <summary>
//         // /// Creates an instance of PooledStack from the given items.
//         // /// </summary>
//         // public static PooledStack<T> ToPooledStack<T>(this Memory<T> memory)
//         //     => new PooledStack<T>(memory.Span);
//
//         #endregion
//
//         #region PooledQueue
//
//         // /// <summary>
//         // /// Creates an instance of PooledQueue from the given items.
//         // /// </summary>
//         // public static PooledQueue<T> ToPooledQueue<T>(this ReadOnlyMemory<T> memory)
//         //     => new PooledQueue<T>(memory.Span);
//         //
//         // /// <summary>
//         // /// Creates an instance of PooledQueue from the given items.
//         // /// </summary>
//         // public static PooledQueue<T> ToPooledQueue<T>(this Memory<T> memory)
//         //     => new PooledQueue<T>(memory.Span);
//
//         #endregion
//
//         #endregion
//
//         #region PooledList
//
//         /// <summary>
//         /// Creates a <see cref="PooledList{T}"/> from an <see cref="IEnumerable{T}"/>.
//         /// </summary>
//         public static PooledList<T> ToPooledList<T>(this IEnumerable<T> items)
//             => new PooledList<T>(items);
//
//         /// <summary>
//         /// Creates a <see cref="PooledList{T}"/> from an <see cref="IEnumerable{T}"/> with a suggested capacity.
//         /// </summary>
//         public static PooledList<T> ToPooledList<T>(this IEnumerable<T> items, int suggestCapacity)
//             => new PooledList<T>(items, suggestCapacity);
//
//         /// <summary>
//         /// Creates a <see cref="PooledList{T}"/> from an <see cref="IEnumerable{T}"/> with a suggested capacity.
//         /// </summary>
//         public static PooledList<T> ToPooledList<T>(this T[] array)
//             => new PooledList<T>(array);
//
//         /// <summary>
//         /// Creates a <see cref="PooledList{T}"/> from an <see cref="IReadOnlyList{T}"/>.
//         /// </summary>
//         public static PooledList<T> ToPooledList<T>(this IReadOnlyList<T> span)
//             => new PooledList<T>(span);
//
//         /// <summary>
//         /// Creates a <see cref="PooledList{T}"/> from an <see cref="IList{T}"/>.
//         /// </summary>
//         public static PooledList<T> ToPooledList<T>(this IList<T> span)
//             => new PooledList<T>(span);
//
//         #endregion
//
//         #region PooledDictionary
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="IEnumerable{TSource}"/> according to specified 
//         /// key selector and element selector functions, as well as a comparer.
//         /// </summary>
//         public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(
//             this IEnumerable<TSource> source,
//             Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector,
//             IEqualityComparer<TKey> comparer = null)
//         {
//             if (source == null)
//             {
//                 ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
//                 return new PooledDictionary<TKey, TValue>(0, comparer);
//             }
//
//             var dict = new PooledDictionary<TKey, TValue>((source as ICollection<TSource>)?.Count ?? 0, comparer);
//             foreach (var item in source) dict.Add(keySelector(item), valueSelector(item));
//             return dict;
//         }
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a <see cref="IReadOnlyList{TSource}"/> according to specified 
//         /// key selector and element selector functions, as well as a comparer.
//         /// </summary>
//         public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(
//             this IReadOnlyList<TSource> source,
//             Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector,
//             IEqualityComparer<TKey> comparer = null)
//         {
//             var dict = new PooledDictionary<TKey, TValue>(source.Count, comparer);
//             foreach (var item in source) dict.Add(keySelector(item), valueSelector(item));
//             return dict;
//         }
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a <see cref="IList{TSource}"/> according to specified 
//         /// key selector and element selector functions, as well as a comparer.
//         /// </summary>
//         public static PooledDictionary<TKey, TValue> ToPooledDictionary<TSource, TKey, TValue>(
//             this IList<TSource> source,
//             Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector, IEqualityComparer<TKey> comparer)
//         {
//             return ToPooledDictionary((IReadOnlyList<TSource>)source, keySelector, valueSelector, comparer);
//         }
//
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="IEnumerable{TSource}"/> according to specified 
//         /// key selector and comparer.
//         /// </summary>
//         public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(
//             this IEnumerable<TSource> source,
//             Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
//         {
//             if (source is null)
//             {
//                 ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
//                 return new PooledDictionary<TKey, TSource>(0, comparer);
//             }
//
//             var dict = new PooledDictionary<TKey, TSource>((source as ICollection<TSource>)?.Count ?? 0, comparer);
//             foreach (var item in source) dict.Add(keySelector(item), item);
//             return dict;
//         }
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="IReadOnlyList{TSource}"/> according to specified 
//         /// key selector and comparer.
//         /// </summary>
//         public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(
//             this IReadOnlyList<TSource> source,
//             Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
//         {
//             var dict = new PooledDictionary<TKey, TSource>(source.Count, comparer);
//             foreach (var item in source) dict.Add(keySelector(item), item);
//             return dict;
//         }
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from an <see cref="IList{TSource}"/> according to specified 
//         /// key selector and comparer.
//         /// </summary>
//         public static PooledDictionary<TKey, TSource> ToPooledDictionary<TSource, TKey>(this IList<TSource> source,
//             Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
//         {
//             return ToPooledDictionary((IReadOnlyList<TSource>)source, keySelector, comparer);
//         }
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a sequence of key/value tuples.
//         /// </summary>
//         public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(
//             this IEnumerable<(TKey, TValue)> source,
//             IEqualityComparer<TKey> comparer = null)
//         {
//             return new PooledDictionary<TKey, TValue>(source, comparer);
//         }
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a sequence of KeyValuePair values.
//         /// </summary>
//         public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(
//             this IEnumerable<KeyValuePair<TKey, TValue>> source,
//             IEqualityComparer<TKey> comparer = null)
//         {
//             return new PooledDictionary<TKey, TValue>(source, comparer);
//         }
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a sequence of key/value tuples.
//         /// </summary>
//         public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(
//             this IEnumerable<Tuple<TKey, TValue>> source,
//             IEqualityComparer<TKey> comparer = null)
//         {
//             if (source == null)
//             {
//                 ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
//                 return new PooledDictionary<TKey, TValue>(0, comparer);
//             }
//
//             var dict = new PooledDictionary<TKey, TValue>(
//                 (source as ICollection<Tuple<TKey, TValue>>)?.Count ?? 0, comparer);
//             foreach (var pair in source) dict.Add(pair.Item1, pair.Item2);
//             return dict;
//         }
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a span of key/value tuples.
//         /// </summary>
//         public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(
//             this IReadOnlyList<(TKey, TValue)> source,
//             IEqualityComparer<TKey> comparer = null)
//         {
//             return new PooledDictionary<TKey, TValue>(source, comparer);
//         }
//
//         /// <summary>
//         /// Creates a <see cref="PooledDictionary{TKey,TValue}"/> from a span of key/value tuples.
//         /// </summary>
//         public static PooledDictionary<TKey, TValue> ToPooledDictionary<TKey, TValue>(this IList<(TKey, TValue)> source,
//             IEqualityComparer<TKey> comparer = null)
//         {
//             return new PooledDictionary<TKey, TValue>(source, comparer);
//         }
//
//         #endregion
//
//         #region PooledSet
//
//         /// <summary>
//         /// Creates a <see cref="PooledSet{T}"/> from an <see cref="IEnumerable{T}"/>.
//         /// </summary>
//         public static PooledSet<T> ToPooledSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
//             => new PooledSet<T>(source, comparer);
//
//         /// <summary>
//         /// Creates a <see cref="PooledSet{T}"/> from an <see cref="IReadOnlyList{T}"/>.
//         /// </summary>
//         public static PooledSet<T> ToPooledSet<T>(this IList<T> source, IEqualityComparer<T> comparer = null)
//             => new PooledSet<T>(source, comparer);
//
//         /// <summary>
//         /// Creates a <see cref="PooledSet{T}"/> from an <see cref="IReadOnlyList{T}"/>.
//         /// </summary>
//         public static PooledSet<T> ToPooledSet<T>(this IReadOnlyList<T> source, IEqualityComparer<T> comparer = null)
//             => new PooledSet<T>(source, comparer);
//
//         #endregion
//
//         #region PooledStack
//
//         /// <summary>
//         /// Creates an instance of PooledStack from the given items.
//         /// </summary>
//         public static PooledStack<T> ToPooledStack<T>(this IEnumerable<T> items)
//             => new PooledStack<T>(items);
//
//         /// <summary>
//         /// Creates an instance of PooledStack from the given items.
//         /// </summary>
//         public static PooledStack<T> ToPooledStack<T>(this T[] array)
//             => new PooledStack<T>(array);
//
//         /// <summary>
//         /// Creates an instance of PooledStack from the given items.
//         /// </summary>
//         public static PooledStack<T> ToPooledStack<T>(this IReadOnlyList<T> span)
//             => new PooledStack<T>(span);
//
//         /// <summary>
//         /// Creates an instance of PooledStack from the given items.
//         /// </summary>
//         public static PooledStack<T> ToPooledStack<T>(this IList<T> span)
//             => new PooledStack<T>(span);
//
//         #endregion
//
//         #region PooledQueue
//
//         /// <summary>
//         /// Creates an instance of PooledQueue from the given items.
//         /// </summary>
//         public static PooledQueue<T> ToPooledQueue<T>(this IEnumerable<T> items)
//             => new PooledQueue<T>(items);
//
//         /// <summary>
//         /// Creates an instance of PooledQueue from the given items.
//         /// </summary>
//         public static PooledQueue<T> ToPooledQueue<T>(this IReadOnlyList<T> span)
//             => new PooledQueue<T>(span);
//
//         /// <summary>
//         /// Creates an instance of PooledQueue from the given items.
//         /// </summary>
//         public static PooledQueue<T> ToPooledQueue<T>(this IList<T> span)
//             => new PooledQueue<T>(span);
//
//         /// <summary>
//         /// Creates an instance of PooledQueue from the given items.
//         /// </summary>
//         public static PooledQueue<T> ToPooledQueue<T>(this T[] array)
//             => new PooledQueue<T>(array);
//
//         #endregion
//     }
// }