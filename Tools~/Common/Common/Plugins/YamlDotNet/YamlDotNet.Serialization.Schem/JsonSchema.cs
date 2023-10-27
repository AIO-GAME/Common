using YamlDotNet.Core;

namespace YamlDotNet.Serialization.Schemas
{
	internal sealed class JsonSchema
	{
		internal static class Tags
		{
			public static readonly TagName Null = new TagName("tag:yaml.org,2002:null");

			public static readonly TagName Bool = new TagName("tag:yaml.org,2002:bool");

			public static readonly TagName Int = new TagName("tag:yaml.org,2002:int");

			public static readonly TagName Float = new TagName("tag:yaml.org,2002:float");
		}
	}
}
