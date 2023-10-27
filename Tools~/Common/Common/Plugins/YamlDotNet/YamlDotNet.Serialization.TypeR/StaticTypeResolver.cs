#nullable enable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;

namespace YamlDotNet.Serialization.TypeResolvers
{
	internal sealed class StaticTypeResolver : ITypeResolver
	{
		public Type Resolve(Type staticType, object? actualValue)
		{
			return staticType;
		}
	}
}
