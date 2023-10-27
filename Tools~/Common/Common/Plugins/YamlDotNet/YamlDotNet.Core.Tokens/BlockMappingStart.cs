namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class BlockMappingStart : Token
	{
		public BlockMappingStart()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public BlockMappingStart(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
