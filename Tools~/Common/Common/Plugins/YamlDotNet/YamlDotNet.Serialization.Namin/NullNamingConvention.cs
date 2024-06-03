using System;
#pragma warning disable
namespace AIO.YamlDotNet.Serialization.NamingConventions
{
	internal sealed class NullNamingConvention : INamingConvention
	{
		public static readonly INamingConvention Instance = new NullNamingConvention();

		[Obsolete("Use the Instance static field instead of creating new instances")]
		public NullNamingConvention()
		{
		}

		public string Apply(string value)
		{
			return value;
		}
	}
}
