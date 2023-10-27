using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	internal sealed class YamlConvertibleNodeDeserializer : INodeDeserializer
	{
		private readonly IObjectFactory objectFactory;

		public YamlConvertibleNodeDeserializer(IObjectFactory objectFactory)
		{
			this.objectFactory = objectFactory;
		}

		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			Func<IParser, Type, object?> nestedObjectDeserializer2 = nestedObjectDeserializer;
			IParser parser2 = parser;
			if (typeof(IYamlConvertible).IsAssignableFrom(expectedType))
			{
				IYamlConvertible yamlConvertible = (IYamlConvertible)objectFactory.Create(expectedType);
				yamlConvertible.Read(parser2, expectedType, (Type type) => nestedObjectDeserializer2(parser2, type));
				value = yamlConvertible;
				return true;
			}
			value = null;
			return false;
		}
	}
}
