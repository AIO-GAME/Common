// /*|============|*|
// |*|Author:     |*| Star fire
// |*|Date:       |*| 2024-03-26
// |*|E-Mail:     |*| xinansky99@foxmail.com
// |*|============|*/
//
// using System.Threading;
// using System.Runtime.CompilerServices;
// using System.Runtime.Serialization;
//
// namespace AIO.Collections
// {
//     internal static partial class HashHelpers
//     {
//         private static ConditionalWeakTable<object, SerializationInfo> s_serializationInfoTable;
//
//         public static ConditionalWeakTable<object, SerializationInfo> SerializationInfoTable
//         {
//             get
//             {
//                 if (s_serializationInfoTable == null)
//                     Interlocked.CompareExchange(ref s_serializationInfoTable,
//                         new ConditionalWeakTable<object, SerializationInfo>(), null);
//
//                 return s_serializationInfoTable;
//             }
//         }
//     }
// }