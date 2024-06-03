using System;
using AIO.YamlDotNet.Core;

#nullable enable
namespace AIO.YamlDotNet.Serialization
{
	internal interface INodeDeserializer
	{
		bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value);
	}
}
