using System.Collections.Generic;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
	public struct NodeGUIData
	{
		public Rect fullArea;

		public Rect dragArea;

		public Rect extraArea0;

		public Color mainColor;

		public bool disableSnapping;

		public List<Rect> connectorAreas;

		public bool isVisible { get; internal set; }

		public NodeGUIData(Rect fullArea, Color? mainColor = null, Rect? extraArea0 = null)
		{
			this = default(NodeGUIData);
			this.fullArea = fullArea;
			this.extraArea0 = ((!extraArea0.HasValue) ? default(Rect) : extraArea0.Value);
			this.mainColor = ((!mainColor.HasValue) ? new Color(0.5f, 0.5f, 0.5f, 1f) : mainColor.Value);
			dragArea = fullArea;
			connectorAreas = null;
		}

		public NodeGUIData(Rect fullArea, Rect dragArea, Rect? extraArea0 = null)
		{
			this = default(NodeGUIData);
			this.fullArea = fullArea;
			this.dragArea = dragArea;
			this.extraArea0 = ((!extraArea0.HasValue) ? default(Rect) : extraArea0.Value);
			mainColor = new Color(0.5f, 0.5f, 0.5f, 1f);
			connectorAreas = null;
		}

		public NodeGUIData(Rect fullArea, Rect dragArea, Color mainColor, Rect? extraArea0 = null)
		{
			this = default(NodeGUIData);
			this.fullArea = fullArea;
			this.dragArea = dragArea;
			this.extraArea0 = ((!extraArea0.HasValue) ? default(Rect) : extraArea0.Value);
			this.mainColor = mainColor;
			connectorAreas = null;
		}

		/// <summary>
		/// Recalculates all rects based on the given Y shift
		/// </summary>
		public void ShiftYBy(float value)
		{
			fullArea.y += value;
			dragArea.y += value;
			extraArea0.y += value;
			if (connectorAreas != null)
			{
				for (int i = 0; i < connectorAreas.Count; i++)
				{
					Rect value2 = connectorAreas[i];
					value2.y += value;
					connectorAreas[i] = value2;
				}
			}
		}

		public override string ToString()
		{
			return $"fullArea: {fullArea} - dragArea: {dragArea} - extraArea0: {extraArea0}";
		}
	}
}
