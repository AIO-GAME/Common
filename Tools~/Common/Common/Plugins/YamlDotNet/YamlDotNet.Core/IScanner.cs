using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core
{
	internal interface IScanner
	{
		Mark CurrentPosition { get; }

		Token? Current { get; }

		bool MoveNext();

		bool MoveNextWithoutConsuming();

		void ConsumeCurrent();
	}
}
