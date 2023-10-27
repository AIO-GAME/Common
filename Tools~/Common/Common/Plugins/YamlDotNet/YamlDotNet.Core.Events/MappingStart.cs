namespace YamlDotNet.Core.Events
{
	internal sealed class MappingStart : NodeEvent
	{
	public override int NestingIncrease => 1;

		internal override EventType Type => EventType.MappingStart;

		public bool IsImplicit { get; }

		internal override bool IsCanonical => !IsImplicit;

		public MappingStyle Style { get; }

		public MappingStart(AnchorName anchor, TagName tag, bool isImplicit, MappingStyle style, Mark start, Mark end)
			: base(anchor, tag, start, end)
		{
			IsImplicit = isImplicit;
			Style = style;
		}

		public MappingStart(AnchorName anchor, TagName tag, bool isImplicit, MappingStyle style)
			: this(anchor, tag, isImplicit, style, Mark.Empty, Mark.Empty)
		{
		}

		public MappingStart()
			: this(AnchorName.Empty, TagName.Empty, isImplicit: true, MappingStyle.Any, Mark.Empty, Mark.Empty)
		{
		}

		public override string ToString()
		{
			return $"Mapping start [anchor = {base.Anchor}, tag = {base.Tag}, isImplicit = {IsImplicit}, style = {Style}]";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
