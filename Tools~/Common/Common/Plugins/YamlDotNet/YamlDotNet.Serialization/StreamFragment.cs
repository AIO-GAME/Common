using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization
{
	internal sealed class StreamFragment : IYamlConvertible
	{
		private readonly List<ParsingEvent> events = new List<ParsingEvent>();

		public IList<ParsingEvent> Events => events;

		void IYamlConvertible.Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
		{
			events.Clear();
			int num = 0;
			do
			{
				if (!parser.MoveNext())
				{
					throw new InvalidOperationException("The parser has reached the end before deserialization completed.");
				}
				ParsingEvent current = parser.Current;
				events.Add(current);
				num += current.NestingIncrease;
			}
			while (num > 0);
		}

		void IYamlConvertible.Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
		{
			foreach (ParsingEvent @event in events)
			{
				emitter.Emit(@event);
			}
		}
	}
}
