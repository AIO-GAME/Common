using System;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization
{
	internal interface IYamlConvertible
	{
		void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer);

		void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer);
	}
}
