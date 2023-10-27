using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.RepresentationModel
{
	internal class YamlAliasNode : YamlNode
	{
		internal override YamlNodeType NodeType => YamlNodeType.Alias;

		internal YamlAliasNode(AnchorName anchor)
		{
			base.Anchor = anchor;
		}

		internal override void ResolveAliases(DocumentLoadingState state)
		{
			throw new NotSupportedException("Resolving an alias on an alias node does not make sense");
		}

		internal override void Emit(IEmitter emitter, EmitterState state)
		{
			throw new NotSupportedException("A YamlAliasNode is an implementation detail and should never be saved.");
		}

		internal override void Accept(IYamlVisitor visitor)
		{
			throw new NotSupportedException("A YamlAliasNode is an implementation detail and should never be visited.");
		}

		public override bool Equals(object? obj)
		{
			if (obj is YamlAliasNode yamlAliasNode && Equals(yamlAliasNode))
			{
				return object.Equals(base.Anchor, yamlAliasNode.Anchor);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		internal override string ToString(RecursionLevel level)
		{
			return "*" + base.Anchor.ToString();
		}

		internal override IEnumerable<YamlNode> SafeAllNodes(RecursionLevel level)
		{
			yield return this;
		}
	}
}
