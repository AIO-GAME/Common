using System;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Serialization.Utilities;

namespace AIO.YamlDotNet.Serialization
{
	internal interface IValueDeserializer
	{
		object? DeserializeValue(IParser parser, Type expectedType, SerializerState state, IValueDeserializer nestedObjectDeserializer);
	}
}
