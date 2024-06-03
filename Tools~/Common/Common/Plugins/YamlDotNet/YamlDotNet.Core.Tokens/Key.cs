namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class Key : Token
	{
		public Key()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public Key(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
