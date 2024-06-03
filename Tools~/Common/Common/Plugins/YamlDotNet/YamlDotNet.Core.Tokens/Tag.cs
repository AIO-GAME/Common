using System;

namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class Tag : Token
	{
		public string Handle { get; }

		public string Suffix { get; }

		public Tag(string handle, string suffix)
			: this(handle, suffix, Mark.Empty, Mark.Empty)
		{
		}

		public Tag(string handle, string suffix, Mark start, Mark end)
			: base(in start, in end)
		{
			Handle = handle ?? throw new ArgumentNullException("handle");
			Suffix = suffix ?? throw new ArgumentNullException("suffix");
		}
	}
}
