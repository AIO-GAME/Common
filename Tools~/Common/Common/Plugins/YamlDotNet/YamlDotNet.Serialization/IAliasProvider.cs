using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization
{
	internal interface IAliasProvider
	{
		AnchorName GetAlias(object target);
	}
}
