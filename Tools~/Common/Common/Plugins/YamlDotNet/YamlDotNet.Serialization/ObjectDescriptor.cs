using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal sealed class ObjectDescriptor : IObjectDescriptor
	{
		public object? Value { get; private set; }

		public Type Type { get; private set; }

		public Type StaticType { get; private set; }

		public ScalarStyle ScalarStyle { get; private set; }

		public ObjectDescriptor(object? value, Type type, Type staticType)
			: this(value, type, staticType, ScalarStyle.Any)
		{
		}

		public ObjectDescriptor(object? value, Type type, Type staticType, ScalarStyle scalarStyle)
		{
			Value = value;
			Type = type ?? throw new ArgumentNullException("type");
			StaticType = staticType ?? throw new ArgumentNullException("staticType");
			ScalarStyle = scalarStyle;
		}
	}
}
