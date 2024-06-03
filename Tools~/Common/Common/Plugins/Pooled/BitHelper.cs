// /*|============|*|
// |*|Author:     |*| Star fire
// |*|Date:       |*| 2024-03-26
// |*|E-Mail:     |*| xinansky99@foxmail.com
// |*|============|*/
//
// using System.Collections.Generic;
// using System.Linq;
//
// namespace AIO.Collections
// {
//     /// <summary>
//     /// nameof(BitHelper)
//     /// </summary>
//     internal readonly ref struct BitHelper
//     {
//         private const int IntSize = sizeof(int) * 8;
//         private readonly int[] _span;
//
//         internal BitHelper(ICollection<int> span, bool clear)
//         {
//             if (clear) span.Clear();
//             _span = span.ToArray();
//         }
//
//         internal BitHelper(int[] span)
//         {
//             _span = span;
//         }
//
//         internal void MarkBit(int bitPosition)
//         {
//             var bitArrayIndex = bitPosition / IntSize;
//             if (bitArrayIndex < _span.Length)
//             {
//                 _span[bitArrayIndex] |= (1 << (bitPosition % IntSize));
//             }
//         }
//
//         internal bool IsMarked(int bitPosition)
//         {
//             var bitArrayIndex = bitPosition / IntSize;
//             return (bitArrayIndex < _span.Length) &&
//                    (_span[bitArrayIndex] & (1 << (bitPosition % IntSize))) != 0;
//         }
//
//         internal int FindFirstUnmarked(int startPosition = 0)
//         {
//             var i = startPosition;
//             for (var bi = i / IntSize; bi < _span.Length; bi = ++i / IntSize)
//             {
//                 if ((_span[bi] & (1 << (i % IntSize))) == 0)
//                     return i;
//             }
//
//             return -1;
//         }
//
//         internal int FindFirstMarked(int startPosition = 0)
//         {
//             var i = startPosition;
//             for (var bi = i / IntSize; bi < _span.Length; bi = ++i / IntSize)
//             {
//                 if ((_span[bi] & (1 << (i % IntSize))) != 0)
//                     return i;
//             }
//
//             return -1;
//         }
//
//         /// <summary>How many ints must be allocated to represent n bits. Returns (n+31)/32, but avoids overflow.</summary>
//         internal static int ToIntArrayLength(int n) => n > 0 ? ((n - 1) / IntSize + 1) : 0;
//     }
// }