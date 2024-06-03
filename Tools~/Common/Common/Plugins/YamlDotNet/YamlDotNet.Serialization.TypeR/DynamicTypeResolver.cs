#nullable enable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace AIO.YamlDotNet.Serialization.TypeResolvers
{
	internal sealed class DynamicTypeResolver : ITypeResolver
	{
		public Type Resolve(Type staticType, object? actualValue)
		{
			if (actualValue == null)
			{
				return staticType;
			}
			return actualValue.GetType();
		}
	}
}
