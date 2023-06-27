using UnityEngine;

namespace DG.Tweening.Core.Easing
{
	/// <summary>
	/// Used to interpret AnimationCurves as eases.
	/// Public so it can be used by external ease factories
	/// </summary>
	public class EaseCurve
	{
		private readonly AnimationCurve _animCurve;

		public EaseCurve(AnimationCurve animCurve)
		{
			_animCurve = animCurve;
		}

		public float Evaluate(float time, float duration, float unusedOvershoot, float unusedPeriod)
		{
			float time2 = _animCurve[_animCurve.length - 1].time;
			float num = time / duration;
			return _animCurve.Evaluate(num * time2);
		}
	}
}
