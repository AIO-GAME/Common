using YamlDotNet.Core.Events;

namespace YamlDotNet.Core
{
	internal interface IEmitter
	{
		void Emit(ParsingEvent @event);
	}
}
