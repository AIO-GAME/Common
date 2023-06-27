using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public class Vector3Plugin : ABSTweenPlugin<Vector3, Vector3, VectorOptions>
	{
		public override void Reset(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
		}

		public override void SetFrom(TweenerCore<Vector3, Vector3, VectorOptions> t, bool isRelative)
		{
			Vector3 endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			Vector3 pNewValue = t.endValue;
			switch (t.plugOptions.axisConstraint)
			{
			case AxisConstraint.X:
				pNewValue.x = t.startValue.x;
				break;
			case AxisConstraint.Y:
				pNewValue.y = t.startValue.y;
				break;
			case AxisConstraint.Z:
				pNewValue.z = t.startValue.z;
				break;
			default:
				pNewValue = t.startValue;
				break;
			}
			if (t.plugOptions.snapping)
			{
				pNewValue.x = (float)Math.Round(pNewValue.x);
				pNewValue.y = (float)Math.Round(pNewValue.y);
				pNewValue.z = (float)Math.Round(pNewValue.z);
			}
			t.setter(pNewValue);
		}

		public override void SetFrom(TweenerCore<Vector3, Vector3, VectorOptions> t, Vector3 fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				Vector3 vector = t.getter();
				t.endValue += vector;
				fromValue += vector;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				Vector3 pNewValue;
				switch (t.plugOptions.axisConstraint)
				{
				case AxisConstraint.X:
					pNewValue = t.getter();
					pNewValue.x = fromValue.x;
					break;
				case AxisConstraint.Y:
					pNewValue = t.getter();
					pNewValue.y = fromValue.y;
					break;
				case AxisConstraint.Z:
					pNewValue = t.getter();
					pNewValue.z = fromValue.z;
					break;
				default:
					pNewValue = fromValue;
					break;
				}
				if (t.plugOptions.snapping)
				{
					pNewValue.x = (float)Math.Round(pNewValue.x);
					pNewValue.y = (float)Math.Round(pNewValue.y);
					pNewValue.z = (float)Math.Round(pNewValue.z);
				}
				t.setter(pNewValue);
			}
		}

		public override Vector3 ConvertToStartValue(TweenerCore<Vector3, Vector3, VectorOptions> t, Vector3 value)
		{
			return value;
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
			switch (t.plugOptions.axisConstraint)
			{
			case AxisConstraint.X:
				t.changeValue = new Vector3(t.endValue.x - t.startValue.x, 0f, 0f);
				break;
			case AxisConstraint.Y:
				t.changeValue = new Vector3(0f, t.endValue.y - t.startValue.y, 0f);
				break;
			case AxisConstraint.Z:
				t.changeValue = new Vector3(0f, 0f, t.endValue.z - t.startValue.z);
				break;
			default:
				t.changeValue = t.endValue - t.startValue;
				break;
			}
		}

		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				startValue += changeValue * ((t.loopType != LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			switch (options.axisConstraint)
			{
			case AxisConstraint.X:
			{
				Vector3 pNewValue2 = getter();
				pNewValue2.x = startValue.x + changeValue.x * num;
				if (options.snapping)
				{
					pNewValue2.x = (float)Math.Round(pNewValue2.x);
				}
				setter(pNewValue2);
				return;
			}
			case AxisConstraint.Y:
			{
				Vector3 pNewValue = getter();
				pNewValue.y = startValue.y + changeValue.y * num;
				if (options.snapping)
				{
					pNewValue.y = (float)Math.Round(pNewValue.y);
				}
				setter(pNewValue);
				return;
			}
			case AxisConstraint.Z:
			{
				Vector3 pNewValue3 = getter();
				pNewValue3.z = startValue.z + changeValue.z * num;
				if (options.snapping)
				{
					pNewValue3.z = (float)Math.Round(pNewValue3.z);
				}
				setter(pNewValue3);
				return;
			}
			}
			startValue.x += changeValue.x * num;
			startValue.y += changeValue.y * num;
			startValue.z += changeValue.z * num;
			if (options.snapping)
			{
				startValue.x = (float)Math.Round(startValue.x);
				startValue.y = (float)Math.Round(startValue.y);
				startValue.z = (float)Math.Round(startValue.z);
			}
			setter(startValue);
		}
	}
}
