using System;

namespace AIO.YamlDotNet.Serialization
{
	internal interface ITypeResolver
	{
		Type Resolve(Type staticType, object? actualValue);
	}
}
