using System;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NamingConventions
{
	internal sealed class CamelCaseNamingConvention : INamingConvention
	{
		public static readonly INamingConvention Instance = new CamelCaseNamingConvention();

		[Obsolete("Use the Instance static field instead of creating new instances")]
		public CamelCaseNamingConvention()
		{
		}

		public string Apply(string value)
		{
			return value.ToCamelCase();
		}
	}
}
