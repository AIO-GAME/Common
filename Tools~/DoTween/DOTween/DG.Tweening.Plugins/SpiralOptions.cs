using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public struct SpiralOptions : IPlugOptions
	{
		public float depth;

		public float frequency;

		public float speed;

		public SpiralMode mode;

		public bool snapping;

		internal float unit;

		internal Quaternion axisQ;

		public void Reset()
		{
			depth = (frequency = (speed = 0f));
			mode = SpiralMode.Expand;
			snapping = false;
		}
	}
}
