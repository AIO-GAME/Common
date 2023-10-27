namespace YamlDotNet.Core.Tokens
{
	internal sealed class DocumentStart : Token
	{
		public DocumentStart()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public DocumentStart(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
