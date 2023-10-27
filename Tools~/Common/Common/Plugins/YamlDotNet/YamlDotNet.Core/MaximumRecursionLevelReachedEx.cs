using System;

namespace YamlDotNet.Core
{
	internal sealed class MaximumRecursionLevelReachedException : YamlException
	{
		public MaximumRecursionLevelReachedException(string message)
			: base(message)
		{
		}

		public MaximumRecursionLevelReachedException(in Mark start, in Mark end, string message)
			: base(in start, in end, message)
		{
		}

		public MaximumRecursionLevelReachedException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
