using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal interface IValueSerializer
	{
		void SerializeValue(IEmitter emitter, object? value, Type? type);
	}
}
