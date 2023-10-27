namespace YamlDotNet.Core.Events
{
	internal sealed class SequenceStart : NodeEvent
	{
	public override int NestingIncrease => 1;

		internal override EventType Type => EventType.SequenceStart;

		public bool IsImplicit { get; }

		internal override bool IsCanonical => !IsImplicit;

		public SequenceStyle Style { get; }

		public SequenceStart(AnchorName anchor, TagName tag, bool isImplicit, SequenceStyle style, Mark start, Mark end)
			: base(anchor, tag, start, end)
		{
			IsImplicit = isImplicit;
			Style = style;
		}

		public SequenceStart(AnchorName anchor, TagName tag, bool isImplicit, SequenceStyle style)
			: this(anchor, tag, isImplicit, style, Mark.Empty, Mark.Empty)
		{
		}

		public override string ToString()
		{
			return $"Sequence start [anchor = {base.Anchor}, tag = {base.Tag}, isImplicit = {IsImplicit}, style = {Style}]";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
