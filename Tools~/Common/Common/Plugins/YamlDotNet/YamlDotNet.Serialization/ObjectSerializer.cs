using System;

namespace YamlDotNet.Serialization
{
	internal delegate void ObjectSerializer(object? value, Type? type = null);
}
