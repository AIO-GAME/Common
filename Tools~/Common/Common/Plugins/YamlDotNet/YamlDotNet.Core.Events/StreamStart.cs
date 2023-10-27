namespace YamlDotNet.Core.Events
{
	internal sealed class StreamStart : ParsingEvent
	{
	public override int NestingIncrease => 1;

		internal override EventType Type => EventType.StreamStart;

		public StreamStart()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public StreamStart(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}

		public override string ToString()
		{
			return "Stream start";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
