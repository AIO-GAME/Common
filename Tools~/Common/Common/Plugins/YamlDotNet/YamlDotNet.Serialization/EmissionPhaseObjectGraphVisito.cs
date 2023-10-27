using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal sealed class EmissionPhaseObjectGraphVisitorArgs
	{
		private readonly IEnumerable<IObjectGraphVisitor<Nothing>> preProcessingPhaseVisitors;

		public IObjectGraphVisitor<IEmitter> InnerVisitor { get; private set; }

		public IEventEmitter EventEmitter { get; private set; }

		public ObjectSerializer NestedObjectSerializer { get; private set; }

		public IEnumerable<IYamlTypeConverter> TypeConverters { get; private set; }

		public EmissionPhaseObjectGraphVisitorArgs(IObjectGraphVisitor<IEmitter> innerVisitor, IEventEmitter eventEmitter, IEnumerable<IObjectGraphVisitor<Nothing>> preProcessingPhaseVisitors, IEnumerable<IYamlTypeConverter> typeConverters, ObjectSerializer nestedObjectSerializer)
		{
			InnerVisitor = innerVisitor ?? throw new ArgumentNullException("innerVisitor");
			EventEmitter = eventEmitter ?? throw new ArgumentNullException("eventEmitter");
			this.preProcessingPhaseVisitors = preProcessingPhaseVisitors ?? throw new ArgumentNullException("preProcessingPhaseVisitors");
			TypeConverters = typeConverters ?? throw new ArgumentNullException("typeConverters");
			NestedObjectSerializer = nestedObjectSerializer ?? throw new ArgumentNullException("nestedObjectSerializer");
		}

		public T GetPreProcessingPhaseObjectGraphVisitor<T>() where T : IObjectGraphVisitor<Nothing>
		{
			return preProcessingPhaseVisitors.OfType<T>().Single();
		}
	}
}
