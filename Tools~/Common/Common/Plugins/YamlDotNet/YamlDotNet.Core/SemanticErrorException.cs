using System;

namespace YamlDotNet.Core
{
	internal class SemanticErrorException : YamlException
	{
		public SemanticErrorException(string message)
			: base(message)
		{
		}

		public SemanticErrorException(in Mark start, in Mark end, string message)
			: base(in start, in end, message)
		{
		}

		public SemanticErrorException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
