using System.Collections.Generic;
using DG.DemiLib;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core
{
	internal class SnappingManager
	{
		private const float _MaxSnappingDistance = 7f;

		private readonly List<float> _topSnappingPs = new List<float>();

		private readonly List<float> _bottomSnappingPs = new List<float>();

		private readonly List<float> _leftSnappingPs = new List<float>();

		private readonly List<float> _rightSnappingPs = new List<float>();

		public bool hasSnapX { get; private set; }

		public bool hasSnapY { get; private set; }

		public int snapXPosition { get; private set; }

		public int snapYPosition { get; private set; }

		public bool showVerticalGuide { get; private set; }

		public bool showHorizontalGuide { get; private set; }

		public float snapX { get; private set; }

		public float snapY { get; private set; }

		public void EvaluateSnapping(IEditorGUINode forNode, Rect forArea, List<IEditorGUINode> allNodes, List<IEditorGUINode> excludedNodes, Dictionary<IEditorGUINode, NodeGUIData> nodeToGuiData, Rect processRelativeArea)
		{
			bool flag2 = (showVerticalGuide = false);
			bool flag4 = (showHorizontalGuide = flag2);
			bool flag6 = (hasSnapY = flag4);
			hasSnapX = flag6;
			int num3 = (snapXPosition = (snapYPosition = 0));
			_topSnappingPs.Clear();
			_bottomSnappingPs.Clear();
			_leftSnappingPs.Clear();
			_rightSnappingPs.Clear();
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			if (Event.current.alt || nodeToGuiData[forNode].disableSnapping)
			{
				return;
			}
			int count = allNodes.Count;
			for (int i = 0; i < count; i++)
			{
				IEditorGUINode editorGUINode = allNodes[i];
				if (editorGUINode == forNode || excludedNodes.Contains(editorGUINode))
				{
					continue;
				}
				NodeGUIData nodeGUIData = nodeToGuiData[editorGUINode];
				if (nodeGUIData.disableSnapping)
				{
					continue;
				}
				Rect fullArea = nodeGUIData.fullArea;
				if (!flag7 && forArea.yMax > fullArea.y && forArea.y < fullArea.yMax)
				{
					if (forArea.xMax < fullArea.x && fullArea.x - forArea.xMax <= 12f)
					{
						flag7 = (hasSnapX = true);
						snapX = fullArea.x - forArea.width - 12f;
						if (flag8)
						{
							break;
						}
					}
					else if (forArea.x > fullArea.xMax && forArea.x - fullArea.xMax <= 12f)
					{
						flag7 = (hasSnapX = true);
						snapX = fullArea.xMax + 12f;
						if (flag8)
						{
							break;
						}
					}
				}
				if (flag8 || !(forArea.xMax >= fullArea.x) || !(forArea.x < fullArea.xMax))
				{
					continue;
				}
				if (forArea.yMax < fullArea.y && fullArea.y - forArea.yMax <= 12f)
				{
					flag8 = (hasSnapY = true);
					snapY = fullArea.y - forArea.height - 12f;
					if (flag7)
					{
						break;
					}
				}
				else if (forArea.y > fullArea.yMax && forArea.y - fullArea.yMax <= 12f)
				{
					flag8 = (hasSnapY = true);
					snapY = fullArea.yMax + 12f;
					if (flag7)
					{
						break;
					}
				}
			}
			if (flag7 && flag8)
			{
				return;
			}
			for (int j = 0; j < count; j++)
			{
				IEditorGUINode editorGUINode2 = allNodes[j];
				if (editorGUINode2 == forNode || excludedNodes.Contains(editorGUINode2))
				{
					continue;
				}
				NodeGUIData nodeGUIData2 = nodeToGuiData[editorGUINode2];
				if (nodeGUIData2.disableSnapping)
				{
					continue;
				}
				Rect fullArea2 = nodeGUIData2.fullArea;
				if (!processRelativeArea.Overlaps(fullArea2))
				{
					continue;
				}
				if (!flag7)
				{
					if (ValuesAreWithinBorderSnappingRange(forArea.x, fullArea2.x))
					{
						_leftSnappingPs.Add(fullArea2.x);
						flag6 = (showVerticalGuide = (flag9 = (flag12 = true)));
						hasSnapX = flag6;
					}
					else if (ValuesAreWithinBorderSnappingRange(forArea.x, fullArea2.xMax))
					{
						_rightSnappingPs.Add(fullArea2.xMax);
						flag6 = (showVerticalGuide = (flag9 = (flag13 = true)));
						hasSnapX = flag6;
					}
					else if (ValuesAreWithinBorderSnappingRange(forArea.xMax, fullArea2.xMax))
					{
						_rightSnappingPs.Add(fullArea2.xMax);
						flag6 = (showVerticalGuide = (flag9 = (flag13 = true)));
						hasSnapX = flag6;
						snapXPosition = 1;
					}
				}
				if (!flag8)
				{
					if (ValuesAreWithinBorderSnappingRange(forArea.y, fullArea2.y))
					{
						_topSnappingPs.Add(fullArea2.y);
						flag6 = (showHorizontalGuide = (flag9 = (flag10 = true)));
						hasSnapY = flag6;
					}
					else if (ValuesAreWithinBorderSnappingRange(forArea.y, fullArea2.yMax))
					{
						_bottomSnappingPs.Add(fullArea2.yMax);
						flag6 = (showHorizontalGuide = (flag9 = (flag11 = true)));
						hasSnapY = flag6;
					}
					else if (ValuesAreWithinBorderSnappingRange(forArea.yMax, fullArea2.yMax))
					{
						_bottomSnappingPs.Add(fullArea2.yMax);
						flag6 = (showHorizontalGuide = (flag9 = (flag11 = true)));
						hasSnapY = flag6;
						snapYPosition = 1;
					}
				}
			}
			if (flag9)
			{
				if (flag12)
				{
					snapX = FindNearestValueTo(forArea.x, _leftSnappingPs);
				}
				else if (flag13)
				{
					snapX = ((snapXPosition == 0) ? FindNearestValueTo(forArea.x, _rightSnappingPs) : (FindNearestValueTo(forArea.xMax, _rightSnappingPs) - forArea.width));
				}
				if (flag10)
				{
					snapY = FindNearestValueTo(forArea.y, _topSnappingPs);
				}
				else if (flag11)
				{
					snapY = ((snapYPosition == 0) ? FindNearestValueTo(forArea.y, _bottomSnappingPs) : (FindNearestValueTo(forArea.yMax, _bottomSnappingPs) - forArea.height));
				}
			}
		}

		private bool ValuesAreWithinBorderSnappingRange(float a, float b)
		{
			return Mathf.Abs(a - b) <= 7f;
		}

		private float FindNearestValueTo(float a, List<float> values)
		{
			float result = a;
			float num = 10000f;
			for (int i = 0; i < values.Count; i++)
			{
				float num2 = values[i];
				float num3 = Mathf.Abs(a - num2);
				if (num3 < num)
				{
					num = num3;
					result = num2;
				}
			}
			return result;
		}
	}
}
