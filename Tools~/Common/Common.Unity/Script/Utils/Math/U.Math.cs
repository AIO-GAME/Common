using System;

using UnityEngine;

public static partial class UtilsEngine
{
	/// <summary>
	/// Unity 计算
	/// </summary>
	public static partial class MathX
	{
		/// <summary> 
		/// 两点距离
		/// </summary>
		public static float Distance(Vector2 one, Vector2 two)
		{
			return Math.Abs(one.x - two.x) + Math.Abs(one.y - two.y);
		}

		/// <summary> 
		/// 矩形相交
		/// </summary>
		public static bool IsRect(in Rect one, in Rect two)
		{
			var point = new Vector2(two.x, two.y);//左上角
			for (int i = 0; i < 4; i++)
			{
				if (one.Contains(point))
					return true;
				if (i == 0)
					point = new Vector2(two.x, two.y + two.height);
				else if (i == 1)
					point = new Vector2(two.x + two.width, two.y);
				else if (i == 2)
					point = new Vector2(two.x + two.width, two.y + two.width);
			}
			point = new Vector2(one.x, one.y);
			for (int i = 0; i < 4; i++)
			{
				if (two.Contains(point))
					return true;
				if (i == 0)
					point = new Vector2(one.x, one.y + one.height);
				else if (i == 1)
					point = new Vector2(one.x + one.width, one.y);
				else if (i == 2)
					point = new Vector2(one.x + one.width, one.y + one.width);
			}
			return false;
		}
	}
}