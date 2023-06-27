using System;
using System.Collections.Generic;
using DG.DemiEditor.DeGUINodeSystem.Core;
using DG.DemiEditor.DeGUINodeSystem.Core.DebugSystem;
using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
	/// <summary>
	/// Main class for DeGUI Node system.
	/// Create it, then enclose your GUI node calls inside a <see cref="!:NodeProcessScope" />.<para />
	/// CODING ORDER:<para />
	/// - Create a <see cref="T:DG.DemiEditor.DeGUINodeSystem.NodeProcess" /> to use for your node system (create it once, obviously)<para />
	/// - Inside OnGUI, write all your nodes GUI code inside a <see cref="!:NodeProcessScope" /><para />
	/// - To draw the nodes, loop through the <see cref="F:DG.DemiEditor.DeGUINodeSystem.NodeProcess.orderedNodes" /> list and call <see cref="M:DG.DemiEditor.DeGUINodeSystem.NodeProcess.Draw``1(DG.DemiLib.IEditorGUINode,System.Nullable{DG.DemiEditor.DeGUINodeSystem.NodeConnectionOptions})" /> for each node
	/// </summary>
	public class NodeProcess
	{
		public enum GUIChangeType
		{
			None,
			Pan,
			DragNodes,
			SortedNodes,
			DeletedNodes,
			AddedNodes,
			NodeConnection,
			GUIScale
		}

		public enum ScreenshotMode
		{
			VisibleArea,
			AllNodes
		}

		private class Styles
		{
			public GUIStyle selectionRect;

			public GUIStyle nodeSelectionOutline;

			public GUIStyle nodeSelectionOutlineThick;

			public GUIStyle endNodeOutline;

			public GUIStyle draggingTooltip;

			private bool _initialized;

			public void Init()
			{
				if (!_initialized)
				{
					_initialized = true;
					selectionRect = DeGUI.styles.box.flat.Clone().Background(DeStylePalette.squareBorderAlpha15);
					nodeSelectionOutline = DeGUI.styles.box.outline01.Clone().Border(new RectOffset(5, 5, 5, 5)).Background(DeStylePalette.squareBorderCurvedEmpty);
					nodeSelectionOutlineThick = nodeSelectionOutline.Clone().Background(DeStylePalette.squareBorderCurvedEmptyThick);
					endNodeOutline = nodeSelectionOutlineThick.Clone().Background(DeStylePalette.squareCornersEmpty02).Border(new RectOffset(7, 7, 7, 7));
					draggingTooltip = new GUIStyle(GUI.skin.label).Add(new DeSkinColor(0.85f), TextAnchor.MiddleLeft, Format.RichText).Padding(5, 0, 0, 0).Background(DeStylePalette.blackSquare);
				}
			}
		}

		public const string Version = "1.0.060";

		/// <summary>Distance at which nodes will be placed when snapping next to each other</summary>
		public const int SnapOffset = 12;

		public EditorWindow editor;

		public readonly ProcessOptions options = new ProcessOptions();

		public readonly InteractionManager interaction;

		public readonly SelectionManager selection;

		public readonly HelpPanel helpPanel;

		public readonly Dictionary<IEditorGUINode, NodeGUIData> nodeToGUIData = new Dictionary<IEditorGUINode, NodeGUIData>();

		/// <summary>Contains the nodes passed to NodeProcessScope ordered by depth.
		/// You should loop through this list when drawing nodes</summary>
		public readonly List<IEditorGUINode> orderedNodes = new List<IEditorGUINode>();

		private readonly NodeProcessDebug _debug = new NodeProcessDebug();

		internal readonly List<IEditorGUINode> nodes = new List<IEditorGUINode>();

		internal readonly Dictionary<string, IEditorGUINode> idToNode = new Dictionary<string, IEditorGUINode>();

		internal readonly Dictionary<IEditorGUINode, NodeConnectionOptions> nodeToConnectionOptions = new Dictionary<IEditorGUINode, NodeConnectionOptions>();

		private readonly Dictionary<Type, ABSDeGUINode> _typeToGUINode = new Dictionary<Type, ABSDeGUINode>();

		private readonly List<IEditorGUINode> _endGUINodes = new List<IEditorGUINode>();

		private readonly Connector _connector;

		private readonly NodeDragManager _nodeDragManager;

		private readonly NodesClipboard _clipboard;

		private readonly ContextPanel _contextPanel;

		private Minimap _minimap;

		private readonly List<IEditorGUINode> _tmp_nodes = new List<IEditorGUINode>();

		private readonly List<string> _tmp_string = new List<string>();

		private static readonly Styles _Styles = new Styles();

		private readonly Func<List<IEditorGUINode>, bool> _onDeleteNodesCallback;

		private readonly Func<IEditorGUINode, IEditorGUINode, bool> _onCloneNodeCallback;

		private bool _guiInitialized;

		private bool _isDockableEditor;

		private bool _repaintOnEnd;

		private bool _resetInteractionOnEnd;

		private Vector2 _forceApplyAreaShiftBy;

		private Vector2 _forceApplyAreaShift;

		private bool _doForceApplyAreaShift;

		public GUIChangeType guiChangeType { get; private set; }

		/// <summary>Full area without zeroed coordinates</summary>
		public Rect position { get; private set; }

		/// <summary>Position with zeroed coordinates (used by all node GUI since it's inside a GUILayout(area))</summary>
		public Rect relativeArea { get; private set; }

		public Vector2 areaShift { get; private set; }

		public float guiScale { get; internal set; }

		internal Vector2 guiScalePositionDiff { get; private set; }

		public event Action<GUIChangeType> OnGUIChange;

		internal void DispatchOnGUIChange(GUIChangeType type)
		{
			if (this.OnGUIChange != null)
			{
				this.OnGUIChange(type);
			}
		}

		/// <summary>
		/// Creates a new NodeProcess.
		/// </summary>
		/// <param name="editor">EditorWindow for this process</param>
		/// <param name="onDeleteNodesCallback">Callback called when one or more nodes are going to be deleted.
		/// Return FALSE if you want the deletion to be canceled.
		/// Can be NULL, in which case it will be ignored</param>
		/// <param name="onCloneNodeCallback">Callback called when a node is cloned.
		/// Return FALSE if you want the cloning to be canceled.
		/// Can be NULL, in which case it will be ignored</param>
		public NodeProcess(EditorWindow editor, Func<List<IEditorGUINode>, bool> onDeleteNodesCallback = null, Func<IEditorGUINode, IEditorGUINode, bool> onCloneNodeCallback = null)
		{
			this.editor = editor;
			_onDeleteNodesCallback = onDeleteNodesCallback;
			_onCloneNodeCallback = onCloneNodeCallback;
			interaction = new InteractionManager(this);
			selection = new SelectionManager();
			guiScale = 1f;
			_connector = new Connector(this);
			_nodeDragManager = new NodeDragManager(this);
			_clipboard = new NodesClipboard(this);
			_contextPanel = new ContextPanel(this);
			helpPanel = new HelpPanel(this);
			Undo.undoRedoPerformed = (Undo.UndoRedoCallback)Delegate.Remove(Undo.undoRedoPerformed, new Undo.UndoRedoCallback(OnUndoRedoCallback));
			Undo.undoRedoPerformed = (Undo.UndoRedoCallback)Delegate.Combine(Undo.undoRedoPerformed, new Undo.UndoRedoCallback(OnUndoRedoCallback));
		}

		/// <summary>
		/// Needs to be called when loading a complete new series of nodes
		/// </summary>
		public void Reset()
		{
			interaction.Reset();
			selection.Reset();
			_connector.Reset();
			orderedNodes.Clear();
			if (_minimap != null)
			{
				_minimap.RefreshMapTextureOnNextPass();
			}
		}

		/// <summary>
		/// Call this when the layout/size of one or more nodes changed because of external intervention
		/// (if a whole new range of nodes has been loaded, just call <see cref="M:DG.DemiEditor.DeGUINodeSystem.NodeProcess.Reset" /> instead)
		/// </summary>
		public void MarkLayoutAsDirty()
		{
			_connector.Reset();
			if (_minimap != null)
			{
				_minimap.RefreshMapTextureOnNextPass();
			}
		}

		/// <summary>
		/// Forces the refresh of the area calculations. Useful if you need them before the first GUI call has run
		/// </summary>
		public void ForceRefreshAreas(Rect nodeArea)
		{
			position = nodeArea;
			relativeArea = new Rect(0f, 0f, nodeArea.width / guiScale, nodeArea.height / guiScale);
		}

		/// <summary>
		/// Shifts the visible are to the given coordinates and repaints on end
		/// </summary>
		public void ShiftAreaBy(Vector2 shift)
		{
			_forceApplyAreaShiftBy = shift;
			DispatchOnGUIChange(GUIChangeType.Pan);
			RepaintOnEnd();
		}

		/// <summary>
		/// Shifts the visible are to the given coordinates and repaints on end
		/// </summary>
		public void SetAreaShift(Vector2 shift)
		{
			_doForceApplyAreaShift = true;
			_forceApplyAreaShift = shift;
			DispatchOnGUIChange(GUIChangeType.Pan);
			RepaintOnEnd();
		}

		/// <summary>
		/// Tells the process to repaint once the process has ended.
		/// Calling this
		/// </summary>
		public void RepaintOnEnd()
		{
			_repaintOnEnd = true;
		}

		/// <summary>
		/// Draws the given node using the given T editor GUINode type.
		/// Returns the full area of the node
		/// </summary>
		public Rect Draw<T>(IEditorGUINode node, NodeConnectionOptions? connectionOptions = null) where T : ABSDeGUINode, new()
		{
			Type typeFromHandle = typeof(T);
			ABSDeGUINode aBSDeGUINode;
			if (!_typeToGUINode.ContainsKey(typeFromHandle))
			{
				aBSDeGUINode = new T
				{
					process = this
				};
				_typeToGUINode.Add(typeFromHandle, aBSDeGUINode);
			}
			else
			{
				aBSDeGUINode = _typeToGUINode[typeFromHandle];
			}
			Vector2 vector = new Vector2((int)(node.guiPosition.x + relativeArea.x + areaShift.x), (int)(node.guiPosition.y + relativeArea.y + areaShift.y));
			NodeGUIData areas = aBSDeGUINode.GetAreas(vector, node);
			areas.isVisible = AreaIsVisible(areas.fullArea);
			aBSDeGUINode.OnGUI(areas, node);
			if (areas.isVisible && selection.selectedNodes.Count > 1 && !selection.IsSelected(node))
			{
				using (new DeGUI.ColorScope(null, null, new Color(1f, 1f, 1f, 0.4f)))
				{
					GUI.DrawTexture(areas.fullArea, DeStylePalette.blackSquare);
				}
			}
			bool flag = options.evidenceEndNodes != ProcessOptions.EvidenceEndNodesMode.None;
			switch (Event.current.type)
			{
			case EventType.Layout:
			{
				nodes.Add(node);
				idToNode.Add(node.id, node);
				nodeToGUIData.Add(node, areas);
				nodeToConnectionOptions.Add(node, (!connectionOptions.HasValue) ? new NodeConnectionOptions(allowManualConnections: true) : connectionOptions.Value);
				if (!flag)
				{
					break;
				}
				NodeConnectionOptions nodeConnectionOptions = nodeToConnectionOptions[node];
				if (nodeConnectionOptions.neverMarkAsEndNode)
				{
					break;
				}
				bool flag2 = false;
				switch (nodeConnectionOptions.connectionMode)
				{
				case ConnectionMode.Flexible:
					flag2 = node.connectedNodesIds.Count == 0;
					break;
				case ConnectionMode.Dual:
					flag2 = string.IsNullOrEmpty(node.connectedNodesIds[0]) || string.IsNullOrEmpty(node.connectedNodesIds[1]);
					break;
				default:
				{
					for (int i = 0; i < node.connectedNodesIds.Count; i++)
					{
						if (string.IsNullOrEmpty(node.connectedNodesIds[i]))
						{
							flag2 = true;
							break;
						}
					}
					break;
				}
				}
				if (flag2)
				{
					_endGUINodes.Add(node);
				}
				break;
			}
			case EventType.Repaint:
				nodeToGUIData[node] = areas;
				if (!areas.isVisible)
				{
					break;
				}
				if (options.evidenceSelectedNodes && selection.IsSelected(node))
				{
					using (new DeGUI.ColorScope(options.evidenceSelectedNodesColor))
					{
						GUI.Box(areas.fullArea.Expand(5f), "", _Styles.nodeSelectionOutlineThick);
					}
				}
				if (flag && _endGUINodes.Contains(node))
				{
					float num = Mathf.Min(areas.fullArea.height, 20f);
					GUI.DrawTexture(new Rect(areas.fullArea.xMax - num * 0.5f, areas.fullArea.yMax - num * 0.5f, num, num), DeStylePalette.ico_end);
				}
				break;
			}
			return areas.fullArea;
		}

		/// <summary>
		/// Opens the Help Panel
		/// </summary>
		public void OpenHelpPanel()
		{
			helpPanel.Open(doOpen: true);
			_repaintOnEnd = true;
		}

		/// <summary>
		/// Closes the Help Panel
		/// </summary>
		public void CloseHelpPanel()
		{
			helpPanel.Open(doOpen: false);
			_repaintOnEnd = true;
		}

		/// <summary>
		/// Opens or closes the Help panel based on its current state
		/// </summary>
		public void ToggleHelpPanel()
		{
			if (helpPanel.isOpen)
			{
				CloseHelpPanel();
			}
			else
			{
				OpenHelpPanel();
			}
		}

		/// <summary>
		/// Returns TRUE if the given area is visible (even if partially) inside the current nodeProcess area
		/// </summary>
		public bool AreaIsVisible(Rect area)
		{
			if (area.xMax > relativeArea.xMin && area.xMin < relativeArea.xMax && area.yMax > relativeArea.yMin)
			{
				return area.yMin < relativeArea.yMax;
			}
			return false;
		}

		/// <summary>
		/// Captures a screenshot of the node editor area and returns it when calling the onComplete method.<para />
		/// Sadly this requires a callback because if called immediately the capture will fail
		/// with a "[d3d11] attempting to ReadPixels outside of RenderTexture bounds!" error in most cases
		/// </summary>
		/// <param name="screenshotMode">Screenshot mode</param>
		/// <param name="onComplete">A callback that accepts the generated Texture2D object</param>
		/// <param name="allNodesScaleFactor">Screenshot scale factor (only used if screenshotMode is set to <see cref="F:DG.DemiEditor.DeGUINodeSystem.NodeProcess.ScreenshotMode.AllNodes" />)</param>
		/// <param name="useProgressBar">If TRUE (default) displays a progress bar during the operation.
		/// You'll want to set this to FALSE when you're already using a custom progressBar
		/// and the screenshot is only part of a larger queue of operations</param>
		public void CaptureScreenshot(ScreenshotMode screenshotMode, Action<Texture2D> onComplete, float allNodesScaleFactor = 1f, bool useProgressBar = true)
		{
			ScreenshotManager.CaptureScreenshot(this, screenshotMode, onComplete, allNodesScaleFactor, useProgressBar);
		}

		internal Rect EvaluateFullNodesArea()
		{
			Rect rect = nodeToGUIData[nodes[0]].fullArea;
			foreach (IEditorGUINode node in nodes)
			{
				rect = rect.Add(nodeToGUIData[node].fullArea);
			}
			return rect;
		}

		internal void BeginGUI<T>(Rect nodeArea, ref Vector2 refAreaShift, IList<T> controlNodes) where T : class, IEditorGUINode, new()
		{
			if (!_guiInitialized)
			{
				_guiInitialized = true;
				_isDockableEditor = DeEditorPanelUtils.IsDockableWindow(editor);
			}
			if (options.debug_showFps)
			{
				_debug.OnNodeProcessStart(interaction.state);
			}
			if (_isDockableEditor)
			{
				GUI.EndGroup();
				nodeArea.y += 22f;
			}
			if (controlNodes == null)
			{
				if (orderedNodes.Count > 0)
				{
					orderedNodes.Clear();
				}
			}
			else
			{
				if (orderedNodes.Count != controlNodes.Count)
				{
					orderedNodes.Clear();
				}
				if (orderedNodes.Count == 0)
				{
					for (int i = 0; i < controlNodes.Count; i++)
					{
						orderedNodes.Add(controlNodes[i]);
					}
				}
			}
			_Styles.Init();
			position = nodeArea;
			if (_doForceApplyAreaShift)
			{
				refAreaShift = _forceApplyAreaShift;
				_doForceApplyAreaShift = false;
			}
			else if (_forceApplyAreaShiftBy != Vector2.zero)
			{
				refAreaShift += _forceApplyAreaShiftBy;
				_forceApplyAreaShiftBy = Vector2.zero;
			}
			areaShift = new Vector2((int)refAreaShift.x, (int)refAreaShift.y);
			if (options.showMinimap)
			{
				if (_minimap == null)
				{
					_minimap = new Minimap(this);
				}
			}
			else
			{
				_minimap = null;
			}
			EditorGUI.BeginDisabledGroup(helpPanel.isOpen);
			if (!Mathf.Approximately(guiScale, 1f))
			{
				GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * guiScale);
			}
			relativeArea = new Rect(0f, 0f, nodeArea.width / guiScale, nodeArea.height / guiScale);
			guiScalePositionDiff = new Vector2(nodeArea.x - nodeArea.x / guiScale, nodeArea.y - nodeArea.y / guiScale);
			GUILayout.BeginArea(new Rect(nodeArea.x / guiScale, nodeArea.y / guiScale, relativeArea.width, relativeArea.height));
			if (Event.current.type == EventType.MouseDown && interaction.readyForState != 0)
			{
				interaction.SetReadyFor(InteractionManager.ReadyFor.Unset);
			}
			if (!interaction.mouseTargetIsLocked)
			{
				EvaluateAndStoreMouseTarget();
			}
			if (Event.current.type == EventType.Layout)
			{
				nodes.Clear();
				idToNode.Clear();
				nodeToGUIData.Clear();
				nodeToConnectionOptions.Clear();
				_endGUINodes.Clear();
			}
			DeGUIKey.KeysRefreshResult keysRefreshResult = DeGUIKey.Refresh("INTERNAL_DeGUINodeProcess");
			if (keysRefreshResult.pressed.alt || keysRefreshResult.released.alt)
			{
				_repaintOnEnd = true;
			}
			if (interaction.Update())
			{
				_repaintOnEnd = true;
			}
			if (options.drawBackgroundGrid)
			{
				if (options.gridTextureOverride == null)
				{
					DeGUI.BackgroundGrid(relativeArea, areaShift, options.forceDarkSkin, 1f / guiScale);
				}
				else
				{
					DeGUI.BackgroundGrid(relativeArea, areaShift, options.gridTextureOverride, 1f / guiScale);
				}
			}
			if (options.evidenceEndNodes == ProcessOptions.EvidenceEndNodesMode.Invasive && Event.current.type == EventType.Repaint)
			{
				foreach (IEditorGUINode endGUINode in _endGUINodes)
				{
					Rect rect = nodeToGUIData[endGUINode].fullArea.Expand(options.evidenceEndNodesBackgroundBorder);
					Texture2D tileBars_slanted = DeStylePalette.tileBars_slanted;
					using (new DeGUI.ColorScope(null, null, options.evidenceEndNodesBackgroundColor))
					{
						GUI.DrawTextureWithTexCoords(rect, tileBars_slanted, new Rect(0f, 0f, rect.width / (float)tileBars_slanted.width, rect.height / (float)tileBars_slanted.height));
					}
				}
			}
			if (_minimap != null)
			{
				_minimap.DrawButton();
			}
			switch (Event.current.type)
			{
			case EventType.MouseDown:
				if (_contextPanel.HasMouseOver())
				{
					break;
				}
				switch (Event.current.button)
				{
				case 0:
					interaction.mousePositionOnLMBPress = Event.current.mousePosition;
					switch (interaction.mouseTargetType)
					{
					case InteractionManager.TargetType.Background:
						if (DeGUIKey.Exclusive.shift)
						{
							selection.selectionMode = SelectionManager.Mode.Add;
							selection.StoreSnapshot();
						}
						else if (selection.DeselectAll())
						{
							_repaintOnEnd = true;
						}
						UnfocusAll();
						interaction.SetReadyFor(InteractionManager.ReadyFor.DrawingSelection);
						break;
					case InteractionManager.TargetType.Node:
						if (DeGUIKey.Exclusive.ctrl)
						{
							NodeConnectionOptions nodeConnectionOptions = nodeToConnectionOptions[interaction.targetNode];
							if (nodeConnectionOptions.allowManualConnections && (nodeConnectionOptions.connectionMode == ConnectionMode.Flexible || interaction.targetNode.connectedNodesIds.Count >= 1))
							{
								interaction.SetReadyFor(InteractionManager.ReadyFor.DraggingConnector);
								UnfocusAll();
								Event.current.Use();
							}
						}
						else
						{
							bool flag2 = selection.IsSelected(interaction.targetNode);
							if (interaction.nodeTargetType == InteractionManager.NodeTargetType.DraggableArea)
							{
								interaction.SetReadyFor(InteractionManager.ReadyFor.DraggingNodes);
								UnfocusAll();
								if (DeGUIKey.Exclusive.shiftAlt)
								{
									if (!flag2)
									{
										selection.Select(interaction.targetNode, keepExistingSelections: true);
									}
									SelectAllForwardConnectedNodes(interaction.targetNode);
									_repaintOnEnd = true;
								}
								else if (DeGUIKey.Exclusive.shift)
								{
									selection.StoreSnapshot();
									if (!flag2)
									{
										selection.Select(interaction.targetNode, keepExistingSelections: true);
									}
									_repaintOnEnd = true;
								}
								else if (!flag2)
								{
									selection.Select(interaction.targetNode, keepExistingSelections: false);
									_repaintOnEnd = true;
								}
							}
							else if (!flag2)
							{
								UnfocusAll();
								selection.Select(interaction.targetNode, keepExistingSelections: false);
								_repaintOnEnd = true;
							}
						}
						if (controlNodes != null)
						{
							UpdateOrderedNodesSorting();
						}
						break;
					}
					break;
				case 1:
					switch (interaction.mouseTargetType)
					{
					case InteractionManager.TargetType.Background:
						if (selection.selectedNodes.Count > 0)
						{
							_repaintOnEnd = true;
						}
						selection.DeselectAll();
						break;
					case InteractionManager.TargetType.Node:
						if (!selection.IsSelected(interaction.targetNode))
						{
							_repaintOnEnd = true;
							selection.Select(interaction.targetNode, keepExistingSelections: false);
						}
						break;
					}
					break;
				}
				break;
			case EventType.MouseDrag:
				switch (interaction.readyForState)
				{
				case InteractionManager.ReadyFor.DrawingSelection:
					interaction.SetState(InteractionManager.State.DrawingSelection);
					break;
				case InteractionManager.ReadyFor.DraggingNodes:
					if (!((Event.current.mousePosition - interaction.mousePositionOnLMBPress).magnitude >= InteractionManager.MinDragStartupDistance))
					{
						break;
					}
					if (DeGUIKey.Exclusive.ctrlShift && options.allowCopyPaste)
					{
						CloneAndCopySelectedNodes(controlNodes);
						if (PasteNodesFromClipboard(controlNodes, adaptGuiPositionToMouse: false))
						{
							selection.DeselectAll();
							foreach (IEditorGUINode tmp_node in _tmp_nodes)
							{
								selection.Select(tmp_node, keepExistingSelections: true);
							}
							foreach (IEditorGUINode currClone in _clipboard.currClones)
							{
								selection.Select(currClone, keepExistingSelections: true);
							}
							IEditorGUINode cloneByOriginalId = _clipboard.GetCloneByOriginalId(interaction.targetNode.id);
							if (cloneByOriginalId != null)
							{
								interaction.targetNode = cloneByOriginalId;
							}
						}
					}
					_nodeDragManager.BeginDrag(interaction.targetNode, selection.selectedNodes, nodes, nodeToGUIData);
					_nodeDragManager.ApplyDrag(Event.current.mousePosition - interaction.mousePositionOnLMBPress - Event.current.delta);
					interaction.SetState(InteractionManager.State.DraggingNodes);
					break;
				case InteractionManager.ReadyFor.DraggingConnector:
					if ((Event.current.mousePosition - interaction.mousePositionOnLMBPress).magnitude >= InteractionManager.MinDragStartupDistance)
					{
						interaction.SetState(InteractionManager.State.DraggingConnector);
					}
					break;
				}
				switch (Event.current.button)
				{
				case 0:
					switch (interaction.state)
					{
					case InteractionManager.State.DrawingSelection:
						selection.selectionRect = new Rect(Mathf.Min(interaction.mousePositionOnLMBPress.x, Event.current.mousePosition.x), Mathf.Min(interaction.mousePositionOnLMBPress.y, Event.current.mousePosition.y), Mathf.Abs(Event.current.mousePosition.x - interaction.mousePositionOnLMBPress.x), Mathf.Abs(Event.current.mousePosition.y - interaction.mousePositionOnLMBPress.y));
						if (selection.selectionMode == SelectionManager.Mode.Add)
						{
							selection.Select(selection.selectedNodesSnapshot, keepExistingSelections: false);
						}
						else
						{
							selection.DeselectAll();
						}
						foreach (IEditorGUINode node in nodes)
						{
							if (selection.selectionRect.Includes(nodeToGUIData[node].fullArea))
							{
								selection.Select(node, keepExistingSelections: true);
							}
						}
						_repaintOnEnd = true;
						break;
					case InteractionManager.State.DraggingNodes:
						_nodeDragManager.ApplyDrag(Event.current.delta);
						guiChangeType = GUIChangeType.DragNodes;
						GUI.changed = (_repaintOnEnd = true);
						DispatchOnGUIChange(GUIChangeType.DragNodes);
						break;
					case InteractionManager.State.DraggingConnector:
						_repaintOnEnd = true;
						break;
					}
					break;
				case 2:
					interaction.SetState(InteractionManager.State.Panning);
					refAreaShift += Event.current.delta;
					guiChangeType = GUIChangeType.Pan;
					GUI.changed = (_repaintOnEnd = true);
					DispatchOnGUIChange(GUIChangeType.Pan);
					break;
				}
				break;
			case EventType.ContextClick:
				if (!_contextPanel.HasMouseOver())
				{
					interaction.SetState(InteractionManager.State.ContextClick);
					_resetInteractionOnEnd = true;
				}
				break;
			case EventType.ScrollWheel:
			{
				if (!options.mouseWheelScalesGUI || !DeGUIKey.Exclusive.ctrl)
				{
					break;
				}
				bool flag = Event.current.delta.y < 0f;
				if ((flag && Mathf.Approximately(options.guiScaleValues[0], guiScale)) || (!flag && Mathf.Approximately(options.guiScaleValues[options.guiScaleValues.Length - 1], guiScale)))
				{
					break;
				}
				for (int j = 0; j < options.guiScaleValues.Length; j++)
				{
					if (Mathf.Approximately(options.guiScaleValues[j], guiScale))
					{
						float num = guiScale;
						guiScale = (flag ? options.guiScaleValues[j - 1] : options.guiScaleValues[j + 1]);
						Vector2 mousePosition = Event.current.mousePosition;
						Vector2 vector = (flag ? (-mousePosition) : mousePosition);
						float num2 = guiScale / num;
						num2 = ((guiScale < num) ? ((1f - num2) / num2) : ((guiScale - num) / guiScale));
						vector *= num2;
						refAreaShift += vector;
						_repaintOnEnd = true;
						DispatchOnGUIChange(GUIChangeType.GUIScale);
						break;
					}
				}
				break;
			}
			case EventType.KeyDown:
				if (Event.current.keyCode == KeyCode.Escape)
				{
					UnfocusAll();
				}
				if (GUIUtility.keyboardControl > 0)
				{
					break;
				}
				switch (Event.current.keyCode)
				{
				case KeyCode.UpArrow:
				case KeyCode.DownArrow:
				case KeyCode.RightArrow:
				case KeyCode.LeftArrow:
					if (DeGUIKey.Exclusive.softCtrlShift)
					{
						switch (Event.current.keyCode)
						{
						case KeyCode.LeftArrow:
							_contextPanel.AlignAndArrangeNodes(horizontally: false, selection.selectedNodes);
							break;
						case KeyCode.UpArrow:
							_contextPanel.AlignAndArrangeNodes(horizontally: true, selection.selectedNodes);
							break;
						}
					}
					else
					{
						if (selection.selectedNodes.Count == 0 || (!DeGUIKey.none && !DeGUIKey.Exclusive.shift))
						{
							break;
						}
						Vector2 zero = Vector2.zero;
						switch (Event.current.keyCode)
						{
						case KeyCode.UpArrow:
							zero.y = -1f;
							break;
						case KeyCode.DownArrow:
							zero.y = 1f;
							break;
						case KeyCode.LeftArrow:
							zero.x = -1f;
							break;
						case KeyCode.RightArrow:
							zero.x = 1f;
							break;
						}
						if (DeGUIKey.Exclusive.shift)
						{
							zero *= 10f;
						}
						foreach (IEditorGUINode selectedNode in selection.selectedNodes)
						{
							selectedNode.guiPosition += zero;
						}
					}
					guiChangeType = GUIChangeType.DragNodes;
					GUI.changed = (_repaintOnEnd = true);
					DispatchOnGUIChange(GUIChangeType.DragNodes);
					break;
				}
				break;
			case EventType.KeyUp:
				if (GUIUtility.keyboardControl > 0)
				{
					break;
				}
				switch (Event.current.keyCode)
				{
				case KeyCode.F1:
					OpenHelpPanel();
					break;
				case KeyCode.Backspace:
				case KeyCode.Delete:
					if (options.allowDeletion && selection.selectedNodes.Count != 0 && (_onDeleteNodesCallback == null || _onDeleteNodesCallback(selection.selectedNodes)))
					{
						DeleteSelectedNodesInList(controlNodes);
						selection.DeselectAll();
						guiChangeType = GUIChangeType.DeletedNodes;
						GUI.changed = (_repaintOnEnd = true);
						DispatchOnGUIChange(GUIChangeType.DeletedNodes);
					}
					break;
				case KeyCode.A:
					if (DeGUIKey.Exclusive.softCtrl)
					{
						selection.Select(nodes, keepExistingSelections: false);
						_repaintOnEnd = true;
					}
					break;
				case KeyCode.C:
					if (DeGUIKey.Exclusive.softCtrl && options.allowCopyPaste)
					{
						CloneAndCopySelectedNodes(controlNodes);
					}
					break;
				case KeyCode.X:
					if (DeGUIKey.Exclusive.softCtrl && options.allowCopyPaste && CloneAndCopySelectedNodes(controlNodes, cut: true))
					{
						DeleteSelectedNodesInList(controlNodes);
						selection.DeselectAll();
						guiChangeType = GUIChangeType.DeletedNodes;
						GUI.changed = (_repaintOnEnd = true);
						DispatchOnGUIChange(GUIChangeType.DeletedNodes);
					}
					break;
				case KeyCode.V:
					if (!DeGUIKey.Exclusive.softCtrl || !options.allowCopyPaste || !PasteNodesFromClipboard(controlNodes, adaptGuiPositionToMouse: true))
					{
						break;
					}
					selection.DeselectAll();
					foreach (IEditorGUINode currClone2 in _clipboard.currClones)
					{
						selection.Select(currClone2, keepExistingSelections: true);
					}
					guiChangeType = GUIChangeType.AddedNodes;
					GUI.changed = (_repaintOnEnd = true);
					DispatchOnGUIChange(GUIChangeType.AddedNodes);
					break;
				}
				break;
			}
			if (Event.current.rawType != EventType.MouseUp)
			{
				return;
			}
			switch (interaction.state)
			{
			case InteractionManager.State.Inactive:
				if (!_contextPanel.HasMouseOver() && DeGUIKey.Exclusive.shift && interaction.nodeTargetType == InteractionManager.NodeTargetType.DraggableArea && selection.IsSelected(interaction.targetNode) && selection.selectedNodesSnapshot.Contains(interaction.targetNode))
				{
					selection.Deselect(interaction.targetNode);
					_repaintOnEnd = true;
				}
				break;
			case InteractionManager.State.DrawingSelection:
				selection.selectionMode = SelectionManager.Mode.Default;
				selection.ClearSnapshot();
				selection.selectionRect = default(Rect);
				_repaintOnEnd = true;
				break;
			case InteractionManager.State.DraggingConnector:
			{
				IEditorGUINode mouseOverNode = GetMouseOverNode();
				if (mouseOverNode != null && mouseOverNode != interaction.targetNode)
				{
					switch (nodeToConnectionOptions[Connector.dragData.node].connectionMode)
					{
					case ConnectionMode.Dual:
					{
						int index = ((DeGUIKey.Exclusive.ctrl && DeGUIKey.Extra.space) ? 1 : 0);
						if (Connector.dragData.node.connectedNodesIds[index] != mouseOverNode.id)
						{
							Connector.dragData.node.connectedNodesIds[index] = mouseOverNode.id;
							guiChangeType = GUIChangeType.NodeConnection;
							GUI.changed = true;
							DispatchOnGUIChange(GUIChangeType.NodeConnection);
						}
						break;
					}
					case ConnectionMode.Flexible:
					{
						if (NodeIsForwardConnectedTo(Connector.dragData.node, mouseOverNode.id))
						{
							break;
						}
						bool flag3 = false;
						for (int k = 0; k < Connector.dragData.node.connectedNodesIds.Count; k++)
						{
							if (string.IsNullOrEmpty(Connector.dragData.node.connectedNodesIds[k]))
							{
								Connector.dragData.node.connectedNodesIds[k] = mouseOverNode.id;
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							Connector.dragData.node.connectedNodesIds.Add(mouseOverNode.id);
						}
						guiChangeType = GUIChangeType.NodeConnection;
						GUI.changed = true;
						DispatchOnGUIChange(GUIChangeType.NodeConnection);
						break;
					}
					case ConnectionMode.NormalPlus:
						if (DeGUIKey.Exclusive.ctrl && DeGUIKey.Extra.space)
						{
							int index2 = Connector.dragData.node.connectedNodesIds.Count - 1;
							if (Connector.dragData.node.connectedNodesIds[index2] != mouseOverNode.id)
							{
								Connector.dragData.node.connectedNodesIds[index2] = mouseOverNode.id;
								guiChangeType = GUIChangeType.NodeConnection;
								GUI.changed = true;
								DispatchOnGUIChange(GUIChangeType.NodeConnection);
							}
						}
						else if (Connector.dragData.node.connectedNodesIds[interaction.targetNodeConnectorAreaIndex] != mouseOverNode.id)
						{
							Connector.dragData.node.connectedNodesIds[interaction.targetNodeConnectorAreaIndex] = mouseOverNode.id;
							guiChangeType = GUIChangeType.NodeConnection;
							GUI.changed = true;
							DispatchOnGUIChange(GUIChangeType.NodeConnection);
						}
						break;
					default:
						if (Connector.dragData.node.connectedNodesIds[interaction.targetNodeConnectorAreaIndex] != mouseOverNode.id)
						{
							Connector.dragData.node.connectedNodesIds[interaction.targetNodeConnectorAreaIndex] = mouseOverNode.id;
							guiChangeType = GUIChangeType.NodeConnection;
							GUI.changed = true;
							DispatchOnGUIChange(GUIChangeType.NodeConnection);
						}
						break;
					}
					_repaintOnEnd = true;
					break;
				}
				if (nodeToConnectionOptions[Connector.dragData.node].connectionMode != ConnectionMode.Flexible)
				{
					bool num3 = !string.IsNullOrEmpty(Connector.dragData.node.connectedNodesIds[interaction.targetNodeConnectorAreaIndex]);
					Connector.dragData.node.connectedNodesIds[interaction.targetNodeConnectorAreaIndex] = null;
					if (num3)
					{
						guiChangeType = GUIChangeType.NodeConnection;
						GUI.changed = true;
						DispatchOnGUIChange(GUIChangeType.NodeConnection);
					}
				}
				_repaintOnEnd = true;
				break;
			}
			}
			if (interaction.EvaluateMouseUp())
			{
				interaction.SetState(InteractionManager.State.DoubleClick);
				_resetInteractionOnEnd = true;
			}
			else
			{
				interaction.SetState(InteractionManager.State.Inactive);
			}
		}

		internal void EndGUI()
		{
			EventType type = Event.current.type;
			if ((uint)type <= 1u || type == EventType.Repaint)
			{
				bool flag = false;
				for (int i = 0; i < nodes.Count; i++)
				{
					IEditorGUINode editorGUINode = nodes[i];
					List<string> connectedNodesIds = editorGUINode.connectedNodesIds;
					int num = editorGUINode.connectedNodesIds.Count;
					for (int num2 = num - 1; num2 > -1; num2--)
					{
						string text = connectedNodesIds[num2];
						if (!string.IsNullOrEmpty(text))
						{
							if (!idToNode.ContainsKey(text))
							{
								idToNode.Remove(text);
							}
							else
							{
								Connector.ConnectResult connectResult = _connector.Connect(num2, num, nodeToConnectionOptions[editorGUINode], editorGUINode);
								if (connectResult.changed)
								{
									flag = true;
									if (nodeToConnectionOptions[editorGUINode].connectionMode == ConnectionMode.Flexible)
									{
										if (connectResult.aConnectionWasAdded)
										{
											num++;
										}
										else
										{
											num--;
											editorGUINode.connectedNodesIds.RemoveAt(num2);
										}
									}
									else if (connectResult.aConnectionWasDeleted)
									{
										editorGUINode.connectedNodesIds[num2] = null;
									}
								}
							}
						}
					}
				}
				if (flag)
				{
					guiChangeType = GUIChangeType.NodeConnection;
					GUI.changed = (_repaintOnEnd = true);
					DispatchOnGUIChange(GUIChangeType.NodeConnection);
				}
			}
			if (!_repaintOnEnd)
			{
				if (Event.current.type == EventType.Repaint)
				{
					if (options.evidenceSelectedNodesArea && selection.selectedNodes.Count > 1)
					{
						Rect rect = nodeToGUIData[selection.selectedNodes[0]].fullArea;
						for (int j = 1; j < selection.selectedNodes.Count; j++)
						{
							rect = rect.Add(nodeToGUIData[selection.selectedNodes[j]].fullArea);
						}
						using (new DeGUI.ColorScope(options.evidenceSelectedNodesColor.SetAlpha(0.4f)))
						{
							GUI.Box(rect.Expand(5f), "", _Styles.nodeSelectionOutline);
						}
					}
					switch (interaction.state)
					{
					case InteractionManager.State.DrawingSelection:
						using (new DeGUI.ColorScope(options.evidenceSelectedNodesColor))
						{
							GUI.Box(selection.selectionRect, "", _Styles.selectionRect);
						}
						break;
					case InteractionManager.State.DraggingConnector:
					{
						Color color = _connector.Drag(interaction, Event.current.mousePosition, nodeToGUIData[interaction.targetNode], nodeToConnectionOptions[interaction.targetNode], options.connectorsThickness + 2f);
						DeGUI.DrawColoredSquare(interaction.targetNodeConnectorArea.Expand(1f), color.SetAlpha(0.32f));
						using (new DeGUI.ColorScope(color))
						{
							GUI.Box(interaction.targetNodeConnectorArea.Expand(4f), "", _Styles.nodeSelectionOutlineThick);
						}
						IEditorGUINode mouseOverNode = GetMouseOverNode();
						if (mouseOverNode != null && mouseOverNode != interaction.targetNode)
						{
							using (new DeGUI.ColorScope(color))
							{
								GUI.Box(nodeToGUIData[mouseOverNode].fullArea.Expand(4f), "", _Styles.nodeSelectionOutlineThick);
							}
						}
						if (DeGUIKey.Extra.space)
						{
							break;
						}
						ConnectionMode connectionMode = nodeToConnectionOptions[interaction.targetNode].connectionMode;
						if ((uint)(connectionMode - 2) <= 1u)
						{
							Vector2 mousePosition = Event.current.mousePosition;
							Rect rect2 = new Rect(mousePosition.x, mousePosition.y - 32f - 6f, 176f, 32f);
							using (new DeGUI.ColorScope(new Color(0f, 0f, 0f, 0.8f)))
							{
								GUI.Label(rect2, "<b><color=#ffffcd>CTRL+SPACE</color></b>\nto drag alternate connection", _Styles.draggingTooltip);
							}
						}
						break;
					}
					}
					if (interaction.state == InteractionManager.State.DraggingNodes)
					{
						_nodeDragManager.EndGUI();
					}
				}
				if (_minimap != null && Event.current.type != EventType.Layout)
				{
					_minimap.Draw();
				}
				_contextPanel.Draw();
				if (options.debug_showFps)
				{
					_debug.Draw(relativeArea);
				}
				if (helpPanel.isOpen)
				{
					bool enabled = GUI.enabled;
					GUI.enabled = true;
					bool num3 = !helpPanel.Draw();
					GUI.enabled = enabled;
					if (num3)
					{
						CloseHelpPanel();
					}
				}
			}
			guiChangeType = GUIChangeType.None;
			if (_resetInteractionOnEnd)
			{
				_resetInteractionOnEnd = false;
				interaction.SetState(InteractionManager.State.Inactive);
			}
			if (_repaintOnEnd)
			{
				_repaintOnEnd = false;
				if (_minimap != null)
				{
					_minimap.RefreshMapTextureOnNextPass();
				}
				editor.Repaint();
			}
			GUILayout.EndArea();
			EditorGUI.EndDisabledGroup();
			GUI.matrix = Matrix4x4.identity;
			if (options.debug_showFps)
			{
				_debug.OnNodeProcessEnd();
			}
			if (_isDockableEditor)
			{
				GUI.BeginGroup(editor.position.ResetXY().SetY(22f));
			}
		}

		private void OnUndoRedoCallback()
		{
			selection.DeselectAll();
		}

		private void EvaluateAndStoreMouseTarget()
		{
			if (!relativeArea.Contains(Event.current.mousePosition))
			{
				interaction.SetMouseTargetType(InteractionManager.TargetType.None);
				interaction.targetNode = null;
				return;
			}
			IEditorGUINode mouseOverNode = GetMouseOverNode();
			if (mouseOverNode != null)
			{
				interaction.targetNode = mouseOverNode;
				NodeGUIData nodeGUIData = nodeToGUIData[mouseOverNode];
				interaction.targetNodeConnectorAreaIndex = 0;
				interaction.targetNodeConnectorArea = nodeGUIData.fullArea;
				if (nodeGUIData.dragArea.Contains(Event.current.mousePosition))
				{
					interaction.SetMouseTargetType(InteractionManager.TargetType.Node, InteractionManager.NodeTargetType.DraggableArea);
				}
				else
				{
					interaction.SetMouseTargetType(InteractionManager.TargetType.Node, InteractionManager.NodeTargetType.NonDraggableArea);
					if (mouseOverNode.connectedNodesIds.Count > 1 && nodeGUIData.connectorAreas != null)
					{
						for (int i = 0; i < nodeGUIData.connectorAreas.Count; i++)
						{
							Rect targetNodeConnectorArea = nodeGUIData.connectorAreas[i];
							if (targetNodeConnectorArea.Contains(Event.current.mousePosition))
							{
								interaction.targetNodeConnectorAreaIndex = i;
								interaction.targetNodeConnectorArea = targetNodeConnectorArea;
								break;
							}
						}
					}
				}
				interaction.SetMouseTargetType(InteractionManager.TargetType.Node, nodeToGUIData[mouseOverNode].dragArea.Contains(Event.current.mousePosition) ? InteractionManager.NodeTargetType.DraggableArea : InteractionManager.NodeTargetType.NonDraggableArea);
			}
			else
			{
				interaction.SetMouseTargetType(InteractionManager.TargetType.Background);
				interaction.targetNode = null;
			}
		}

		private IEditorGUINode GetMouseOverNode()
		{
			for (int num = nodes.Count - 1; num > -1; num--)
			{
				IEditorGUINode editorGUINode = nodes[num];
				if (nodeToGUIData[editorGUINode].fullArea.Contains(Event.current.mousePosition))
				{
					return editorGUINode;
				}
			}
			return null;
		}

		private bool CloneAndCopySelectedNodes<T>(IList<T> controlNodes, bool cut = false) where T : IEditorGUINode, new()
		{
			if (selection.selectedNodes.Count == 0)
			{
				return false;
			}
			_tmp_nodes.Clear();
			_clipboard.Clear();
			foreach (IEditorGUINode selectedNode in selection.selectedNodes)
			{
				string id = selectedNode.id;
				if (IndexOfNodeById(id, controlNodes) == -1)
				{
					_tmp_nodes.Add(selectedNode);
					continue;
				}
				IEditorGUINode editorGUINode = _clipboard.CloneNode<T>(selectedNode);
				if (_onCloneNodeCallback != null && !_onCloneNodeCallback(selectedNode, editorGUINode))
				{
					_tmp_nodes.Add(selectedNode);
				}
				else
				{
					_clipboard.Add(selectedNode, editorGUINode, nodeToConnectionOptions[selectedNode], _onCloneNodeCallback);
				}
			}
			if (_clipboard.currClones.Count == 0)
			{
				return false;
			}
			if (cut)
			{
				DeleteSelectedNodesInList(controlNodes);
			}
			return true;
		}

		private bool PasteNodesFromClipboard<T>(IList<T> controlNodes, bool adaptGuiPositionToMouse) where T : class, IEditorGUINode, new()
		{
			if (!_clipboard.hasContent)
			{
				return false;
			}
			List<T> nodesToPaste = _clipboard.GetNodesToPaste<T>();
			Vector2 vector = Vector2.zero;
			if (adaptGuiPositionToMouse)
			{
				Vector2 guiPosition = nodesToPaste[0].guiPosition;
				for (int i = 1; i < nodesToPaste.Count; i++)
				{
					IEditorGUINode editorGUINode = nodesToPaste[i];
					if (editorGUINode.guiPosition.x < guiPosition.x)
					{
						guiPosition.x = editorGUINode.guiPosition.x;
					}
					if (editorGUINode.guiPosition.y < guiPosition.y)
					{
						guiPosition.y = editorGUINode.guiPosition.y;
					}
				}
				vector = Event.current.mousePosition - (guiPosition + areaShift);
			}
			foreach (T item in nodesToPaste)
			{
				T current = item;
				current.guiPosition += vector;
				controlNodes.Add(current);
				nodeToGUIData.Add(current, _clipboard.GetGuiDataByCloneId(current.id));
				nodeToConnectionOptions.Add(current, _clipboard.GetConnectionOptionsByCloneId(current.id));
				idToNode.Add(current.id, current);
				orderedNodes.Add(current);
			}
			return true;
		}

		private void DeleteSelectedNodesInList<T>(IList<T> removeFrom) where T : IEditorGUINode
		{
			_tmp_string.Clear();
			foreach (IEditorGUINode selectedNode in selection.selectedNodes)
			{
				int num = IndexOfNodeById(selectedNode.id, removeFrom);
				if (num != -1)
				{
					_tmp_string.Add(selectedNode.id);
					removeFrom.RemoveAt(num);
					num = orderedNodes.IndexOf(selectedNode);
					if (num != -1)
					{
						orderedNodes.RemoveAt(num);
					}
				}
			}
			foreach (IEditorGUINode node in nodes)
			{
				for (int i = 0; i < node.connectedNodesIds.Count; i++)
				{
					if (_tmp_string.Contains(node.connectedNodesIds[i]))
					{
						node.connectedNodesIds[i] = null;
					}
				}
			}
		}

		private void UpdateOrderedNodesSorting()
		{
			int count = selection.selectedNodes.Count;
			int count2 = orderedNodes.Count;
			if (count == 0 || count2 == 0 || count >= count2)
			{
				return;
			}
			bool flag = false;
			if (count == 1)
			{
				if (!(selection.selectedNodes[0].id != orderedNodes[count2 - 1].id))
				{
					return;
				}
				for (int i = 0; i < count2; i++)
				{
					if (!(orderedNodes[i].id != interaction.targetNode.id))
					{
						orderedNodes.Shift(i, count2 - 1);
						break;
					}
				}
			}
			else
			{
				for (int num = count2 - 1; num > count2 - count - 1; num--)
				{
					if (!selection.selectedNodes.Contains(orderedNodes[num]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return;
				}
				int num2 = 0;
				for (int num3 = count2 - 1; num3 > -1; num3--)
				{
					if (selection.selectedNodes.Contains(orderedNodes[num3]))
					{
						orderedNodes.Shift(num3, count2 - 1 - num2);
						num2++;
					}
				}
			}
			guiChangeType = GUIChangeType.SortedNodes;
			GUI.changed = (_repaintOnEnd = true);
			DispatchOnGUIChange(GUIChangeType.SortedNodes);
		}

		private void SelectAllForwardConnectedNodes(IEditorGUINode fromNode)
		{
			_tmp_nodes.Clear();
			SelectAllForwardConnectedNodes_Select(fromNode);
		}

		private void SelectAllForwardConnectedNodes_Select(IEditorGUINode fromNode)
		{
			foreach (string connectedNodesId in fromNode.connectedNodesIds)
			{
				if (!string.IsNullOrEmpty(connectedNodesId))
				{
					IEditorGUINode editorGUINode = idToNode[connectedNodesId];
					if (!_tmp_nodes.Contains(editorGUINode))
					{
						_tmp_nodes.Add(editorGUINode);
						selection.Select(editorGUINode, keepExistingSelections: true);
						SelectAllForwardConnectedNodes_Select(editorGUINode);
					}
				}
			}
		}

		private void UnfocusAll()
		{
			if (GUIUtility.keyboardControl > 0)
			{
				_repaintOnEnd = true;
			}
			GUI.FocusControl(null);
		}

		private bool NodeIsForwardConnectedTo(IEditorGUINode node, string id)
		{
			for (int i = 0; i < node.connectedNodesIds.Count; i++)
			{
				if (node.connectedNodesIds[i] == id)
				{
					return true;
				}
			}
			return false;
		}

		private IEditorGUINode GetNodeById<T>(string id, IList<T> lookInto) where T : IEditorGUINode
		{
			for (int i = 0; i < lookInto.Count; i++)
			{
				if (lookInto[i].id == id)
				{
					return lookInto[i];
				}
			}
			return null;
		}

		private int IndexOfNodeById<T>(string id, IList<T> lookInto) where T : IEditorGUINode
		{
			for (int i = 0; i < lookInto.Count; i++)
			{
				if (lookInto[i].id == id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
