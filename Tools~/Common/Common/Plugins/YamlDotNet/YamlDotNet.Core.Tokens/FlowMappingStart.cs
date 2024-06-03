namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class FlowMappingStart : Token
	{
		public FlowMappingStart()
			: this(in Mark.Empty, in Mark.Empty)
		{
		}

		public FlowMappingStart(in Mark start, in Mark end)
			: base(in start, in end)
		{
		}
	}
}
