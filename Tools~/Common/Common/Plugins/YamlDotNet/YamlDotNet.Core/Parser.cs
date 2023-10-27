using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Core.Events;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core
{
	internal class Parser : IParser
	{
		private class EventQueue
		{
			private readonly Queue<ParsingEvent> highPriorityEvents = new Queue<ParsingEvent>();

			private readonly Queue<ParsingEvent> normalPriorityEvents = new Queue<ParsingEvent>();

			public int Count => highPriorityEvents.Count + normalPriorityEvents.Count;

			public void Enqueue(ParsingEvent @event)
			{
				EventType type = @event.Type;
				if (type == EventType.StreamStart || type == EventType.DocumentStart)
				{
					highPriorityEvents.Enqueue(@event);
				}
				else
				{
					normalPriorityEvents.Enqueue(@event);
				}
			}

			public ParsingEvent Dequeue()
			{
				if (highPriorityEvents.Count <= 0)
				{
					return normalPriorityEvents.Dequeue();
				}
				return highPriorityEvents.Dequeue();
			}
		}

		private readonly Stack<ParserState> states = new Stack<ParserState>();

		private readonly TagDirectiveCollection tagDirectives = new TagDirectiveCollection();

		private ParserState state;

		private readonly IScanner scanner;

		private Token? currentToken;

		private VersionDirective? version;

		private readonly EventQueue pendingEvents = new EventQueue();

		public ParsingEvent? Current { get; private set; }

		private Token? GetCurrentToken()
		{
			if (currentToken == null)
			{
				while (scanner.MoveNextWithoutConsuming())
				{
					currentToken = scanner.Current;
					if (!(currentToken is YamlDotNet.Core.Tokens.Comment comment))
					{
						break;
					}
					pendingEvents.Enqueue(new YamlDotNet.Core.Events.Comment(comment.Value, comment.IsInline, comment.Start, comment.End));
					scanner.ConsumeCurrent();
				}
			}
			return currentToken;
		}

		public Parser(TextReader input)
			: this(new Scanner(input))
		{
		}

		public Parser(IScanner scanner)
		{
			this.scanner = scanner;
		}

		public bool MoveNext()
		{
			if (state == ParserState.StreamEnd)
			{
				Current = null;
				return false;
			}
			if (pendingEvents.Count == 0)
			{
				pendingEvents.Enqueue(StateMachine());
			}
			Current = pendingEvents.Dequeue();
			return true;
		}

		private ParsingEvent StateMachine()
		{
			return state switch
			{
				ParserState.StreamStart => ParseStreamStart(), 
				ParserState.ImplicitDocumentStart => ParseDocumentStart(isImplicit: true), 
				ParserState.DocumentStart => ParseDocumentStart(isImplicit: false), 
				ParserState.DocumentContent => ParseDocumentContent(), 
				ParserState.DocumentEnd => ParseDocumentEnd(), 
				ParserState.BlockNode => ParseNode(isBlock: true, isIndentlessSequence: false), 
				ParserState.BlockNodeOrIndentlessSequence => ParseNode(isBlock: true, isIndentlessSequence: true), 
				ParserState.FlowNode => ParseNode(isBlock: false, isIndentlessSequence: false), 
				ParserState.BlockSequenceFirstEntry => ParseBlockSequenceEntry(isFirst: true), 
				ParserState.BlockSequenceEntry => ParseBlockSequenceEntry(isFirst: false), 
				ParserState.IndentlessSequenceEntry => ParseIndentlessSequenceEntry(), 
				ParserState.BlockMappingFirstKey => ParseBlockMappingKey(isFirst: true), 
				ParserState.BlockMappingKey => ParseBlockMappingKey(isFirst: false), 
				ParserState.BlockMappingValue => ParseBlockMappingValue(), 
				ParserState.FlowSequenceFirstEntry => ParseFlowSequenceEntry(isFirst: true), 
				ParserState.FlowSequenceEntry => ParseFlowSequenceEntry(isFirst: false), 
				ParserState.FlowSequenceEntryMappingKey => ParseFlowSequenceEntryMappingKey(), 
				ParserState.FlowSequenceEntryMappingValue => ParseFlowSequenceEntryMappingValue(), 
				ParserState.FlowSequenceEntryMappingEnd => ParseFlowSequenceEntryMappingEnd(), 
				ParserState.FlowMappingFirstKey => ParseFlowMappingKey(isFirst: true), 
				ParserState.FlowMappingKey => ParseFlowMappingKey(isFirst: false), 
				ParserState.FlowMappingValue => ParseFlowMappingValue(isEmpty: false), 
				ParserState.FlowMappingEmptyValue => ParseFlowMappingValue(isEmpty: true), 
				_ => throw new InvalidOperationException(), 
			};
		}

		private void Skip()
		{
			if (currentToken != null)
			{
				currentToken = null;
				scanner.ConsumeCurrent();
			}
		}

		private ParsingEvent ParseStreamStart()
		{
			Token token = GetCurrentToken();
			Mark start;
			Mark end;
			if (!(token is YamlDotNet.Core.Tokens.StreamStart streamStart))
			{
				start = token?.Start ?? Mark.Empty;
				end = token?.End ?? Mark.Empty;
				throw new SemanticErrorException(in start, in end, "Did not find expected <stream-start>.");
			}
			Skip();
			state = ParserState.ImplicitDocumentStart;
			start = streamStart.Start;
			end = streamStart.End;
			return new YamlDotNet.Core.Events.StreamStart(in start, in end);
		}

		private ParsingEvent ParseDocumentStart(bool isImplicit)
		{
			if (currentToken is VersionDirective)
			{
				throw new SyntaxErrorException("While parsing a document start node, could not find document end marker before version directive.");
			}
			Token token = GetCurrentToken();
			if (!isImplicit)
			{
				while (token is YamlDotNet.Core.Tokens.DocumentEnd)
				{
					Skip();
					token = GetCurrentToken();
				}
			}
			if (token == null)
			{
				throw new SyntaxErrorException("Reached the end of the stream while parsing a document start.");
			}
			if (token is YamlDotNet.Core.Tokens.Scalar && (state == ParserState.ImplicitDocumentStart || state == ParserState.DocumentStart))
			{
				isImplicit = true;
			}
			if ((isImplicit && !(token is VersionDirective) && !(token is TagDirective) && !(token is YamlDotNet.Core.Tokens.DocumentStart) && !(token is YamlDotNet.Core.Tokens.StreamEnd) && !(token is YamlDotNet.Core.Tokens.DocumentEnd)) || token is BlockMappingStart)
			{
				TagDirectiveCollection tags = new TagDirectiveCollection();
				ProcessDirectives(tags);
				states.Push(ParserState.DocumentEnd);
				state = ParserState.BlockNode;
				return new YamlDotNet.Core.Events.DocumentStart(null, tags, isImplicit: true, token.Start, token.End);
			}
			Mark start2;
			Mark end;
			if (!(token is YamlDotNet.Core.Tokens.StreamEnd) && !(token is YamlDotNet.Core.Tokens.DocumentEnd))
			{
				Mark start = token.Start;
				TagDirectiveCollection tags2 = new TagDirectiveCollection();
				VersionDirective? versionDirective = ProcessDirectives(tags2);
				token = GetCurrentToken() ?? throw new SemanticErrorException("Reached the end of the stream while parsing a document start");
				if (!(token is YamlDotNet.Core.Tokens.DocumentStart))
				{
					start2 = token.Start;
					end = token.End;
					throw new SemanticErrorException(in start2, in end, "Did not find expected <document start>.");
				}
				states.Push(ParserState.DocumentEnd);
				state = ParserState.DocumentContent;
				Mark end2 = token.End;
				Skip();
				return new YamlDotNet.Core.Events.DocumentStart(versionDirective, tags2, isImplicit: false, start, end2);
			}
			if (token is YamlDotNet.Core.Tokens.DocumentEnd)
			{
				Skip();
			}
			state = ParserState.StreamEnd;
			token = GetCurrentToken() ?? throw new SemanticErrorException("Reached the end of the stream while parsing a document start");
			start2 = token.Start;
			end = token.End;
			YamlDotNet.Core.Events.StreamEnd result = new YamlDotNet.Core.Events.StreamEnd(in start2, in end);
			if (scanner.MoveNextWithoutConsuming())
			{
				throw new InvalidOperationException("The scanner should contain no more tokens.");
			}
			return result;
		}

		private VersionDirective? ProcessDirectives(TagDirectiveCollection tags)
		{
			bool flag = false;
			VersionDirective result = null;
			while (true)
			{
				if (GetCurrentToken() is VersionDirective versionDirective)
				{
					if (version != null)
					{
						Mark start = versionDirective.Start;
						Mark end = versionDirective.End;
						throw new SemanticErrorException(in start, in end, "Found duplicate %YAML directive.");
					}
					if (versionDirective.Version.Major != 1 || versionDirective.Version.Minor > 3)
					{
						Mark start = versionDirective.Start;
						Mark end = versionDirective.End;
						throw new SemanticErrorException(in start, in end, "Found incompatible YAML document.");
					}
					result = (version = versionDirective);
					flag = true;
				}
				else
				{
					if (!(GetCurrentToken() is TagDirective tagDirective))
					{
						break;
					}
					if (tags.Contains(tagDirective.Handle))
					{
						Mark start = tagDirective.Start;
						Mark end = tagDirective.End;
						throw new SemanticErrorException(in start, in end, "Found duplicate %TAG directive.");
					}
					tags.Add(tagDirective);
					flag = true;
				}
				Skip();
			}
			if (GetCurrentToken() is YamlDotNet.Core.Tokens.DocumentStart && (version == null || (version.Version.Major == 1 && version.Version.Minor > 1)))
			{
				if (GetCurrentToken() is YamlDotNet.Core.Tokens.DocumentStart && version == null)
				{
					version = new VersionDirective(new Version(1, 2));
				}
				flag = true;
			}
			AddTagDirectives(tags, Constants.DefaultTagDirectives);
			if (flag)
			{
				tagDirectives.Clear();
			}
			AddTagDirectives(tagDirectives, tags);
			return result;
		}

		private static void AddTagDirectives(TagDirectiveCollection directives, IEnumerable<TagDirective> source)
		{
			foreach (TagDirective item in source)
			{
				if (!directives.Contains(item))
				{
					directives.Add(item);
				}
			}
		}

		private ParsingEvent ParseDocumentContent()
		{
			if (GetCurrentToken() is VersionDirective || GetCurrentToken() is TagDirective || GetCurrentToken() is YamlDotNet.Core.Tokens.DocumentStart || GetCurrentToken() is YamlDotNet.Core.Tokens.DocumentEnd || GetCurrentToken() is YamlDotNet.Core.Tokens.StreamEnd)
			{
				state = states.Pop();
				Mark position = scanner.CurrentPosition;
				return ProcessEmptyScalar(in position);
			}
			return ParseNode(isBlock: true, isIndentlessSequence: false);
		}

		private static ParsingEvent ProcessEmptyScalar(in Mark position)
		{
			return new YamlDotNet.Core.Events.Scalar(AnchorName.Empty, TagName.Empty, string.Empty, ScalarStyle.Plain, isPlainImplicit: true, isQuotedImplicit: false, position, position);
		}

		private ParsingEvent ParseNode(bool isBlock, bool isIndentlessSequence)
		{
			Mark start;
			Mark end;
			if (GetCurrentToken() is Error error)
			{
				start = error.Start;
				end = error.End;
				throw new SemanticErrorException(in start, in end, error.Value);
			}
			Token token = GetCurrentToken() ?? throw new SemanticErrorException("Reached the end of the stream while parsing a node");
			if (token is YamlDotNet.Core.Tokens.AnchorAlias anchorAlias)
			{
				state = states.Pop();
				YamlDotNet.Core.Events.AnchorAlias result = new YamlDotNet.Core.Events.AnchorAlias(anchorAlias.Value, anchorAlias.Start, anchorAlias.End);
				Skip();
				return result;
			}
			Mark start2 = token.Start;
			AnchorName anchor = AnchorName.Empty;
			TagName tag = TagName.Empty;
			Anchor anchor2 = null;
			Tag tag2 = null;
			while (true)
			{
				if (anchor.IsEmpty && token is Anchor anchor3)
				{
					anchor2 = anchor3;
					anchor = anchor3.Value;
					Skip();
				}
				else
				{
					if (!tag.IsEmpty || !(token is Tag tag3))
					{
						if (token is Anchor anchor4)
						{
							start = anchor4.Start;
							end = anchor4.End;
							throw new SemanticErrorException(in start, in end, "While parsing a node, found more than one anchor.");
						}
						if (token is YamlDotNet.Core.Tokens.AnchorAlias anchorAlias2)
						{
							start = anchorAlias2.Start;
							end = anchorAlias2.End;
							throw new SemanticErrorException(in start, in end, "While parsing a node, did not find expected token.");
						}
						if (!(token is Error error2))
						{
							break;
						}
						if (tag2 != null && anchor2 != null && !anchor.IsEmpty)
						{
							return new YamlDotNet.Core.Events.Scalar(anchor, default(TagName), string.Empty, ScalarStyle.Any, isPlainImplicit: false, isQuotedImplicit: false, anchor2.Start, anchor2.End);
						}
						start = error2.Start;
						end = error2.End;
						throw new SemanticErrorException(in start, in end, error2.Value);
					}
					tag2 = tag3;
					if (string.IsNullOrEmpty(tag3.Handle))
					{
						tag = new TagName(tag3.Suffix);
					}
					else
					{
						if (!tagDirectives.Contains(tag3.Handle))
						{
							start = tag3.Start;
							end = tag3.End;
							throw new SemanticErrorException(in start, in end, "While parsing a node, found undefined tag handle.");
						}
						tag = new TagName(tagDirectives[tag3.Handle].Prefix + tag3.Suffix);
					}
					Skip();
				}
				token = GetCurrentToken() ?? throw new SemanticErrorException("Reached the end of the stream while parsing a node");
			}
			bool isEmpty = tag.IsEmpty;
			if (isIndentlessSequence && GetCurrentToken() is BlockEntry)
			{
				state = ParserState.IndentlessSequenceEntry;
				return new SequenceStart(anchor, tag, isEmpty, SequenceStyle.Block, start2, token.End);
			}
			if (token is YamlDotNet.Core.Tokens.Scalar scalar)
			{
				bool isPlainImplicit = false;
				bool isQuotedImplicit = false;
				if ((scalar.Style == ScalarStyle.Plain && tag.IsEmpty) || tag.IsNonSpecific)
				{
					isPlainImplicit = true;
				}
				else if (tag.IsEmpty)
				{
					isQuotedImplicit = true;
				}
				state = states.Pop();
				Skip();
				YamlDotNet.Core.Events.Scalar result2 = new YamlDotNet.Core.Events.Scalar(anchor, tag, scalar.Value, scalar.Style, isPlainImplicit, isQuotedImplicit, start2, scalar.End, scalar.IsKey);
				if (!anchor.IsEmpty && scanner.MoveNextWithoutConsuming())
				{
					currentToken = scanner.Current;
					if (currentToken is Error)
					{
						Error error3 = currentToken as Error;
						start = error3.Start;
						end = error3.End;
						throw new SemanticErrorException(in start, in end, error3.Value);
					}
				}
				if (state == ParserState.FlowMappingKey && scanner.MoveNextWithoutConsuming())
				{
					currentToken = scanner.Current;
					if (currentToken != null && !(currentToken is FlowEntry) && !(currentToken is FlowMappingEnd))
					{
						start = currentToken.Start;
						end = currentToken.End;
						throw new SemanticErrorException(in start, in end, "While parsing a flow mapping, did not find expected ',' or '}'.");
					}
				}
				return result2;
			}
			if (token is FlowSequenceStart flowSequenceStart)
			{
				state = ParserState.FlowSequenceFirstEntry;
				return new SequenceStart(anchor, tag, isEmpty, SequenceStyle.Flow, start2, flowSequenceStart.End);
			}
			if (token is FlowMappingStart flowMappingStart)
			{
				state = ParserState.FlowMappingFirstKey;
				return new MappingStart(anchor, tag, isEmpty, MappingStyle.Flow, start2, flowMappingStart.End);
			}
			if (isBlock)
			{
				if (token is BlockSequenceStart blockSequenceStart)
				{
					state = ParserState.BlockSequenceFirstEntry;
					return new SequenceStart(anchor, tag, isEmpty, SequenceStyle.Block, start2, blockSequenceStart.End);
				}
				if (token is BlockMappingStart blockMappingStart)
				{
					state = ParserState.BlockMappingFirstKey;
					return new MappingStart(anchor, tag, isEmpty, MappingStyle.Block, start2, blockMappingStart.End);
				}
			}
			if (!anchor.IsEmpty || !tag.IsEmpty)
			{
				state = states.Pop();
				return new YamlDotNet.Core.Events.Scalar(anchor, tag, string.Empty, ScalarStyle.Plain, isEmpty, isQuotedImplicit: false, start2, token.End);
			}
			start = token.Start;
			end = token.End;
			throw new SemanticErrorException(in start, in end, "While parsing a node, did not find expected node content.");
		}

		private ParsingEvent ParseDocumentEnd()
		{
			Token token = GetCurrentToken() ?? throw new SemanticErrorException("Reached the end of the stream while parsing a document end");
			bool isImplicit = true;
			Mark start = token.Start;
			Mark end = start;
			if (token is YamlDotNet.Core.Tokens.DocumentEnd)
			{
				end = token.End;
				Skip();
				isImplicit = false;
			}
			else if (!(currentToken is YamlDotNet.Core.Tokens.StreamEnd) && !(currentToken is YamlDotNet.Core.Tokens.DocumentStart) && !(currentToken is FlowSequenceEnd) && !(currentToken is VersionDirective) && (!(Current is YamlDotNet.Core.Events.Scalar) || !(currentToken is Error)))
			{
				throw new SemanticErrorException(in start, in end, "Did not find expected <document end>.");
			}
			if (version != null && version.Version.Major == 1 && version.Version.Minor > 1)
			{
				version = null;
			}
			state = ParserState.DocumentStart;
			return new YamlDotNet.Core.Events.DocumentEnd(isImplicit, start, end);
		}

		private ParsingEvent ParseBlockSequenceEntry(bool isFirst)
		{
			if (isFirst)
			{
				GetCurrentToken();
				Skip();
			}
			Token token = GetCurrentToken();
			if (token is BlockEntry blockEntry)
			{
				Mark position = blockEntry.End;
				Skip();
				token = GetCurrentToken();
				if (!(token is BlockEntry) && !(token is BlockEnd))
				{
					states.Push(ParserState.BlockSequenceEntry);
					return ParseNode(isBlock: true, isIndentlessSequence: false);
				}
				state = ParserState.BlockSequenceEntry;
				return ProcessEmptyScalar(in position);
			}
			Mark start;
			Mark end;
			if (token is BlockEnd blockEnd)
			{
				state = states.Pop();
				start = blockEnd.Start;
				end = blockEnd.End;
				SequenceEnd result = new SequenceEnd(in start, in end);
				Skip();
				return result;
			}
			start = token?.Start ?? Mark.Empty;
			end = token?.End ?? Mark.Empty;
			throw new SemanticErrorException(in start, in end, "While parsing a block collection, did not find expected '-' indicator.");
		}

		private ParsingEvent ParseIndentlessSequenceEntry()
		{
			Token token = GetCurrentToken();
			if (token is BlockEntry blockEntry)
			{
				Mark position = blockEntry.End;
				Skip();
				token = GetCurrentToken();
				if (!(token is BlockEntry) && !(token is Key) && !(token is Value) && !(token is BlockEnd))
				{
					states.Push(ParserState.IndentlessSequenceEntry);
					return ParseNode(isBlock: true, isIndentlessSequence: false);
				}
				state = ParserState.IndentlessSequenceEntry;
				return ProcessEmptyScalar(in position);
			}
			state = states.Pop();
			Mark start = token?.Start ?? Mark.Empty;
			Mark end = token?.End ?? Mark.Empty;
			return new SequenceEnd(in start, in end);
		}

		private ParsingEvent ParseBlockMappingKey(bool isFirst)
		{
			if (isFirst)
			{
				GetCurrentToken();
				Skip();
			}
			Token token = GetCurrentToken();
			if (token is Key key)
			{
				Mark position = key.End;
				Skip();
				token = GetCurrentToken();
				if (!(token is Key) && !(token is Value) && !(token is BlockEnd))
				{
					states.Push(ParserState.BlockMappingValue);
					return ParseNode(isBlock: true, isIndentlessSequence: true);
				}
				state = ParserState.BlockMappingValue;
				return ProcessEmptyScalar(in position);
			}
			Mark position2;
			if (token is Value value)
			{
				Skip();
				position2 = value.End;
				return ProcessEmptyScalar(in position2);
			}
			if (token is YamlDotNet.Core.Tokens.AnchorAlias anchorAlias)
			{
				Skip();
				return new YamlDotNet.Core.Events.AnchorAlias(anchorAlias.Value, anchorAlias.Start, anchorAlias.End);
			}
			Mark end;
			if (token is BlockEnd blockEnd)
			{
				state = states.Pop();
				position2 = blockEnd.Start;
				end = blockEnd.End;
				MappingEnd result = new MappingEnd(in position2, in end);
				Skip();
				return result;
			}
			if (GetCurrentToken() is Error error)
			{
				position2 = error.Start;
				end = error.End;
				throw new SyntaxErrorException(in position2, in end, error.Value);
			}
			position2 = token?.Start ?? Mark.Empty;
			end = token?.End ?? Mark.Empty;
			throw new SemanticErrorException(in position2, in end, "While parsing a block mapping, did not find expected key.");
		}

		private ParsingEvent ParseBlockMappingValue()
		{
			Token token = GetCurrentToken();
			if (token is Value value)
			{
				Mark position = value.End;
				Skip();
				token = GetCurrentToken();
				if (!(token is Key) && !(token is Value) && !(token is BlockEnd))
				{
					states.Push(ParserState.BlockMappingKey);
					return ParseNode(isBlock: true, isIndentlessSequence: true);
				}
				state = ParserState.BlockMappingKey;
				return ProcessEmptyScalar(in position);
			}
			Mark start;
			if (token is Error error)
			{
				start = error.Start;
				Mark end = error.End;
				throw new SemanticErrorException(in start, in end, error.Value);
			}
			state = ParserState.BlockMappingKey;
			start = token?.Start ?? Mark.Empty;
			return ProcessEmptyScalar(in start);
		}

		private ParsingEvent ParseFlowSequenceEntry(bool isFirst)
		{
			if (isFirst)
			{
				GetCurrentToken();
				Skip();
			}
			Token token = GetCurrentToken();
			Mark start;
			Mark end;
			if (!(token is FlowSequenceEnd))
			{
				if (!isFirst)
				{
					if (!(token is FlowEntry))
					{
						start = token?.Start ?? Mark.Empty;
						end = token?.End ?? Mark.Empty;
						throw new SemanticErrorException(in start, in end, "While parsing a flow sequence, did not find expected ',' or ']'.");
					}
					Skip();
					token = GetCurrentToken();
				}
				if (token is Key)
				{
					state = ParserState.FlowSequenceEntryMappingKey;
					MappingStart result = new MappingStart(AnchorName.Empty, TagName.Empty, isImplicit: true, MappingStyle.Flow);
					Skip();
					return result;
				}
				if (!(token is FlowSequenceEnd))
				{
					states.Push(ParserState.FlowSequenceEntry);
					return ParseNode(isBlock: false, isIndentlessSequence: false);
				}
			}
			state = states.Pop();
			start = token?.Start ?? Mark.Empty;
			end = token?.End ?? Mark.Empty;
			SequenceEnd result2 = new SequenceEnd(in start, in end);
			Skip();
			return result2;
		}

		private ParsingEvent ParseFlowSequenceEntryMappingKey()
		{
			Token token = GetCurrentToken();
			if (!(token is Value) && !(token is FlowEntry) && !(token is FlowSequenceEnd))
			{
				states.Push(ParserState.FlowSequenceEntryMappingValue);
				return ParseNode(isBlock: false, isIndentlessSequence: false);
			}
			Mark position = token?.End ?? Mark.Empty;
			Skip();
			state = ParserState.FlowSequenceEntryMappingValue;
			return ProcessEmptyScalar(in position);
		}

		private ParsingEvent ParseFlowSequenceEntryMappingValue()
		{
			Token token = GetCurrentToken();
			if (token is Value)
			{
				Skip();
				token = GetCurrentToken();
				if (!(token is FlowEntry) && !(token is FlowSequenceEnd))
				{
					states.Push(ParserState.FlowSequenceEntryMappingEnd);
					return ParseNode(isBlock: false, isIndentlessSequence: false);
				}
			}
			state = ParserState.FlowSequenceEntryMappingEnd;
			Mark position = token?.Start ?? Mark.Empty;
			return ProcessEmptyScalar(in position);
		}

		private ParsingEvent ParseFlowSequenceEntryMappingEnd()
		{
			state = ParserState.FlowSequenceEntry;
			Token token = GetCurrentToken();
			Mark start = token?.Start ?? Mark.Empty;
			Mark end = token?.End ?? Mark.Empty;
			return new MappingEnd(in start, in end);
		}

		private ParsingEvent ParseFlowMappingKey(bool isFirst)
		{
			if (isFirst)
			{
				GetCurrentToken();
				Skip();
			}
			Token token = GetCurrentToken();
			Mark start;
			Mark end;
			if (!(token is FlowMappingEnd))
			{
				if (!isFirst)
				{
					if (token is FlowEntry)
					{
						Skip();
						token = GetCurrentToken();
					}
					else if (!(token is YamlDotNet.Core.Tokens.Scalar))
					{
						start = token?.Start ?? Mark.Empty;
						end = token?.End ?? Mark.Empty;
						throw new SemanticErrorException(in start, in end, "While parsing a flow mapping,  did not find expected ',' or '}'.");
					}
				}
				if (token is Key)
				{
					Skip();
					token = GetCurrentToken();
					if (!(token is Value) && !(token is FlowEntry) && !(token is FlowMappingEnd))
					{
						states.Push(ParserState.FlowMappingValue);
						return ParseNode(isBlock: false, isIndentlessSequence: false);
					}
					state = ParserState.FlowMappingValue;
					start = token?.Start ?? Mark.Empty;
					return ProcessEmptyScalar(in start);
				}
				if (token is YamlDotNet.Core.Tokens.Scalar)
				{
					states.Push(ParserState.FlowMappingValue);
					return ParseNode(isBlock: false, isIndentlessSequence: false);
				}
				if (!(token is FlowMappingEnd))
				{
					states.Push(ParserState.FlowMappingEmptyValue);
					return ParseNode(isBlock: false, isIndentlessSequence: false);
				}
			}
			state = states.Pop();
			Skip();
			start = token?.Start ?? Mark.Empty;
			end = token?.End ?? Mark.Empty;
			return new MappingEnd(in start, in end);
		}

		private ParsingEvent ParseFlowMappingValue(bool isEmpty)
		{
			Token token = GetCurrentToken();
			if (!isEmpty && token is Value)
			{
				Skip();
				token = GetCurrentToken();
				if (!(token is FlowEntry) && !(token is FlowMappingEnd))
				{
					states.Push(ParserState.FlowMappingKey);
					return ParseNode(isBlock: false, isIndentlessSequence: false);
				}
			}
			state = ParserState.FlowMappingKey;
			if (!isEmpty && token is YamlDotNet.Core.Tokens.Scalar scalar)
			{
				Skip();
				return new YamlDotNet.Core.Events.Scalar(AnchorName.Empty, TagName.Empty, scalar.Value, scalar.Style, isPlainImplicit: false, isQuotedImplicit: false, token.Start, scalar.End);
			}
			Mark position = token?.Start ?? Mark.Empty;
			return ProcessEmptyScalar(in position);
		}
	}
}
