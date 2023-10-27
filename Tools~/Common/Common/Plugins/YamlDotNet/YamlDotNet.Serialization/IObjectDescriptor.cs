using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal interface IObjectDescriptor
	{
		object? Value { get; }

		Type Type { get; }

		Type StaticType { get; }

		ScalarStyle ScalarStyle { get; }
	}
}
