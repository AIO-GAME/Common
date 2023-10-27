using System;

namespace YamlDotNet.Core
{
	internal class YamlException : Exception
	{
		public Mark Start { get; }

		public Mark End { get; }

		public YamlException(string message)
			: this(in Mark.Empty, in Mark.Empty, message)
		{
		}

		public YamlException(in Mark start, in Mark end, string message)
			: this(in start, in end, message, null)
		{
		}

		public YamlException(in Mark start, in Mark end, string message, Exception? innerException)
			: base(message, innerException)
		{
			Start = start;
			End = end;
		}

		public YamlException(string message, Exception inner)
			: this(in Mark.Empty, in Mark.Empty, message, inner)
		{
		}

		public override string ToString()
		{
			return $"({Start}) - ({End}): {Message}";
		}
	}
}
