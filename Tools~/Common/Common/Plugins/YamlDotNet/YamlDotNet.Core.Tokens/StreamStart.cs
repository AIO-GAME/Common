namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class StreamStart : Token
	{
		public StreamStart()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public StreamStart(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
