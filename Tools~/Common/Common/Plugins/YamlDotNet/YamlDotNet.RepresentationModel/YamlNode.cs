using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.RepresentationModel
{
	internal abstract class YamlNode
	{
		private const int MaximumRecursionLevel = 1000;

		internal const string MaximumRecursionLevelReachedToStringValue = "WARNING! INFINITE RECURSION!";

		public AnchorName Anchor { get; set; }

		public TagName Tag { get; set; }

		public Mark Start { get; private set; } = Mark.Empty;


		public Mark End { get; private set; } = Mark.Empty;


		public IEnumerable<YamlNode> AllNodes
		{
			get
			{
				RecursionLevel level = new RecursionLevel(1000);
				return SafeAllNodes(level);
			}
		}

		internal abstract YamlNodeType NodeType { get; }

		public YamlNode this[int index]
		{
			get
			{
				if (!(this is YamlSequenceNode yamlSequenceNode))
				{
					throw new ArgumentException($"Accessed '{NodeType}' with an invalid index: {index}. Only Sequences can be indexed by number.");
				}
				return yamlSequenceNode.Children[index];
			}
		}

		public YamlNode this[YamlNode key]
		{
			get
			{
				if (!(this is YamlMappingNode yamlMappingNode))
				{
					throw new ArgumentException($"Accessed '{NodeType}' with an invalid index: {key}. Only Mappings can be indexed by key.");
				}
				return yamlMappingNode.Children[key];
			}
		}

		internal void Load(NodeEvent yamlEvent, DocumentLoadingState state)
		{
			Tag = yamlEvent.Tag;
			if (!yamlEvent.Anchor.IsEmpty)
			{
				Anchor = yamlEvent.Anchor;
				state.AddAnchor(this);
			}
			Start = yamlEvent.Start;
			End = yamlEvent.End;
		}

		internal static YamlNode ParseNode(IParser parser, DocumentLoadingState state)
		{
			if (parser.Accept<Scalar>(out var _))
			{
				return new YamlScalarNode(parser, state);
			}
			if (parser.Accept<SequenceStart>(out var _))
			{
				return new YamlSequenceNode(parser, state);
			}
			if (parser.Accept<MappingStart>(out var _))
			{
				return new YamlMappingNode(parser, state);
			}
			if (parser.TryConsume<AnchorAlias>(out var event4))
			{
				if (!state.TryGetNode(event4.Value, out var node))
				{
					return new YamlAliasNode(event4.Value);
				}
				return node;
			}
			throw new ArgumentException("The current event is of an unsupported type.", "parser");
		}

		internal abstract void ResolveAliases(DocumentLoadingState state);

		internal void Save(IEmitter emitter, EmitterState state)
		{
			if (!Anchor.IsEmpty && !state.EmittedAnchors.Add(Anchor))
			{
				emitter.Emit(new AnchorAlias(Anchor));
			}
			else
			{
				Emit(emitter, state);
			}
		}

		internal abstract void Emit(IEmitter emitter, EmitterState state);

		internal abstract void Accept(IYamlVisitor visitor);

		public override string ToString()
		{
			RecursionLevel recursionLevel = new RecursionLevel(1000);
			return ToString(recursionLevel);
		}

		internal abstract string ToString(RecursionLevel level);

		internal abstract IEnumerable<YamlNode> SafeAllNodes(RecursionLevel level);

		public static implicit operator YamlNode(string value)
		{
			return new YamlScalarNode(value);
		}

		public static implicit operator YamlNode(string[] sequence)
		{
			return new YamlSequenceNode(((IEnumerable<string>)sequence).Select((Func<string, YamlNode>)((string i) => i)));
		}

		public static explicit operator string?(YamlNode node)
		{
			if (!(node is YamlScalarNode yamlScalarNode))
			{
				throw new ArgumentException($"Attempted to convert a '{node.NodeType}' to string. This conversion is valid only for Scalars.");
			}
			return yamlScalarNode.Value;
		}
	}
}
