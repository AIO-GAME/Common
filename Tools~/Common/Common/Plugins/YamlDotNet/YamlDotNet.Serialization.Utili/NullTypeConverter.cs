#nullable enable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace YamlDotNet.Serialization.Utilities
{
	internal class NullTypeConverter : ITypeConverter
	{
		public object? ChangeType(object? value, Type expectedType)
		{
			return value;
		}
	}
}
