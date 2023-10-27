using System;
using System.Collections.Generic;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	internal sealed class AnchorAssigningObjectGraphVisitor : ChainedObjectGraphVisitor
	{
		private readonly IEventEmitter eventEmitter;

		private readonly IAliasProvider aliasProvider;

		private readonly HashSet<AnchorName> emittedAliases = new HashSet<AnchorName>();

		public AnchorAssigningObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor, IEventEmitter eventEmitter, IAliasProvider aliasProvider)
			: base(nextVisitor)
		{
			this.eventEmitter = eventEmitter;
			this.aliasProvider = aliasProvider;
		}

		public override bool Enter(IObjectDescriptor value, IEmitter context)
		{
			if (value.Value != null)
			{
				AnchorName alias = aliasProvider.GetAlias(value.Value);
				if (!alias.IsEmpty && !emittedAliases.Add(alias))
				{
					AliasEventInfo aliasEventInfo = new AliasEventInfo(value, alias);
					eventEmitter.Emit(aliasEventInfo, context);
					return aliasEventInfo.NeedsExpansion;
				}
			}
			return base.Enter(value, context);
		}

		public override void VisitMappingStart(IObjectDescriptor mapping, Type keyType, Type valueType, IEmitter context)
		{
			AnchorName alias = aliasProvider.GetAlias(mapping.NonNullValue());
			eventEmitter.Emit(new MappingStartEventInfo(mapping)
			{
				Anchor = alias
			}, context);
		}

		public override void VisitSequenceStart(IObjectDescriptor sequence, Type elementType, IEmitter context)
		{
			AnchorName alias = aliasProvider.GetAlias(sequence.NonNullValue());
			eventEmitter.Emit(new SequenceStartEventInfo(sequence)
			{
				Anchor = alias
			}, context);
		}

		public override void VisitScalar(IObjectDescriptor scalar, IEmitter context)
		{
			ScalarEventInfo scalarEventInfo = new ScalarEventInfo(scalar);
			if (scalar.Value != null)
			{
				scalarEventInfo.Anchor = aliasProvider.GetAlias(scalar.Value);
			}
			eventEmitter.Emit(scalarEventInfo, context);
		}
	}
}
