namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class Value : Token
	{
		public Value()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public Value(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
