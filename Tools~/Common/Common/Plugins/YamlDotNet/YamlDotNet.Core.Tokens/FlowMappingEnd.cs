namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class FlowMappingEnd : Token
	{
		public FlowMappingEnd()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public FlowMappingEnd(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
