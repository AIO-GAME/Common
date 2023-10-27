using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	internal class ObjectEventInfo : EventInfo
	{
		public AnchorName Anchor { get; set; }

		public TagName Tag { get; set; }

		protected ObjectEventInfo(IObjectDescriptor source)
			: base(source)
		{
		}
	}
}
