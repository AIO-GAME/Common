using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Core
{
	internal interface IEmitter
	{
		void Emit(ParsingEvent @event);
	}
}
