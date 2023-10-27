#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.TypeInspectors
{
	internal sealed class ReadableFieldsTypeInspector : TypeInspectorSkeleton
	{
		private sealed class ReflectionFieldDescriptor : IPropertyDescriptor
		{
			private readonly FieldInfo fieldInfo;

			private readonly ITypeResolver typeResolver;

			public string Name => fieldInfo.Name;

			public Type Type => fieldInfo.FieldType;

			public Type? TypeOverride { get; set; }

			public int Order { get; set; }

			public bool CanWrite => !fieldInfo.IsInitOnly;

			public ScalarStyle ScalarStyle { get; set; }

			public ReflectionFieldDescriptor(FieldInfo fieldInfo, ITypeResolver typeResolver)
			{
				this.fieldInfo = fieldInfo;
				this.typeResolver = typeResolver;
				ScalarStyle = ScalarStyle.Any;
			}

			public void Write(object target, object? value)
			{
				fieldInfo.SetValue(target, value);
			}

			public T? GetCustomAttribute<T>() where T : Attribute
			{
				return (T)fieldInfo.GetCustomAttributes(typeof(T), inherit: true).FirstOrDefault();
			}

			public IObjectDescriptor Read(object target)
			{
				object value = fieldInfo.GetValue(target);
				Type type = TypeOverride ?? typeResolver.Resolve(Type, value);
				return new ObjectDescriptor(value, type, Type, ScalarStyle);
			}
		}

		private readonly ITypeResolver typeResolver;

		public ReadableFieldsTypeInspector(ITypeResolver typeResolver)
		{
			this.typeResolver = typeResolver ?? throw new ArgumentNullException("typeResolver");
		}

		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container)
		{
			return type.GetPublicFields().Select<FieldInfo, IPropertyDescriptor>((Func<FieldInfo, IPropertyDescriptor>)((FieldInfo p) => new ReflectionFieldDescriptor(p, typeResolver)));
		}
	}
}
