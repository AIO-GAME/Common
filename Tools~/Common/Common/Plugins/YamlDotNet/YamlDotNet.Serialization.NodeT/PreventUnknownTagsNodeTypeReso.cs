using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeTypeResolvers
{
	internal class PreventUnknownTagsNodeTypeResolver : INodeTypeResolver
	{
		bool INodeTypeResolver.Resolve(NodeEvent? nodeEvent, ref Type currentType)
		{
			if (nodeEvent != null && !nodeEvent.Tag.IsEmpty)
			{
				Mark start = nodeEvent.Start;
				Mark end = nodeEvent.End;
				throw new YamlException(in start, in end, $"Encountered an unresolved tag '{nodeEvent.Tag}'");
			}
			return false;
		}
	}
}
