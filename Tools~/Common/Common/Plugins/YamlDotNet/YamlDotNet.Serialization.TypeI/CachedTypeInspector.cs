using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization.TypeInspectors
{
	internal sealed class CachedTypeInspector : TypeInspectorSkeleton
	{
		private readonly ITypeInspector innerTypeDescriptor;

		private readonly ConcurrentDictionary<Type, List<IPropertyDescriptor>> cache = new ConcurrentDictionary<Type, List<IPropertyDescriptor>>();

		public CachedTypeInspector(ITypeInspector innerTypeDescriptor)
		{
			this.innerTypeDescriptor = innerTypeDescriptor ?? throw new ArgumentNullException("innerTypeDescriptor");
		}

		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container)
		{
			object container2 = container;
			return cache.GetOrAdd(type, (Type t) => innerTypeDescriptor.GetProperties(t, container2).ToList());
		}
	}
}
