using System;

namespace YamlDotNet.Serialization
{
	internal abstract class EventInfo
	{
		public IObjectDescriptor Source { get; }

		protected EventInfo(IObjectDescriptor source)
		{
			Source = source ?? throw new ArgumentNullException("source");
		}
	}
}
