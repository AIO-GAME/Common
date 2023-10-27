using System.Collections;
using System.Collections.Generic;

namespace YamlDotNet.Helpers
{
	internal interface IOrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : notnull
	{
		KeyValuePair<TKey, TValue> this[int index] { get; set; }

		void Insert(int index, TKey key, TValue value);

		void RemoveAt(int index);
	}
}
