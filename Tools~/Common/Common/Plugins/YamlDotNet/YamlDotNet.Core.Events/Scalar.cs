namespace YamlDotNet.Core.Events
{
	internal sealed class Scalar : NodeEvent
	{
		internal override EventType Type => EventType.Scalar;

		public string Value { get; }

		public ScalarStyle Style { get; }

		public bool IsPlainImplicit { get; }

		public bool IsQuotedImplicit { get; }

		internal override bool IsCanonical
		{
			get
			{
				if (!IsPlainImplicit)
				{
					return !IsQuotedImplicit;
				}
				return false;
			}
		}

		public bool IsKey { get; }

		public Scalar(AnchorName anchor, TagName tag, string value, ScalarStyle style, bool isPlainImplicit, bool isQuotedImplicit, Mark start, Mark end, bool isKey = false)
			: base(anchor, tag, start, end)
		{
			Value = value;
			Style = style;
			IsPlainImplicit = isPlainImplicit;
			IsQuotedImplicit = isQuotedImplicit;
			IsKey = isKey;
		}

		public Scalar(AnchorName anchor, TagName tag, string value, ScalarStyle style, bool isPlainImplicit, bool isQuotedImplicit)
			: this(anchor, tag, value, style, isPlainImplicit, isQuotedImplicit, Mark.Empty, Mark.Empty)
		{
		}

		public Scalar(string value)
			: this(AnchorName.Empty, TagName.Empty, value, ScalarStyle.Any, isPlainImplicit: true, isQuotedImplicit: true, Mark.Empty, Mark.Empty)
		{
		}

		public Scalar(TagName tag, string value)
			: this(AnchorName.Empty, tag, value, ScalarStyle.Any, isPlainImplicit: true, isQuotedImplicit: true, Mark.Empty, Mark.Empty)
		{
		}

		public Scalar(AnchorName anchor, TagName tag, string value)
			: this(anchor, tag, value, ScalarStyle.Any, isPlainImplicit: true, isQuotedImplicit: true, Mark.Empty, Mark.Empty)
		{
		}

		public override string ToString()
		{
			return $"Scalar [anchor = {base.Anchor}, tag = {base.Tag}, value = {Value}, style = {Style}, isPlainImplicit = {IsPlainImplicit}, isQuotedImplicit = {IsQuotedImplicit}]";
		}

		internal override void Accept(IParsingEventVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
