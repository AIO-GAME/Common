using System;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization.NodeTypeResolvers
{
	internal sealed class YamlSerializableTypeResolver : INodeTypeResolver
	{
		public bool Resolve(NodeEvent? nodeEvent, ref Type currentType)
		{
			return typeof(IYamlSerializable).IsAssignableFrom(currentType);
		}
	}
}
