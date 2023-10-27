using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal interface IYamlConvertible
	{
		void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer);

		void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer);
	}
}
