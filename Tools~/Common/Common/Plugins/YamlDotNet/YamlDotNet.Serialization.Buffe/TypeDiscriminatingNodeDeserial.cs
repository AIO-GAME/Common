using System;
using System.Collections.Generic;
using System.Linq;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;
using AIO.YamlDotNet.Serialization.BufferedDeserialization.TypeDiscriminators;

namespace AIO.YamlDotNet.Serialization.BufferedDeserialization
{
	internal class TypeDiscriminatingNodeDeserializer : INodeDeserializer
	{
		private readonly IList<INodeDeserializer> innerDeserializers;

		private readonly IList<ITypeDiscriminator> typeDiscriminators;

		private readonly int maxDepthToBuffer;

		private readonly int maxLengthToBuffer;

		public TypeDiscriminatingNodeDeserializer(IList<INodeDeserializer> innerDeserializers, IList<ITypeDiscriminator> typeDiscriminators, int maxDepthToBuffer, int maxLengthToBuffer)
		{
			this.innerDeserializers = innerDeserializers;
			this.typeDiscriminators = typeDiscriminators;
			this.maxDepthToBuffer = maxDepthToBuffer;
			this.maxLengthToBuffer = maxLengthToBuffer;
		}

		public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			Type expectedType2 = expectedType;
			if (!reader.Accept<MappingStart>(out var _))
			{
				value = null;
				return false;
			}
			IEnumerable<ITypeDiscriminator> enumerable = typeDiscriminators.Where((ITypeDiscriminator t) => t.BaseType.IsAssignableFrom(expectedType2));
			if (!enumerable.Any())
			{
				value = null;
				return false;
			}
			Mark start = reader.Current.Start;
			Type expectedType3 = expectedType2;
			ParserBuffer parserBuffer;
			try
			{
				parserBuffer = new ParserBuffer(reader, maxDepthToBuffer, maxLengthToBuffer);
			}
			catch (Exception innerException)
			{
				Mark end = reader.Current.End;
				throw new YamlException(in start, in end, "Failed to buffer yaml node", innerException);
			}
			try
			{
				foreach (ITypeDiscriminator item in enumerable)
				{
					parserBuffer.Reset();
					if (item.TryDiscriminate(parserBuffer, out var suggestedType))
					{
						expectedType3 = suggestedType;
						break;
					}
				}
			}
			catch (Exception innerException2)
			{
				Mark end = reader.Current.End;
				throw new YamlException(in start, in end, "Failed to discriminate type", innerException2);
			}
			parserBuffer.Reset();
			foreach (INodeDeserializer innerDeserializer in innerDeserializers)
			{
				if (innerDeserializer.Deserialize(parserBuffer, expectedType3, nestedObjectDeserializer, out value))
				{
					return true;
				}
			}
			value = null;
			return false;
		}
	}
	internal class TypeDiscriminatingNodeDeserializerOptions : ITypeDiscriminatingNodeDeserializerOptions
	{
		internal readonly List<ITypeDiscriminator> discriminators = new List<ITypeDiscriminator>();

		public void AddTypeDiscriminator(ITypeDiscriminator discriminator)
		{
			discriminators.Add(discriminator);
		}

		public void AddKeyValueTypeDiscriminator<T>(string discriminatorKey, IDictionary<string, Type> valueTypeMapping)
		{
			discriminators.Add(new KeyValueTypeDiscriminator(typeof(T), discriminatorKey, valueTypeMapping));
		}

		public void AddUniqueKeyTypeDiscriminator<T>(IDictionary<string, Type> uniqueKeyTypeMapping)
		{
			discriminators.Add(new UniqueKeyTypeDiscriminator(typeof(T), uniqueKeyTypeMapping));
		}
	}
}
