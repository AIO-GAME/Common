using System;

namespace YamlDotNet.Serialization
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	internal sealed class YamlIgnoreAttribute : Attribute
	{
	}
}
