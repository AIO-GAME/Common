using System;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization.BufferedDeserialization.TypeDiscriminators
{
	internal interface ITypeDiscriminator
	{
		Type BaseType { get; }

		bool TryDiscriminate(IParser buffer, out Type? suggestedType);
	}
}
