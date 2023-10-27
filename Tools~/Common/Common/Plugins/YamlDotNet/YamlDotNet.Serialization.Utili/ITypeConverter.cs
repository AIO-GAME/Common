#nullable enable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace AIO.YamlDotNet.Serialization.Utilities
{
	internal interface ITypeConverter
	{
		object? ChangeType(object? value, Type expectedType);
	}
}
