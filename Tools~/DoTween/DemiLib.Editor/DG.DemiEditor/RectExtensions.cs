using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Replicates DeExtensions.RectExtensions for internal usage
	/// </summary>
	public static class RectExtensions
	{
		/// <summary>
		/// Adds one rect into another, and returns the resulting a
		/// </summary>
		public static Rect Add(this Rect a, Rect b)
		{
			if (b.xMin < a.xMin)
			{
				a.xMin = b.xMin;
			}
			if (b.xMax > a.xMax)
			{
				a.xMax = b.xMax;
			}
			if (b.yMin < a.yMin)
			{
				a.yMin = b.yMin;
			}
			if (b.yMax > a.yMax)
			{
				a.yMax = b.yMax;
			}
			return a;
		}

		/// <summary>
		/// Returns a copy or the Rect expanded around its center by the given amount
		/// </summary>
		/// <param name="amount">Indicates how much to expand the rect on each size</param>
		public static Rect Expand(this Rect r, float amount)
		{
			float num = amount * 2f;
			return r.Shift(0f - amount, 0f - amount, num, num);
		}

		/// <summary>
		/// Returns a copy or the Rect expanded around its center by the given amount
		/// </summary>
		/// <param name="amountX">Indicates how much to expand the rect on each horizontal side</param>
		/// <param name="amountY">Indicates how much to expand the rect on each vertical side</param>
		public static Rect Expand(this Rect r, float amountX, float amountY)
		{
			return r.Shift(0f - amountX, 0f - amountY, amountX * 2f, amountY * 2f);
		}

		/// <summary>
		/// Returns a copy or the Rect contracted around its center by the given amount
		/// </summary>
		/// <param name="amount">Indicates how much to contract the rect on each size</param>
		public static Rect Contract(this Rect r, float amount)
		{
			float num = amount * 2f;
			return r.Shift(amount, amount, 0f - num, 0f - num);
		}

		/// <summary>
		/// Returns a copy or the Rect contracted around its center by the given amount
		/// </summary>
		/// <param name="amountX">Indicates how much to contract the rect on each horizontal side</param>
		/// <param name="amountY">Indicates how much to contract the rect on each vertical side</param>
		public static Rect Contract(this Rect r, float amountX, float amountY)
		{
			return r.Shift(amountX, amountY, (0f - amountX) * 2f, (0f - amountY) * 2f);
		}

		/// <summary>
		/// Returns a copy of the Rect resized so it fits proportionally within the given size limits
		/// </summary>
		/// <param name="w">Width to fit</param>
		/// <param name="h">Height to fit</param>
		/// <param name="shrinkOnly">If TRUE (default) only shrinks the rect if needed, if FALSE also enlarges it to fit</param>
		/// <returns></returns>
		public static Rect Fit(this Rect r, float w, float h, bool shrinkOnly = true)
		{
			if (shrinkOnly && r.width <= w && r.height <= h)
			{
				return r;
			}
			float num = w / r.width;
			float num2 = h / r.height;
			float num3 = ((num < num2) ? num : num2);
			r.width *= num3;
			r.height *= num3;
			return r;
		}

		/// <summary>
		/// Returns TRUE if the first rect includes the second one
		/// </summary>
		/// <param name="full">If TRUE, returns TRUE only if the second rect is fully included,
		/// otherwise just if some part of it is included</param>
		public static bool Includes(this Rect a, Rect b, bool full = true)
		{
			if (full)
			{
				if (a.xMin <= b.xMin && a.xMax > b.xMax && a.yMin <= b.yMin)
				{
					return a.yMax >= b.yMax;
				}
				return false;
			}
			if (b.xMax > a.xMin && b.xMin < a.xMax && b.yMax > a.yMin)
			{
				return b.yMin < a.yMax;
			}
			return false;
		}

		/// <summary>
		/// Returns a copy of the Rect with its X/Y coordinates set to 0
		/// </summary>
		public static Rect ResetXY(this Rect r)
		{
			float num3 = (r.x = (r.y = 0f));
			return r;
		}

		/// <summary>
		/// Returns a copy of the Rect with its values shifted according the the given parameters
		/// </summary>
		public static Rect Shift(this Rect r, float x, float y, float width, float height)
		{
			return new Rect(r.x + x, r.y + y, r.width + width, r.height + height);
		}

		/// <summary>
		/// Returns a copy of the Rect with its X value shifted by the given value
		/// </summary>
		public static Rect ShiftX(this Rect r, float x)
		{
			return new Rect(r.x + x, r.y, r.width, r.height);
		}

		/// <summary>
		/// Returns a copy of the Rect with its Y value shifted by the given value
		/// </summary>
		public static Rect ShiftY(this Rect r, float y)
		{
			return new Rect(r.x, r.y + y, r.width, r.height);
		}

		/// <summary>
		/// Returns a copy of the Rect with its x shifted by the given value and its width shrinked/expanded accordingly
		/// (so that the xMax value will stay the same as before)
		/// </summary>
		public static Rect ShiftXAndResize(this Rect r, float x)
		{
			return new Rect(r.x + x, r.y, r.width - x, r.height);
		}

		/// <summary>
		/// Returns a copy of the Rect with its y shifted by the given value and its height shrinked/expanded accordingly
		/// (so that the yMax value will stay the same as before)
		/// </summary>
		public static Rect ShiftYAndResize(this Rect r, float y)
		{
			return new Rect(r.x, r.y + y, r.width, r.height - y);
		}

		/// <summary>
		/// Returns a copy of the Rect with its X property set to the given value
		/// </summary>
		public static Rect SetX(this Rect r, float value)
		{
			r.x = value;
			return r;
		}

		/// <summary>
		/// Returns a copy of the Rect with its Y property set to the given value
		/// </summary>
		public static Rect SetY(this Rect r, float value)
		{
			r.y = value;
			return r;
		}

		/// <summary>
		/// Returns a copy of the Rect with its height property set to the given value
		/// </summary>
		public static Rect SetHeight(this Rect r, float value)
		{
			r.height = value;
			return r;
		}

		/// <summary>
		/// Returns a copy of the Rect with its width property set to the given value
		/// </summary>
		public static Rect SetWidth(this Rect r, float value)
		{
			r.width = value;
			return r;
		}

		/// <summary>
		/// Returns a copy of the Rect with its X,Y properties set so the rect center corresponds to the given values
		/// </summary>
		public static Rect SetCenter(this Rect r, float x, float y)
		{
			r.x = x - r.width * 0.5f;
			r.y = y - r.height * 0.5f;
			return r;
		}

		/// <summary>
		/// Returns a copy of the Rect with its X property set so the rect X center corresponds to the given value
		/// </summary>
		public static Rect SetCenterX(this Rect r, float value)
		{
			r.x = value - r.width * 0.5f;
			return r;
		}

		/// <summary>
		/// Returns a copy of the Rect with its Y property set so the rect Y center corresponds to the given value
		/// </summary>
		public static Rect SetCenterY(this Rect r, float value)
		{
			r.y = value - r.height * 0.5f;
			return r;
		}
	}
}
