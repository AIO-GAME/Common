using System;

namespace AIO.YamlDotNet.Core
{
	internal class AnchorNotFoundException : YamlException
	{
		public AnchorNotFoundException(string message)
			: base(message)
		{
		}

		public AnchorNotFoundException(in Mark start, in Mark end, string message)
			: base(in start, in end, message)
		{
		}

		public AnchorNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
