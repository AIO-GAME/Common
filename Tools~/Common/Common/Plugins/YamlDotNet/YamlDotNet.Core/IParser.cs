using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Core
{
	internal interface IParser
	{
		ParsingEvent? Current { get; }

		bool MoveNext();
	}
}
