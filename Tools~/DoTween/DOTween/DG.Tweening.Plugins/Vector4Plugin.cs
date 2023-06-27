using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public class Vector4Plugin : ABSTweenPlugin<Vector4, Vector4, VectorOptions>
	{
		public override void Reset(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
		}

		public override void SetFrom(TweenerCore<Vector4, Vector4, VectorOptions> t, bool isRelative)
		{
			Vector4 endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue + endValue) : endValue);
			Vector4 pNewValue = t.endValue;
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
			case AxisConstraint.W:
				pNewValue.w = t.startValue.w;
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
				pNewValue.w = (float)Math.Round(pNewValue.w);
			}
			t.setter(pNewValue);
		}

		public override void SetFrom(TweenerCore<Vector4, Vector4, VectorOptions> t, Vector4 fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				Vector4 vector = t.getter();
				t.endValue += vector;
				fromValue += vector;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				Vector4 pNewValue;
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
				case AxisConstraint.W:
					pNewValue = t.getter();
					pNewValue.w = fromValue.w;
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
					pNewValue.w = (float)Math.Round(pNewValue.w);
				}
				t.setter(pNewValue);
			}
		}

		public override Vector4 ConvertToStartValue(TweenerCore<Vector4, Vector4, VectorOptions> t, Vector4 value)
		{
			return value;
		}

		public override void SetRelativeEndValue(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
			t.endValue += t.startValue;
		}

		public override void SetChangeValue(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
			switch (t.plugOptions.axisConstraint)
			{
			case AxisConstraint.X:
				t.changeValue = new Vector4(t.endValue.x - t.startValue.x, 0f, 0f, 0f);
				break;
			case AxisConstraint.Y:
				t.changeValue = new Vector4(0f, t.endValue.y - t.startValue.y, 0f, 0f);
				break;
			case AxisConstraint.Z:
				t.changeValue = new Vector4(0f, 0f, t.endValue.z - t.startValue.z, 0f);
				break;
			case AxisConstraint.W:
				t.changeValue = new Vector4(0f, 0f, 0f, t.endValue.w - t.startValue.w);
				break;
			default:
				t.changeValue = t.endValue - t.startValue;
				break;
			}
		}

		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector4 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector4> getter, DOSetter<Vector4> setter, float elapsed, Vector4 startValue, Vector4 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
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
				Vector4 pNewValue2 = getter();
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
				Vector4 pNewValue4 = getter();
				pNewValue4.y = startValue.y + changeValue.y * num;
				if (options.snapping)
				{
					pNewValue4.y = (float)Math.Round(pNewValue4.y);
				}
				setter(pNewValue4);
				return;
			}
			case AxisConstraint.Z:
			{
				Vector4 pNewValue = getter();
				pNewValue.z = startValue.z + changeValue.z * num;
				if (options.snapping)
				{
					pNewValue.z = (float)Math.Round(pNewValue.z);
				}
				setter(pNewValue);
				return;
			}
			case AxisConstraint.W:
			{
				Vector4 pNewValue3 = getter();
				pNewValue3.w = startValue.w + changeValue.w * num;
				if (options.snapping)
				{
					pNewValue3.w = (float)Math.Round(pNewValue3.w);
				}
				setter(pNewValue3);
				return;
			}
			}
			startValue.x += changeValue.x * num;
			startValue.y += changeValue.y * num;
			startValue.z += changeValue.z * num;
			startValue.w += changeValue.w * num;
			if (options.snapping)
			{
				startValue.x = (float)Math.Round(startValue.x);
				startValue.y = (float)Math.Round(startValue.y);
				startValue.z = (float)Math.Round(startValue.z);
				startValue.w = (float)Math.Round(startValue.w);
			}
			setter(startValue);
		}
	}
}
