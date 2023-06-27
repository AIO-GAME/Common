using System;

namespace DG.Tweening
{
	/// <summary>
	/// What axis to constrain in case of Vector tweens
	/// </summary>
	[Flags]
	public enum AxisConstraint
	{
		None = 0,
		X = 2,
		Y = 4,
		Z = 8,
		W = 0x10
	}
}
