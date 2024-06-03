namespace AIO.YamlDotNet.Core.Events
{
	internal abstract class ParsingEvent
	{
		public virtual int NestingIncrease => 0;

		internal abstract EventType Type { get; }

		public Mark Start { get; }

		public Mark End { get; }

		internal abstract void Accept(IParsingEventVisitor visitor);

		internal ParsingEvent(in Mark start, in Mark end)
		{
			Start = start;
			End = end;
		}
	}
}
