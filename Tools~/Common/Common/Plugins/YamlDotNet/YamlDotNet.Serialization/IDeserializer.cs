using System;
using System.IO;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal interface IDeserializer
	{
		T Deserialize<T>(string input);

		T Deserialize<T>(TextReader input);

		object? Deserialize(TextReader input);

		object? Deserialize(string input, Type type);

		object? Deserialize(TextReader input, Type type);

		T Deserialize<T>(IParser parser);

		object? Deserialize(IParser parser);

		object? Deserialize(IParser parser, Type type);
	}
}
