#nullable enable
#pragma warning disable
using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;
using AIO.YamlDotNet.Serialization.Utilities;

namespace AIO.YamlDotNet.Serialization.ValueDeserializers
{
	internal sealed class AliasValueDeserializer : IValueDeserializer
	{
		private sealed class AliasState : Dictionary<AnchorName, ValuePromise>, IPostDeserializationCallback
		{
			public void OnDeserialization()
			{
				foreach (ValuePromise value in base.Values)
				{
					if (!value.HasValue)
					{
						AnchorAlias alias = value.Alias;
						Mark start = alias.Start;
						Mark end = alias.End;
						throw new AnchorNotFoundException(in start, in end, $"Anchor '{alias.Value}' not found");
					}
				}
			}
		}

		private sealed class ValuePromise : IValuePromise
		{
			private object? value;

			public readonly AnchorAlias? Alias;

			public bool HasValue { get; private set; }

			public object? Value
			{
				get
				{
					if (!HasValue)
					{
						throw new InvalidOperationException("Value not set");
					}
					return value;
				}
				set
				{
					if (HasValue)
					{
						throw new InvalidOperationException("Value already set");
					}
					HasValue = true;
					this.value = value;
					this.ValueAvailable?.Invoke(value);
				}
			}

			public event Action<object?>? ValueAvailable;

			public ValuePromise(AnchorAlias alias)
			{
				Alias = alias;
			}

			public ValuePromise(object? value)
			{
				HasValue = true;
				this.value = value;
			}
		}

		private readonly IValueDeserializer innerDeserializer;

		public AliasValueDeserializer(IValueDeserializer innerDeserializer)
		{
			this.innerDeserializer = innerDeserializer ?? throw new ArgumentNullException("innerDeserializer");
		}

		public object? DeserializeValue(IParser parser, Type expectedType, SerializerState state, IValueDeserializer nestedObjectDeserializer)
		{
			if (parser.TryConsume<AnchorAlias>(out var @event))
			{
				if (!state.Get<AliasState>().TryGetValue(@event.Value, out var value))
				{
					Mark start = @event.Start;
					Mark end = @event.End;
					throw new AnchorNotFoundException(in start, in end, $"Alias ${@event.Value} cannot precede anchor declaration");
				}
				if (!value.HasValue)
				{
					return value;
				}
				return value.Value;
			}
			AnchorName anchorName = AnchorName.Empty;
			if (parser.Accept<NodeEvent>(out var event2) && !event2.Anchor.IsEmpty)
			{
				anchorName = event2.Anchor;
				AliasState aliasState = state.Get<AliasState>();
				if (!aliasState.ContainsKey(anchorName))
				{
					aliasState[anchorName] = new ValuePromise(new AnchorAlias(anchorName));
				}
			}
			object obj = innerDeserializer.DeserializeValue(parser, expectedType, state, nestedObjectDeserializer);
			if (!anchorName.IsEmpty)
			{
				AliasState aliasState2 = state.Get<AliasState>();
				if (!aliasState2.TryGetValue(anchorName, out var value2))
				{
					aliasState2.Add(anchorName, new ValuePromise(obj));
				}
				else if (!value2.HasValue)
				{
					value2.Value = obj;
				}
				else
				{
					aliasState2[anchorName] = new ValuePromise(obj);
				}
			}
			return obj;
		}
	}
}
