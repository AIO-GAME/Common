using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.CustomPlugins
{
	/// <summary>
	/// Straight Quaternion plugin. Instead of using Vector3 values accepts Quaternion values directly.
	/// <para>Beware: doesn't work with LoopType.Incremental (neither directly nor if inside a LoopType.Incremental Sequence).</para>
	/// <para>To use it, call DOTween.To with the plugin parameter overload, passing it <c>PureQuaternionPlugin.Plug()</c> as first parameter
	/// (do not use any of the other public PureQuaternionPlugin methods):</para>
	/// <code>DOTween.To(PureQuaternionPlugin.Plug(), ()=&gt; myQuaternionProperty, x=&gt; myQuaternionProperty = x, myQuaternionEndValue, duration);</code>
	/// </summary>
	public class PureQuaternionPlugin : ABSTweenPlugin<Quaternion, Quaternion, NoOptions>
	{
		private static PureQuaternionPlugin _plug;

		/// <summary>
		/// Plug this plugin inside a DOTween.To call.
		/// <para>Example:</para>
		/// <code>DOTween.To(PureQuaternionPlugin.Plug(), ()=&gt; myQuaternionProperty, x=&gt; myQuaternionProperty = x, myQuaternionEndValue, duration);</code>
		/// </summary>
		public static PureQuaternionPlugin Plug()
		{
			if (_plug == null)
			{
				_plug = new PureQuaternionPlugin();
			}
			return _plug;
		}

		/// <summary>INTERNAL: do not use</summary>
		public override void Reset(TweenerCore<Quaternion, Quaternion, NoOptions> t)
		{
		}

		/// <summary>INTERNAL: do not use</summary>
		public override void SetFrom(TweenerCore<Quaternion, Quaternion, NoOptions> t, bool isRelative)
		{
			Quaternion endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue * endValue) : endValue);
			t.setter(t.startValue);
		}

		/// <summary>INTERNAL: do not use</summary>
		public override void SetFrom(TweenerCore<Quaternion, Quaternion, NoOptions> t, Quaternion fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				Quaternion quaternion = t.getter();
				t.endValue = quaternion * t.endValue;
				fromValue = quaternion * fromValue;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		/// <summary>INTERNAL: do not use</summary>
		public override Quaternion ConvertToStartValue(TweenerCore<Quaternion, Quaternion, NoOptions> t, Quaternion value)
		{
			return value;
		}

		/// <summary>INTERNAL: do not use</summary>
		public override void SetRelativeEndValue(TweenerCore<Quaternion, Quaternion, NoOptions> t)
		{
			t.endValue *= t.startValue;
		}

		/// <summary>INTERNAL: do not use</summary>
		public override void SetChangeValue(TweenerCore<Quaternion, Quaternion, NoOptions> t)
		{
			t.changeValue = t.endValue;
		}

		/// <summary>INTERNAL: do not use</summary>
		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, Quaternion changeValue)
		{
			return changeValue.eulerAngles.magnitude / unitsXSecond;
		}

		/// <summary>INTERNAL: do not use</summary>
		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, float elapsed, Quaternion startValue, Quaternion changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			float t2 = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			setter(Quaternion.Slerp(startValue, changeValue, t2));
		}
	}
}
