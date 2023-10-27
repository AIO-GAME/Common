namespace YamlDotNet.Core.Tokens
{
	internal sealed class BlockSequenceStart : Token
	{
		public BlockSequenceStart()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public BlockSequenceStart(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
