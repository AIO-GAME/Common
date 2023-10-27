using YamlDotNet.Core;

namespace YamlDotNet.Serialization.Schemas
{
	internal sealed class DefaultSchema
	{
		internal static class Tags
		{
			public static readonly TagName Timestamp = new TagName("tag:yaml.org,2002:timestamp");
		}
	}
}
