using AIO.YamlDotNet.Core.Tokens;

namespace AIO.YamlDotNet.Core
{
	internal static class Constants
	{
		public static readonly TagDirective[] DefaultTagDirectives = new TagDirective[2]
		{
			new TagDirective("!", "!"),
			new TagDirective("!!", "tag:yaml.org,2002:")
		};

		public const int MajorVersion = 1;

		public const int MinorVersion = 3;
	}
}
