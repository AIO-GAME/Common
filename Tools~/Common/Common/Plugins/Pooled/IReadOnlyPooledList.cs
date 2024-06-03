// using System;
// using System.Collections.Generic;
//
// namespace AIO.Collections
// {
//     /// <summary>
//     /// Represents a read-only collection of pooled elements that can be accessed by index
//     /// </summary>
//     /// <typeparam name="T">The type of elements in the read-only pooled list.</typeparam>
//     public interface IReadOnlyPooledList<out T> : IReadOnlyList<T>
//     {
//         /// <summary>
//         /// Gets a <see cref="IReadOnlyList{T}"/> for the items currently in the collection.
//         /// </summary>
//         IReadOnlyList<T> Span { get; }
//     }
// }