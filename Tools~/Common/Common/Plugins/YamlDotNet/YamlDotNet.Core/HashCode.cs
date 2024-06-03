namespace AIO.YamlDotNet.Core
{
	internal static class HashCode
	{
		public static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		public static int CombineHashCodes(int h1, object? o2)
		{
			return CombineHashCodes(h1, GetHashCode(o2));
		}

		private static int GetHashCode(object? obj)
		{
			return obj?.GetHashCode() ?? 0;
		}
	}
}
