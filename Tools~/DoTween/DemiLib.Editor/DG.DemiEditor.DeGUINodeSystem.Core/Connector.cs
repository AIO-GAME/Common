using System;
using System.Collections.Generic;
using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core
{
	/// <summary>
	/// Always connects a node from BottomOrRight side to TopOrLeft side
	/// </summary>
	internal class Connector
	{
		private enum ConnectionSide
		{
			Top,
			Bottom,
			Left,
			Right
		}

		internal class DragData
		{
			public IEditorGUINode node;

			public void Reset()
			{
				node = null;
			}

			public void Set(IEditorGUINode node)
			{
				this.node = node;
			}
		}

		internal struct ConnectResult
		{
			public bool changed;

			public bool aConnectionWasDeleted;

			public bool aConnectionWasAdded;

			public ConnectResult(bool changed = false, bool aConnectionWasDeleted = false, bool aConnectionWasAdded = false)
			{
				this.changed = changed;
				this.aConnectionWasDeleted = aConnectionWasDeleted;
				this.aConnectionWasAdded = aConnectionWasAdded;
			}
		}

		private struct AnchorsData
		{
			public bool isSet;

			public Vector2 fromMarkP;

			public Vector2 fromLineP;

			public Vector2 toArrowP;

			public Vector2 toLineP;

			public Vector2 fromTangent;

			public Vector2 toTangent;

			public ConnectionSide fromSide;

			public ConnectionSide toSide;

			public bool fromIsSide;

			public bool toIsSide;

			public bool isStraight;

			public bool arrowRequiresRotation;

			public float arrowRotationAngle;
		}

		private struct RectCache
		{
			public float x;

			public float y;

			public float xMax;

			public float yMax;

			public Vector2 center;

			public RectCache(Rect rect)
			{
				x = rect.x;
				y = rect.y;
				xMax = rect.xMax;
				yMax = rect.yMax;
				center = new Vector2(rect.center.x, rect.center.y);
			}
		}

		private struct ConnectionData
		{
			public int index;

			public int totConnections;

			public ConnectionData(int index, int totConnections)
			{
				this.index = index;
				this.totConnections = totConnections;
			}
		}

		private class Styles
		{
			public GUIStyle btDelete;

			private bool _initialized;

			public void Init()
			{
				if (!_initialized)
				{
					_initialized = true;
					btDelete = new GUIStyle().StretchWidth().StretchHeight().Background(DeStylePalette.circle);
				}
			}
		}

		public static readonly DragData dragData = new DragData();

		private const int _MaxDistanceForSmartStraight = 10;

		private const int _TangentDistance = 50;

		private const int _TangentDistanceIfInverse = 90;

		private const int _FromSquareWidth = 2;

		private const int _FromSquareHeight = 8;

		private static readonly Styles _Styles = new Styles();

		private static readonly Color _LineShadowColor = new Color(0f, 0f, 0f, 0.4f);

		private bool _anchorsDataRefreshRequired = true;

		private NodeProcess _process;

		private readonly Dictionary<IEditorGUINode, AnchorsData[]> _nodeToAnchorsData = new Dictionary<IEditorGUINode, AnchorsData[]>();

		public Connector(NodeProcess process)
		{
			_process = process;
			Undo.undoRedoPerformed = (Undo.UndoRedoCallback)Delegate.Combine(Undo.undoRedoPerformed, (Undo.UndoRedoCallback)delegate
			{
				_anchorsDataRefreshRequired = true;
			});
			process.OnGUIChange += delegate(NodeProcess.GUIChangeType x)
			{
				if (x != NodeProcess.GUIChangeType.SortedNodes)
				{
					_anchorsDataRefreshRequired = true;
				}
			};
		}

		public void Reset()
		{
			_nodeToAnchorsData.Clear();
			_anchorsDataRefreshRequired = true;
		}

		/// <summary>
		/// Always connects from BottomOrRight side to TopOrLeft side.
		/// If ALT is pressed shows the delete connection button.
		/// Called during Repaint or MouseDown/Up.
		/// Returns TRUE if the connection was deleted using the delete connection button.
		/// </summary>
		public ConnectResult Connect(int connectionIndex, int fromTotConnections, NodeConnectionOptions fromOptions, IEditorGUINode fromNode)
		{
			_Styles.Init();
			if (_anchorsDataRefreshRequired)
			{
				_anchorsDataRefreshRequired = false;
				RefreshAnchorsData();
			}
			NodeGUIData nodeGuiData = _process.nodeToGUIData[fromNode];
			if (!_nodeToAnchorsData.ContainsKey(fromNode))
			{
				return default(ConnectResult);
			}
			if (connectionIndex >= _nodeToAnchorsData[fromNode].Length)
			{
				return new ConnectResult(changed: true, aConnectionWasDeleted: false, aConnectionWasAdded: true);
			}
			AnchorsData anchorsData = _nodeToAnchorsData[fromNode][connectionIndex];
			if (!anchorsData.isSet)
			{
				return default(ConnectResult);
			}
			Color connectionColor = GetConnectionColor(connectionIndex, fromTotConnections, nodeGuiData, fromOptions);
			if (_process.options.connectorsShadow)
			{
				Handles.DrawBezier(anchorsData.fromLineP, anchorsData.toLineP, anchorsData.fromTangent, anchorsData.toTangent, _LineShadowColor, null, _process.options.connectorsThickness + 2f);
			}
			Handles.DrawBezier(anchorsData.fromLineP, anchorsData.toLineP, anchorsData.fromTangent, anchorsData.toTangent, connectionColor, null, _process.options.connectorsThickness);
			Rect position;
			switch (anchorsData.fromSide)
			{
			case ConnectionSide.Top:
				position = new Rect(anchorsData.fromMarkP.x - 4f, anchorsData.fromMarkP.y - 2f, 8f, 2f);
				break;
			case ConnectionSide.Bottom:
				position = new Rect(anchorsData.fromMarkP.x - 4f, anchorsData.fromMarkP.y, 8f, 2f);
				break;
			case ConnectionSide.Left:
				position = new Rect(anchorsData.fromMarkP.x - 2f, anchorsData.fromMarkP.y - 4f, 2f, 8f);
				break;
			default:
				position = new Rect(anchorsData.fromMarkP.x, anchorsData.fromMarkP.y - 4f, 2f, 8f);
				break;
			}
			using (new DeGUI.ColorScope(null, null, connectionColor))
			{
				GUI.DrawTexture(position, DeStylePalette.whiteSquare);
			}
			Rect position2 = new Rect(anchorsData.toArrowP.x - (float)DeStylePalette.ico_nodeArrow.width, anchorsData.toArrowP.y - (float)DeStylePalette.ico_nodeArrow.height * 0.5f, DeStylePalette.ico_nodeArrow.width, DeStylePalette.ico_nodeArrow.height);
			Matrix4x4 matrix = GUI.matrix;
			if (anchorsData.arrowRequiresRotation)
			{
				GUIUtility.RotateAroundPivot(anchorsData.arrowRotationAngle, anchorsData.toArrowP * _process.guiScale + _process.guiScalePositionDiff);
			}
			using (new DeGUI.ColorScope(null, null, connectionColor))
			{
				GUI.DrawTexture(position2, DeStylePalette.ico_nodeArrow);
			}
			GUI.matrix = matrix;
			if (DeGUIKey.Exclusive.alt)
			{
				Vector2 vector = anchorsData.fromTangent + (anchorsData.toTangent - anchorsData.fromTangent) * 0.5f;
				Vector2 vector2 = anchorsData.fromLineP + (anchorsData.toLineP - anchorsData.fromLineP) * 0.5f;
				vector += (vector2 - vector) * 0.25f;
				Rect rect = new Rect(vector.x - 5f, vector.y - 5f, 10f, 10f);
				using (new DeGUI.ColorScope(null, null, connectionColor))
				{
					GUI.DrawTexture(rect.Expand(2f), DeStylePalette.circle);
				}
				using (new DeGUI.ColorScope(null, null, DeGUI.colors.global.red))
				{
					if (GUI.Button(rect, "", _Styles.btDelete))
					{
						return new ConnectResult(changed: true, aConnectionWasDeleted: true);
					}
				}
				GUI.DrawTexture(rect.Contract(2f), DeStylePalette.ico_delete);
			}
			return default(ConnectResult);
		}

		public Color Drag(InteractionManager interaction, Vector2 mousePosition, NodeGUIData nodeGuiData, NodeConnectionOptions connectionOptions, float lineThickness)
		{
			dragData.Set(interaction.targetNode);
			int connectionIndex;
			switch (connectionOptions.connectionMode)
			{
			case ConnectionMode.Dual:
				connectionIndex = ((DeGUIKey.Exclusive.ctrl && DeGUIKey.Extra.space) ? 1 : 0);
				break;
			case ConnectionMode.NormalPlus:
				connectionIndex = ((DeGUIKey.Exclusive.ctrl && DeGUIKey.Extra.space) ? (interaction.targetNode.connectedNodesIds.Count - 1) : interaction.targetNodeConnectorAreaIndex);
				break;
			default:
				connectionIndex = interaction.targetNodeConnectorAreaIndex;
				break;
			}
			Color connectionColor = GetConnectionColor(connectionIndex, interaction.targetNode.connectedNodesIds.Count, nodeGuiData, connectionOptions);
			Vector2 center = interaction.targetNodeConnectorArea.center;
			Rect rect = new Rect(center.x - 4f, center.y - 4f, 8f, 8f);
			Rect rect2 = new Rect(mousePosition.x - 4f, mousePosition.y - 4f, 8f, 8f);
			using (new DeGUI.ColorScope(null, null, Color.black))
			{
				GUI.DrawTexture(rect.Expand(4f), DeStylePalette.circle);
				GUI.DrawTexture(rect2.Expand(4f), DeStylePalette.circle);
			}
			using (new DeGUI.ColorScope(null, null, connectionColor))
			{
				GUI.DrawTexture(rect, DeStylePalette.circle);
				GUI.DrawTexture(rect2, DeStylePalette.circle);
			}
			Handles.DrawBezier(center, mousePosition, center, mousePosition, Color.black, null, lineThickness + 2f);
			Handles.DrawBezier(center, mousePosition, center, mousePosition, connectionColor, null, lineThickness);
			return connectionColor;
		}

		private void RefreshAnchorsData()
		{
			_nodeToAnchorsData.Clear();
			for (int i = 0; i < _process.nodes.Count; i++)
			{
				IEditorGUINode editorGUINode = _process.nodes[i];
				NodeGUIData nodeGUIData = _process.nodeToGUIData[editorGUINode];
				NodeConnectionOptions connectionOptions = _process.nodeToConnectionOptions[editorGUINode];
				List<string> connectedNodesIds = editorGUINode.connectedNodesIds;
				int count = editorGUINode.connectedNodesIds.Count;
				AnchorsData[] array = null;
				for (int num = count - 1; num > -1; num--)
				{
					string text = connectedNodesIds[num];
					if (string.IsNullOrEmpty(text))
					{
						continue;
					}
					if (array == null)
					{
						array = new AnchorsData[count];
					}
					IEditorGUINode editorGUINode2 = _process.idToNode[text];
					NodeGUIData nodeGUIData2 = _process.nodeToGUIData[editorGUINode2];
					if (!nodeGUIData.isVisible && !nodeGUIData2.isVisible)
					{
						Rect area = nodeGUIData.fullArea.Add(nodeGUIData2.fullArea);
						if (!_process.AreaIsVisible(area))
						{
							continue;
						}
					}
					bool flag = connectionOptions.connectionMode != ConnectionMode.Dual && nodeGUIData.connectorAreas != null && (connectionOptions.connectionMode != ConnectionMode.NormalPlus || num < count - 1);
					Rect rect = (flag ? nodeGUIData.connectorAreas[num] : nodeGUIData.fullArea);
					AnchorsData anchorsAllSides = GetAnchorsAllSides(_process, num, editorGUINode, new RectCache(rect), editorGUINode2, new RectCache(nodeGUIData2.fullArea), connectionOptions, flag);
					anchorsAllSides.isSet = true;
					array[num] = anchorsAllSides;
				}
				if (array != null)
				{
					_nodeToAnchorsData.Add(editorGUINode, array);
				}
			}
		}

		private static AnchorsData GetAnchorsAllSides(NodeProcess process, int connectionIndex, IEditorGUINode fromNode, RectCache fromArea, IEditorGUINode toNode, RectCache toArea, NodeConnectionOptions connectionOptions, bool sideOnly)
		{
			AnchorsData result = default(AnchorsData);
			bool flag = false;
			int num;
			int num2;
			if (sideOnly)
			{
				result.fromSide = ConnectionSide.Right;
			}
			else if (toArea.x >= fromArea.x)
			{
				if (toArea.x < fromArea.xMax)
				{
					if (!FromAnchorCanBeVertical(process, fromNode, fromArea, toNode, toArea))
					{
						if (toArea.y > fromArea.yMax && fromArea.yMax - toArea.y <= 12f)
						{
							num = ((fromArea.xMax < toArea.center.x) ? 1 : 0);
							if (num != 0)
							{
								num2 = 0;
								goto IL_00d3;
							}
						}
						else
						{
							num = 0;
						}
						num2 = ((fromArea.x > toArea.center.x) ? 1 : 0);
						goto IL_00d3;
					}
					result.fromSide = ((!(toArea.center.y < fromArea.center.y)) ? ConnectionSide.Bottom : ConnectionSide.Top);
				}
				else if (toArea.yMax > fromArea.y && toArea.y < fromArea.yMax)
				{
					result.fromSide = ConnectionSide.Right;
				}
				else
				{
					float num3 = toArea.x - fromArea.xMax;
					float num4 = ((toArea.center.y < fromArea.y) ? (toArea.center.y - fromArea.y) : (toArea.center.y - fromArea.yMax));
					if (num3 > Mathf.Abs(num4) || !FromAnchorCanBeVertical(process, fromNode, fromArea, toNode, toArea))
					{
						result.fromSide = ConnectionSide.Right;
					}
					else
					{
						result.fromSide = ((!(num4 < 0f)) ? ConnectionSide.Bottom : ConnectionSide.Top);
					}
				}
			}
			else if (toArea.xMax > fromArea.x)
			{
				if (FromAnchorCanBeVertical(process, fromNode, fromArea, toNode, toArea))
				{
					result.fromSide = ((!(toArea.center.y < fromArea.center.y)) ? ConnectionSide.Bottom : ConnectionSide.Top);
				}
				else
				{
					result.fromSide = ConnectionSide.Right;
					result.toSide = ConnectionSide.Right;
					flag = true;
				}
			}
			else if (toArea.yMax > fromArea.y && toArea.y < fromArea.yMax)
			{
				result.fromSide = ConnectionSide.Left;
			}
			else
			{
				float num5 = fromArea.x - toArea.xMax;
				float num6 = ((toArea.yMax < fromArea.y) ? (toArea.yMax - fromArea.y) : (toArea.y - fromArea.yMax));
				if (num5 > Mathf.Abs(num6) || !FromAnchorCanBeVertical(process, fromNode, fromArea, toNode, toArea))
				{
					result.fromSide = ConnectionSide.Left;
				}
				else
				{
					result.fromSide = ((!(num6 < 0f)) ? ConnectionSide.Bottom : ConnectionSide.Top);
				}
			}
			goto IL_02c1;
			IL_02c1:
			if (!flag)
			{
				result.toSide = ConnectionSide.Left;
				switch (result.fromSide)
				{
				case ConnectionSide.Top:
					result.toSide = (ToAnchorCanBeVertical(process, fromNode, fromArea, result.fromSide, toNode, toArea) ? ConnectionSide.Bottom : ((toArea.center.x < fromArea.center.x) ? ConnectionSide.Right : ConnectionSide.Left));
					break;
				case ConnectionSide.Bottom:
					result.toSide = ((!ToAnchorCanBeVertical(process, fromNode, fromArea, result.fromSide, toNode, toArea)) ? ((toArea.center.x < fromArea.center.x) ? ConnectionSide.Right : ConnectionSide.Left) : ConnectionSide.Top);
					break;
				case ConnectionSide.Left:
					result.toSide = ConnectionSide.Right;
					break;
				default:
					if (sideOnly && toArea.x < fromArea.xMax)
					{
						result.toSide = ((!(toArea.center.x >= fromArea.xMax)) ? ConnectionSide.Right : ((toArea.yMax < fromArea.y) ? ConnectionSide.Bottom : ConnectionSide.Top));
					}
					else
					{
						result.toSide = ConnectionSide.Left;
					}
					break;
				}
			}
			result.fromIsSide = result.fromSide == ConnectionSide.Left || result.fromSide == ConnectionSide.Right;
			result.toIsSide = result.toSide == ConnectionSide.Left || result.toSide == ConnectionSide.Right;
			int num7 = ((!sideOnly) ? 8 : 0);
			switch (result.fromSide)
			{
			case ConnectionSide.Top:
				result.fromLineP = (result.fromMarkP = new Vector2(fromArea.center.x + (float)num7, fromArea.y));
				result.fromLineP.y -= 2f;
				if (connectionOptions.connectionMode == ConnectionMode.Dual)
				{
					result.fromLineP.x = (result.fromMarkP.x += ((connectionIndex == 1) ? 4 : (-4)));
				}
				break;
			case ConnectionSide.Bottom:
				result.fromLineP = (result.fromMarkP = new Vector2(fromArea.center.x + (float)num7, fromArea.yMax));
				result.fromLineP.y += 2f;
				if (connectionOptions.connectionMode == ConnectionMode.Dual)
				{
					result.fromLineP.x = (result.fromMarkP.x += ((connectionIndex == 1) ? 4 : (-4)));
				}
				break;
			case ConnectionSide.Left:
				result.fromLineP = (result.fromMarkP = new Vector2(fromArea.x, fromArea.center.y + (float)num7));
				result.fromLineP.x -= 2f;
				if (connectionOptions.connectionMode == ConnectionMode.Dual)
				{
					result.fromLineP.y = (result.fromMarkP.y += ((connectionIndex == 1) ? 4 : (-4)));
				}
				break;
			case ConnectionSide.Right:
				result.fromLineP = (result.fromMarkP = new Vector2(fromArea.xMax, fromArea.center.y + (float)num7));
				result.fromLineP.x += 2f;
				if (connectionOptions.connectionMode == ConnectionMode.Dual)
				{
					result.fromLineP.y = (result.fromMarkP.y += ((connectionIndex == 1) ? 4 : (-4)));
				}
				break;
			}
			switch (result.toSide)
			{
			case ConnectionSide.Top:
				result.toArrowP = (result.toLineP = new Vector2(toArea.center.x, toArea.y));
				if (Vector2.Distance(result.fromLineP, result.toLineP) < 20f)
				{
					result.toArrowP.x = (result.toLineP.x += num7);
				}
				break;
			case ConnectionSide.Bottom:
				result.toArrowP = (result.toLineP = new Vector2(toArea.center.x, toArea.yMax));
				if (Vector2.Distance(result.fromLineP, result.toLineP) < 20f)
				{
					result.toArrowP.x = (result.toLineP.x += num7);
				}
				break;
			case ConnectionSide.Left:
				result.toArrowP = (result.toLineP = new Vector2(toArea.x, toArea.center.y));
				if (Vector2.Distance(result.fromLineP, result.toLineP) < 20f)
				{
					result.toArrowP.y = (result.toLineP.y += num7);
				}
				break;
			case ConnectionSide.Right:
				result.toArrowP = (result.toLineP = new Vector2(toArea.xMax, toArea.center.y));
				if (Vector2.Distance(result.fromLineP, result.toLineP) < 20f)
				{
					result.toArrowP.y = (result.toLineP.y += num7);
				}
				break;
			}
			bool flag2 = result.fromSide == ConnectionSide.Right && result.toArrowP.x < fromArea.center.x;
			float num8 = Vector2.Distance(result.toArrowP, result.fromLineP);
			result.isStraight = connectionOptions.connectorMode == ConnectorMode.Straight || (!flag2 && connectionOptions.connectorMode == ConnectorMode.Smart && num8 <= 10f) || Mathf.Approximately(result.toLineP.x, result.fromLineP.x) || Mathf.Approximately(result.toLineP.y, result.fromLineP.y);
			if (result.isStraight)
			{
				result.fromTangent = result.fromLineP;
				result.toTangent = result.toArrowP;
				result.arrowRequiresRotation = true;
				result.arrowRotationAngle = 0f - AngleBetween(Vector2.right, result.toArrowP - result.fromLineP);
			}
			else
			{
				float num9 = (result.fromIsSide ? Mathf.Abs(result.toArrowP.x - result.fromLineP.x) : Mathf.Abs(result.toArrowP.y - result.fromLineP.y));
				float num10 = (flag2 ? 90f : Mathf.Min(50f, num9 * 0.2f + num8 * 0.2f));
				Vector2 vector;
				switch (result.fromSide)
				{
				case ConnectionSide.Top:
					vector = Vector2.up * (0f - num10);
					break;
				case ConnectionSide.Bottom:
					vector = Vector2.up * num10;
					break;
				case ConnectionSide.Left:
					vector = Vector2.right * (0f - num10);
					break;
				default:
					vector = Vector2.right * num10;
					break;
				}
				Vector2 vector2;
				switch (result.toSide)
				{
				case ConnectionSide.Top:
					result.toLineP.y -= DeStylePalette.ico_nodeArrow.width;
					vector2 = Vector2.up * (0f - num10);
					result.arrowRequiresRotation = true;
					result.arrowRotationAngle = 90f;
					break;
				case ConnectionSide.Bottom:
					result.toLineP.y += DeStylePalette.ico_nodeArrow.width;
					vector2 = Vector2.up * num10;
					result.arrowRequiresRotation = true;
					result.arrowRotationAngle = -90f;
					break;
				case ConnectionSide.Left:
					result.toLineP.x -= DeStylePalette.ico_nodeArrow.width;
					vector2 = Vector2.right * (0f - num10);
					break;
				default:
					result.toLineP.x += DeStylePalette.ico_nodeArrow.width;
					vector2 = Vector2.right * num10;
					result.arrowRequiresRotation = true;
					result.arrowRotationAngle = 180f;
					break;
				}
				result.fromTangent = result.fromLineP + vector;
				result.toTangent = result.toLineP + vector2;
			}
			return result;
			IL_00d3:
			bool flag3 = (byte)num2 != 0;
			if (num != 0)
			{
				result.fromSide = ConnectionSide.Right;
				result.toSide = ConnectionSide.Top;
			}
			else if (flag3)
			{
				result.fromSide = ConnectionSide.Left;
				result.toSide = ConnectionSide.Top;
			}
			else
			{
				result.fromSide = ConnectionSide.Left;
				result.toSide = ConnectionSide.Left;
			}
			flag = true;
			goto IL_02c1;
		}

		private static AnchorsData GetAnchors_2Sides(NodeProcess process, int connectionIndex, IEditorGUINode fromNode, Rect fromArea, IEditorGUINode toNode, Rect toArea, NodeConnectionOptions connectionOptions, bool sideOnly)
		{
			AnchorsData result = default(AnchorsData);
			float num = toArea.x - fromArea.xMax;
			float num2 = toArea.y - fromArea.yMax;
			bool flag = !sideOnly && fromArea.yMax < toArea.y && num2 >= num && FromAnchorCanBeBottom(process, fromNode, fromArea, toNode, toArea);
			result.fromMarkP = (flag ? new Vector2(fromArea.center.x, fromArea.yMax) : new Vector2(fromArea.xMax, fromArea.center.y));
			if (connectionOptions.connectionMode == ConnectionMode.Dual)
			{
				if (flag)
				{
					result.fromMarkP.x += ((connectionIndex == 1) ? 4 : (-4));
				}
				else
				{
					result.fromMarkP.y += ((connectionIndex == 1) ? 4 : (-4));
				}
			}
			result.fromLineP = result.fromMarkP;
			if (flag)
			{
				result.fromLineP.y += 2f;
			}
			else
			{
				result.fromLineP.x += 2f;
			}
			bool flag2 = toArea.y > result.fromMarkP.y && (fromArea.xMax > toArea.x || toArea.y - result.fromMarkP.y > toArea.center.x - result.fromMarkP.x) && ToAnchorCanBeTop(process, fromNode, fromArea, toNode, toArea);
			result.toArrowP = (result.toLineP = (flag2 ? new Vector2(toArea.center.x, toArea.y) : new Vector2(toArea.x, toArea.center.y)));
			result.fromIsSide = !flag;
			result.toIsSide = !flag2;
			bool flag3 = result.toArrowP.x < result.fromMarkP.x && result.toArrowP.y < fromArea.yMax;
			float num3 = Vector2.Distance(result.toArrowP, result.fromLineP);
			result.isStraight = connectionOptions.connectorMode == ConnectorMode.Straight || (!flag3 && connectionOptions.connectorMode == ConnectorMode.Smart && num3 <= 10f);
			if (result.isStraight)
			{
				result.fromTangent = result.fromLineP;
				result.toTangent = result.toArrowP;
			}
			else
			{
				if (flag2)
				{
					result.toLineP.y -= DeStylePalette.ico_nodeArrow.width;
				}
				else
				{
					result.toLineP.x -= DeStylePalette.ico_nodeArrow.width;
				}
				float num4 = (result.fromIsSide ? Mathf.Abs(result.toArrowP.x - result.fromLineP.x) : Mathf.Abs(result.toArrowP.y - result.fromLineP.y));
				float num5 = (flag3 ? 90f : Mathf.Min(50f, num4 * 0.2f + num3 * 0.2f));
				result.fromTangent = result.fromLineP + (flag ? (Vector2.up * num5) : (Vector2.right * num5));
				result.toTangent = result.toLineP + (flag2 ? (Vector2.up * (0f - num5)) : (Vector2.right * (0f - num5)));
			}
			if (result.isStraight)
			{
				result.arrowRequiresRotation = true;
				result.arrowRotationAngle = 0f - AngleBetween(Vector2.right, result.toArrowP - result.fromLineP);
			}
			else if (flag2)
			{
				result.arrowRequiresRotation = true;
				result.arrowRotationAngle = 90f;
			}
			return result;
		}

		private static bool FromAnchorCanBeVertical(NodeProcess process, IEditorGUINode fromNode, RectCache fromArea, IEditorGUINode toNode, RectCache toArea)
		{
			bool flag = fromArea.center.y <= toArea.y;
			if (flag && toArea.y - fromArea.yMax <= 12f && toArea.center.x > fromArea.xMax + 8f && toArea.center.x - fromArea.center.x > 20f)
			{
				return false;
			}
			int count = process.nodes.Count;
			for (int i = 0; i < count; i++)
			{
				IEditorGUINode editorGUINode = process.nodes[i];
				if (editorGUINode == fromNode || editorGUINode == toNode)
				{
					continue;
				}
				Rect fullArea = process.nodeToGUIData[editorGUINode].fullArea;
				if (flag)
				{
					if (fullArea.y > toArea.center.y || fullArea.yMax < fromArea.yMax || fullArea.x > fromArea.center.x || fullArea.xMax < fromArea.center.x)
					{
						continue;
					}
				}
				else if (fullArea.yMax < toArea.center.y || fullArea.y > fromArea.y || fullArea.xMax < fromArea.center.x || fullArea.x > fromArea.center.x)
				{
					continue;
				}
				return false;
			}
			return true;
		}

		private static bool ToAnchorCanBeVertical(NodeProcess process, IEditorGUINode fromNode, RectCache fromArea, ConnectionSide fromSide, IEditorGUINode toNode, RectCache toArea)
		{
			bool flag = fromArea.center.y <= toArea.y;
			if (flag && fromSide == ConnectionSide.Bottom && toArea.y - fromArea.yMax <= 12f && (toArea.xMax < fromArea.center.x - 8f || toArea.x > fromArea.center.x + 8f) && Mathf.Abs(fromArea.center.x - toArea.center.x) > 20f)
			{
				return false;
			}
			int count = process.nodes.Count;
			for (int i = 0; i < count; i++)
			{
				IEditorGUINode editorGUINode = process.nodes[i];
				if (editorGUINode == fromNode || editorGUINode == toNode)
				{
					continue;
				}
				Rect fullArea = process.nodeToGUIData[editorGUINode].fullArea;
				if (flag)
				{
					if (fullArea.y > toArea.y || fullArea.yMax < fromArea.center.y || fullArea.x > toArea.center.x || fullArea.xMax < toArea.x)
					{
						continue;
					}
				}
				else if (fullArea.yMax < toArea.y || fullArea.y > fromArea.center.y || fullArea.xMax < toArea.center.x || fullArea.x > toArea.x)
				{
					continue;
				}
				return false;
			}
			return true;
		}

		private static bool FromAnchorCanBeBottom(NodeProcess process, IEditorGUINode fromNode, Rect fromArea, IEditorGUINode toNode, Rect toArea)
		{
			if (toArea.xMax <= fromArea.center.x)
			{
				return true;
			}
			foreach (IEditorGUINode node in process.nodes)
			{
				if (node != fromNode && node != toNode)
				{
					Rect fullArea = process.nodeToGUIData[node].fullArea;
					if (!(fullArea.y > toArea.center.y) && !(fullArea.yMax < fromArea.yMax) && !(fullArea.x > fromArea.center.x) && !(fullArea.xMax < fromArea.center.x))
					{
						return false;
					}
				}
			}
			return true;
		}

		private static bool ToAnchorCanBeTop(NodeProcess process, IEditorGUINode fromNode, Rect fromArea, IEditorGUINode toNode, Rect toArea)
		{
			if (toArea.x < fromArea.x)
			{
				return true;
			}
			foreach (IEditorGUINode node in process.nodes)
			{
				if (node != fromNode && node != toNode)
				{
					Rect fullArea = process.nodeToGUIData[node].fullArea;
					if (!(fullArea.y > toArea.y) && !(fullArea.yMax < fromArea.center.y) && !(fullArea.x > toArea.center.x) && !(fullArea.xMax < toArea.x))
					{
						return false;
					}
				}
			}
			return true;
		}

		private static float AngleBetween(Vector2 from, Vector2 to)
		{
			from.Normalize();
			to.Normalize();
			float num = Mathf.Acos(Mathf.Clamp(Vector2.Dot(from, to), -1f, 1f)) * 57.29578f;
			if (to.x * from.y - to.y * from.x < 0f)
			{
				num = 0f - num;
			}
			return num;
		}

		private static Color GetConnectionColor(int connectionIndex, int totConnections, NodeGUIData nodeGuiData, NodeConnectionOptions connectionOptions)
		{
			if (connectionOptions.connectionMode == ConnectionMode.NormalPlus)
			{
				if (connectionIndex == totConnections - 1 || connectionOptions.gradientColor == null)
				{
					if (!(connectionOptions.startColor == Color.clear))
					{
						return connectionOptions.startColor;
					}
					return nodeGuiData.mainColor;
				}
				float time = ((totConnections <= 2) ? 0f : ((float)connectionIndex / (float)(totConnections - 2)));
				return connectionOptions.gradientColor.Evaluate(time);
			}
			if (totConnections >= 2 && connectionOptions.gradientColor != null)
			{
				return connectionOptions.gradientColor.Evaluate((float)connectionIndex / (float)(totConnections - 1));
			}
			if (!(connectionOptions.startColor == Color.clear))
			{
				return connectionOptions.startColor;
			}
			return nodeGuiData.mainColor;
		}
	}
}
