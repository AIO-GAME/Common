namespace YamlDotNet.Core.Events
{
	internal sealed class StreamEnd : ParsingEvent
	{
	public override int NestingIncrease => -1;

		internal override EventType Type => EventType.StreamEnd;

		public StreamEnd(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}

		public StreamEnd()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public override string ToString()
		{
			return "Stream end";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
