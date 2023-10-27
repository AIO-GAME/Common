using System;

namespace YamlDotNet.Helpers
{
	internal static class Lazy
	{
		public static Lazy<T> FromValue<T>(T value)
		{
			T value2 = value;
			Lazy<T> lazy = new Lazy<T>(() => value2, isThreadSafe: false);
			_ = lazy.Value;
			return lazy;
		}
	}
}
