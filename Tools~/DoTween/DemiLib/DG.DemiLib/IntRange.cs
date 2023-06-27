using System;
using UnityEngine;

namespace DG.DemiLib
{
	/// <summary>
	/// A serializable struct including a min and a max int value
	/// </summary>
	[Serializable]
	public struct IntRange
	{
		/// <summary>Min value</summary>
		public int min;

		/// <summary>Max value</summary>
		public int max;

		/// <summary>
		/// Creates a new Range
		/// </summary>
		public IntRange(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		/// <summary>
		/// Returns a random value within this range (min/max included)
		/// </summary>
		public float RandomWithin()
		{
			return UnityEngine.Random.Range(min, max + 1);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return string.Concat("(", min.ToString(), "/", max.ToString(), ")");
		}
	}
}
