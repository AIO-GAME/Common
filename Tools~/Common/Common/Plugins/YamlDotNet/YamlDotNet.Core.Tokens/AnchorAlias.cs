using System;

namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class AnchorAlias : Token
	{
		public AnchorName Value { get; }

		public AnchorAlias(AnchorName value)
			: this(value, Mark.Empty, Mark.Empty)
		{
		}

		public AnchorAlias(AnchorName value, Mark start, Mark end)
			: base(in start, in end)
		{
			if (value.IsEmpty)
			{
				throw new ArgumentNullException("value");
			}
			Value = value;
		}
	}
}
