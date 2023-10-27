#nullable enable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization.Utilities
{
	internal sealed class SerializerState : IDisposable
	{
		private readonly IDictionary<Type, object> items = new Dictionary<Type, object>();

		public T Get<T>() where T : class, new()
		{
			if (!items.TryGetValue(typeof(T), out var value))
			{
				value = new T();
				items.Add(typeof(T), value);
			}
			return (T)value;
		}

		public void OnDeserialization()
		{
			foreach (IPostDeserializationCallback item in items.Values.OfType<IPostDeserializationCallback>())
			{
				item.OnDeserialization();
			}
		}

		public void Dispose()
		{
			foreach (IDisposable item in items.Values.OfType<IDisposable>())
			{
				item.Dispose();
			}
		}
	}
}
