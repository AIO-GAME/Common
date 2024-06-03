using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AIO.YamlDotNet.Serialization
{
	internal interface ITypeInspector
	{
		IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container);

		IPropertyDescriptor GetProperty(Type type, object? container, string name, [MaybeNullWhen(true)] bool ignoreUnmatched);
	}
}
