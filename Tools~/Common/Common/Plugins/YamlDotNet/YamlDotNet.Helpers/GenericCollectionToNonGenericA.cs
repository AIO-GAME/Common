using System;
using System.Collections;
using System.Collections.Generic;
#nullable enable
namespace AIO.YamlDotNet.Helpers
{
	internal sealed class GenericCollectionToNonGenericAdapter<T> : IList, ICollection, IEnumerable
	{
		private readonly ICollection<T> genericCollection;

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

		public object? this[int index]
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				((IList<T>)genericCollection)[index] = (T)value;
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

		public GenericCollectionToNonGenericAdapter(ICollection<T> genericCollection)
		{
			this.genericCollection = genericCollection ?? throw new ArgumentNullException("genericCollection");
		}

		public int Add(object? value)
		{
			int count = genericCollection.Count;
			genericCollection.Add((T)value);
			return count;
		}

		public void Clear()
		{
			genericCollection.Clear();
		}

		public bool Contains(object? value)
		{
			throw new NotSupportedException();
		}

		public int IndexOf(object? value)
		{
			throw new NotSupportedException();
		}

		public void Insert(int index, object? value)
		{
			throw new NotSupportedException();
		}

		public void Remove(object? value)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		public IEnumerator GetEnumerator()
		{
			return genericCollection.GetEnumerator();
		}
	}
}
