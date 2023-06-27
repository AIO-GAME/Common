using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DG.DemiEditor;
using DG.DOTweenEditor.Core;
using DG.DOTweenEditor.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DG.DOTweenEditor
{
	[CustomEditor(typeof(DOTweenPath))]
	public class DOTweenPathInspector : ABSAnimationInspector
	{
		private readonly Color _wpColor = Color.white;

		private readonly Color _arrowsColor = new Color(1f, 1f, 1f, 0.85f);

		private readonly Color _wpColorEnd = Color.red;

		private DOTweenPath _src;

		private readonly List<WpHandle> _wpsByDepth = new List<WpHandle>();

		private int _minHandleControlId;

		private int _maxHandleControlId;

		private int _selectedWpIndex = -1;

		private int _lastSelectedWpIndex = -1;

		private int _lastCreatedWpIndex = -1;

		private bool _changed;

		private Vector3 _lastSceneViewCamPosition;

		private Quaternion _lastSceneViewCamRotation;

		private bool _isDragging;

		private bool _reselectAfterDrag;

		private bool _sceneCamStored;

		private bool _refreshAfterEnable;

		private bool _isWithinUICanvas;

		private MethodInfo _miHasRigidbody;

		private static readonly Regex _CopyWpsFromClipboardRegex = new Regex("(\\(|\\,)([-+]?[0-9]*\\.?[0-9]+)");

		private Camera _fooSceneCam;

		private Transform _fooSceneCamTrans;

		private ReorderableList _wpsList;

		public bool updater;

		private bool _showAddManager
		{
			get
			{
				if (_src.inspectorMode != 0)
				{
					return _src.inspectorMode == DOTweenInspectorMode.Developer;
				}
				return true;
			}
		}

		private bool _showTweenSettings
		{
			get
			{
				if (_src.inspectorMode != 0)
				{
					return _src.inspectorMode == DOTweenInspectorMode.Developer;
				}
				return true;
			}
		}

		private Camera _sceneCam
		{
			get
			{
				if (_fooSceneCam == null)
				{
					SceneView currentDrawingSceneView = SceneView.currentDrawingSceneView;
					if (currentDrawingSceneView == null)
					{
						return null;
					}
					_fooSceneCam = currentDrawingSceneView.camera;
				}
				return _fooSceneCam;
			}
		}

		private Transform _sceneCamTrans
		{
			get
			{
				if (_fooSceneCamTrans == null)
				{
					if (_sceneCam == null)
					{
						return null;
					}
					_fooSceneCamTrans = _sceneCam.transform;
				}
				return _fooSceneCamTrans;
			}
		}

		private void OnEnable()
		{
			_src = base.target as DOTweenPath;
			_isWithinUICanvas = _src.GetComponentInParent<Canvas>();
			StoreSceneCamData();
			if (_src.path == null)
			{
				ResetPath(RepaintMode.None);
			}
			onStartProperty = base.serializedObject.FindProperty("onStart");
			onPlayProperty = base.serializedObject.FindProperty("onPlay");
			onUpdateProperty = base.serializedObject.FindProperty("onUpdate");
			onStepCompleteProperty = base.serializedObject.FindProperty("onStepComplete");
			onCompleteProperty = base.serializedObject.FindProperty("onComplete");
			onRewindProperty = base.serializedObject.FindProperty("onRewind");
			onTweenCreatedProperty = base.serializedObject.FindProperty("onTweenCreated");
			_refreshAfterEnable = true;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (_miHasRigidbody == null)
			{
				Type looseScriptType = Utils.GetLooseScriptType("DG.Tweening.DOTweenModuleUtils+Physics");
				_miHasRigidbody = looseScriptType.GetMethod("HasRigidbody", BindingFlags.Static | BindingFlags.Public);
			}
			EditorGUIUtils.SetGUIStyles();
			GUILayout.Space(3f);
			EditorGUIUtils.InspectorLogo();
			if (Application.isPlaying)
			{
				GUILayout.Space(8f);
				GUILayout.Label("Path Editor disabled while in play mode", EditorGUIUtils.wordWrapLabelStyle);
				GUILayout.Space(10f);
				return;
			}
			if (_refreshAfterEnable)
			{
				_refreshAfterEnable = false;
				if (_src.path == null)
				{
					ResetPath(RepaintMode.None);
				}
				else
				{
					RefreshPath(RepaintMode.Scene, refreshWpIndexByDepth: true);
				}
				_wpsList = new ReorderableList(_src.wps, typeof(Vector3), draggable: true, displayHeader: true, displayAddButton: true, displayRemoveButton: true);
				_wpsList.drawHeaderCallback = delegate(Rect rect)
				{
					EditorGUI.LabelField(rect, "Path Waypoints");
				};
				_wpsList.onReorderCallback = delegate
				{
					RefreshPath(RepaintMode.Scene, refreshWpIndexByDepth: true);
				};
				_wpsList.drawElementCallback = delegate(Rect rect, int index, bool isActive, bool isFocused)
				{
					Rect position = new Rect(rect.xMin, rect.yMin, 23f, rect.height);
					Rect position2 = new Rect(position.xMax, position.yMin, rect.width - 23f, position.height);
					GUI.Label(position, (index + 1).ToString());
					_src.wps[index] = EditorGUI.Vector3Field(position2, "", _src.wps[index]);
				};
			}
			bool flag = false;
			Undo.RecordObject(_src, "DOTween Path");
			if (_src.inspectorMode != 0)
			{
				GUILayout.Label(string.Concat("Inspector Mode: <b>", _src.inspectorMode.ToString(), "</b>"), ABSAnimationInspector.styles.custom.warningLabel);
				GUILayout.Space(2f);
			}
			if (!(_src.GetComponent<DOTweenVisualManager>() != null) && _showAddManager)
			{
				if (GUILayout.Button(new GUIContent("Add Manager", "Adds a manager component which allows you to choose additional options for this gameObject")))
				{
					_src.gameObject.AddComponent<DOTweenVisualManager>();
				}
				GUILayout.Space(4f);
			}
			if (_isWithinUICanvas)
			{
				EditorGUILayout.HelpBox("Beware: paths are not made to be used inside a UI canvas, you should create them on a world object instead", MessageType.Error);
			}
			AnimationInspectorGUI.StickyTitle("Scene View Commands");
			DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
			GUILayout.Label(string.Concat("➲ SHIFT + ", EditorUtils.isOSXEditor ? "CMD" : "CTRL", ": add a waypoint\n➲ SHIFT + ALT: remove a waypoint"));
			EditorGUILayout.HelpBox("Enable Gizmos in your Scene/Game view in order to see the path", MessageType.Info);
			DeGUILayout.EndVBox();
			AnimationInspectorGUI.StickyTitle("Info");
			DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
			GUILayout.Label(string.Concat("Path Length: ", (_src.path == null) ? "-" : _src.path.length.ToString()));
			DeGUILayout.EndVBox();
			if (_showTweenSettings)
			{
				AnimationInspectorGUI.StickyTitle("Tween Options");
				GUILayout.BeginHorizontal();
				_src.autoPlay = DeGUILayout.ToggleButton(_src.autoPlay, new GUIContent("AutoPlay", "If selected, the tween will play automatically"), DeGUI.styles.button.tool);
				_src.autoKill = DeGUILayout.ToggleButton(_src.autoKill, new GUIContent("AutoKill", "If selected, the tween will be killed when it completes, and won't be reusable"), DeGUI.styles.button.tool);
				GUILayout.EndHorizontal();
				DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
				GUILayout.BeginHorizontal();
				_src.duration = EditorGUILayout.FloatField("Duration", _src.duration);
				if (_src.duration < 0f)
				{
					_src.duration = 0f;
				}
				_src.isSpeedBased = DeGUILayout.ToggleButton(_src.isSpeedBased, new GUIContent("SpeedBased", "If selected, the duration will count as units/degree x second"), DeGUI.styles.button.tool, GUILayout.Width(75f));
				GUILayout.EndHorizontal();
				_src.delay = EditorGUILayout.FloatField("Delay", _src.delay);
				if (_src.delay < 0f)
				{
					_src.delay = 0f;
				}
				_src.easeType = EditorGUIUtils.FilteredEasePopup("Ease", _src.easeType);
				if (_src.easeType == Ease.INTERNAL_Custom)
				{
					_src.easeCurve = EditorGUILayout.CurveField("   Ease Curve", _src.easeCurve);
				}
				_src.loops = EditorGUILayout.IntField(new GUIContent("Loops", "Set to -1 for infinite loops"), _src.loops);
				if (_src.loops < -1)
				{
					_src.loops = -1;
				}
				if (_src.loops > 1 || _src.loops == -1)
				{
					_src.loopType = (LoopType)(object)EditorGUILayout.EnumPopup("   Loop Type", _src.loopType);
				}
				_src.id = EditorGUILayout.TextField("ID", _src.id);
				_src.updateType = (UpdateType)(object)EditorGUILayout.EnumPopup("Update Type", _src.updateType);
				if (_src.inspectorMode == DOTweenInspectorMode.Developer)
				{
					GUILayout.BeginHorizontal();
					bool flag2 = (bool)_miHasRigidbody.Invoke(null, new object[1] { _src });
					_src.tweenRigidbody = EditorGUILayout.Toggle("Tween Rigidbody", flag2 && _src.tweenRigidbody);
					if (!flag2)
					{
						GUILayout.Label("No rigidbody found", ABSAnimationInspector.styles.custom.warningLabel);
					}
					GUILayout.EndHorizontal();
					if (_src.tweenRigidbody)
					{
						EditorGUILayout.HelpBox("Tweening a rigidbody works correctly only when it's kinematic", MessageType.Warning);
					}
				}
				DeGUILayout.EndVBox();
				AnimationInspectorGUI.StickyTitle("Path Tween Options");
				DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
				PathType pathType = _src.pathType;
				DOTweenPathType dOTweenPathType = (DOTweenPathType)((_src.pathType == PathType.CubicBezier) ? PathType.CatmullRom : _src.pathType);
				_src.pathType = (PathType)(object)EditorGUILayout.EnumPopup("Path Type", dOTweenPathType);
				if (pathType != _src.pathType)
				{
					flag = true;
				}
				if (_src.pathType != 0)
				{
					_src.pathResolution = EditorGUILayout.IntSlider("   Path resolution", _src.pathResolution, 2, 20);
				}
				bool isClosedPath = _src.isClosedPath;
				_src.isClosedPath = EditorGUILayout.Toggle("Close Path", _src.isClosedPath);
				if (isClosedPath != _src.isClosedPath)
				{
					flag = true;
				}
				_src.isLocal = EditorGUILayout.Toggle(new GUIContent("Local Movement", "If checked, the path will tween the localPosition (instead than the position) of its target"), _src.isLocal);
				_src.pathMode = (PathMode)(object)EditorGUILayout.EnumPopup("Path Mode", _src.pathMode);
				_src.lockRotation = (AxisConstraint)(object)EditorGUILayout.EnumPopup("Lock Rotation", _src.lockRotation);
				_src.orientType = (OrientType)(object)EditorGUILayout.EnumPopup("Orientation", _src.orientType);
				if (_src.orientType != 0)
				{
					switch (_src.orientType)
					{
					case OrientType.LookAtTransform:
						_src.lookAtTransform = EditorGUILayout.ObjectField("   LookAt Target", _src.lookAtTransform, typeof(Transform), true) as Transform;
						break;
					case OrientType.LookAtPosition:
						_src.lookAtPosition = EditorGUILayout.Vector3Field("   LookAt Position", _src.lookAtPosition);
						break;
					case OrientType.ToPath:
						_src.lookAhead = EditorGUILayout.Slider("   LookAhead", _src.lookAhead, 0f, 1f);
						break;
					}
				}
				DeGUILayout.EndVBox();
			}
			AnimationInspectorGUI.StickyTitle("Path Editor Options");
			DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
			_src.relative = EditorGUILayout.Toggle(new GUIContent("Move/Rotate W Target", "If toggled, the whole path moves and rotates with the target"), _src.relative);
			_src.pathColor = EditorGUILayout.ColorField("Color", _src.pathColor);
			_src.showIndexes = EditorGUILayout.Toggle("Show Indexes", _src.showIndexes);
			_src.showWpLength = EditorGUILayout.Toggle("Show WPs Lengths", _src.showWpLength);
			_src.livePreview = EditorGUILayout.Toggle("Live Preview", _src.livePreview);
			GUILayout.BeginHorizontal();
			GUILayout.Label("Handles Type/Mode", GUILayout.Width(EditorGUIUtility.labelWidth - 11f));
			_src.handlesType = (HandlesType)(object)EditorGUILayout.EnumPopup(_src.handlesType);
			_src.handlesDrawMode = (HandlesDrawMode)(object)EditorGUILayout.EnumPopup(_src.handlesDrawMode);
			GUILayout.EndHorizontal();
			if (_src.handlesDrawMode == HandlesDrawMode.Perspective)
			{
				_src.perspectiveHandleSize = EditorGUILayout.FloatField("   Handle Size", _src.perspectiveHandleSize);
			}
			if (GUILayout.Button("Open Path Scaler Tool"))
			{
				DOTweenPathScaler.Open(_src, this);
			}
			DeGUILayout.EndVBox();
			if (_showTweenSettings)
			{
				AnimationInspectorGUI.AnimationEvents(this, _src);
			}
			DrawExtras();
			GUILayout.Space(10f);
			DeGUILayout.BeginToolbar();
			_src.wpsDropdown = DeGUILayout.ToolbarFoldoutButton(_src.wpsDropdown, "Waypoints");
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(new GUIContent("Copy to clipboard", "Copies the current waypoints to clipboard, as an array ready to be pasted in your code"), DeGUI.styles.button.tool))
			{
				CopyWaypointsToClipboard();
			}
			else if (GUILayout.Button(new GUIContent("Paste from clipboard", "Pastes the current waypoints from the clipboard, if they're in the correct format"), DeGUI.styles.button.tool))
			{
				PasteWaypointsFromClipboard();
			}
			DeGUILayout.EndToolbar();
			if (_src.wpsDropdown)
			{
				DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
				bool changed = GUI.changed;
				_wpsList.DoLayoutList();
				if (!changed && GUI.changed)
				{
					flag = true;
				}
				DeGUILayout.EndVBox();
			}
			else
			{
				GUILayout.Space(5f);
			}
			if (flag)
			{
				RefreshPath(RepaintMode.Scene, refreshWpIndexByDepth: false);
			}
			else if (GUI.changed)
			{
				EditorUtility.SetDirty(_src);
				DORepaint(RepaintMode.Scene, refreshWpIndexByDepth: false);
			}
		}

		private void OnSceneGUI()
		{
			if (Application.isPlaying)
			{
				return;
			}
			StoreSceneCamData();
			if (!_src.gameObject.activeInHierarchy || !_sceneCamStored)
			{
				return;
			}
			if (_wpsByDepth.Count != _src.wps.Count)
			{
				FillWpIndexByDepth();
			}
			EditorGUIUtils.SetGUIStyles();
			Event current = Event.current;
			Undo.RecordObject(_src, "DOTween Path");
			if (current.type == EventType.MouseDown)
			{
				if (current.shift)
				{
					if (Event.current.keyCode == KeyCode.LeftControl || Event.current.keyCode == KeyCode.RightControl || Event.current.keyCode == KeyCode.LeftMeta || Event.current.keyCode == KeyCode.RightMeta || EditorGUI.actionKey)
					{
						Vector2 vector = current.mousePosition * DeEditorUtils.GetEditorUIScaling();
						Vector3 vector2 = ((_lastCreatedWpIndex != -1) ? _src.wps[_lastCreatedWpIndex] : ((_selectedWpIndex != -1) ? _src.wps[_selectedWpIndex] : ((_lastSelectedWpIndex != -1) ? _src.wps[_lastSelectedWpIndex] : _src.transform.position)));
						Matrix4x4 worldToCameraMatrix = _sceneCam.worldToCameraMatrix;
						float z = 0f - (worldToCameraMatrix.m20 * vector2.x + worldToCameraMatrix.m21 * vector2.y + worldToCameraMatrix.m22 * vector2.z + worldToCameraMatrix.m23);
						Vector3 item = _sceneCam.ViewportToWorldPoint(new Vector3(vector.x / _sceneCam.pixelRect.width, 1f - vector.y / _sceneCam.pixelRect.height, z));
						if (_selectedWpIndex != -1 && _selectedWpIndex < _src.wps.Count - 1)
						{
							_src.wps.Insert(_selectedWpIndex + 1, item);
							_lastCreatedWpIndex = _selectedWpIndex + 1;
							_selectedWpIndex = _lastCreatedWpIndex;
						}
						else
						{
							_src.wps.Add(item);
							_lastCreatedWpIndex = _src.wps.Count - 1;
							_selectedWpIndex = _lastCreatedWpIndex;
						}
						RefreshPath(RepaintMode.Scene, refreshWpIndexByDepth: true);
						return;
					}
					if (current.alt && _src.wps.Count > 1)
					{
						FindSelectedWaypointIndex();
						if (_selectedWpIndex != -1)
						{
							_src.wps.RemoveAt(_selectedWpIndex);
							ResetIndexes();
							RefreshPath(RepaintMode.Scene, refreshWpIndexByDepth: true);
							return;
						}
					}
				}
				FindSelectedWaypointIndex();
			}
			if (_src.wps.Count < 1)
			{
				return;
			}
			if (current.type == EventType.MouseDrag)
			{
				_isDragging = true;
				if (_src.livePreview)
				{
					bool flag = CheckTargetMoveOrRotate();
					if (_selectedWpIndex != -1)
					{
						flag = true;
					}
					if (flag)
					{
						RefreshPath(RepaintMode.Scene, refreshWpIndexByDepth: false);
					}
				}
			}
			else if (_isDragging && current.rawType == EventType.MouseUp)
			{
				if (_isDragging && _selectedWpIndex != -1)
				{
					_reselectAfterDrag = true;
				}
				_isDragging = false;
				if (_selectedWpIndex != -1 || CheckTargetMoveOrRotate())
				{
					EditorUtility.SetDirty(_src);
					RefreshPath(RepaintMode.Scene, refreshWpIndexByDepth: true);
				}
			}
			else if (CheckTargetMoveOrRotate())
			{
				RefreshPath(RepaintMode.Scene, refreshWpIndexByDepth: false);
			}
			if (_changed && !_isDragging)
			{
				FillWpIndexByDepth();
				_changed = false;
			}
			int count = _src.wps.Count;
			for (int num = 0; num < count; num++)
			{
				WpHandle wpHandle = _wpsByDepth[num];
				bool flag2 = wpHandle.wpIndex == _selectedWpIndex;
				Vector3 vector3 = _src.wps[wpHandle.wpIndex];
				float num2 = ((_src.handlesDrawMode == HandlesDrawMode.Orthographic) ? (HandleUtility.GetHandleSize(vector3) * 0.2f) : _src.perspectiveHandleSize);
				int num3;
				Vector3 obj;
				if (wpHandle.wpIndex >= 0)
				{
					num3 = ((wpHandle.wpIndex < (_src.isClosedPath ? count : (count - 1))) ? 1 : 0);
					if (num3 != 0)
					{
						obj = ((wpHandle.wpIndex >= count - 1) ? _src.transform.position : _src.wps[wpHandle.wpIndex + 1]);
						goto IL_04a1;
					}
				}
				else
				{
					num3 = 0;
				}
				obj = Vector3.zero;
				goto IL_04a1;
				IL_04a1:
				Vector3 vector4 = obj;
				bool flag3 = num3 != 0 && Vector3.Distance(_sceneCamTrans.position, vector3) < Vector3.Distance(_sceneCamTrans.position, vector3 + Vector3.ClampMagnitude(vector4 - vector3, num2 * 1.75f));
				if (flag2)
				{
					Handles.color = Color.yellow;
				}
				else if (wpHandle.wpIndex == count - 1 && !_src.isClosedPath)
				{
					Handles.color = _wpColorEnd;
				}
				else
				{
					Handles.color = _wpColor;
				}
				if (((uint)num3 & (flag3 ? 1u : 0u)) != 0)
				{
					DrawArrowFor(wpHandle.wpIndex, num2, vector4);
				}
				int controlID = GUIUtility.GetControlID(FocusType.Passive);
				if (num == 0)
				{
					_minHandleControlId = controlID;
				}
				vector3 = ((_src.handlesType != 0) ? Handles.PositionHandle(vector3, Quaternion.identity) : Handles.FreeMoveHandle(vector3, Quaternion.identity, num2, Vector3.one, Handles.SphereHandleCap));
				_src.wps[wpHandle.wpIndex] = vector3;
				int controlID2 = GUIUtility.GetControlID(FocusType.Passive);
				wpHandle.controlId = ((num == 0) ? (controlID2 - 1) : (controlID + 1));
				_maxHandleControlId = controlID2;
				if (num3 != 0 && !flag3)
				{
					DrawArrowFor(wpHandle.wpIndex, num2, vector4);
				}
				Vector3 position = _sceneCamTrans.InverseTransformPoint(vector3) + new Vector3(num2 * 0.75f, 0.1f, 0f);
				position = _sceneCamTrans.TransformPoint(position);
				if (_src.showIndexes || _src.showWpLength)
				{
					string text = ((_src.showIndexes && _src.showWpLength) ? string.Concat((wpHandle.wpIndex + 1).ToString(), "(", _src.path.wpLengths[wpHandle.wpIndex + 1].ToString("N2"), ")") : (_src.showIndexes ? (wpHandle.wpIndex + 1).ToString() : _src.path.wpLengths[wpHandle.wpIndex + 1].ToString("N2")));
					Handles.Label(position, text, flag2 ? EditorGUIUtils.handleSelectedLabelStyle : EditorGUIUtils.handlelabelStyle);
				}
			}
			Handles.color = _src.pathColor;
			if (_src.pathType == PathType.Linear)
			{
				Handles.DrawPolyLine(_src.path.wps);
			}
			else if (_src.path.nonLinearDrawWps != null)
			{
				Handles.DrawPolyLine(_src.path.nonLinearDrawWps);
			}
			if (_reselectAfterDrag && current.type == EventType.Repaint)
			{
				_reselectAfterDrag = false;
			}
			if (!_changed)
			{
				_changed = Changed();
			}
			if (_changed)
			{
				EditorUtility.SetDirty(_src);
			}
		}

		private void DORepaint(RepaintMode repaintMode, bool refreshWpIndexByDepth)
		{
			switch (repaintMode)
			{
			case RepaintMode.Scene:
				SceneView.RepaintAll();
				break;
			case RepaintMode.Inspector:
				EditorUtility.SetDirty(_src);
				break;
			case RepaintMode.SceneAndInspector:
				EditorUtility.SetDirty(_src);
				SceneView.RepaintAll();
				break;
			}
			if (refreshWpIndexByDepth)
			{
				FillWpIndexByDepth();
			}
		}

		private bool Changed()
		{
			if (GUI.changed)
			{
				return true;
			}
			if (_lastSelectedWpIndex != _selectedWpIndex)
			{
				_lastSelectedWpIndex = _selectedWpIndex;
				return true;
			}
			if (CheckTargetMoveOrRotate())
			{
				return true;
			}
			if (_sceneCamTrans.position != _lastSceneViewCamPosition || _sceneCamTrans.rotation != _lastSceneViewCamRotation)
			{
				_lastSceneViewCamPosition = _sceneCamTrans.position;
				_lastSceneViewCamRotation = _sceneCamTrans.rotation;
				return true;
			}
			return false;
		}

		private void DrawArrowFor(int wpIndex, float handleSize, Vector3 arrowPointsAt)
		{
			Color color = Handles.color;
			Handles.color = _arrowsColor;
			Vector3 vector = _src.wps[wpIndex];
			Vector3 vector2 = arrowPointsAt - vector;
			if (vector2.magnitude >= handleSize * 1.75f)
			{
				Handles.ConeHandleCap(wpIndex, vector + Vector3.ClampMagnitude(vector2, handleSize), Quaternion.LookRotation(vector2), handleSize * 0.65f, EventType.Repaint);
			}
			Handles.color = color;
		}

		private void DrawExtras()
		{
			AnimationInspectorGUI.StickyTitle("Extras");
			DeGUILayout.BeginVBox(DeGUI.styles.box.sticky);
			if (GUILayout.Button("Reset Path"))
			{
				ResetPath(RepaintMode.SceneAndInspector);
			}
			DeGUILayout.EndVBox();
			GUILayout.Space(2f);
			GUILayout.BeginHorizontal(DeGUI.styles.box.stickyTop);
			if (GUILayout.Button("Drop To Floor"))
			{
				DropToFloor(_src.dropToFloorOffset);
			}
			GUILayout.Space(7f);
			GUILayout.Label("Offset Y", GUILayout.Width(49f));
			_src.dropToFloorOffset = EditorGUILayout.FloatField(_src.dropToFloorOffset, GUILayout.Width(40f));
			GUILayout.EndHorizontal();
		}

		private void StoreSceneCamData()
		{
			if (_sceneCam == null)
			{
				_sceneCamStored = false;
			}
			else if (!_sceneCamStored && !(_sceneCam == null))
			{
				_sceneCamStored = true;
				_lastSceneViewCamPosition = _sceneCamTrans.position;
				_lastSceneViewCamRotation = _sceneCamTrans.rotation;
			}
		}

		private void FillWpIndexByDepth()
		{
			if (!_sceneCamStored)
			{
				return;
			}
			int count = _src.wps.Count;
			if (count == 0)
			{
				return;
			}
			_wpsByDepth.Clear();
			for (int i = 0; i < count; i++)
			{
				_wpsByDepth.Add(new WpHandle(i));
			}
			_wpsByDepth.Sort(delegate(WpHandle x, WpHandle y)
			{
				float num = Vector3.Distance(_sceneCamTrans.position, _src.wps[x.wpIndex]);
				float num2 = Vector3.Distance(_sceneCamTrans.position, _src.wps[y.wpIndex]);
				if (num > num2)
				{
					return -1;
				}
				return (num < num2) ? 1 : 0;
			});
		}

		private void FindSelectedWaypointIndex()
		{
			_lastSelectedWpIndex = _selectedWpIndex;
			_selectedWpIndex = -1;
			int count = _src.wps.Count;
			if (count == 0)
			{
				return;
			}
			int nearestControl = HandleUtility.nearestControl;
			if (nearestControl == 0 || nearestControl < _minHandleControlId || nearestControl > _maxHandleControlId)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < count; i++)
			{
				int controlId = _wpsByDepth[i].controlId;
				if (controlId != -1 && controlId != 0)
				{
					int wpIndex = _wpsByDepth[i].wpIndex;
					if (controlId > nearestControl)
					{
						_selectedWpIndex = _wpsByDepth[(num == -1) ? i : num].wpIndex;
						_lastCreatedWpIndex = -1;
						return;
					}
					if (controlId == nearestControl)
					{
						_selectedWpIndex = wpIndex;
						_lastCreatedWpIndex = -1;
						return;
					}
					num = i;
				}
			}
			if (_selectedWpIndex == -1)
			{
				_selectedWpIndex = _wpsByDepth[num].wpIndex;
				_lastCreatedWpIndex = -1;
			}
		}

		private void ResetPath(RepaintMode repaintMode)
		{
			_src.wps.Clear();
			_src.lastSrcPosition = _src.transform.position;
			_src.lastSrcRotation = _src.transform.rotation;
			_src.path = new Path(_src.pathType, _src.wps.ToArray(), 10, _src.pathColor);
			_wpsByDepth.Clear();
			ResetIndexes();
			DORepaint(repaintMode, refreshWpIndexByDepth: false);
		}

		private void ResetIndexes()
		{
			_selectedWpIndex = (_lastSelectedWpIndex = (_lastCreatedWpIndex = -1));
		}

		private bool CheckTargetMoveOrRotate()
		{
			bool result = false;
			if (_src.lastSrcPosition != _src.transform.position)
			{
				if (_src.relative)
				{
					Vector3 vector = _src.transform.position - _src.lastSrcPosition;
					int count = _src.wps.Count;
					for (int i = 0; i < count; i++)
					{
						_src.wps[i] = _src.wps[i] + vector;
					}
				}
				_src.lastSrcPosition = _src.transform.position;
				result = true;
			}
			if (_src.lastSrcRotation != _src.transform.rotation)
			{
				if (_src.relative)
				{
					Quaternion rotation = _src.transform.rotation * Quaternion.Inverse(_src.lastSrcRotation);
					int count2 = _src.wps.Count;
					for (int j = 0; j < count2; j++)
					{
						_src.wps[j] = Utils.RotateAroundPivot(_src.wps[j], _src.transform.position, rotation);
					}
				}
				_src.lastSrcRotation = _src.transform.rotation;
				result = true;
			}
			return result;
		}

		internal void RefreshPath(RepaintMode repaintMode, bool refreshWpIndexByDepth)
		{
			if (_src.wps.Count >= 1)
			{
				_src.path.AssignDecoder(_src.pathType);
				_src.path.AssignWaypoints(_src.GetFullWps());
				_src.path.FinalizePath(_src.isClosedPath, AxisConstraint.None, _src.transform.position);
				if (_src.pathType != 0)
				{
					Path.RefreshNonLinearDrawWps(_src.path);
				}
				DORepaint(repaintMode, refreshWpIndexByDepth);
			}
		}

		private void DropToFloor(float offsetY)
		{
			bool flag = false;
			for (int i = 0; i < _src.wps.Count; i++)
			{
				Vector3 origin = _src.wps[i];
				origin.y += 0.01f;
				if (Physics.Raycast(origin, Vector3.down, out var hitInfo, float.PositiveInfinity))
				{
					flag = true;
					Vector3 point = hitInfo.point;
					point.y += offsetY;
					_src.wps[i] = point;
					RefreshPath(RepaintMode.SceneAndInspector, refreshWpIndexByDepth: true);
				}
			}
			if (!flag)
			{
				EditorUtility.DisplayDialog("Drop To Floor", "No colliders to drop on.", "Ok");
			}
			else
			{
				EditorUtility.SetDirty(_src);
			}
		}

		private void CopyWaypointsToClipboard()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Vector3[] waypoints = new[] { ");
			for (int i = 0; i < _src.wps.Count; i++)
			{
				Vector3 vector = _src.wps[i];
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append($"new Vector3({vector.x.ToString(CultureInfo.InvariantCulture)}f,{vector.y.ToString(CultureInfo.InvariantCulture)}f,{vector.z.ToString(CultureInfo.InvariantCulture)}f)");
			}
			stringBuilder.Append(" };");
			EditorGUIUtility.systemCopyBuffer = stringBuilder.ToString();
		}

		private void PasteWaypointsFromClipboard()
		{
			string systemCopyBuffer = EditorGUIUtility.systemCopyBuffer;
			MatchCollection matchCollection = _CopyWpsFromClipboardRegex.Matches(systemCopyBuffer);
			if (matchCollection.Count != 0)
			{
				List<Vector3> list = new List<Vector3>();
				for (int i = 0; i < matchCollection.Count; i += 3)
				{
					list.Add(new Vector3(float.Parse(matchCollection[i].Groups[2].Value, CultureInfo.InvariantCulture), float.Parse(matchCollection[i + 1].Groups[2].Value, CultureInfo.InvariantCulture), float.Parse(matchCollection[i + 2].Groups[2].Value, CultureInfo.InvariantCulture)));
				}
				_src.wps = list;
				_wpsList.list = _src.wps;
				RefreshPath(RepaintMode.SceneAndInspector, refreshWpIndexByDepth: false);
			}
		}
	}
}
