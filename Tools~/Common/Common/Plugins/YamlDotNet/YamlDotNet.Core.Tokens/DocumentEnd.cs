namespace YamlDotNet.Core.Tokens
{
	internal sealed class DocumentEnd : Token
	{
		public DocumentEnd()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public DocumentEnd(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
