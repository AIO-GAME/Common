using System;

namespace YamlDotNet.Serialization
{
	internal interface ITypeResolver
	{
		Type Resolve(Type staticType, object? actualValue);
	}
}
