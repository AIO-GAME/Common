using System;
using System.Collections;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Serialization.ObjectFactories;
#nullable enable
namespace AIO.YamlDotNet.Serialization.NodeDeserializers
{
	internal class StaticDictionaryNodeDeserializer : DictionaryDeserializer, INodeDeserializer
	{
		private readonly StaticObjectFactory _objectFactory;

		public StaticDictionaryNodeDeserializer(StaticObjectFactory objectFactory, bool duplicateKeyChecking)
			: base(duplicateKeyChecking)
		{
			_objectFactory = objectFactory ?? throw new ArgumentNullException("objectFactory");
		}

		public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			if (_objectFactory.IsDictionary(expectedType))
			{
				if (!(_objectFactory.Create(expectedType) is IDictionary dictionary))
				{
					value = null;
					return false;
				}
				Type keyType = _objectFactory.GetKeyType(expectedType);
				Type valueType = _objectFactory.GetValueType(expectedType);
				value = dictionary;
				base.Deserialize(keyType, valueType, reader, nestedObjectDeserializer, dictionary);
				return true;
			}
			value = null;
			return false;
		}
	}
}
