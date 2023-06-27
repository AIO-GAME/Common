using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public class QuaternionPlugin : ABSTweenPlugin<Quaternion, Vector3, QuaternionOptions>
	{
		public override void Reset(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
		}

		public override void SetFrom(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, bool isRelative)
		{
			Vector3 endValue = t.endValue;
			t.endValue = t.getter().eulerAngles;
			if (t.plugOptions.rotateMode == RotateMode.Fast && !t.isRelative)
			{
				t.startValue = endValue;
			}
			else if (t.plugOptions.rotateMode == RotateMode.FastBeyond360)
			{
				t.startValue = t.endValue + endValue;
			}
			else
			{
				Quaternion quaternion = t.getter();
				if (t.plugOptions.rotateMode == RotateMode.WorldAxisAdd)
				{
					t.startValue = (quaternion * Quaternion.Inverse(quaternion) * Quaternion.Euler(endValue) * quaternion).eulerAngles;
				}
				else
				{
					t.startValue = (quaternion * Quaternion.Euler(endValue)).eulerAngles;
				}
				t.endValue = -endValue;
			}
			t.setter(Quaternion.Euler(t.startValue));
		}

		public override void SetFrom(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, Vector3 fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				Vector3 eulerAngles = t.getter().eulerAngles;
				t.endValue += eulerAngles;
				fromValue += eulerAngles;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(Quaternion.Euler(fromValue));
			}
		}

		public override Vector3 ConvertToStartValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, Quaternion value)
		{
			return value.eulerAngles;
		}

		public override void SetRelativeEndValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			if (t.plugOptions.rotateMode == RotateMode.Fast && !t.isRelative)
			{
				Vector3 endValue = t.endValue;
				if (endValue.x > 360f)
				{
					endValue.x %= 360f;
				}
				if (endValue.y > 360f)
				{
					endValue.y %= 360f;
				}
				if (endValue.z > 360f)
				{
					endValue.z %= 360f;
				}
				Vector3 changeValue = endValue - t.startValue;
				float num = ((changeValue.x > 0f) ? changeValue.x : (0f - changeValue.x));
				if (num > 180f)
				{
					changeValue.x = ((changeValue.x > 0f) ? (0f - (360f - num)) : (360f - num));
				}
				num = ((changeValue.y > 0f) ? changeValue.y : (0f - changeValue.y));
				if (num > 180f)
				{
					changeValue.y = ((changeValue.y > 0f) ? (0f - (360f - num)) : (360f - num));
				}
				num = ((changeValue.z > 0f) ? changeValue.z : (0f - changeValue.z));
				if (num > 180f)
				{
					changeValue.z = ((changeValue.z > 0f) ? (0f - (360f - num)) : (360f - num));
				}
				t.changeValue = changeValue;
			}
			else if (t.plugOptions.rotateMode == RotateMode.FastBeyond360 || t.isRelative)
			{
				t.changeValue = t.endValue - t.startValue;
			}
			else
			{
				t.changeValue = t.endValue;
			}
		}

		public override float GetSpeedBasedDuration(QuaternionOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		public override void EvaluateAndApply(QuaternionOptions options, Tween t, bool isRelative, DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			Vector3 euler = startValue;
			if (t.loopType == LoopType.Incremental)
			{
				euler += changeValue * (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				euler += changeValue * ((t.loopType != LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			RotateMode rotateMode = options.rotateMode;
			if ((uint)(rotateMode - 2) <= 1u)
			{
				Quaternion quaternion = Quaternion.Euler(startValue);
				euler.x = changeValue.x * num;
				euler.y = changeValue.y * num;
				euler.z = changeValue.z * num;
				if (options.rotateMode == RotateMode.WorldAxisAdd)
				{
					setter(quaternion * Quaternion.Inverse(quaternion) * Quaternion.Euler(euler) * quaternion);
				}
				else
				{
					setter(quaternion * Quaternion.Euler(euler));
				}
			}
			else
			{
				euler.x += changeValue.x * num;
				euler.y += changeValue.y * num;
				euler.z += changeValue.z * num;
				setter(Quaternion.Euler(euler));
			}
		}
	}
}
