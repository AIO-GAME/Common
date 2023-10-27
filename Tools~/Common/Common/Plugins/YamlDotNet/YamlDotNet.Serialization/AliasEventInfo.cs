using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal class AliasEventInfo : EventInfo
	{
		public AnchorName Alias { get; }

		public bool NeedsExpansion { get; set; }

		public AliasEventInfo(IObjectDescriptor source, AnchorName alias)
			: base(source)
		{
			if (alias.IsEmpty)
			{
				throw new ArgumentNullException("alias");
			}
			Alias = alias;
		}
	}
}
