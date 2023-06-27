using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins.Core
{
	internal static class SpecialPluginsUtils
	{
		internal static bool SetLookAt(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			Transform transform = t.target as Transform;
			Vector3 endValue = t.endValue;
			endValue -= transform.position;
			switch (t.plugOptions.axisConstraint)
			{
			case AxisConstraint.X:
				endValue.x = 0f;
				break;
			case AxisConstraint.Y:
				endValue.y = 0f;
				break;
			case AxisConstraint.Z:
				endValue.z = 0f;
				break;
			}
			Vector3 vector = (t.endValue = Quaternion.LookRotation(endValue, t.plugOptions.up).eulerAngles);
			return true;
		}

		internal static bool SetPunch(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			Vector3 vector;
			try
			{
				vector = t.getter();
			}
			catch
			{
				return false;
			}
			t.isRelative = (t.isSpeedBased = false);
			t.easeType = Ease.OutQuad;
			t.customEase = null;
			int num = t.endValue.Length;
			for (int i = 0; i < num; i++)
			{
				t.endValue[i] = t.endValue[i] + vector;
			}
			return true;
		}

		internal static bool SetShake(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			if (!SetPunch(t))
			{
				return false;
			}
			t.easeType = Ease.Linear;
			return true;
		}

		internal static bool SetCameraShakePosition(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			if (!SetShake(t))
			{
				return false;
			}
			Camera camera = t.target as Camera;
			if (camera == null)
			{
				return false;
			}
			Vector3 vector = t.getter();
			Transform transform = camera.transform;
			int num = t.endValue.Length;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector2 = t.endValue[i];
				t.endValue[i] = transform.localRotation * (vector2 - vector) + vector;
			}
			return true;
		}
	}
}
