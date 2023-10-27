using System;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Helpers;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	internal class DictionaryNodeDeserializer : DictionaryDeserializer, INodeDeserializer
	{
		private readonly IObjectFactory objectFactory;

		public DictionaryNodeDeserializer(IObjectFactory objectFactory, bool duplicateKeyChecking)
			: base(duplicateKeyChecking)
		{
			this.objectFactory = objectFactory ?? throw new ArgumentNullException("objectFactory");
		}

		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			Type implementedGenericInterface = ReflectionUtility.GetImplementedGenericInterface(expectedType, typeof(IDictionary<, >));
			Type type;
			Type type2;
			IDictionary dictionary;
			if (implementedGenericInterface != null)
			{
				Type[] genericArguments = implementedGenericInterface.GetGenericArguments();
				type = genericArguments[0];
				type2 = genericArguments[1];
				value = objectFactory.Create(expectedType);
				dictionary = value as IDictionary;
				if (dictionary == null)
				{
					dictionary = (IDictionary)Activator.CreateInstance(typeof(GenericDictionaryToNonGenericAdapter<, >).MakeGenericType(type, type2), value);
				}
			}
			else
			{
				if (!typeof(IDictionary).IsAssignableFrom(expectedType))
				{
					value = null;
					return false;
				}
				type = typeof(object);
				type2 = typeof(object);
				value = objectFactory.Create(expectedType);
				dictionary = (IDictionary)value;
			}
			Deserialize(type, type2, parser, nestedObjectDeserializer, dictionary);
			return true;
		}
	}
}
