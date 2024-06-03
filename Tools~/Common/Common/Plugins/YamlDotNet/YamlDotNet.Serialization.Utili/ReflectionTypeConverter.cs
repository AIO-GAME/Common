#nullable enable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace AIO.YamlDotNet.Serialization.Utilities
{
	internal class ReflectionTypeConverter : ITypeConverter
	{
		public object? ChangeType(object? value, Type expectedType)
		{
			return TypeConverter.ChangeType(value, expectedType);
		}
	}
}
