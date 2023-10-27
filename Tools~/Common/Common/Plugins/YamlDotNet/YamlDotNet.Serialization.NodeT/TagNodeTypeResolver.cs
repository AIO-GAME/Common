using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeTypeResolvers
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
