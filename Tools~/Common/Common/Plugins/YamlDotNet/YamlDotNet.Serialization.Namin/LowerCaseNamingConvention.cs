using AIO.YamlDotNet.Serialization.Utilities;

namespace AIO.YamlDotNet.Serialization.NamingConventions
{
	internal sealed class LowerCaseNamingConvention : INamingConvention
	{
		public static readonly INamingConvention Instance = new LowerCaseNamingConvention();

		private LowerCaseNamingConvention()
		{
		}

		public string Apply(string value)
		{
			return value.ToCamelCase().ToLower();
		}
	}
}
