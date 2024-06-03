#nullable enable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization.Utilities
{
	internal sealed class ObjectAnchorCollection
	{
		private readonly IDictionary<string, object> objectsByAnchor = new Dictionary<string, object>();

		private readonly IDictionary<object, string> anchorsByObject = new Dictionary<object, string>();

		public object this[string anchor]
		{
			get
			{
				if (objectsByAnchor.TryGetValue(anchor, out var value))
				{
					return value;
				}
				throw new AnchorNotFoundException("The anchor '" + anchor + "' does not exists");
			}
		}

		public void Add(string anchor, object @object)
		{
			objectsByAnchor.Add(anchor, @object);
			if (@object != null)
			{
				anchorsByObject.Add(@object, anchor);
			}
		}

		public bool TryGetAnchor(object @object, [MaybeNullWhen(false)] out string? anchor)
		{
			return anchorsByObject.TryGetValue(@object, out anchor);
		}
	}
}
