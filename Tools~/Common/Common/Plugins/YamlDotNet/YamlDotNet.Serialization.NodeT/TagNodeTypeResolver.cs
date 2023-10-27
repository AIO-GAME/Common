using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization.NodeTypeResolvers
{
	internal sealed class TagNodeTypeResolver : INodeTypeResolver
	{
		private readonly IDictionary<TagName, Type> tagMappings;

		public TagNodeTypeResolver(IDictionary<TagName, Type> tagMappings)
		{
			this.tagMappings = tagMappings ?? throw new ArgumentNullException("tagMappings");
		}

		bool INodeTypeResolver.Resolve(NodeEvent? nodeEvent, ref Type currentType)
		{
			if (nodeEvent != null && !nodeEvent.Tag.IsEmpty && tagMappings.TryGetValue(nodeEvent.Tag, out var value))
			{
				currentType = value;
				return true;
			}
			return false;
		}
	}
}
