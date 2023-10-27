using YamlDotNet.Core.Events;

namespace YamlDotNet.Core
{
	internal interface IParser
	{
		ParsingEvent? Current { get; }

		bool MoveNext();
	}
}
