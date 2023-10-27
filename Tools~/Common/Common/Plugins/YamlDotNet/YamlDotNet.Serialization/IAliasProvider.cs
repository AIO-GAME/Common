using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal interface IAliasProvider
	{
		AnchorName GetAlias(object target);
	}
}
