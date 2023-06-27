using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	public class RectOffsetPlugin : ABSTweenPlugin<RectOffset, RectOffset, NoOptions>
	{
		private static RectOffset _r = new RectOffset();

		public override void Reset(TweenerCore<RectOffset, RectOffset, NoOptions> t)
		{
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		public override void SetFrom(TweenerCore<RectOffset, RectOffset, NoOptions> t, bool isRelative)
		{
			RectOffset endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			if (isRelative)
			{
				t.startValue.left += t.endValue.left;
				t.startValue.right += t.endValue.right;
				t.startValue.top += t.endValue.top;
				t.startValue.bottom += t.endValue.bottom;
			}
			t.setter(t.startValue);
		}

		public override void SetFrom(TweenerCore<RectOffset, RectOffset, NoOptions> t, RectOffset fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				RectOffset rectOffset = t.getter();
				t.endValue.left += rectOffset.left;
				t.endValue.right += rectOffset.right;
				t.endValue.top += rectOffset.top;
				t.endValue.bottom += rectOffset.bottom;
				fromValue.left += rectOffset.left;
				fromValue.right += rectOffset.right;
				fromValue.top += rectOffset.top;
				fromValue.bottom += rectOffset.bottom;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		public override RectOffset ConvertToStartValue(TweenerCore<RectOffset, RectOffset, NoOptions> t, RectOffset value)
		{
			return new RectOffset(value.left, value.right, value.top, value.bottom);
		}

		public override void SetRelativeEndValue(TweenerCore<RectOffset, RectOffset, NoOptions> t)
		{
			t.endValue.left += t.startValue.left;
			t.endValue.right += t.startValue.right;
			t.endValue.top += t.startValue.top;
			t.endValue.bottom += t.startValue.bottom;
		}

		public override void SetChangeValue(TweenerCore<RectOffset, RectOffset, NoOptions> t)
		{
			t.changeValue = new RectOffset(t.endValue.left - t.startValue.left, t.endValue.right - t.startValue.right, t.endValue.top - t.startValue.top, t.endValue.bottom - t.startValue.bottom);
		}

		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, RectOffset changeValue)
		{
			float num = changeValue.right;
			if (num < 0f)
			{
				num = 0f - num;
			}
			float num2 = changeValue.bottom;
			if (num2 < 0f)
			{
				num2 = 0f - num2;
			}
			return (float)Math.Sqrt(num * num + num2 * num2) / unitsXSecond;
		}

		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<RectOffset> getter, DOSetter<RectOffset> setter, float elapsed, RectOffset startValue, RectOffset changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			_r.left = startValue.left;
			_r.right = startValue.right;
			_r.top = startValue.top;
			_r.bottom = startValue.bottom;
			if (t.loopType == LoopType.Incremental)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				_r.left += changeValue.left * num;
				_r.right += changeValue.right * num;
				_r.top += changeValue.top * num;
				_r.bottom += changeValue.bottom * num;
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				int num2 = ((t.loopType != LoopType.Incremental) ? 1 : t.loops) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				_r.left += changeValue.left * num2;
				_r.right += changeValue.right * num2;
				_r.top += changeValue.top * num2;
				_r.bottom += changeValue.bottom * num2;
			}
			float num3 = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			setter(new RectOffset((int)Math.Round((float)_r.left + (float)changeValue.left * num3), (int)Math.Round((float)_r.right + (float)changeValue.right * num3), (int)Math.Round((float)_r.top + (float)changeValue.top * num3), (int)Math.Round((float)_r.bottom + (float)changeValue.bottom * num3)));
		}
	}
}
