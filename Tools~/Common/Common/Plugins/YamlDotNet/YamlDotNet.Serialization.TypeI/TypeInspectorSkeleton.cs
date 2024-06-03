using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace AIO.YamlDotNet.Serialization.TypeInspectors
{
	internal abstract class TypeInspectorSkeleton : ITypeInspector
	{
		public abstract IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container);

		public IPropertyDescriptor GetProperty(Type type, object? container, string name, [MaybeNullWhen(true)] bool ignoreUnmatched)
		{
			string name2 = name;
			IEnumerable<IPropertyDescriptor> enumerable = from p in GetProperties(type, container)
				where p.Name == name2
				select p;
			using IEnumerator<IPropertyDescriptor> enumerator = enumerable.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				if (ignoreUnmatched)
				{
					return null;
				}
				throw new SerializationException("Property '" + name2 + "' not found on type '" + type.FullName + "'.");
			}
			IPropertyDescriptor current = enumerator.Current;
			if (enumerator.MoveNext())
			{
				throw new SerializationException("Multiple properties with the name/alias '" + name2 + "' already exists on type '" + type.FullName + "', maybe you're misusing YamlAlias or maybe you are using the wrong naming convention? The matching properties are: " + string.Join(", ", enumerable.Select((IPropertyDescriptor p) => p.Name).ToArray()));
			}
			return current;
		}
	}
}
