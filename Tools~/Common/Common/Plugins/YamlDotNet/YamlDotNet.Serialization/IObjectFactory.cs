using System;
using System.Collections;
#nullable enable
namespace YamlDotNet.Serialization
{
	internal interface IObjectFactory
	{
		object Create(Type type);

		object? CreatePrimitive(Type type);

		bool GetDictionary(IObjectDescriptor descriptor, out IDictionary? dictionary, out Type[]? genericArguments);

		Type GetValueType(Type type);
	}
}
