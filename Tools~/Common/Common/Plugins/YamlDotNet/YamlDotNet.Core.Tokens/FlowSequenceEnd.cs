namespace YamlDotNet.Core.Tokens
{
	internal sealed class FlowSequenceEnd : Token
	{
		public FlowSequenceEnd()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public FlowSequenceEnd(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
