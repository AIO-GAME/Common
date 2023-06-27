using System;
using System.Reflection;
using UnityEngine;

namespace DG.Tweening.Core
{
	internal static class Utils
	{
		private static Assembly[] _loadedAssemblies;

		private static readonly string[] _defAssembliesToQuery = new string[3] { "DOTween.Modules", "Assembly-CSharp", "Assembly-CSharp-firstpass" };

		/// <summary>
		/// Returns a Vector3 with z = 0
		/// </summary>
		internal static Vector3 Vector3FromAngle(float degrees, float magnitude)
		{
			float f = degrees * ((float)Math.PI / 180f);
			return new Vector3(magnitude * Mathf.Cos(f), magnitude * Mathf.Sin(f), 0f);
		}

		/// <summary>
		/// Returns the 2D angle between two vectors
		/// </summary>
		internal static float Angle2D(Vector3 from, Vector3 to)
		{
			Vector2 right = Vector2.right;
			to -= from;
			float num = Vector2.Angle(right, to);
			if (Vector3.Cross(right, to).z > 0f)
			{
				num = 360f - num;
			}
			return num * -1f;
		}

		internal static Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
		{
			return rotation * (point - pivot) + pivot;
		}

		/// <summary>
		/// Uses approximate equality on each axis instead of Unity's Vector3 equality,
		/// because the latter fails (in some cases) when assigning a Vector3 to a transform.position and then checking it.
		/// </summary>
		internal static bool Vector3AreApproximatelyEqual(Vector3 a, Vector3 b)
		{
			if (Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y))
			{
				return Mathf.Approximately(a.z, b.z);
			}
			return false;
		}

		/// <summary>
		/// Looks for the type within all possible project assembly names
		/// </summary>
		internal static Type GetLooseScriptType(string typeName)
		{
			for (int i = 0; i < _defAssembliesToQuery.Length; i++)
			{
				Type type = Type.GetType($"{typeName}, {_defAssembliesToQuery[i]}");
				if (type != null)
				{
					return type;
				}
			}
			if (_loadedAssemblies == null)
			{
				_loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			}
			for (int j = 0; j < _loadedAssemblies.Length; j++)
			{
				Type type2 = Type.GetType($"{typeName}, {_loadedAssemblies[j].GetName()}");
				if (type2 != null)
				{
					return type2;
				}
			}
			return null;
		}
	}
}
