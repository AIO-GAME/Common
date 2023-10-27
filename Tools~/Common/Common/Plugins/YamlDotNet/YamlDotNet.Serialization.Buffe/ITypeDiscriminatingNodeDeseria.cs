using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Serialization.BufferedDeserialization.TypeDiscriminators;

namespace AIO.YamlDotNet.Serialization.BufferedDeserialization
{
	internal interface ITypeDiscriminatingNodeDeserializerOptions
	{
		void AddTypeDiscriminator(ITypeDiscriminator discriminator);

		void AddKeyValueTypeDiscriminator<T>(string discriminatorKey, IDictionary<string, Type> valueTypeMapping);

		void AddUniqueKeyTypeDiscriminator<T>(IDictionary<string, Type> uniqueKeyTypeMapping);
	}
}
