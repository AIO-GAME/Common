using System;
using System.Collections.Generic;
using System.Globalization;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.RepresentationModel
{
	internal class YamlDocument
	{
		private class AnchorAssigningVisitor : YamlVisitorBase
		{
			private readonly HashSet<AnchorName> existingAnchors = new HashSet<AnchorName>();

			private readonly Dictionary<YamlNode, bool> visitedNodes = new Dictionary<YamlNode, bool>();

			public void AssignAnchors(YamlDocument document)
			{
				existingAnchors.Clear();
				visitedNodes.Clear();
				document.Accept(this);
				Random random = new Random();
				foreach (KeyValuePair<YamlNode, bool> visitedNode in visitedNodes)
				{
					if (!visitedNode.Value)
					{
						continue;
					}
					AnchorName anchorName;
					if (!visitedNode.Key.Anchor.IsEmpty && !existingAnchors.Contains(visitedNode.Key.Anchor))
					{
						anchorName = visitedNode.Key.Anchor;
					}
					else
					{
						do
						{
							anchorName = new AnchorName(random.Next().ToString(CultureInfo.InvariantCulture));
						}
						while (existingAnchors.Contains(anchorName));
					}
					existingAnchors.Add(anchorName);
					visitedNode.Key.Anchor = anchorName;
				}
			}

			private bool VisitNodeAndFindDuplicates(YamlNode node)
			{
				if (visitedNodes.TryGetValue(node, out var value))
				{
					if (!value)
					{
						visitedNodes[node] = true;
					}
					return !value;
				}
				visitedNodes.Add(node, value: false);
				return false;
			}

			public override void Visit(YamlScalarNode scalar)
			{
				VisitNodeAndFindDuplicates(scalar);
			}

			public override void Visit(YamlMappingNode mapping)
			{
				if (!VisitNodeAndFindDuplicates(mapping))
				{
					base.Visit(mapping);
				}
			}

			public override void Visit(YamlSequenceNode sequence)
			{
				if (!VisitNodeAndFindDuplicates(sequence))
				{
					base.Visit(sequence);
				}
			}
		}

		public YamlNode RootNode { get; private set; }

		public IEnumerable<YamlNode> AllNodes => RootNode.AllNodes;

		public YamlDocument(YamlNode rootNode)
		{
			RootNode = rootNode;
		}

		public YamlDocument(string rootNode)
		{
			RootNode = new YamlScalarNode(rootNode);
		}

		internal YamlDocument(IParser parser)
		{
			DocumentLoadingState documentLoadingState = new DocumentLoadingState();
			parser.Consume<DocumentStart>();
			DocumentEnd @event;
			while (!parser.TryConsume<DocumentEnd>(out @event))
			{
				RootNode = YamlNode.ParseNode(parser, documentLoadingState);
				if (RootNode is YamlAliasNode)
				{
					throw new YamlException("A document cannot contain only an alias");
				}
			}
			documentLoadingState.ResolveAliases();
			if (RootNode == null)
			{
				throw new ArgumentException("Atempted to parse an empty document");
			}
		}

		private void AssignAnchors()
		{
			new AnchorAssigningVisitor().AssignAnchors(this);
		}

		internal void Save(IEmitter emitter, bool assignAnchors = true)
		{
			if (assignAnchors)
			{
				AssignAnchors();
			}
			emitter.Emit(new DocumentStart());
			RootNode.Save(emitter, new EmitterState());
			emitter.Emit(new DocumentEnd(isImplicit: false));
		}

		public void Accept(IYamlVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
