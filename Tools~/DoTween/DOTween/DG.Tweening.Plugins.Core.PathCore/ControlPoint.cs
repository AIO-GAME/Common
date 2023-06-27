using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	/// <summary>
	/// Path control point
	/// </summary>
	[Serializable]
	public struct ControlPoint
	{
		public Vector3 a;

		public Vector3 b;

		public ControlPoint(Vector3 a, Vector3 b)
		{
			this.a = a;
			this.b = b;
		}

		public static ControlPoint operator +(ControlPoint cp, Vector3 v)
		{
			return new ControlPoint(cp.a + v, cp.b + v);
		}

		public override string ToString()
		{
			return string.Concat("[", a.ToString(), " | ", b.ToString(), "]");
		}
	}
}
