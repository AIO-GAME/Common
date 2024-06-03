namespace AIO.YamlDotNet.Core.Events
{
	internal sealed class DocumentEnd : ParsingEvent
	{
		public override int NestingIncrease => -1;

		internal override EventType Type => EventType.DocumentEnd;

		public bool IsImplicit { get; }

		public DocumentEnd(bool isImplicit, Mark start, Mark end)
			: base(in start, in end)
		{
			IsImplicit = isImplicit;
		}

		public DocumentEnd(bool isImplicit)
			: this(isImplicit, Mark.Empty, Mark.Empty)
		{
		}

		public override string ToString()
		{
			return $"Document end [isImplicit = {IsImplicit}]";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
