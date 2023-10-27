namespace YamlDotNet.Core.Events
{
	internal class MappingEnd : ParsingEvent
	{
	public override int NestingIncrease => -1;

		internal override EventType Type => EventType.MappingEnd;

		public MappingEnd(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}

		public MappingEnd()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public override string ToString()
		{
			return "Mapping end";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
