namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class BlockEnd : Token
	{
		public BlockEnd()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public BlockEnd(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
