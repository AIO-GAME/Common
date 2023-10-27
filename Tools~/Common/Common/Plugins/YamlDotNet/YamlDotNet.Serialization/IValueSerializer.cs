using System;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization
{
	internal interface IValueSerializer
	{
		void SerializeValue(IEmitter emitter, object? value, Type? type);
	}
}
