using System;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization
{
	[Obsolete("Please use IYamlConvertible instead")]
	internal interface IYamlSerializable
	{
		void ReadYaml(IParser parser);

		void WriteYaml(IEmitter emitter);
	}
}
