using System;

namespace AIO.YamlDotNet.Serialization
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	internal sealed class YamlIgnoreAttribute : Attribute
	{
	}
}
