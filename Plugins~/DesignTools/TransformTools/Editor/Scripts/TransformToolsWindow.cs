/*
Copyright (c) 2020 Omar Duarte
Unauthorized copying of this file, via any medium is strictly prohibited.
Writen by Omar Duarte, 2020.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using UnityEngine;
using UnityEditor;
using UnityEditor.ShortcutManagement;

namespace PluginMaster
{
    public class TransformToolsWindow : BaseToolWindow
    {
        #region MAIN
        private bool _alignGroupOpen = true;
        private bool _distributeGroupOpen = true;
        private bool _arrangeGroupOpen = true;
        private bool _randomizeGroupOpen = true;
        private bool _homogenizeGroupOpen = true;
        private bool _progressionGroupOpen = true;
        private bool _editPivotGroupOpen = true;
        private bool _miscellaneousGroupOpen = true;

        private Vector2 _scrollPosition = Vector2.zero;
        private GameObject _pivot = null;

        private GUIStyle _buttonStyle = null;

        [MenuItem("Tools/Plugin Master/Transform Tools/Transform Tools", false, 1001)]
        public static void ShowWindow() => GetWindow<TransformToolsWindow>();

        protected override void OnEnable()
        {
            base.OnEnable();
            _buttonStyle = _skin.GetStyle("ToolButton");
#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui += TransformTools.DuringSceneGUI;
#else
            SceneView.onSceneGUIDelegate += TransformTools.DuringSceneGUI;
#endif
            LoadAlignButtons();
            LoadDistributeButtons();
            LoadArrangeButtons();
            LoadProgressionButtons();
            LoadRandomizeButtons();
            LoadHomogenizeButtons();
            LoadEditPivotButtons();
            LoadMiscellaneusButtons();
        }

        private void OnDisable()
        {
#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui -= TransformTools.DuringSceneGUI;
#else
            SceneView.onSceneGUIDelegate -= TransformTools.DuringSceneGUI;
#endif
        }

        protected override void OnGUI()
        {
            base.OnGUI();

            minSize = new Vector2(position.height > 588 ? 136 : 148, 240);
            titleContent = new GUIContent("Transform Tools", null, "Transform Tools");
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, false, GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
            {
#if UNITY_2019_1_OR_NEWER
                _alignGroupOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_alignGroupOpen, "Align");
#else
                _alignGroupOpen = EditorGUILayout.Foldout(_alignGroupOpen, "Align");
#endif
                if (_alignGroupOpen)
                {
                    OnGuiAlignGroup();
                }
#if UNITY_2019_1_OR_NEWER
                EditorGUILayout.EndFoldoutHeaderGroup();
#endif
#if UNITY_2019_1_OR_NEWER
                _distributeGroupOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_distributeGroupOpen, "Distribute");
#else
                _distributeGroupOpen = EditorGUILayout.Foldout(_distributeGroupOpen, "Distribute");
#endif
                if (_distributeGroupOpen)
                {
                    OnGuiDistributeGroup();
                }
#if UNITY_2019_1_OR_NEWER
                EditorGUILayout.EndFoldoutHeaderGroup();
#endif

#if UNITY_2019_1_OR_NEWER
                _arrangeGroupOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_arrangeGroupOpen, "Arrange");
#else
                _arrangeGroupOpen = EditorGUILayout.Foldout(_arrangeGroupOpen, "Arrange");
#endif
                if (_arrangeGroupOpen)
                {
                    OnGuiArrangeGroup();
                }
#if UNITY_2019_1_OR_NEWER
                EditorGUILayout.EndFoldoutHeaderGroup();
#endif
#if UNITY_2019_1_OR_NEWER
                _progressionGroupOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_progressionGroupOpen, "Progression");
#else
                _progressionGroupOpen = EditorGUILayout.Foldout(_progressionGroupOpen, "Progression");
#endif
                if (_progressionGroupOpen)
                {
                    OnGuiProgressionGroup();
                }
#if UNITY_2019_1_OR_NEWER
                EditorGUILayout.EndFoldoutHeaderGroup();
#endif
#if UNITY_2019_1_OR_NEWER
                _randomizeGroupOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_randomizeGroupOpen, "Randomize");
#else
                _randomizeGroupOpen = EditorGUILayout.Foldout(_randomizeGroupOpen, "Randomize");
#endif
                if (_randomizeGroupOpen)
                {
                    OnGuiRandomizeGroup();
                }
#if UNITY_2019_1_OR_NEWER
                EditorGUILayout.EndFoldoutHeaderGroup();
#endif
#if UNITY_2019_1_OR_NEWER
                _homogenizeGroupOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_homogenizeGroupOpen, "Homogenize");
#else
                _homogenizeGroupOpen = EditorGUILayout.Foldout(_homogenizeGroupOpen, "Homogenize");
#endif
                if (_homogenizeGroupOpen)
                {
                    OnGuiHomogenizeGroup();
                }
#if UNITY_2019_1_OR_NEWER
                EditorGUILayout.EndFoldoutHeaderGroup();
#endif
#if UNITY_2019_1_OR_NEWER
                _editPivotGroupOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_editPivotGroupOpen, "Edit Pivot");
#else
                _editPivotGroupOpen = EditorGUILayout.Foldout(_editPivotGroupOpen, "Edit Pivot");
#endif
                if (_editPivotGroupOpen)
                {
                    OnGuiEditPivotGroup();
                }
#if UNITY_2019_1_OR_NEWER
                EditorGUILayout.EndFoldoutHeaderGroup();
#endif
#if UNITY_2019_1_OR_NEWER
                _miscellaneousGroupOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_miscellaneousGroupOpen, "Miscellaneous");
#else
                _miscellaneousGroupOpen = EditorGUILayout.Foldout(_miscellaneousGroupOpen, "Miscellaneous");
#endif
                if (_miscellaneousGroupOpen)
                {
                    OnGuiPlaceOnSurfaceGroup();
                }
#if UNITY_2019_1_OR_NEWER
                EditorGUILayout.EndFoldoutHeaderGroup();
#endif
            }
            EditorGUILayout.EndScrollView();
        }

        private void OnDestroy()
        {
            if (_pivot == null) return;
            Selection.activeGameObject = _pivot.transform.parent.gameObject;
            DestroyImmediate(_pivot);
        }

        protected override void OnSelectionChange()
        {
            base.OnSelectionChange();
            if (_pivot == null) return;
            if (Selection.activeObject != _pivot)
            {
                CancelEditPivot(false);
            }
        }
#if UNITY_2019_1_OR_NEWER
        private static void UpdateShortcut(GUIContent button, string tooltip, string shortcut) => button.tooltip = shortcut == string.Empty ? tooltip : tooltip + " ... " + shortcut;
#endif
        #endregion

        #region ALIGN
        private readonly string[] _relativeToPopupOptions = new string[]
        {
            "Last Selected",
            "First Selected",
            "Biggest Object",
            "Smallest Object",
            "Selection",
            "Canvas"
        };

        private readonly string[] _alingObjPropOptions = new string[]
        {
            "Bounding Box",
            "Center",
            "Pivot",
        };

        private TransformTools.RelativeTo _relativeTo = TransformTools.RelativeTo.LAST_SELECTED;
        private bool _filteredByTopLevel = true;
        private BoundsUtils.ObjectProperty _alignObjectProperty = BoundsUtils.ObjectProperty.BOUNDING_BOX;

        private GUIContent _alignRightToAnchorLeftButton = null;
        private GUIContent _alignLeftButton = null;
        private GUIContent _alignCenterXButton = null;
        private GUIContent _alignRightButton = null;
        private GUIContent _alignLeftToAnchorRightButton = null;

        private GUIContent _alignTopToAnchorBottomButton = null;
        private GUIContent _alignBottomButton = null;
        private GUIContent _alignCenterYButton = null;
        private GUIContent _alignTopButton = null;
        private GUIContent _alignBottomToAnchorTopButton = null;

        private GUIContent _alignFrontToAnchorBackButton = null;
        private GUIContent _alignBackButton = null;
        private GUIContent _alignCenterZButton = null;
        private GUIContent _alignFrontButton = null;
        private GUIContent _alignBackToAnchorFrontButton = null;

        private void LoadAlignButtons()
        {
            _alignRightToAnchorLeftButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignRightToAnchorLeft"), "Align right edges of objects to the left edge of the anchor");
            _alignLeftButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignLeft"), "Align left edges");
            _alignCenterXButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignCenterX"), "Center on X axis");
            _alignRightButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignRight"), "Align right edges");
            _alignLeftToAnchorRightButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignLeftToAnchorRight"), "Align left edges of objects to the right edge of the anchor");

            _alignTopToAnchorBottomButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignTopToAnchorBottom"), "Align top edges of objects to the bottom edge of the anchor");
            _alignBottomButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignBottom"), "Align bottom edges");
            _alignCenterYButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignCenterY"), "Center on Y axis");
            _alignTopButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignTop"), "Align top edges");
            _alignBottomToAnchorTopButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignBottomToAnchorTop"), "Align bottom edges of objects to the top edge of the anchor");

            _alignFrontToAnchorBackButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignFrontToAnchorBack"), "Align front edges of objects to the back edge of the anchor");
            _alignBackButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignBack"), "Align back edges");
            _alignCenterZButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignCenterZ"), "Center on Z axis");
            _alignFrontButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignFront"), "Align front edges");
            _alignBackToAnchorFrontButton = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignBackToAnchorFront"), "Align back edges of objects to the front edge of the anchor");
        }

        private void OnGuiAlignGroup()
        {
            using (new GUILayout.VerticalScope(_skin.box))
            {
                GUILayout.Label("Relative to:", _skin.label, GUILayout.Height(18));
                _relativeTo = (TransformTools.RelativeTo)EditorGUILayout.Popup((int)_relativeTo, _relativeToPopupOptions, GUILayout.Width(128));

                GUILayout.Label("Align property:", _skin.label, GUILayout.Width(88), GUILayout.Height(18));
                _alignObjectProperty = (BoundsUtils.ObjectProperty)EditorGUILayout.Popup((int)_alignObjectProperty, _alingObjPropOptions, GUILayout.Width(128));

                using (new GUILayout.HorizontalScope())
                {
                    _filteredByTopLevel = EditorGUILayout.Toggle(_filteredByTopLevel, GUILayout.Width(14));
                    GUILayout.Label("Topmost filter", _skin.label, GUILayout.Height(18));
                    GUILayout.FlexibleSpace();
                }
                GUILayout.Space(4);
                ///// X
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_alignRightToAnchorLeftButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.X, TransformTools.Bound.MAX, true, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignLeftButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.X, TransformTools.Bound.MIN, false, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignCenterXButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.X, TransformTools.Bound.CENTER, false, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignRightButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.X, TransformTools.Bound.MAX, false, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignLeftToAnchorRightButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.X, TransformTools.Bound.MIN, true, _filteredByTopLevel, _alignObjectProperty);
                    GUILayout.FlexibleSpace();
                }
                GUILayout.Space(4);
                ///// Y
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_alignTopToAnchorBottomButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Y, TransformTools.Bound.MAX, true, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignBottomButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Y, TransformTools.Bound.MIN, false, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignCenterYButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Y, TransformTools.Bound.CENTER, false, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignTopButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Y, TransformTools.Bound.MAX, false, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignBottomToAnchorTopButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Y, TransformTools.Bound.MIN, true, _filteredByTopLevel, _alignObjectProperty);
                    GUILayout.FlexibleSpace();
                }
                GUILayout.Space(4);
                ///// Z
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_alignFrontToAnchorBackButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Z, TransformTools.Bound.MAX, true, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignBackButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Z, TransformTools.Bound.MIN, false, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignCenterZButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Z, TransformTools.Bound.CENTER, false, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignFrontButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Z, TransformTools.Bound.MAX, false, _filteredByTopLevel, _alignObjectProperty);
                    if (GUILayout.Button(_alignBackToAnchorFrontButton, _buttonStyle))
                        TransformTools.Align(GetSelection(_filteredByTopLevel).ToArray(), _relativeTo, TransformTools.Axis.Z, TransformTools.Bound.MIN, true, _filteredByTopLevel, _alignObjectProperty);
                    GUILayout.FlexibleSpace();
                }
            }
        }
        #endregion

        #region DISTRIBUTE
        private GUIContent _distributeLeftButton = null;
        private GUIContent _distributeCenterXButton = null;
        private GUIContent _distributeRightButton = null;
        private GUIContent _distributeGapXButton = null;

        private GUIContent _distributeBottomButton = null;
        private GUIContent _distributeCenterYButton = null;
        private GUIContent _distributeTopButton = null;
        private GUIContent _distributeGapYButton = null;

        private GUIContent _distributeBackButton = null;
        private GUIContent _distributeCenterZButton = null;
        private GUIContent _distributeFrontButton = null;
        private GUIContent _distributeGapZButton = null;

        private void LoadDistributeButtons()
        {
            _distributeLeftButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeLeft"), "Distribute left edges equidistantly");
            _distributeCenterXButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeCenterX"), "Distribute centers equidistantly on the X axis");
            _distributeRightButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeRight"), "Distribute right edges equidistantly");
            _distributeGapXButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeGapX"), "Make equal gaps between objects on the X axis");

            _distributeBottomButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeBottom"), "Distribute bottom edges equidistantly");
            _distributeCenterYButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeCenterY"), "Distribute centers equidistantly on the Y axis");
            _distributeTopButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeTop"), "Distribute top edges equidistantly");
            _distributeGapYButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeGapY"), "Make equal gaps between objects on the Y axis");

            _distributeBackButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeBack"), "Distribute back edges equidistantly");
            _distributeCenterZButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeCenterZ"), "Distribute centers equidistantly on the Z axis");
            _distributeFrontButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeFront"), "Distribute front edges equidistantly");
            _distributeGapZButton = new GUIContent(Resources.Load<Texture2D>("Sprites/DistributeGapZ"), "Make equal gaps between objects on the Z axis");
        }

        private void OnGuiDistributeGroup()
        {
            using (new GUILayout.VerticalScope(_skin.box))
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_distributeLeftButton, _buttonStyle)) TransformTools.Distribute(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.X, TransformTools.Bound.MIN);
                    if (GUILayout.Button(_distributeCenterXButton, _buttonStyle)) TransformTools.Distribute(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.X, TransformTools.Bound.CENTER);
                    if (GUILayout.Button(_distributeRightButton, _buttonStyle)) TransformTools.Distribute(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.X, TransformTools.Bound.MAX);
                    if (GUILayout.Button(_distributeGapXButton, _buttonStyle)) TransformTools.DistributeGaps(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.X);
                }
                GUILayout.Space(4);
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_distributeBottomButton, _buttonStyle)) TransformTools.Distribute(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.Y, TransformTools.Bound.MIN);
                    if (GUILayout.Button(_distributeCenterYButton, _buttonStyle)) TransformTools.Distribute(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.Y, TransformTools.Bound.CENTER);
                    if (GUILayout.Button(_distributeTopButton, _buttonStyle)) TransformTools.Distribute(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.Y, TransformTools.Bound.MAX);
                    if (GUILayout.Button(_distributeGapYButton, _buttonStyle)) TransformTools.DistributeGaps(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.Y);
                }
                GUILayout.Space(4);
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_distributeBackButton, _buttonStyle)) TransformTools.Distribute(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.Z, TransformTools.Bound.MIN);
                    if (GUILayout.Button(_distributeCenterZButton, _buttonStyle)) TransformTools.Distribute(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.Z, TransformTools.Bound.CENTER);
                    if (GUILayout.Button(_distributeFrontButton, _buttonStyle)) TransformTools.Distribute(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.Z, TransformTools.Bound.MAX);
                    if (GUILayout.Button(_distributeGapZButton, _buttonStyle)) TransformTools.DistributeGaps(_selectionOrderedTopLevel.ToArray(), TransformTools.Axis.Z);
                }
            }
        }
        #endregion

        #region ARRANGE
        private GUIContent _gridArrangeButton = null;
        private GUIContent _radialArrangeButton = null;
        private GUIContent _rearrangeSelectionButton = null;
        private GUIContent _rearrangeHierarchyButton = null;

        private const string GRID_ARRANGEMENT_TOOLTIP = "Grid Arrangement";
        private const string RADIAL_ARRANGEMENT_TOOLTIP = "Radial Arrangement";
        private const string REARRENGE_SELECTION_TOOLTIP = "Exchange positions - Selection Order";
        private const string REARRANGE_HIERARCHY_TOOLTIP = "Exchange positions - Hierarchy Order";

        private void LoadArrangeButtons()
        {
            _gridArrangeButton = new GUIContent(Resources.Load<Texture2D>("Sprites/GridArrange"), GRID_ARRANGEMENT_TOOLTIP);
            _radialArrangeButton = new GUIContent(Resources.Load<Texture2D>("Sprites/RadialArrange"), RADIAL_ARRANGEMENT_TOOLTIP);
            _rearrangeSelectionButton = new GUIContent(Resources.Load<Texture2D>("Sprites/RearrangeSelectionOrder"), REARRENGE_SELECTION_TOOLTIP);
            _rearrangeHierarchyButton = new GUIContent(Resources.Load<Texture2D>("Sprites/RearrangeHierarchyOrder"), REARRANGE_HIERARCHY_TOOLTIP);
        }

#if UNITY_2019_1_OR_NEWER
        public const string REARRANGE_SELECTION_SHORTCUT_ID = "Transform Tools/Rearrange - Selection Order";
        public static string rearrangeSelectionShortcut => ShortcutManager.instance.GetShortcutBinding(REARRANGE_SELECTION_SHORTCUT_ID).ToString();
        [Shortcut(REARRANGE_SELECTION_SHORTCUT_ID)]
        private static void RearrangeSelection()
        {
            var selection = Selection.GetFiltered<GameObject>(SelectionMode.Editable | SelectionMode.ExcludePrefab | SelectionMode.TopLevel);
            TransformTools.Rearrange(selection, TransformTools.ArrangeBy.SELECTION_ORDER);
        }

        public const string REARRANGE_HIERARCHY_SHORTCUT_ID = "Transform Tools/Rearrange - Hierarchy Order";
        public static string rearrangeHierarchyShortcut => ShortcutManager.instance.GetShortcutBinding(REARRANGE_HIERARCHY_SHORTCUT_ID).ToString();
        [Shortcut(REARRANGE_HIERARCHY_SHORTCUT_ID)]
        private static void RearrangeHierarchy()
        {
            var selection = Selection.GetFiltered<GameObject>(SelectionMode.Editable | SelectionMode.ExcludePrefab | SelectionMode.TopLevel);
            TransformTools.Rearrange(selection, TransformTools.ArrangeBy.HIERARCHY_ORDER);
        }
        private void UpdateArrangeTooltips()
        {
            UpdateShortcut(_gridArrangeButton, GRID_ARRANGEMENT_TOOLTIP, GridArrangementToolWindow.shortcut);
            UpdateShortcut(_radialArrangeButton, RADIAL_ARRANGEMENT_TOOLTIP, RadialArrangeToolWindow.shortcut);
            UpdateShortcut(_rearrangeSelectionButton, REARRENGE_SELECTION_TOOLTIP, rearrangeSelectionShortcut);
            UpdateShortcut(_rearrangeHierarchyButton, REARRANGE_HIERARCHY_TOOLTIP, rearrangeHierarchyShortcut);
        }
#endif

        private void OnGuiArrangeGroup()
        {
#if UNITY_2019_1_OR_NEWER
            UpdateArrangeTooltips();
#endif
            using (new GUILayout.VerticalScope(_skin.box))
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_gridArrangeButton, _buttonStyle)) GridArrangementToolWindow.ShowWindow();
                    if (GUILayout.Button(_radialArrangeButton, _buttonStyle)) RadialArrangeToolWindow.ShowWindow();
                    if (GUILayout.Button(_rearrangeSelectionButton, _buttonStyle))
                        TransformTools.Rearrange(_selectionOrderedTopLevel.ToArray(), TransformTools.ArrangeBy.SELECTION_ORDER);
                    if (GUILayout.Button(_rearrangeHierarchyButton, _buttonStyle))
                        TransformTools.Rearrange(_selectionOrderedTopLevel.ToArray(), TransformTools.ArrangeBy.HIERARCHY_ORDER);
                    GUILayout.FlexibleSpace();
                }
            }
        }
        #endregion

        #region PROGRESSION
        private GUIContent _positionProgressionButton = null;
        private GUIContent _rotationProgressionButton = null;
        private GUIContent _scaleProgressionButton = null;

        private const string POSITION_PROGRESION_TOOLTIP = "Place objects incrementally";
        private const string ROTATION_PROGRESION_TOOLTIP = "Rotate objects incrementally";
        private const string SCALE_PROGRESION_TOOLTIP = "Scale objects incrementally";

        private void LoadProgressionButtons()
        {
            _positionProgressionButton = new GUIContent(Resources.Load<Texture2D>("Sprites/IncrementalPosition"), POSITION_PROGRESION_TOOLTIP);
            _rotationProgressionButton = new GUIContent(Resources.Load<Texture2D>("Sprites/IncrementalRotation"), ROTATION_PROGRESION_TOOLTIP);
            _scaleProgressionButton = new GUIContent(Resources.Load<Texture2D>("Sprites/IncrementalScale"), SCALE_PROGRESION_TOOLTIP);
        }

#if UNITY_2019_1_OR_NEWER
        private void UpdateProgressionTooltips()
        {
            UpdateShortcut(_positionProgressionButton, POSITION_PROGRESION_TOOLTIP, PositionProgressionWindow.shortcut);
            UpdateShortcut(_rotationProgressionButton, ROTATION_PROGRESION_TOOLTIP, RotationProgressionWindow.shortcut);
            UpdateShortcut(_scaleProgressionButton, SCALE_PROGRESION_TOOLTIP, ScaleProgressionWindow.shortcut);
        }
#endif

        private void OnGuiProgressionGroup()
        {
#if UNITY_2019_1_OR_NEWER
            UpdateProgressionTooltips();
#endif
            using (new GUILayout.VerticalScope(_skin.box))
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_positionProgressionButton, _buttonStyle)) PositionProgressionWindow.ShowWindow();
                    if (GUILayout.Button(_rotationProgressionButton, _buttonStyle)) RotationProgressionWindow.ShowWindow();
                    if (GUILayout.Button(_scaleProgressionButton, _buttonStyle)) ScaleProgressionWindow.ShowWindow();
                    GUILayout.FlexibleSpace();
                }
            }
        }
        #endregion

        #region RANDOMIZE
        private GUIContent _randomizePositionsButton = null;
        private GUIContent _randomizeRotationsButton = null;
        private GUIContent _randomizeScalesButton = null;

        private const string RANDOMIZE_POSITIONS_TOOLTIP = "Randomize Positions";
        private const string RANDOMIZE_ROTATIONS_TOOLTIP = "Randomize Rotations";
        private const string RANDOMIZE_SCALES_TOOLTIP = "Randomize Scales";

        private void LoadRandomizeButtons()
        {
            _randomizePositionsButton = new GUIContent(Resources.Load<Texture2D>("Sprites/RandomizePosition"), RANDOMIZE_POSITIONS_TOOLTIP);
            _randomizeRotationsButton = new GUIContent(Resources.Load<Texture2D>("Sprites/RandomizeRotation"), RANDOMIZE_ROTATIONS_TOOLTIP);
            _randomizeScalesButton = new GUIContent(Resources.Load<Texture2D>("Sprites/RandomizeScale"), RANDOMIZE_SCALES_TOOLTIP);
        }

#if UNITY_2019_1_OR_NEWER
        private void UpdateRandomizeTooltips()
        {
            UpdateShortcut(_randomizePositionsButton, RANDOMIZE_POSITIONS_TOOLTIP, RandomizePositionsWindow.shortcut);
            UpdateShortcut(_randomizeRotationsButton, RANDOMIZE_ROTATIONS_TOOLTIP, RandomizeRotationsWindow.shortcut);
            UpdateShortcut(_randomizeScalesButton, RANDOMIZE_SCALES_TOOLTIP, RandomizeScalesWindow.shortcut);
        }
#endif

        private void OnGuiRandomizeGroup()
        {
#if UNITY_2019_1_OR_NEWER
            UpdateRandomizeTooltips();
#endif
            using (new GUILayout.VerticalScope(_skin.box))
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_randomizePositionsButton, _buttonStyle)) RandomizePositionsWindow.ShowWindow();
                    if (GUILayout.Button(_randomizeRotationsButton, _buttonStyle)) RandomizeRotationsWindow.ShowWindow();
                    if (GUILayout.Button(_randomizeScalesButton, _buttonStyle)) RandomizeScalesWindow.ShowWindow();
                    GUILayout.FlexibleSpace();
                }
            }
        }
        #endregion

        #region HOMOGENIZE
        private GUIContent _homogenizeSpacingButton = null;
        private GUIContent _homogenizeRotationButton = null;
        private GUIContent _homogenizeScaleButton = null;

        private const string HOMOGENIZE_SPACING_TOOLTIP = "Homogenize Spacing";
        private const string HOMOGENIZE_ROTATION_TOOLTIP = "Homogenize Rotation";
        private const string HOMOGENIZE_SCALE_TOOLTIP = "Homogenize Scale";

        private void LoadHomogenizeButtons()
        {
            _homogenizeSpacingButton = new GUIContent(Resources.Load<Texture2D>("Sprites/HomogenizeSpacing"), HOMOGENIZE_SPACING_TOOLTIP);
            _homogenizeRotationButton = new GUIContent(Resources.Load<Texture2D>("Sprites/HomogenizeRotation"), HOMOGENIZE_ROTATION_TOOLTIP);
            _homogenizeScaleButton = new GUIContent(Resources.Load<Texture2D>("Sprites/HomogenizeScale"), HOMOGENIZE_SCALE_TOOLTIP);
        }

#if UNITY_2019_1_OR_NEWER
        private void UpdateHomogenizeTooltips()
        {
            UpdateShortcut(_homogenizeSpacingButton, HOMOGENIZE_SPACING_TOOLTIP, HomogenizeSpacingWindow.shortcut);
            UpdateShortcut(_homogenizeRotationButton, HOMOGENIZE_ROTATION_TOOLTIP, HomogenizeRotationWindow.shortcut);
            UpdateShortcut(_homogenizeScaleButton, HOMOGENIZE_SCALE_TOOLTIP, HomogenizeScaleWindow.shortcut);
        }
#endif

        private void OnGuiHomogenizeGroup()
        {
#if UNITY_2019_1_OR_NEWER
            UpdateHomogenizeTooltips();
#endif
            using (new GUILayout.VerticalScope(_skin.box))
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_homogenizeSpacingButton, _buttonStyle)) HomogenizeSpacingWindow.ShowWindow();
                    if (GUILayout.Button(_homogenizeRotationButton, _buttonStyle)) HomogenizeRotationWindow.ShowWindow();
                    if (GUILayout.Button(_homogenizeScaleButton, _buttonStyle)) HomogenizeScaleWindow.ShowWindow();
                    GUILayout.FlexibleSpace();
                }
            }
        }
        #endregion

        #region EDIT PIVOT
        private GUIContent _centerPivotButton = null;
        private GUIContent _editPivotButton = null;
        private GUIContent _applyPivotButton = null;
        private GUIContent _cancelPivotButton = null;

        private const string CENTER_PIVOT_TOOLTIP = "Center Pivot";

        private void LoadEditPivotButtons()
        {
            _centerPivotButton = new GUIContent(Resources.Load<Texture2D>("Sprites/CenterPivot"), CENTER_PIVOT_TOOLTIP);
            _editPivotButton = new GUIContent(Resources.Load<Texture2D>("Sprites/EditPivot"), "Edit pivot position and rotation");
            _applyPivotButton = new GUIContent(Resources.Load<Texture2D>("Sprites/Apply"), "Apply");
            _cancelPivotButton = new GUIContent(Resources.Load<Texture2D>("Sprites/Cancel"), "Cancel");
        }

#if UNITY_2019_1_OR_NEWER
        public const string CENTER_PIVOT_SHORTCUT_ID = "Transform Tools/Center Pivot";
        public static string centerPivotShortcut => ShortcutManager.instance.GetShortcutBinding(CENTER_PIVOT_SHORTCUT_ID).ToString();
        [Shortcut(CENTER_PIVOT_SHORTCUT_ID)]
#endif
        private static void CenterPivot()
        {
            var obj = Selection.activeGameObject;
            if (obj == null) return;
            var meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter == null) TransformTools.CenterPivot(obj.transform);
            else if (MeshChangeWarning(obj))
            {
                if (meshFilter.sharedMesh != null)
                {
                    string savePath = EditorUtility.SaveFilePanelInProject("Save As", meshFilter.sharedMesh.name, "asset", string.Empty);
                    if (!string.IsNullOrEmpty(savePath)) TransformTools.CenterPivot(obj.transform, savePath);
                }
            }
        }

        private void OnGuiEditPivotGroup()
        {
#if UNITY_2019_1_OR_NEWER
            UpdateShortcut(_centerPivotButton, CENTER_PIVOT_TOOLTIP, centerPivotShortcut);
#endif
            using (new GUILayout.VerticalScope(_skin.box))
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_centerPivotButton, _buttonStyle)) CenterPivot();
                    if (GUILayout.Button(_editPivotButton, _buttonStyle)) _pivot = TransformTools.StartEditingPivot(Selection.activeGameObject);
                    if (_pivot != null)
                    {
                        if (GUILayout.Button(_applyPivotButton, _buttonStyle))
                        {
                            var meshFilter = _pivot.transform.parent.GetComponent<MeshFilter>();
                            if (meshFilter != null && meshFilter.sharedMesh != null)
                            {
                                if (MeshChangeWarning(_pivot.transform.parent.gameObject))
                                {
                                    string savePath = EditorUtility.SaveFilePanelInProject("Save As", meshFilter.sharedMesh.name, "asset", string.Empty);
                                    if (!string.IsNullOrEmpty(savePath)) TransformTools.SaveMesh(meshFilter, savePath, _pivot.transform);
                                }
                            }
                            else TransformTools.ApplyPivot(_pivot.transform);
                            CancelEditPivot(true);
                        }
                        if (GUILayout.Button(_cancelPivotButton, _buttonStyle)) CancelEditPivot(true);
                    }
                    GUILayout.FlexibleSpace();
                }
            }
        }

        private static bool MeshChangeWarning(GameObject target)
        {
            if (target == null) return false;
            var meshFilter = target.GetComponent<MeshFilter>();

            return EditorUtility.DisplayDialog(
                "Warning: Mesh will be modified",
                "Changing the pivot will modify the mesh.\n" +
                "Would you like to continue and save the mesh as new Asset?",
                "Continue", "Cancel");
        }

        private void CancelEditPivot(bool selectTarget)
        {
            if (_pivot == null) return;
            if (selectTarget) Selection.activeObject = _pivot.transform.parent.gameObject;
            DestroyImmediate(_pivot);
            _pivot = null;
            Repaint();
        }
        #endregion

        #region MISCELLANEUS
        private GUIContent _placeOnSurfaceButton = null;
        private GUIContent _simulateGravityButton = null;
        private GUIContent _unoverlapButton = null;

        private const string PLACE_ON_SURFACE_TOOLTIP = "Place on the surface";
        private const string SIMULATE_GRAVITY_TOOLTIP = "Simulate Gravity";
        private const string UNOVERLAP_TOOLTIP = "Move objects so that their bounding boxes don't overlap";

        private void LoadMiscellaneusButtons()
        {
            _placeOnSurfaceButton = new GUIContent(Resources.Load<Texture2D>("Sprites/PlaceOnSurface"), PLACE_ON_SURFACE_TOOLTIP);
            _simulateGravityButton = new GUIContent(Resources.Load<Texture2D>("Sprites/SimulateGravity"), SIMULATE_GRAVITY_TOOLTIP);
            _unoverlapButton = new GUIContent(Resources.Load<Texture2D>("Sprites/Unoverlap"), UNOVERLAP_TOOLTIP);
        }

#if UNITY_2019_1_OR_NEWER
        private void UpdateMiscellaneusTooltips()
        {
            UpdateShortcut(_placeOnSurfaceButton, PLACE_ON_SURFACE_TOOLTIP, PlaceOnSurfaceWindow.shortcut);
            UpdateShortcut(_simulateGravityButton, SIMULATE_GRAVITY_TOOLTIP, SimulateGravityWindow.shortcut);
            UpdateShortcut(_unoverlapButton, UNOVERLAP_TOOLTIP, UnoverlapToolWindow.shortcut);
        }
#endif

        private void OnGuiPlaceOnSurfaceGroup()
        {
#if UNITY_2019_1_OR_NEWER
            UpdateMiscellaneusTooltips();
#endif
            using (new GUILayout.VerticalScope(_skin.box))
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(_placeOnSurfaceButton, _buttonStyle)) PlaceOnSurfaceWindow.ShowWindow();
                    if (GUILayout.Button(_simulateGravityButton, _buttonStyle)) SimulateGravityWindow.ShowWindow();
                    if (GUILayout.Button(_unoverlapButton, _buttonStyle)) UnoverlapToolWindow.ShowWindow();
                    GUILayout.FlexibleSpace();
                }
            }
        }
        #endregion
    }
}
