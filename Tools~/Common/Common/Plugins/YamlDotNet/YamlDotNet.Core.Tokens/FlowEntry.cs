namespace YamlDotNet.Core.Tokens
{
	internal sealed class FlowEntry : Token
	{
		public FlowEntry()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public FlowEntry(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
