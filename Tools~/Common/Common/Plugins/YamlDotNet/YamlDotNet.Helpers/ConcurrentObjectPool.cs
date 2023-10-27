#nullable enable
using System;
using System.Diagnostics;
using System.Threading;

namespace YamlDotNet.Helpers
{
	[DebuggerStepThrough]
	internal sealed class ConcurrentObjectPool<T> where T : class
	{
		[DebuggerDisplay("{value,nq}")]
		private struct Element
		{
			internal T? value;
		}

		internal delegate T Factory();

		private T? firstItem;

		private readonly Element[] items;

		private readonly Factory factory;

		internal ConcurrentObjectPool(Factory factory)
			: this(factory, Environment.ProcessorCount * 2)
		{
		}

		internal ConcurrentObjectPool(Factory factory, int size)
		{
			this.factory = factory;
			items = new Element[size - 1];
		}

		private T CreateInstance()
		{
			return factory();
		}

		internal T Allocate()
		{
			T val = firstItem;
			if (val == null || val != Interlocked.CompareExchange(ref firstItem, null, val))
			{
				val = AllocateSlow();
			}
			return val;
		}

		private T AllocateSlow()
		{
			Element[] array = items;
			for (int i = 0; i < array.Length; i++)
			{
				T value = array[i].value;
				if (value != null && value == Interlocked.CompareExchange(ref array[i].value, null, value))
				{
					return value;
				}
			}
			return CreateInstance();
		}

		internal void Free(T obj)
		{
			if (firstItem == null)
			{
				firstItem = obj;
			}
			else
			{
				FreeSlow(obj);
			}
		}

		private void FreeSlow(T obj)
		{
			Element[] array = items;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].value == null)
				{
					array[i].value = obj;
					break;
				}
			}
		}

		[Conditional("DEBUG")]
		private void Validate(object obj)
		{
			Element[] array = items;
			for (int i = 0; i < array.Length && array[i].value != null; i++)
			{
			}
		}
	}
}
