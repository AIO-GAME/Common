namespace YamlDotNet.Core.Tokens
{
	internal sealed class StreamEnd : Token
	{
		public StreamEnd()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public StreamEnd(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
