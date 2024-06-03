namespace AIO.YamlDotNet.Core.Events
{
	internal abstract class NodeEvent : ParsingEvent
	{
		public AnchorName Anchor { get; }

		public TagName Tag { get; }

		internal abstract bool IsCanonical { get; }

		protected NodeEvent(AnchorName anchor, TagName tag, Mark start, Mark end)
			: base(in start, in end)
		{
			Anchor = anchor;
			Tag = tag;
		}

		protected NodeEvent(AnchorName anchor, TagName tag)
			: this(anchor, tag, Mark.Empty, Mark.Empty)
		{
		}
	}
}
