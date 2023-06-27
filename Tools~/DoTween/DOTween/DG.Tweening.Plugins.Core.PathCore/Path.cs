using System;
using DG.Tweening.Core;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	[Serializable]
	public class Path
	{
		private static CatmullRomDecoder _catmullRomDecoder;

		private static LinearDecoder _linearDecoder;

		private static CubicBezierDecoder _cubicBezierDecoder;

		public float[] wpLengths;

		/// <summary>
		/// Path waypoints (modified by PathPlugin when setting relative end/change value or by CubicBezierDecoder) and by DOTweenPathInspector
		/// </summary>
		[SerializeField]
		public Vector3[] wps;

		[SerializeField]
		internal PathType type;

		[SerializeField]
		internal int subdivisionsXSegment;

		[SerializeField]
		internal int subdivisions;

		[SerializeField]
		internal ControlPoint[] controlPoints;

		[SerializeField]
		internal float length;

		[SerializeField]
		internal bool isFinalized;

		[SerializeField]
		internal float[] timesTable;

		[SerializeField]
		internal float[] lengthsTable;

		internal int linearWPIndex = -1;

		internal bool addedExtraStartWp;

		internal bool addedExtraEndWp;

		private Path _incrementalClone;

		private int _incrementalIndex;

		private ABSPathDecoder _decoder;

		private bool _changed;

		internal Vector3[] nonLinearDrawWps;

		internal Vector3 targetPosition;

		internal Vector3? lookAtPosition;

		internal Color gizmoColor = new Color(1f, 1f, 1f, 0.7f);

		public Path(PathType type, Vector3[] waypoints, int subdivisionsXSegment, Color? gizmoColor = null)
		{
			this.type = type;
			this.subdivisionsXSegment = subdivisionsXSegment;
			if (gizmoColor.HasValue)
			{
				this.gizmoColor = gizmoColor.Value;
			}
			AssignWaypoints(waypoints, cloneWps: true);
			AssignDecoder(type);
			if (TweenManager.isUnityEditor)
			{
				DOTween.GizmosDelegates.Add(Draw);
			}
		}

		internal Path()
		{
		}

		internal void FinalizePath(bool isClosedPath, AxisConstraint lockPositionAxes, Vector3 currTargetVal)
		{
			if (lockPositionAxes != 0)
			{
				bool flag = (lockPositionAxes & AxisConstraint.X) == AxisConstraint.X;
				bool flag2 = (lockPositionAxes & AxisConstraint.Y) == AxisConstraint.Y;
				bool flag3 = (lockPositionAxes & AxisConstraint.Z) == AxisConstraint.Z;
				for (int i = 0; i < wps.Length; i++)
				{
					Vector3 vector = wps[i];
					wps[i] = new Vector3(flag ? currTargetVal.x : vector.x, flag2 ? currTargetVal.y : vector.y, flag3 ? currTargetVal.z : vector.z);
				}
			}
			_decoder.FinalizePath(this, wps, isClosedPath);
			isFinalized = true;
		}

		/// <summary>
		/// Gets the point on the path at the given percentage (0 to 1)
		/// </summary>
		/// <param name="perc">The percentage (0 to 1) at which to get the point</param>
		/// <param name="convertToConstantPerc">If TRUE constant speed is taken into account, otherwise not</param>
		internal Vector3 GetPoint(float perc, bool convertToConstantPerc = false)
		{
			if (convertToConstantPerc)
			{
				perc = ConvertToConstantPathPerc(perc);
			}
			return _decoder.GetPoint(perc, wps, this, controlPoints);
		}

		internal float ConvertToConstantPathPerc(float perc)
		{
			if (type == PathType.Linear)
			{
				return perc;
			}
			if (perc > 0f && perc < 1f)
			{
				float num = length * perc;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				int num6 = lengthsTable.Length;
				for (int i = 0; i < num6; i++)
				{
					if (lengthsTable[i] > num)
					{
						num4 = timesTable[i];
						num5 = lengthsTable[i];
						if (i > 0)
						{
							num3 = lengthsTable[i - 1];
						}
						break;
					}
					num2 = timesTable[i];
				}
				perc = num2 + (num - num3) / (num5 - num3) * (num4 - num2);
			}
			if (perc > 1f)
			{
				perc = 1f;
			}
			else if (perc < 0f)
			{
				perc = 0f;
			}
			return perc;
		}

		internal int GetWaypointIndexFromPerc(float perc, bool isMovingForward)
		{
			if (perc >= 1f)
			{
				return wps.Length - 1;
			}
			if (perc <= 0f)
			{
				return 0;
			}
			float num = length * perc;
			float num2 = 0f;
			int i = 0;
			for (int num3 = wpLengths.Length; i < num3; i++)
			{
				num2 += wpLengths[i];
				if (i == num3 - 1)
				{
					if (!isMovingForward)
					{
						return i;
					}
					return i - 1;
				}
				if (num2 < num)
				{
					continue;
				}
				if (num2 > num)
				{
					if (!isMovingForward)
					{
						return i;
					}
					return i - 1;
				}
				return i;
			}
			return 0;
		}

		internal static Vector3[] GetDrawPoints(Path p, int drawSubdivisionsXSegment)
		{
			int num = p.wps.Length;
			if (p.type == PathType.Linear)
			{
				return p.wps;
			}
			int num2 = num * drawSubdivisionsXSegment;
			Vector3[] array = new Vector3[num2 + 1];
			for (int i = 0; i <= num2; i++)
			{
				float perc = (float)i / (float)num2;
				Vector3 vector = (array[i] = p.GetPoint(perc));
			}
			return array;
		}

		internal static void RefreshNonLinearDrawWps(Path p)
		{
			int num = p.wps.Length * 10;
			if (p.nonLinearDrawWps == null || p.nonLinearDrawWps.Length != num + 1)
			{
				p.nonLinearDrawWps = new Vector3[num + 1];
			}
			for (int i = 0; i <= num; i++)
			{
				float perc = (float)i / (float)num;
				Vector3 point = p.GetPoint(perc);
				p.nonLinearDrawWps[i] = point;
			}
		}

		internal void Destroy()
		{
			if (TweenManager.isUnityEditor)
			{
				DOTween.GizmosDelegates.Remove(Draw);
			}
			wps = null;
			wpLengths = (timesTable = (lengthsTable = null));
			nonLinearDrawWps = null;
			isFinalized = false;
		}

		internal Path CloneIncremental(int loopIncrement)
		{
			if (_incrementalClone != null)
			{
				if (_incrementalIndex == loopIncrement)
				{
					return _incrementalClone;
				}
				_incrementalClone.Destroy();
			}
			int num = wps.Length;
			Vector3 vector = wps[num - 1] - wps[0];
			Vector3[] array = new Vector3[wps.Length];
			for (int i = 0; i < num; i++)
			{
				array[i] = wps[i] + vector * loopIncrement;
			}
			int num2 = controlPoints.Length;
			ControlPoint[] array2 = new ControlPoint[num2];
			for (int j = 0; j < num2; j++)
			{
				array2[j] = controlPoints[j] + vector * loopIncrement;
			}
			Vector3[] array3 = null;
			if (nonLinearDrawWps != null)
			{
				int num3 = nonLinearDrawWps.Length;
				array3 = new Vector3[num3];
				for (int k = 0; k < num3; k++)
				{
					array3[k] = nonLinearDrawWps[k] + vector * loopIncrement;
				}
			}
			_incrementalClone = new Path();
			_incrementalIndex = loopIncrement;
			_incrementalClone.type = type;
			_incrementalClone.subdivisionsXSegment = subdivisionsXSegment;
			_incrementalClone.subdivisions = subdivisions;
			_incrementalClone.wps = array;
			_incrementalClone.controlPoints = array2;
			if (TweenManager.isUnityEditor)
			{
				DOTween.GizmosDelegates.Add(_incrementalClone.Draw);
			}
			_incrementalClone.length = length;
			_incrementalClone.wpLengths = wpLengths;
			_incrementalClone.timesTable = timesTable;
			_incrementalClone.lengthsTable = lengthsTable;
			_incrementalClone._decoder = _decoder;
			_incrementalClone.nonLinearDrawWps = array3;
			_incrementalClone.targetPosition = targetPosition;
			_incrementalClone.lookAtPosition = lookAtPosition;
			_incrementalClone.isFinalized = true;
			return _incrementalClone;
		}

		internal void AssignWaypoints(Vector3[] newWps, bool cloneWps = false)
		{
			if (cloneWps)
			{
				int num = newWps.Length;
				wps = new Vector3[num];
				for (int i = 0; i < num; i++)
				{
					wps[i] = newWps[i];
				}
			}
			else
			{
				wps = newWps;
			}
		}

		internal void AssignDecoder(PathType pathType)
		{
			type = pathType;
			switch (pathType)
			{
			case PathType.Linear:
				if (_linearDecoder == null)
				{
					_linearDecoder = new LinearDecoder();
				}
				_decoder = _linearDecoder;
				break;
			case PathType.CubicBezier:
				if (_cubicBezierDecoder == null)
				{
					_cubicBezierDecoder = new CubicBezierDecoder();
				}
				_decoder = _cubicBezierDecoder;
				break;
			default:
				if (_catmullRomDecoder == null)
				{
					_catmullRomDecoder = new CatmullRomDecoder();
				}
				_decoder = _catmullRomDecoder;
				break;
			}
		}

		internal void Draw()
		{
			Draw(this);
		}

		private static void Draw(Path p)
		{
			if (p.timesTable == null)
			{
				return;
			}
			Color color = p.gizmoColor;
			color.a *= 0.5f;
			Gizmos.color = p.gizmoColor;
			int num = p.wps.Length;
			if (p._changed || (p.type != 0 && p.nonLinearDrawWps == null))
			{
				p._changed = false;
				if (p.type != 0)
				{
					RefreshNonLinearDrawWps(p);
				}
			}
			if (p.type == PathType.Linear)
			{
				Vector3 to = p.wps[0];
				for (int i = 0; i < num; i++)
				{
					Vector3 vector = p.wps[i];
					Gizmos.DrawLine(vector, to);
					to = vector;
				}
			}
			else
			{
				Vector3 to = p.nonLinearDrawWps[0];
				int num2 = p.nonLinearDrawWps.Length;
				for (int j = 1; j < num2; j++)
				{
					Vector3 vector2 = p.nonLinearDrawWps[j];
					Gizmos.DrawLine(vector2, to);
					to = vector2;
				}
			}
			Gizmos.color = color;
			for (int k = 0; k < num; k++)
			{
				Gizmos.DrawSphere(p.wps[k], 0.075f);
			}
			if (p.lookAtPosition.HasValue)
			{
				Vector3 value = p.lookAtPosition.Value;
				Gizmos.DrawLine(p.targetPosition, value);
				Gizmos.DrawWireSphere(value, 0.075f);
			}
		}
	}
}
