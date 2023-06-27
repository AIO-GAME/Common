using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core
{
	/// <summary>
	/// Always connects a node from BottomOrRight side to TopOrLeft side
	/// </summary>
	internal static class Legacy_Connector
	{
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

		private struct AnchorsData
		{
			public Vector2 fromMarkP;

			public Vector2 fromLineP;

			public Vector2 toArrowP;

			public Vector2 toLineP;

			public Vector2 fromTangent;

			public Vector2 toTangent;

			public bool fromIsSide;

			public bool toIsSide;

			public bool isStraight;

			public bool arrowRequiresRotation;

			public float arrowRotationAngle;
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

		private const int _LineSize = 3;

		private const int _MaxDistanceForSmartStraight = 40;

		private const int _TangentDistance = 50;

		private const int _TangentDistanceIfInverse = 120;

		private const int FromSquareWidth = 6;

		private const int FromSquareHeight = 8;

		private static readonly Styles _Styles = new Styles();

		private static Color _lineShadowColor = new Color(0f, 0f, 0f, 0.4f);

		/// <summary>
		/// Always connects from BottomOrRight side to TopOrLeft side.
		/// If ALT is pressed shows the delete connection button.
		/// Called during Repaint or MouseDown/Up.
		/// Returns TRUE if the connection was deleted using the delete connection button.
		/// </summary>
		public static bool Connect(NodeProcess process, int connectionIndex, int fromTotConnections, NodeConnectionOptions fromOptions, IEditorGUINode fromNode, IEditorGUINode toNode)
		{
			_Styles.Init();
			NodeGUIData nodeGuiData = process.nodeToGUIData[fromNode];
			NodeGUIData nodeGUIData = process.nodeToGUIData[toNode];
			bool flag = fromOptions.connectionMode != ConnectionMode.Dual && nodeGuiData.connectorAreas != null;
			Rect fromArea = (flag ? nodeGuiData.connectorAreas[connectionIndex] : nodeGuiData.fullArea);
			AnchorsData anchors = GetAnchors(process, connectionIndex, fromNode, fromArea, toNode, nodeGUIData.fullArea, fromOptions, flag);
			Color connectionColor = GetConnectionColor(connectionIndex, fromTotConnections, nodeGuiData, fromOptions);
			Handles.DrawBezier(anchors.fromLineP, anchors.toLineP, anchors.fromTangent, anchors.toTangent, _lineShadowColor, null, 5f);
			Handles.DrawBezier(anchors.fromLineP, anchors.toLineP, anchors.fromTangent, anchors.toTangent, connectionColor, null, 3f);
			Rect position = (anchors.fromIsSide ? new Rect(anchors.fromMarkP.x, anchors.fromMarkP.y - 4f, 6f, 8f) : new Rect(anchors.fromMarkP.x - 4f, anchors.fromMarkP.y, 8f, 6f));
			using (new DeGUI.ColorScope(null, null, connectionColor))
			{
				GUI.DrawTexture(position, DeStylePalette.whiteSquare);
			}
			Rect position2 = new Rect(anchors.toArrowP.x - (float)DeStylePalette.ico_nodeArrow.width, anchors.toArrowP.y - (float)DeStylePalette.ico_nodeArrow.height * 0.5f, DeStylePalette.ico_nodeArrow.width, DeStylePalette.ico_nodeArrow.height);
			Matrix4x4 matrix = GUI.matrix;
			if (anchors.arrowRequiresRotation)
			{
				GUIUtility.RotateAroundPivot(anchors.arrowRotationAngle, anchors.toArrowP * process.guiScale + process.guiScalePositionDiff);
			}
			using (new DeGUI.ColorScope(null, null, connectionColor))
			{
				GUI.DrawTexture(position2, DeStylePalette.ico_nodeArrow);
			}
			GUI.matrix = matrix;
			if (DeGUIKey.Exclusive.alt)
			{
				Vector2 vector = anchors.fromTangent + (anchors.toTangent - anchors.fromTangent) * 0.5f;
				Vector2 vector2 = anchors.fromLineP + (anchors.toLineP - anchors.fromLineP) * 0.5f;
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
						return true;
					}
				}
				GUI.DrawTexture(rect.Contract(2f), DeStylePalette.ico_delete);
			}
			return false;
		}

		public static Color Drag(InteractionManager interaction, Vector2 mousePosition, NodeGUIData nodeGuiData, NodeConnectionOptions connectionOptions)
		{
			dragData.Set(interaction.targetNode);
			Color connectionColor = GetConnectionColor((connectionOptions.connectionMode != ConnectionMode.Dual) ? interaction.targetNodeConnectorAreaIndex : ((DeGUIKey.Exclusive.ctrl && DeGUIKey.Extra.space) ? 1 : 0), interaction.targetNode.connectedNodesIds.Count, nodeGuiData, connectionOptions);
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
			Handles.DrawBezier(center, mousePosition, center, mousePosition, Color.black, null, 5f);
			Handles.DrawBezier(center, mousePosition, center, mousePosition, connectionColor, null, 5f);
			return connectionColor;
		}

		private static AnchorsData GetAnchors(NodeProcess process, int connectionIndex, IEditorGUINode fromNode, Rect fromArea, IEditorGUINode toNode, Rect toArea, NodeConnectionOptions connectionOptions, bool sideOnly)
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
				result.fromLineP.y += 6f;
			}
			else
			{
				result.fromLineP.x += 6f;
			}
			bool flag2 = toArea.y > result.fromMarkP.y && (fromArea.xMax > toArea.x || toArea.y - result.fromMarkP.y > toArea.center.x - result.fromMarkP.x) && ToAnchorCanBeTop(process, fromNode, fromArea, toNode, toArea);
			result.toArrowP = (result.toLineP = (flag2 ? new Vector2(toArea.center.x, toArea.y) : new Vector2(toArea.x, toArea.center.y)));
			result.fromIsSide = !flag;
			result.toIsSide = !flag2;
			bool flag3 = result.toArrowP.x < result.fromMarkP.x && result.toArrowP.y < fromArea.yMax;
			float num3 = Vector2.Distance(result.toArrowP, result.fromLineP);
			result.isStraight = connectionOptions.connectorMode == ConnectorMode.Straight || (!flag3 && connectionOptions.connectorMode == ConnectorMode.Smart && num3 <= 40f);
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
				float num5 = (flag3 ? 120f : Mathf.Min(50f, num4 * 0.2f + num3 * 0.2f));
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
