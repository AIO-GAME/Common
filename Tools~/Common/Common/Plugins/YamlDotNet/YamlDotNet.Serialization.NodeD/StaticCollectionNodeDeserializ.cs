using System;
using System.Collections;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.ObjectFactories;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	internal sealed class StaticCollectionNodeDeserializer : INodeDeserializer
	{
		private readonly StaticObjectFactory factory;

		public StaticCollectionNodeDeserializer(StaticObjectFactory factory)
		{
			this.factory = factory ?? throw new ArgumentNullException("factory");
		}

		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			if (!factory.IsList(expectedType))
			{
				value = null;
				return false;
			}
			DeserializeHelper(result: (IList)(value = factory.Create(expectedType) as IList), tItem: factory.GetValueType(expectedType), parser: parser, nestedObjectDeserializer: nestedObjectDeserializer, factory: factory);
			return true;
		}

		internal static void DeserializeHelper(Type tItem, IParser parser, Func<IParser, Type, object?> nestedObjectDeserializer, IList result, IObjectFactory factory)
		{
			IList result2 = result;
			parser.Consume<SequenceStart>();
			SequenceEnd @event;
			while (!parser.TryConsume<SequenceEnd>(out @event))
			{
				_ = parser.Current;
				object obj = nestedObjectDeserializer(parser, tItem);
				if (obj is IValuePromise valuePromise)
				{
					int index = result2.Add(factory.CreatePrimitive(tItem));
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
