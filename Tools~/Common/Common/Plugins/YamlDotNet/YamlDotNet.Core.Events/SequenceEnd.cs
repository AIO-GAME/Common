namespace AIO.YamlDotNet.Core.Events
{
	internal sealed class SequenceEnd : ParsingEvent
	{
	public override int NestingIncrease => -1;

		internal override EventType Type => EventType.SequenceEnd;

		public SequenceEnd(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}

		public SequenceEnd()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public override string ToString()
		{
			return "Sequence end";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
