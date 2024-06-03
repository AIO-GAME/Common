using System;

namespace AIO.YamlDotNet.Core
{
	internal sealed class SyntaxErrorException : YamlException
	{
		public SyntaxErrorException(string message)
			: base(message)
		{
		}

		public SyntaxErrorException(in Mark start, in Mark end, string message)
			: base(in start, in end, message)
		{
		}

		public SyntaxErrorException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
