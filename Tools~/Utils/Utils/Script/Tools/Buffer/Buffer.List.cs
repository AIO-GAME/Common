namespace AIO
{
	using Newtonsoft.Json.Linq;

	using System;
	using System.Collections;
	using System.Collections.Generic;

	/// <summary>
	/// 接口
	/// </summary>
	public partial class Buffer<T> : IList<T>, IEnumerable, IDisposable, ICollection<T>
	{
		public virtual bool IsReadOnly => false;

		public virtual T this[int index]
		{
			get => Arrays[index];
			set => Arrays[index] = value;
		}

		public int IndexOf(T item)
		{
			var index = 0;
			foreach (var items in Arrays)
			{
				if (items.Equals(item))
					return index;
				else index++;
			}
			return index;
		}

		public void Insert(int index, T item)
		{
			if (index < 0 || index > WriteIndex) throw new IndexOutOfRangeException();
			if (index < WriteIndex)
			{
				WriteIndex++;
				var copy = new byte[WriteIndex - index];
				Array.Copy(Arrays, index, copy, 0, copy.Length);
				Array.Copy(copy, 0, Arrays, index + 1, copy.Length);
			}
			Arrays[index] = item;
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index > WriteIndex) throw new IndexOutOfRangeException();
			if (index < WriteIndex)
			{
				WriteIndex--;
				var copy = new byte[WriteIndex - index - 1];
				Array.Copy(Arrays, index + 1, copy, 0, copy.Length);
				Array.Copy(copy, 0, Arrays, index, copy.Length);
			}
		}

		public void Add(T item)
		{
			Insert(WriteIndex, item);
		}

		public bool Contains(T item)
		{
			foreach (var items in Arrays)
			{
				if (items.Equals(item))
					return true;
			}
			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(array, arrayIndex, Arrays, 0, WriteIndex);
		}

		public bool Remove(T item)
		{
			var index = 0;
			foreach (var items in Arrays)
			{
				if (items.Equals(item))
					RemoveAt(index);
			}
			return false;
		}

		public IEnumerator<T> GetEnumerator()
		{
			foreach (var item in Arrays)
			{
				yield return item;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (var item in Arrays)
			{
				yield return item;
			}
		}

		/// <summary>
		/// 释放
		/// </summary>
		public abstract void Dispose();
	}
}
