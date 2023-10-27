using System;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization.EventEmitters
{
	internal abstract class ChainedEventEmitter : IEventEmitter
	{
		protected readonly IEventEmitter nextEmitter;

		protected ChainedEventEmitter(IEventEmitter nextEmitter)
		{
			this.nextEmitter = nextEmitter ?? throw new ArgumentNullException("nextEmitter");
		}

		public virtual void Emit(AliasEventInfo eventInfo, IEmitter emitter)
		{
			nextEmitter.Emit(eventInfo, emitter);
		}

		public virtual void Emit(ScalarEventInfo eventInfo, IEmitter emitter)
		{
			nextEmitter.Emit(eventInfo, emitter);
		}

		public virtual void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
		{
			nextEmitter.Emit(eventInfo, emitter);
		}

		public virtual void Emit(MappingEndEventInfo eventInfo, IEmitter emitter)
		{
			nextEmitter.Emit(eventInfo, emitter);
		}

		public virtual void Emit(SequenceStartEventInfo eventInfo, IEmitter emitter)
		{
			nextEmitter.Emit(eventInfo, emitter);
		}

		public virtual void Emit(SequenceEndEventInfo eventInfo, IEmitter emitter)
		{
			nextEmitter.Emit(eventInfo, emitter);
		}
	}
}
