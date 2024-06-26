// // Licensed to the .NET Foundation under one or more agreements.
// // The .NET Foundation licenses this file to you under the MIT license.
// // See the LICENSE file in the project root for more information.
//
// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
//
// namespace AIO.Collections
// {
//     internal sealed class IDictionaryDebugView<K, V>
//     {
//         private readonly IDictionary<K, V> _dict;
//
//         public IDictionaryDebugView(IDictionary<K, V> dictionary)
//         {
//             _dict = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
//         }
//
//         [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
//         public KeyValuePair<K, V>[] Items
//         {
//             get
//             {
//                 KeyValuePair<K, V>[] items = new KeyValuePair<K, V>[_dict.Count];
//                 _dict.CopyTo(items, 0);
//                 return items;
//             }
//         }
//     }
//
//     internal sealed class DictionaryKeyCollectionDebugView<TKey, TValue>
//     {
//         private readonly ICollection<TKey> _collection;
//
//         public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
//         {
//             _collection = collection ?? throw new ArgumentNullException(nameof(collection));
//         }
//
//         [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
//         public TKey[] Items
//         {
//             get
//             {
//                 TKey[] items = new TKey[_collection.Count];
//                 _collection.CopyTo(items, 0);
//                 return items;
//             }
//         }
//     }
//
//     internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
//     {
//         private readonly ICollection<TValue> _collection;
//
//         public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
//         {
//             _collection = collection ?? throw new ArgumentNullException(nameof(collection));
//         }
//
//         [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
//         public TValue[] Items
//         {
//             get
//             {
//                 TValue[] items = new TValue[_collection.Count];
//                 _collection.CopyTo(items, 0);
//                 return items;
//             }
//         }
//     }
// }