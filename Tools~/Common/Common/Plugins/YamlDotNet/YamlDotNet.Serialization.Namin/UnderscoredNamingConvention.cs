using System;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NamingConventions
{
	internal sealed class UnderscoredNamingConvention : INamingConvention
	{
		public static readonly INamingConvention Instance = new UnderscoredNamingConvention();

		[Obsolete("Use the Instance static field instead of creating new instances")]
		public UnderscoredNamingConvention()
		{
		}

		public string Apply(string value)
		{
			return value.FromCamelCase("_");
		}
	}
}
