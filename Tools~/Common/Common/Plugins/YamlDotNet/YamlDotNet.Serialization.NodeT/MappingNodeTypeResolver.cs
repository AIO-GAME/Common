using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization.NodeTypeResolvers
{
	internal class MappingNodeTypeResolver : INodeTypeResolver
	{
		private readonly IDictionary<Type, Type> _mappings;

		public MappingNodeTypeResolver(IDictionary<Type, Type> mappings)
		{
			if (mappings == null)
			{
				throw new ArgumentNullException("mappings");
			}
			foreach (KeyValuePair<Type, Type> mapping in mappings)
			{
				if (!mapping.Key.IsAssignableFrom(mapping.Value))
				{
					throw new InvalidOperationException($"Type '{mapping.Value}' does not implement type '{mapping.Key}'.");
				}
			}
			_mappings = mappings;
		}

		public bool Resolve(NodeEvent? nodeEvent, ref Type currentType)
		{
			if (_mappings.TryGetValue(currentType, out var value))
			{
				currentType = value;
				return true;
			}
			return false;
		}
	}
}
