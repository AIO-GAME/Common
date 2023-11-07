using System;
using System.Collections;
using AIO.YamlDotNet.Core;
#pragma warning disable
#nullable enable
namespace AIO.YamlDotNet.Serialization.NodeDeserializers
{
	internal sealed class ArrayNodeDeserializer : INodeDeserializer
	{
		private sealed class ArrayList : IList, ICollection, IEnumerable
		{
			private object?[] data;

			public bool IsFixedSize => false;

			public bool IsReadOnly => false;

			public object? this[int index]
			{
				get
				{
					return data[index];
				}
				set
				{
					data[index] = value;
				}
			}

			public int Count { get; private set; }

			public bool IsSynchronized => false;

			public object SyncRoot => data;

			public ArrayList()
			{
				Clear();
			}

			public int Add(object? value)
			{
				if (Count == data.Length)
				{
					Array.Resize(ref data, data.Length * 2);
				}
				data[Count] = value;
				return Count++;
			}

			public void Clear()
			{
				data = new object[10];
				Count = 0;
			}

			bool IList.Contains(object? value)
			{
				throw new NotSupportedException();
			}

			int IList.IndexOf(object? value)
			{
				throw new NotSupportedException();
			}

			void IList.Insert(int index, object? value)
			{
				throw new NotSupportedException();
			}

			void IList.Remove(object? value)
			{
				throw new NotSupportedException();
			}

			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			public void CopyTo(Array array, int index)
			{
				Array.Copy(data, 0, array, index, Count);
			}

			public IEnumerator GetEnumerator()
			{
				int i = 0;
				while (i < Count)
				{
					yield return data[i];
					int num = i + 1;
					i = num;
				}
			}
		}

		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			if (!expectedType.IsArray)
			{
				value = false;
				return false;
			}
			Type elementType = expectedType.GetElementType();
			ArrayList arrayList = new ArrayList();
			CollectionNodeDeserializer.DeserializeHelper(elementType, parser, nestedObjectDeserializer, arrayList, canUpdate: true);
			Array array = Array.CreateInstance(elementType, arrayList.Count);
			arrayList.CopyTo(array, 0);
			value = array;
			return true;
		}
	}
}
