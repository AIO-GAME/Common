using System;
using System.Collections;
using System.Collections.Generic;
#nullable enable
namespace YamlDotNet.Helpers
{
	internal sealed class GenericDictionaryToNonGenericAdapter<TKey, TValue> : IDictionary, ICollection, IEnumerable where TKey : notnull
	{
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			private readonly IEnumerator<KeyValuePair<TKey, TValue>> enumerator;

			public DictionaryEntry Entry => new DictionaryEntry(Key, Value);

			public object Key => enumerator.Current.Key;

			public object? Value => enumerator.Current.Value;

			public object Current => Entry;

			public DictionaryEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> enumerator)
			{
				this.enumerator = enumerator;
			}

			public bool MoveNext()
			{
				return enumerator.MoveNext();
			}

			public void Reset()
			{
				enumerator.Reset();
			}
		}

		private readonly IDictionary<TKey, TValue> genericDictionary;

		public bool IsFixedSize
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public ICollection Keys
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public ICollection Values
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public object? this[object key]
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				genericDictionary[(TKey)key] = (TValue)value;
			}
		}

		public int Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public GenericDictionaryToNonGenericAdapter(IDictionary<TKey, TValue> genericDictionary)
		{
			this.genericDictionary = genericDictionary ?? throw new ArgumentNullException("genericDictionary");
		}

		public void Add(object key, object? value)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(object key)
		{
			throw new NotSupportedException();
		}

		public IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryEnumerator(genericDictionary.GetEnumerator());
		}

		public void Remove(object key)
		{
			throw new NotSupportedException();
		}

		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
