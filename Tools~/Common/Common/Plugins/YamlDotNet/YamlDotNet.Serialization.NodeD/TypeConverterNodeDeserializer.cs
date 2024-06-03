using System;
using System.Collections.Generic;
using System.Linq;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization.NodeDeserializers
{
	internal sealed class TypeConverterNodeDeserializer : INodeDeserializer
	{
		private readonly IEnumerable<IYamlTypeConverter> converters;

		public TypeConverterNodeDeserializer(IEnumerable<IYamlTypeConverter> converters)
		{
			this.converters = converters ?? throw new ArgumentNullException("converters");
		}

		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			Type expectedType2 = expectedType;
			IYamlTypeConverter yamlTypeConverter = converters.FirstOrDefault((IYamlTypeConverter c) => c.Accepts(expectedType2));
			if (yamlTypeConverter == null)
			{
				value = null;
				return false;
			}
			value = yamlTypeConverter.ReadYaml(parser, expectedType2);
			return true;
		}
	}
}
