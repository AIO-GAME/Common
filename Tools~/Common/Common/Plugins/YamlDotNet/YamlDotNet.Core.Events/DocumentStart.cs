using AIO.YamlDotNet.Core.Tokens;

#nullable enable
namespace AIO.YamlDotNet.Core.Events
{
	internal sealed class DocumentStart : ParsingEvent
	{
		public override int NestingIncrease => 1;

		internal override EventType Type => EventType.DocumentStart;

		public TagDirectiveCollection? Tags { get; }

		public VersionDirective? Version { get; }

		public bool IsImplicit { get; }

		public DocumentStart(VersionDirective? version, TagDirectiveCollection? tags, bool isImplicit, Mark start, Mark end)
			: base(in start, in end)
		{
			Version = version;
			Tags = tags;
			IsImplicit = isImplicit;
		}

		public DocumentStart(VersionDirective? version, TagDirectiveCollection? tags, bool isImplicit)
			: this(version, tags, isImplicit, Mark.Empty, Mark.Empty)
		{
		}

		public DocumentStart(in Mark start, in Mark end)
			: this(null, null, isImplicit: true, start, end)
		{
		}

		public DocumentStart()
			: this(null, null, isImplicit: true, Mark.Empty, Mark.Empty)
		{
		}

		public override string ToString()
		{
			return $"Document start [isImplicit = {IsImplicit}]";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
