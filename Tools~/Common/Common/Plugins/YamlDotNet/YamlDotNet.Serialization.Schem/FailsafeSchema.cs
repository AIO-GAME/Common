using YamlDotNet.Core;

namespace YamlDotNet.Serialization.Schemas
{
	internal sealed class FailsafeSchema
	{
		internal static class Tags
		{
			public static readonly TagName Map = new TagName("tag:yaml.org,2002:map");

			public static readonly TagName Seq = new TagName("tag:yaml.org,2002:seq");

			public static readonly TagName Str = new TagName("tag:yaml.org,2002:str");
		}
	}
}
