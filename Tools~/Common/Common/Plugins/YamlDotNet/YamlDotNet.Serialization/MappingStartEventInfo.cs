using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization
{
	internal sealed class MappingStartEventInfo : ObjectEventInfo
	{
		public bool IsImplicit { get; set; }

		public MappingStyle Style { get; set; }

		public MappingStartEventInfo(IObjectDescriptor source)
			: base(source)
		{
		}
	}
}
