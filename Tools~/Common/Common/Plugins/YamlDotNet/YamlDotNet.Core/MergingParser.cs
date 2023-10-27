using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Core
{
	internal sealed class MergingParser : IParser
	{
		private sealed class ParsingEventCollection : IEnumerable<LinkedListNode<ParsingEvent>>, IEnumerable
		{
			private readonly LinkedList<ParsingEvent> events;

			private readonly HashSet<LinkedListNode<ParsingEvent>> deleted;

			private readonly Dictionary<AnchorName, LinkedListNode<ParsingEvent>> references;

			public ParsingEventCollection()
			{
				events = new LinkedList<ParsingEvent>();
				deleted = new HashSet<LinkedListNode<ParsingEvent>>();
				references = new Dictionary<AnchorName, LinkedListNode<ParsingEvent>>();
			}

			public void AddAfter(LinkedListNode<ParsingEvent> node, IEnumerable<ParsingEvent> items)
			{
				foreach (ParsingEvent item in items)
				{
					node = events.AddAfter(node, item);
				}
			}

			public void Add(ParsingEvent item)
			{
				LinkedListNode<ParsingEvent> node = events.AddLast(item);
				AddReference(item, node);
			}

			public void MarkDeleted(LinkedListNode<ParsingEvent> node)
			{
				deleted.Add(node);
			}

			public void CleanMarked()
			{
				foreach (LinkedListNode<ParsingEvent> item in deleted)
				{
					events.Remove(item);
				}
			}

			public IEnumerable<LinkedListNode<ParsingEvent>> FromAnchor(AnchorName anchor)
			{
				LinkedListNode<ParsingEvent> next = references[anchor].Next;
				return Enumerate(next);
			}

			public IEnumerator<LinkedListNode<ParsingEvent>> GetEnumerator()
			{
				return Enumerate(events.First).GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			private IEnumerable<LinkedListNode<ParsingEvent>> Enumerate(LinkedListNode<ParsingEvent>? node)
			{
				while (node != null)
				{
					yield return node;
					node = node.Next;
				}
			}

			private void AddReference(ParsingEvent item, LinkedListNode<ParsingEvent> node)
			{
				if (item is MappingStart mappingStart)
				{
					AnchorName anchor = mappingStart.Anchor;
					if (!anchor.IsEmpty)
					{
						references[anchor] = node;
					}
				}
			}
		}

		private sealed class ParsingEventCloner : IParsingEventVisitor
		{
			private ParsingEvent? clonedEvent;

			public ParsingEvent Clone(ParsingEvent e)
			{
				e.Accept(this);
				if (clonedEvent == null)
				{
					throw new InvalidOperationException($"Could not clone event of type '{e.Type}'");
				}
				return clonedEvent;
			}

			void IParsingEventVisitor.Visit(AnchorAlias e)
			{
				clonedEvent = new AnchorAlias(e.Value, e.Start, e.End);
			}

			void IParsingEventVisitor.Visit(StreamStart e)
			{
				throw new NotSupportedException();
			}

			void IParsingEventVisitor.Visit(StreamEnd e)
			{
				throw new NotSupportedException();
			}

			void IParsingEventVisitor.Visit(DocumentStart e)
			{
				throw new NotSupportedException();
			}

			void IParsingEventVisitor.Visit(DocumentEnd e)
			{
				throw new NotSupportedException();
			}

			void IParsingEventVisitor.Visit(Scalar e)
			{
				clonedEvent = new Scalar(AnchorName.Empty, e.Tag, e.Value, e.Style, e.IsPlainImplicit, e.IsQuotedImplicit, e.Start, e.End);
			}

			void IParsingEventVisitor.Visit(SequenceStart e)
			{
				clonedEvent = new SequenceStart(AnchorName.Empty, e.Tag, e.IsImplicit, e.Style, e.Start, e.End);
			}

			void IParsingEventVisitor.Visit(SequenceEnd e)
			{
				Mark start = e.Start;
				Mark end = e.End;
				clonedEvent = new SequenceEnd(in start, in end);
			}

			void IParsingEventVisitor.Visit(MappingStart e)
			{
				clonedEvent = new MappingStart(AnchorName.Empty, e.Tag, e.IsImplicit, e.Style, e.Start, e.End);
			}

			void IParsingEventVisitor.Visit(MappingEnd e)
			{
				Mark start = e.Start;
				Mark end = e.End;
				clonedEvent = new MappingEnd(in start, in end);
			}

			void IParsingEventVisitor.Visit(Comment e)
			{
				throw new NotSupportedException();
			}
		}

		private readonly ParsingEventCollection events;

		private readonly IParser innerParser;

		private IEnumerator<LinkedListNode<ParsingEvent>> iterator;

		private bool merged;

		public ParsingEvent? Current => iterator.Current?.Value;

		public MergingParser(IParser innerParser)
		{
			events = new ParsingEventCollection();
			merged = false;
			iterator = events.GetEnumerator();
			this.innerParser = innerParser;
		}

		public bool MoveNext()
		{
			if (!merged)
			{
				Merge();
				events.CleanMarked();
				iterator = events.GetEnumerator();
				merged = true;
			}
			return iterator.MoveNext();
		}

		private void Merge()
		{
			while (innerParser.MoveNext())
			{
				events.Add(innerParser.Current);
			}
			foreach (LinkedListNode<ParsingEvent> @event in events)
			{
				if (IsMergeToken(@event))
				{
					events.MarkDeleted(@event);
					if (!HandleMerge(@event.Next))
					{
						Mark start = @event.Value.Start;
						Mark end = @event.Value.End;
						throw new SemanticErrorException(in start, in end, "Unrecognized merge key pattern");
					}
				}
			}
		}

		private bool HandleMerge(LinkedListNode<ParsingEvent>? node)
		{
			if (node == null)
			{
				return false;
			}
			if (node.Value is AnchorAlias anchorAlias)
			{
				return HandleAnchorAlias(node, node, anchorAlias);
			}
			if (node.Value is SequenceStart)
			{
				return HandleSequence(node);
			}
			return false;
		}

		private bool HandleMergeSequence(LinkedListNode<ParsingEvent> sequenceStart, LinkedListNode<ParsingEvent>? node)
		{
			if (node == null)
			{
				return false;
			}
			if (node.Value is AnchorAlias anchorAlias)
			{
				return HandleAnchorAlias(sequenceStart, node, anchorAlias);
			}
			if (node.Value is SequenceStart)
			{
				return HandleSequence(node);
			}
			return false;
		}

		private bool IsMergeToken(LinkedListNode<ParsingEvent> node)
		{
			if (node.Value is Scalar scalar)
			{
				return scalar.Value == "<<";
			}
			return false;
		}

		private bool HandleAnchorAlias(LinkedListNode<ParsingEvent> node, LinkedListNode<ParsingEvent> anchorNode, AnchorAlias anchorAlias)
		{
			IEnumerable<ParsingEvent> mappingEvents = GetMappingEvents(anchorAlias.Value);
			events.AddAfter(node, mappingEvents);
			events.MarkDeleted(anchorNode);
			return true;
		}

		private bool HandleSequence(LinkedListNode<ParsingEvent> node)
		{
			events.MarkDeleted(node);
			LinkedListNode<ParsingEvent> linkedListNode = node;
			while (linkedListNode != null)
			{
				if (linkedListNode.Value is SequenceEnd && linkedListNode.Value.Start.Line >= node.Value.Start.Line)
				{
					events.MarkDeleted(linkedListNode);
					return true;
				}
				LinkedListNode<ParsingEvent> next = linkedListNode.Next;
				HandleMergeSequence(node, next);
				linkedListNode = next;
			}
			return true;
		}

		private IEnumerable<ParsingEvent> GetMappingEvents(AnchorName anchor)
		{
			ParsingEventCloner cloner = new ParsingEventCloner();
			int nesting = 0;
			return from e in (from e in events.FromAnchor(anchor)
					select e.Value).TakeWhile((ParsingEvent e) => (nesting += e.NestingIncrease) >= 0)
				select cloner.Clone(e);
		}
	}
}
