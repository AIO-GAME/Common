using System;
using System.Collections;
using System.Collections.Generic;
using AIO.YamlDotNet.Helpers;

namespace AIO.YamlDotNet.Core
{
	internal sealed class InsertionQueue<T> : IEnumerable<T>, IEnumerable
	{
		private const int DefaultInitialCapacity = 128;

		private T[] items;

		private int readPtr;

		private int writePtr;

		private int mask;

		private int count;

		public int Count => count;

		public int Capacity => items.Length;

		public InsertionQueue(int initialCapacity = 128)
		{
			if (initialCapacity <= 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "The initial capacity must be a positive number.");
			}
			if (!initialCapacity.IsPowerOfTwo())
			{
				throw new ArgumentException("The initial capacity must be a power of 2.", "initialCapacity");
			}
			items = new T[initialCapacity];
			readPtr = initialCapacity / 2;
			writePtr = initialCapacity / 2;
			mask = initialCapacity - 1;
		}

		public void Enqueue(T item)
		{
			ResizeIfNeeded();
			items[writePtr] = item;
			writePtr = (writePtr - 1) & mask;
			count++;
		}

		public T Dequeue()
		{
			if (count == 0)
			{
				throw new InvalidOperationException("The queue is empty");
			}
			T result = items[readPtr];
			readPtr = (readPtr - 1) & mask;
			count--;
			return result;
		}

		public void Insert(int index, T item)
		{
			if (index > count)
			{
				throw new InvalidOperationException("Cannot insert outside of the bounds of the queue");
			}
			ResizeIfNeeded();
			CalculateInsertionParameters(mask, count, index, ref readPtr, ref writePtr, out var insertPtr, out var copyIndex, out var copyOffset, out var copyLength);
			if (copyLength != 0)
			{
				Array.Copy(items, copyIndex, items, copyIndex + copyOffset, copyLength);
			}
			items[insertPtr] = item;
			count++;
		}

		private void ResizeIfNeeded()
		{
			int num = items.Length;
			if (count == num)
			{
				T[] destinationArray = new T[num * 2];
				int num2 = readPtr + 1;
				if (num2 > 0)
				{
					Array.Copy(items, 0, destinationArray, 0, num2);
				}
				writePtr += num;
				int num3 = num - num2;
				if (num3 > 0)
				{
					Array.Copy(items, readPtr + 1, destinationArray, writePtr + 1, num3);
				}
				items = destinationArray;
				mask = mask * 2 + 1;
			}
		}

		internal static void CalculateInsertionParameters(int mask, int count, int index, ref int readPtr, ref int writePtr, out int insertPtr, out int copyIndex, out int copyOffset, out int copyLength)
		{
			int num = (readPtr + 1) & mask;
			if (index == 0)
			{
				insertPtr = (readPtr = num);
				copyIndex = 0;
				copyOffset = 0;
				copyLength = 0;
				return;
			}
			insertPtr = (readPtr - index) & mask;
			if (index == count)
			{
				writePtr = (writePtr - 1) & mask;
				copyIndex = 0;
				copyOffset = 0;
				copyLength = 0;
				return;
			}
			int num2 = ((num >= insertPtr) ? (readPtr - insertPtr) : int.MaxValue);
			int num3 = ((writePtr <= insertPtr) ? (insertPtr - writePtr) : int.MaxValue);
			if (num2 <= num3)
			{
				insertPtr++;
				readPtr++;
				copyIndex = insertPtr;
				copyOffset = 1;
				copyLength = num2;
			}
			else
			{
				copyIndex = writePtr + 1;
				copyOffset = -1;
				copyLength = num3;
				writePtr = (writePtr - 1) & mask;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			int ptr = readPtr;
			for (int i = 0; i < Count; i++)
			{
				yield return items[ptr];
				ptr = (ptr - 1) & mask;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
