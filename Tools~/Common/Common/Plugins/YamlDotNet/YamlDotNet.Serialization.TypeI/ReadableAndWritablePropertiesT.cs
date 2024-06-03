using System;
using System.Collections.Generic;
using System.Linq;

namespace AIO.YamlDotNet.Serialization.TypeInspectors
{
	internal sealed class ReadableAndWritablePropertiesTypeInspector : TypeInspectorSkeleton
	{
		private readonly ITypeInspector innerTypeDescriptor;

		public ReadableAndWritablePropertiesTypeInspector(ITypeInspector innerTypeDescriptor)
		{
			this.innerTypeDescriptor = innerTypeDescriptor ?? throw new ArgumentNullException("innerTypeDescriptor");
		}

		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container)
		{
			return from p in innerTypeDescriptor.GetProperties(type, container)
				where p.CanWrite
				select p;
		}
	}
}
