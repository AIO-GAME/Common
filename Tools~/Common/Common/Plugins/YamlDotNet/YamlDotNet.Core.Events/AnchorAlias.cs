namespace AIO.YamlDotNet.Core.Events
{
	internal sealed class AnchorAlias : ParsingEvent
	{
		internal override EventType Type => EventType.Alias;

		public AnchorName Value { get; }

		public AnchorAlias(AnchorName value, Mark start, Mark end)
			: base(in start, in end)
		{
			if (value.IsEmpty)
			{
				throw new YamlException(in start, in end, "Anchor value must not be empty.");
			}
			Value = value;
		}

		public AnchorAlias(AnchorName value)
			: this(value, Mark.Empty, Mark.Empty)
		{
		}

		public override string ToString()
		{
			return $"Alias [value = {Value}]";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
