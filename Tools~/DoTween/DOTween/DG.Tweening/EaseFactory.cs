using DG.Tweening.Core.Easing;
using UnityEngine;

namespace DG.Tweening
{
	/// <summary>
	/// Allows to wrap ease method in special ways, adding extra features
	/// </summary>
	public class EaseFactory
	{
		/// <summary>
		/// Converts the given ease so that it also creates a stop-motion effect, by playing the tween at the given FPS
		/// </summary>
		/// <param name="motionFps">FPS at which the tween should be played</param>
		/// <param name="ease">Ease type</param>
		public static EaseFunction StopMotion(int motionFps, Ease? ease = null)
		{
			EaseFunction customEase = EaseManager.ToEaseFunction((!ease.HasValue) ? DOTween.defaultEaseType : ease.Value);
			return StopMotion(motionFps, customEase);
		}

		/// <summary>
		/// Converts the given ease so that it also creates a stop-motion effect, by playing the tween at the given FPS
		/// </summary>
		/// <param name="motionFps">FPS at which the tween should be played</param>
		/// <param name="animCurve">AnimationCurve to use for the ease</param>
		public static EaseFunction StopMotion(int motionFps, AnimationCurve animCurve)
		{
			return StopMotion(motionFps, new EaseCurve(animCurve).Evaluate);
		}

		/// <summary>
		/// Converts the given ease so that it also creates a stop-motion effect, by playing the tween at the given FPS
		/// </summary>
		/// <param name="motionFps">FPS at which the tween should be played</param>
		/// <param name="customEase">Custom ease function to use</param>
		public static EaseFunction StopMotion(int motionFps, EaseFunction customEase)
		{
			float motionDelay = 1f / (float)motionFps;
			return delegate(float time, float duration, float overshootOrAmplitude, float period)
			{
				float time2 = ((time < duration) ? (time - time % motionDelay) : time);
				return customEase(time2, duration, overshootOrAmplitude, period);
			};
		}
	}
}
