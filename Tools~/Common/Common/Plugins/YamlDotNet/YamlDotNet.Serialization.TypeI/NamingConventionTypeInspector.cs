using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization.TypeInspectors
{
	internal sealed class NamingConventionTypeInspector : TypeInspectorSkeleton
	{
		private readonly ITypeInspector innerTypeDescriptor;

		private readonly INamingConvention namingConvention;

		public NamingConventionTypeInspector(ITypeInspector innerTypeDescriptor, INamingConvention namingConvention)
		{
			this.innerTypeDescriptor = innerTypeDescriptor ?? throw new ArgumentNullException("innerTypeDescriptor");
			this.namingConvention = namingConvention ?? throw new ArgumentNullException("namingConvention");
		}

		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container)
		{
			return innerTypeDescriptor.GetProperties(type, container).Select(delegate(IPropertyDescriptor p)
			{
				YamlMemberAttribute customAttribute = p.GetCustomAttribute<YamlMemberAttribute>();
				return (customAttribute != null && !customAttribute.ApplyNamingConventions) ? p : new PropertyDescriptor(p)
				{
					Name = namingConvention.Apply(p.Name)
				};
			});
		}
	}
}
