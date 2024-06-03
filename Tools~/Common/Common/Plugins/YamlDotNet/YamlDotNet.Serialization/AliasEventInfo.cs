using System;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization
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
