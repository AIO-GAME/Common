namespace AIO.YamlDotNet.Core.Events
{
	internal sealed class Comment : ParsingEvent
	{
		public string Value { get; }

		public bool IsInline { get; }

		internal override EventType Type => EventType.Comment;

		public Comment(string value, bool isInline)
			: this(value, isInline, Mark.Empty, Mark.Empty)
		{
		}

		public Comment(string value, bool isInline, Mark start, Mark end)
			: base(in start, in end)
		{
			Value = value;
			IsInline = isInline;
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}

		public override string ToString()
		{
			return (IsInline ? "Inline" : "Block") + " Comment [" + Value + "]";
		}
	}
}
