using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{
	internal sealed class SequenceStartEventInfo : ObjectEventInfo
	{
		public bool IsImplicit { get; set; }

		public SequenceStyle Style { get; set; }

		public SequenceStartEventInfo(IObjectDescriptor source)
			: base(source)
		{
		}
	}
}
