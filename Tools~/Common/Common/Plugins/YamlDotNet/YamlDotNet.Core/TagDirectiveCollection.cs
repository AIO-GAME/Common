using System.Collections.Generic;
using System.Collections.ObjectModel;
using AIO.YamlDotNet.Core.Tokens;

namespace AIO.YamlDotNet.Core
{
	internal sealed class TagDirectiveCollection : KeyedCollection<string, TagDirective>
	{
		public TagDirectiveCollection()
		{
		}

		public TagDirectiveCollection(IEnumerable<TagDirective> tagDirectives)
		{
			foreach (TagDirective tagDirective in tagDirectives)
			{
				Add(tagDirective);
			}
		}

		protected override string GetKeyForItem(TagDirective item)
		{
			return item.Handle;
		}

		public new bool Contains(TagDirective directive)
		{
			return Contains(GetKeyForItem(directive));
		}
	}
}
