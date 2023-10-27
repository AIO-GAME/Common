namespace System.Collections.Generic
{
	internal static class DeconstructionExtensions
	{
		public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key, out TValue value)
		{
			key = pair.Key;
			value = pair.Value;
		}
	}
}
