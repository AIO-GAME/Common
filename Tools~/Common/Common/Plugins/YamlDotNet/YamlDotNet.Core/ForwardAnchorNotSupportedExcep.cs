using System;

namespace AIO.YamlDotNet.Core
{
	internal sealed class ForwardAnchorNotSupportedException : YamlException
	{
		public ForwardAnchorNotSupportedException(string message)
			: base(message)
		{
		}

		public ForwardAnchorNotSupportedException(in Mark start, in Mark end, string message)
			: base(in start, in end, message)
		{
		}

		public ForwardAnchorNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
