using System;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization
{
	internal interface IObjectDescriptor
	{
		object? Value { get; }

		Type Type { get; }

		Type StaticType { get; }

		ScalarStyle ScalarStyle { get; }
	}
}
