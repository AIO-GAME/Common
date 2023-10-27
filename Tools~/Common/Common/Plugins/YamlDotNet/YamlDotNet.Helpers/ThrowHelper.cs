using System;
using System.Runtime.CompilerServices;

namespace YamlDotNet.Helpers
{
	internal static class ThrowHelper
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ThrowArgumentOutOfRangeException(string paramName, string message)
		{
			throw new ArgumentOutOfRangeException(paramName, message);
		}
	}
}
