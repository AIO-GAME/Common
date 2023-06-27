using System.Collections.Generic;
using DG.DemiLib;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core
{
	internal class ContextPanel
	{
		private enum GroupAlignment
		{
			Left,
			HCenter,
			Right,
			Top,
			VCenter,
			Bottom
		}

		private class Styles
		{
			public GUIStyle headerLabel;

			public GUIStyle btIco;

			private bool _initialized;

			public void Init()
			{
				if (!_initialized)
				{
					_initialized = true;
					headerLabel = new GUIStyle(GUI.skin.label).Add(9, Color.white, TextAnchor.MiddleCenter).Background(DeStylePalette.blueSquare).Padding(0)
						.Margin(0)
						.ContentOffsetY(-1f);
					btIco = DeGUI.styles.button.flatWhite.Clone().Background(null);
				}
			}
		}

		private const int _Margin = 4;

		private const int _HPadding = 6;

		private const int _VPadding = 4;

		private const int _HeaderHeight = 12;

		private const int _IcoSize = 16;

		private const int _IcosDistance = 4;

		private NodeProcess _process;

		private static readonly Styles _Styles = new Styles();

		private Rect _area;

		private bool _isVisible => _process.selection.selectedNodes.Count > 1;

		public ContextPanel(NodeProcess process)
		{
			_process = process;
		}

		public void Draw()
		{
			if (!_isVisible)
			{
				return;
			}
			_Styles.Init();
			_area = _process.relativeArea.SetWidth(173f).SetHeight(36f);
			_area = _area.SetX(_process.relativeArea.xMax - _area.width - 4f).Shift(0f, 4f, 0f, 0f);
			_area.x = (int)_area.x;
			_area.y = (int)_area.y;
			Rect source = new Rect(_area.x + 96f + 24f + 6f, _area.y, 2f, _area.height);
			DeGUI.DrawColoredSquare(_area, Color.black);
			using (new DeGUI.ColorScope(DeGUI.colors.global.blue))
			{
				GUI.Box(_area.Expand(1f), "", DeGUI.styles.box.outline01);
			}
			DeGUI.DrawColoredSquare(new Rect(source), DeGUI.colors.global.blue);
			GUI.Label(_area.SetHeight(12f), $"{_process.selection.selectedNodes.Count} nodes selected", _Styles.headerLabel);
			Rect position = new Rect(_area.x + 6f, _area.y + 4f + 12f, 16f, 16f);
			for (int i = 0; i < 8; i++)
			{
				if (i == 6)
				{
					position.x += 6f;
				}
				if (GUI.Button(position, IndexToIcon(i), _Styles.btIco) && Event.current.button == 0)
				{
					if (i > 5)
					{
						ArrangeSelectedNodes(i > 6, _process.selection.selectedNodes);
					}
					AlignSelectedNodes(IndexToGroupAlignment(i), _process.selection.selectedNodes);
				}
				position.x += 20f;
			}
		}

		public bool HasMouseOver()
		{
			if (!_isVisible)
			{
				return false;
			}
			return _area.Contains(Event.current.mousePosition);
		}

		public void AlignAndArrangeNodes(bool horizontally, List<IEditorGUINode> nodes)
		{
			ArrangeSelectedNodes(horizontally, nodes);
			AlignSelectedNodes(horizontally ? GroupAlignment.Top : GroupAlignment.Left, nodes);
		}

		private void AlignSelectedNodes(GroupAlignment alignment, List<IEditorGUINode> nodes)
		{
			int count = nodes.Count;
			if (count <= 1)
			{
				return;
			}
			GUI.changed = true;
			Vector2 areaShift = _process.areaShift;
			Vector2 vector = Vector2.zero;
			switch (alignment)
			{
			case GroupAlignment.Left:
			{
				for (int k = 0; k < count; k++)
				{
					Rect fullArea2 = _process.nodeToGUIData[nodes[k]].fullArea;
					if (k == 0)
					{
						vector = new Vector2(fullArea2.x, 0f);
					}
					else if (fullArea2.x < vector.x)
					{
						vector.x = fullArea2.x;
					}
				}
				for (int l = 0; l < count; l++)
				{
					IEditorGUINode editorGUINode2 = nodes[l];
					editorGUINode2.guiPosition = new Vector2(vector.x - areaShift.x, editorGUINode2.guiPosition.y);
				}
				break;
			}
			case GroupAlignment.Right:
			{
				for (int m = 0; m < count; m++)
				{
					Rect fullArea3 = _process.nodeToGUIData[nodes[m]].fullArea;
					if (m == 0)
					{
						vector = new Vector2(fullArea3.xMax, 0f);
					}
					else if (fullArea3.xMax > vector.x)
					{
						vector.x = fullArea3.xMax;
					}
				}
				for (int n = 0; n < count; n++)
				{
					IEditorGUINode editorGUINode3 = nodes[n];
					editorGUINode3.guiPosition = new Vector2(vector.x - _process.nodeToGUIData[nodes[n]].fullArea.width - areaShift.x, editorGUINode3.guiPosition.y);
				}
				break;
			}
			case GroupAlignment.VCenter:
			{
				for (int num5 = 0; num5 < count; num5++)
				{
					Rect fullArea6 = _process.nodeToGUIData[nodes[num5]].fullArea;
					if (num5 == 0)
					{
						vector = new Vector2(fullArea6.center.x, 0f);
					}
					else if (fullArea6.center.x > vector.x)
					{
						vector.x += (fullArea6.center.x - vector.x) * 0.5f;
					}
					else if (fullArea6.center.x < vector.x)
					{
						vector.x -= (vector.x - fullArea6.center.x) * 0.5f;
					}
				}
				for (int num6 = 0; num6 < count; num6++)
				{
					IEditorGUINode editorGUINode6 = nodes[num6];
					editorGUINode6.guiPosition = new Vector2(vector.x - _process.nodeToGUIData[nodes[num6]].fullArea.width * 0.5f - areaShift.x, editorGUINode6.guiPosition.y);
				}
				break;
			}
			case GroupAlignment.Top:
			{
				for (int num = 0; num < count; num++)
				{
					Rect fullArea4 = _process.nodeToGUIData[nodes[num]].fullArea;
					if (num == 0)
					{
						vector = new Vector2(0f, fullArea4.y);
					}
					else if (fullArea4.y < vector.y)
					{
						vector.y = fullArea4.y;
					}
				}
				for (int num2 = 0; num2 < count; num2++)
				{
					IEditorGUINode editorGUINode4 = nodes[num2];
					editorGUINode4.guiPosition = new Vector2(editorGUINode4.guiPosition.x, vector.y - areaShift.y);
				}
				break;
			}
			case GroupAlignment.Bottom:
			{
				for (int num3 = 0; num3 < count; num3++)
				{
					Rect fullArea5 = _process.nodeToGUIData[nodes[num3]].fullArea;
					if (num3 == 0)
					{
						vector = new Vector2(0f, fullArea5.yMax);
					}
					else if (fullArea5.yMax > vector.y)
					{
						vector.y = fullArea5.yMax;
					}
				}
				for (int num4 = 0; num4 < count; num4++)
				{
					IEditorGUINode editorGUINode5 = nodes[num4];
					editorGUINode5.guiPosition = new Vector2(editorGUINode5.guiPosition.x, vector.y - _process.nodeToGUIData[nodes[num4]].fullArea.height - areaShift.y);
				}
				break;
			}
			case GroupAlignment.HCenter:
			{
				for (int i = 0; i < count; i++)
				{
					Rect fullArea = _process.nodeToGUIData[nodes[i]].fullArea;
					if (i == 0)
					{
						vector = new Vector2(0f, fullArea.center.y);
					}
					else if (fullArea.center.y > vector.y)
					{
						vector.y += (fullArea.center.y - vector.y) * 0.5f;
					}
					else if (fullArea.center.y < vector.y)
					{
						vector.y -= (vector.y - fullArea.center.y) * 0.5f;
					}
				}
				for (int j = 0; j < count; j++)
				{
					IEditorGUINode editorGUINode = nodes[j];
					editorGUINode.guiPosition = new Vector2(editorGUINode.guiPosition.x, vector.y - _process.nodeToGUIData[nodes[j]].fullArea.height * 0.5f - areaShift.y);
				}
				break;
			}
			}
			_process.MarkLayoutAsDirty();
			_process.DispatchOnGUIChange(NodeProcess.GUIChangeType.DragNodes);
		}

		private void ArrangeSelectedNodes(bool horizontally, List<IEditorGUINode> nodes)
		{
			nodes.Sort(delegate(IEditorGUINode a, IEditorGUINode b)
			{
				if (horizontally)
				{
					if (a.guiPosition.x < b.guiPosition.x)
					{
						return -1;
					}
					if (a.guiPosition.x > b.guiPosition.x)
					{
						return 1;
					}
					if (a.guiPosition.y > b.guiPosition.y)
					{
						return 1;
					}
					if (a.guiPosition.y < b.guiPosition.y)
					{
						return -1;
					}
				}
				else
				{
					if (a.guiPosition.y < b.guiPosition.y)
					{
						return -1;
					}
					if (a.guiPosition.y > b.guiPosition.y)
					{
						return 1;
					}
					if (a.guiPosition.x > b.guiPosition.x)
					{
						return 1;
					}
					if (a.guiPosition.x < b.guiPosition.x)
					{
						return -1;
					}
				}
				return 0;
			});
			for (int i = 1; i < nodes.Count; i++)
			{
				IEditorGUINode editorGUINode = nodes[i - 1];
				NodeGUIData nodeGUIData = _process.nodeToGUIData[editorGUINode];
				nodeGUIData.fullArea.x = editorGUINode.guiPosition.x;
				nodeGUIData.fullArea.y = editorGUINode.guiPosition.y;
				nodes[i].guiPosition = (horizontally ? new Vector2(nodeGUIData.fullArea.xMax + 12f, editorGUINode.guiPosition.y) : new Vector2(editorGUINode.guiPosition.x, nodeGUIData.fullArea.yMax + 12f));
			}
			_process.DispatchOnGUIChange(NodeProcess.GUIChangeType.DragNodes);
		}

		private Texture2D IndexToIcon(int index)
		{
			switch (index)
			{
			case 1:
				return DeStylePalette.ico_alignVC;
			case 2:
				return DeStylePalette.ico_alignR;
			case 3:
				return DeStylePalette.ico_alignT;
			case 4:
				return DeStylePalette.ico_alignHC;
			case 5:
				return DeStylePalette.ico_alignB;
			case 6:
				return DeStylePalette.ico_distributeVAlignL;
			case 7:
				return DeStylePalette.ico_distributeHAlignT;
			default:
				return DeStylePalette.ico_alignL;
			}
		}

		private GroupAlignment IndexToGroupAlignment(int index)
		{
			switch (index)
			{
			case 1:
				return GroupAlignment.VCenter;
			case 2:
				return GroupAlignment.Right;
			case 3:
				return GroupAlignment.Top;
			case 4:
				return GroupAlignment.HCenter;
			case 5:
				return GroupAlignment.Bottom;
			case 6:
				return GroupAlignment.Left;
			case 7:
				return GroupAlignment.Top;
			default:
				return GroupAlignment.Left;
			}
		}
	}
}
