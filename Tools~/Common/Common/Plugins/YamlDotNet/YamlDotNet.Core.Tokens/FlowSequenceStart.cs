namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class FlowSequenceStart : Token
	{
		public FlowSequenceStart()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public FlowSequenceStart(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
