namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class BlockEntry : Token
	{
		public BlockEntry()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public BlockEntry(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
