using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	/// <summary>
	/// Tweens a Vector3 along a spiral.
	/// EndValue represents the direction of the spiral
	/// </summary>
	public class SpiralPlugin : ABSTweenPlugin<Vector3, Vector3, SpiralOptions>
	{
		public static readonly Vector3 DefaultDirection = Vector3.forward;

		public override void Reset(TweenerCore<Vector3, Vector3, SpiralOptions> t)
		{
		}

		public override void SetFrom(TweenerCore<Vector3, Vector3, SpiralOptions> t, bool isRelative)
		{
		}

		public override void SetFrom(TweenerCore<Vector3, Vector3, SpiralOptions> t, Vector3 fromValue, bool setImmediately, bool isRelative)
		{
		}

		public static ABSTweenPlugin<Vector3, Vector3, SpiralOptions> Get()
		{
			return PluginsManager.GetCustomPlugin<SpiralPlugin, Vector3, Vector3, SpiralOptions>();
		}

		public override Vector3 ConvertToStartValue(TweenerCore<Vector3, Vector3, SpiralOptions> t, Vector3 value)
		{
			return value;
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3, SpiralOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<Vector3, Vector3, SpiralOptions> t)
		{
			t.plugOptions.speed *= 10f / t.plugOptions.frequency;
			t.plugOptions.axisQ = Quaternion.LookRotation(t.endValue, Vector3.up);
		}

		public override float GetSpeedBasedDuration(SpiralOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return unitsXSecond;
		}

		public override void EvaluateAndApply(SpiralOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			float num = options.frequency;
			float num2 = options.depth;
			float num3 = options.speed;
			float num4 = EaseManager.Evaluate(t, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			bool flag = options.mode == SpiralMode.ExpandThenContract;
			float num5 = ((flag && num4 > 0.5f) ? (0.5f - (num4 - 0.5f)) : num4);
			if (t.loopType == LoopType.Incremental)
			{
				if (flag)
				{
					int num6 = (t.isComplete ? (t.completedLoops - 1) : (t.completedLoops + 1));
					num /= (float)num6;
					num2 *= (float)num6;
					num3 = num3 / (10f / options.frequency) * (10f / num);
				}
				else
				{
					int num7 = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
					num4 += (float)num7;
					num5 = num4;
				}
			}
			float num8 = duration * num3 * num4;
			options.unit = duration * num3 * num5;
			Vector3 vector = new Vector3(options.unit * Mathf.Cos(num8 * num), options.unit * Mathf.Sin(num8 * num), num2 * num4);
			vector = options.axisQ * vector + startValue;
			if (options.snapping)
			{
				vector.x = (float)Math.Round(vector.x);
				vector.y = (float)Math.Round(vector.y);
				vector.z = (float)Math.Round(vector.z);
			}
			setter(vector);
		}
	}
}
