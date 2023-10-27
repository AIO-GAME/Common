using System;
using System.Collections;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	internal abstract class CollectionDeserializer
	{
		protected static void DeserializeHelper(Type tItem, IParser parser, Func<IParser, Type, object?> nestedObjectDeserializer, IList result, bool canUpdate, IObjectFactory objectFactory)
		{
			IList result2 = result;
			parser.Consume<SequenceStart>();
			SequenceEnd @event;
			while (!parser.TryConsume<SequenceEnd>(out @event))
			{
				ParsingEvent current = parser.Current;
				object obj = nestedObjectDeserializer(parser, tItem);
				if (obj is IValuePromise valuePromise)
				{
					if (!canUpdate)
					{
						Mark start = current?.Start ?? Mark.Empty;
						Mark end = current?.End ?? Mark.Empty;
						throw new ForwardAnchorNotSupportedException(in start, in end, "Forward alias references are not allowed because this type does not implement IList<>");
					}
					int index = result2.Add(objectFactory.CreatePrimitive(tItem));
					valuePromise.ValueAvailable += delegate(object? v)
					{
						result2[index] = v;
					};
				}
				else
				{
					result2.Add(obj);
				}
			}
		}
	}
}
