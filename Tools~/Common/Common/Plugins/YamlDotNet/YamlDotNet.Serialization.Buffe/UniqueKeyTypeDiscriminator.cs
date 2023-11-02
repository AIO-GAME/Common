using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;
#nullable enable
namespace AIO.YamlDotNet.Serialization.BufferedDeserialization.TypeDiscriminators
{
	internal class UniqueKeyTypeDiscriminator : ITypeDiscriminator
	{
		private readonly IDictionary<string, Type> typeMapping;

		public Type BaseType { get; private set; }

		public UniqueKeyTypeDiscriminator(Type baseType, IDictionary<string, Type> typeMapping)
		{
			foreach (KeyValuePair<string, Type> item in typeMapping)
			{
				if (!baseType.IsAssignableFrom(item.Value))
				{
					throw new ArgumentOutOfRangeException("typeMapping", $"{item.Value} is not a assignable to {baseType}");
				}
			}
			BaseType = baseType;
			this.typeMapping = typeMapping;
		}

		public bool TryDiscriminate(IParser parser, out Type? suggestedType)
		{
			if (parser.TryFindMappingEntry((Scalar scalar) => typeMapping.ContainsKey(scalar.Value), out var key, out var _))
			{
				suggestedType = typeMapping[key.Value];
				return true;
			}
			suggestedType = null;
			return false;
		}
	}
}
