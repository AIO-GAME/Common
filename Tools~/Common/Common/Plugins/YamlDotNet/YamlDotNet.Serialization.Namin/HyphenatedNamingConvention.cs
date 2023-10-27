using System;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NamingConventions
{
	internal sealed class HyphenatedNamingConvention : INamingConvention
	{
		public static readonly INamingConvention Instance = new HyphenatedNamingConvention();

		[Obsolete("Use the Instance static field instead of creating new instances")]
		public HyphenatedNamingConvention()
		{
		}

		public string Apply(string value)
		{
			return value.FromCamelCase("-");
		}
	}
}
