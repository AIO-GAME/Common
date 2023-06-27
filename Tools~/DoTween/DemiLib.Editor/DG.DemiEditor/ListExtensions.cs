using System;
using System.Collections.Generic;

namespace DG.DemiEditor
{
	/// <summary>
	/// Replicates parts of DeExtensions.ListExtensions for internal usage
	/// </summary>
	public static class ListExtensions
	{
		private static Random _rng;

		/// <summary>
		/// Shifts an item from an index to another, without modifying the list except than by moving elements around
		/// </summary>
		public static void Shift<T>(this IList<T> list, int fromIndex, int toIndex)
		{
			if (toIndex != fromIndex)
			{
				int num = fromIndex;
				T value = list[fromIndex];
				while (num > toIndex)
				{
					num--;
					list[num + 1] = list[num];
					list[num] = value;
				}
				while (num < toIndex)
				{
					num++;
					list[num - 1] = list[num];
					list[num] = value;
				}
			}
		}

		/// <summary>
		/// Shuffles the list
		/// </summary>
		public static void Shuffle<T>(this IList<T> list)
		{
			if (_rng == null)
			{
				_rng = new Random();
			}
			int num = list.Count;
			while (num > 1)
			{
				num--;
				int index = _rng.Next(num + 1);
				T value = list[index];
				list[index] = list[num];
				list[num] = value;
			}
		}
	}
}
