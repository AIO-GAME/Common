using System;
using AIO.YamlDotNet.Serialization.Utilities;

namespace AIO.YamlDotNet.Serialization.NamingConventions
{
	internal sealed class PascalCaseNamingConvention : INamingConvention
	{
		public static readonly INamingConvention Instance = new PascalCaseNamingConvention();

		[Obsolete("Use the Instance static field instead of creating new instances")]
		public PascalCaseNamingConvention()
		{
		}

		public string Apply(string value)
		{
			return value.ToPascalCase();
		}
	}
}
