using System;
using YamlDotNet.Core;
#nullable enable
namespace YamlDotNet.Serialization
{
	internal interface INodeDeserializer
	{
		bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value);
	}
}
