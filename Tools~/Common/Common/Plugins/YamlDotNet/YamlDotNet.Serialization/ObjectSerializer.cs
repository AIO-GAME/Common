using System;

namespace AIO.YamlDotNet.Serialization
{
	internal delegate void ObjectSerializer(object? value, Type? type = null);
}
