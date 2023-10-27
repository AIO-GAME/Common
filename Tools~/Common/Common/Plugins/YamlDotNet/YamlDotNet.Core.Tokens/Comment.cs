using System;

namespace YamlDotNet.Core.Tokens
{
	internal sealed class Comment : Token
	{
		public string Value { get; }

		public bool IsInline { get; }

		public Comment(string value, bool isInline)
			: this(value, isInline, Mark.Empty, Mark.Empty)
		{
		}

		public Comment(string value, bool isInline, Mark start, Mark end)
			: base(in start, in end)
		{
			Value = value ?? throw new ArgumentNullException("value");
			IsInline = isInline;
		}
	}
}
