using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal interface IYamlTypeConverter
	{
		bool Accepts(Type type);

		object? ReadYaml(IParser parser, Type type);

		void WriteYaml(IEmitter emitter, object? value, Type type);
	}
}
