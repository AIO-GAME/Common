using System;
using System.Collections;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	internal abstract class DictionaryDeserializer
	{
		private readonly bool duplicateKeyChecking;

		public DictionaryDeserializer(bool duplicateKeyChecking)
		{
			this.duplicateKeyChecking = duplicateKeyChecking;
		}

		private void TryAssign(IDictionary result, object key, object value, MappingStart propertyName)
		{
			if (duplicateKeyChecking && result.Contains(key))
			{
				Mark start = propertyName.Start;
				Mark end = propertyName.End;
				throw new YamlException(in start, in end, $"Encountered duplicate key {key}");
			}
			result[key] = value;
		}

		protected virtual void Deserialize(Type tKey, Type tValue, IParser parser, Func<IParser, Type, object?> nestedObjectDeserializer, IDictionary result)
		{
			IDictionary result2 = result;
			MappingStart property = parser.Consume<MappingStart>();
			MappingEnd @event;
			while (!parser.TryConsume<MappingEnd>(out @event))
			{
				object key = nestedObjectDeserializer(parser, tKey);
				object value = nestedObjectDeserializer(parser, tValue);
				IValuePromise valuePromise = value as IValuePromise;
				if (key is IValuePromise valuePromise2)
				{
					if (valuePromise == null)
					{
						valuePromise2.ValueAvailable += delegate(object? v)
						{
							result2[v] = value;
						};
						continue;
					}
					bool hasFirstPart = false;
					valuePromise2.ValueAvailable += delegate(object? v)
					{
						if (hasFirstPart)
						{
							TryAssign(result2, v, value, property);
						}
						else
						{
							key = v;
							hasFirstPart = true;
						}
					};
					valuePromise.ValueAvailable += delegate(object? v)
					{
						if (hasFirstPart)
						{
							TryAssign(result2, key, v, property);
						}
						else
						{
							value = v;
							hasFirstPart = true;
						}
					};
					continue;
				}
				if (key == null)
				{
					throw new ArgumentException("Empty key names are not supported yet.", "key");
				}
				if (valuePromise == null)
				{
					TryAssign(result2, key, value, property);
					continue;
				}
				valuePromise.ValueAvailable += delegate(object? v)
				{
					result2[key] = v;
				};
			}
		}
	}
}
