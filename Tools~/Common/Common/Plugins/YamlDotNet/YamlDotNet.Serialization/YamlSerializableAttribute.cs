using System;

namespace YamlDotNet.Serialization
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	internal sealed class YamlSerializableAttribute : Attribute
	{
	}
}
