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
using System.Collections.Generic;

namespace PluginMaster
{
    public class GridArrangementToolWindow : BaseToolWindow
    {
        #region WINDOW
        private TransformTools.ArrangeData _data = new TransformTools.ArrangeData();
        private GUIStyle _buttonStyle = null;
        
        private GUIContent _warningIcon = null;
        private readonly Dictionary<TransformTools.Axis, GUIContent[]> _alignBtnDict = new Dictionary<TransformTools.Axis, GUIContent[]>
        {
            {TransformTools.Axis.X,  new GUIContent[3]},
            {TransformTools.Axis.Y,  new GUIContent[3]},
            {TransformTools.Axis.Z,  new GUIContent[3]}
        };

        private readonly string[] _priorityOptions = new string[] { "1", "2", "3" };
        private readonly string[] _directionOptionsX = new string[] { "+X", "-X" };
        private readonly string[] _directionOptionsY = new string[] { "+Y", "-Y" };
        private readonly string[] _directionOptionsZ = new string[] { "+Z", "-Z" };
        private readonly string[] _widthOptions = new string[] { "Widest object per column", "Widest object selected", "Custom" };
        private readonly string[] _heightOptions = new string[] { "Tallest object per column", "Tallest object selected", "Custom" };
        private readonly string[] _sortByOptions = new string[] { "Selection Order", "Current Position", "Hierarchy Order" };
        private readonly string[] _arrangeRelativeToOptions = new string[] { "Selection Bounds", "First Object" };
        private readonly string[] _alingObjPropOptions = new string[] { "Bounding Box", "Pivot" };

        [MenuItem("Tools/Plugin Master/Transform Tools/Grid Arrangement", false, 1200)]
        public static void ShowWindow() => GetWindow<GridArrangementToolWindow>();

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Grid Arrangement";
        [Shortcut(SHORTCUT_ID)]
        public static void GridArrange() => GetWindow<GridArrangementToolWindow>().Arrange();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
            _buttonStyle = _skin.GetStyle("AlignmentToggle");
            _alignBtnDict[TransformTools.Axis.X][0] = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignmentLeft"), "Align Left");
            _alignBtnDict[TransformTools.Axis.X][1] = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignmentCenterX"), "Center on X");
            _alignBtnDict[TransformTools.Axis.X][2] = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignmentRight"), "Align Right");
            _alignBtnDict[TransformTools.Axis.Y][0] = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignmentBottom"), "Align Bottom");
            _alignBtnDict[TransformTools.Axis.Y][1] = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignmentCenterY"), "Center on Y");
            _alignBtnDict[TransformTools.Axis.Y][2] = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignmentTop"), "Align Top");
            _alignBtnDict[TransformTools.Axis.Z][0] = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignmentBack"), "Align Back");
            _alignBtnDict[TransformTools.Axis.Z][1] = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignmentCenterZ"), "Center on Z");
            _alignBtnDict[TransformTools.Axis.Z][2] = new GUIContent(Resources.Load<Texture2D>("Sprites/AlignmentFront"), "Align Front");
            _warningIcon = new GUIContent(Resources.Load<Texture2D>("Sprites/Warning"));

#if UNITY_2019_1_OR_NEWER
            maxSize = minSize = new Vector2(536, 296);
#else
            maxSize = minSize = new Vector2(552, 296);
#endif
            titleContent = new GUIContent("Grid Arrangement");
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new GUILayout.HorizontalScope())
                {
                    EditorGUIUtility.labelWidth = 125;
                    EditorGUIUtility.fieldWidth = 110;
                    _data.sortBy = (TransformTools.SortBy)EditorGUILayout.Popup("Arrange according to:", (int)_data.sortBy, _sortByOptions);
                    GUILayout.FlexibleSpace();
                    using (var check = new EditorGUI.ChangeCheckScope())
                    {
                        EditorGUIUtility.labelWidth = 85;
                        EditorGUIUtility.fieldWidth = 120;
                        _data.alignProperty = EditorGUILayout.Popup("Align property:", 
                            _data.alignProperty == BoundsUtils.ObjectProperty.BOUNDING_BOX ? 0 : 1, 
                            _alingObjPropOptions) == 0 ? BoundsUtils.ObjectProperty.BOUNDING_BOX : BoundsUtils.ObjectProperty.PIVOT;
                        if (check.changed && _data.alignProperty == BoundsUtils.ObjectProperty.PIVOT)
                        {
                            _data.z.cellSizeType = _data.y.cellSizeType = _data.x.cellSizeType = TransformTools.CellSizeType.CUSTOM;
                            _data.z.cellSize = _data.y.cellSize = _data.x.cellSize = 1f;
                        }
                    }
                }
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    EditorGUIUtility.labelWidth = 110;
                    EditorGUIUtility.fieldWidth = 120;
                    _data.arrangeRelativeTo = (TransformTools.ArrangeRelativeTo)EditorGUILayout.Popup("Arrange relative to:", (int)_data.arrangeRelativeTo, _arrangeRelativeToOptions);
                }
            }
            EditorGUIUtility.labelWidth = 55;
            EditorGUIUtility.fieldWidth = 100;
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(4);
                AxisGUI(TransformTools.Axis.X, _data.x);
                AxisGUI(TransformTools.Axis.Y, _data.y);
                AxisGUI(TransformTools.Axis.Z, _data.z);
            }
            GUILayout.Space(2);
            using (new GUILayout.HorizontalScope())
            {
                var statusStyle = new GUIStyle(EditorStyles.label);
                GUILayout.Space(8);
                var statusMessage = "";
                if (_selectionOrderedTopLevel.Count < 2) statusMessage = "No objects selected.";
                else if (_selectionOrderedTopLevel.Count > _data.x.cells * _data.y.cells * _data.z.cells) 
                    statusMessage = _selectionOrderedTopLevel.Count + " objects selected. Selection don't fit. Add more rows or columns.";
                else statusMessage = _selectionOrderedTopLevel.Count + " objects selected.";
                if (_selectionOrderedTopLevel.Count < 2 || _selectionOrderedTopLevel.Count > _data.x.cells * _data.y.cells * _data.z.cells)
                    GUILayout.Label(_warningIcon, new GUIStyle() { alignment = TextAnchor.LowerLeft });
                GUILayout.Label(statusMessage, statusStyle);
                GUILayout.FlexibleSpace();
                EditorGUI.BeginDisabledGroup(_selectionOrderedTopLevel.Count > _data.x.cells * _data.y.cells * _data.z.cells || _selectionOrderedTopLevel.Count < 2);
                if (GUILayout.Button("Arrange", EditorStyles.miniButtonRight)) Arrange();
                EditorGUI.EndDisabledGroup();
            }
        }

        private void AxisGUI(TransformTools.Axis axis, TransformTools.ArrangeAxisData axisData)
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(170)))
            {
                EditorGUI.BeginChangeCheck();
                axisData.overwrite = EditorGUILayout.BeginToggleGroup(axis.ToString(), axisData.overwrite);
                if (EditorGUI.EndChangeCheck()) _data.UpdatePriorities(axis);

                using (new EditorGUI.DisabledGroupScope(_data.sortBy == TransformTools.SortBy.POSITION))
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        EditorGUI.BeginChangeCheck();
                        axisData.priority = EditorGUILayout.Popup("Priority:", axisData.priority, _priorityOptions);
                        GUILayout.FlexibleSpace();
                        if (EditorGUI.EndChangeCheck()) _data.UpdatePriorities(axis);
                    }
                    using (new GUILayout.HorizontalScope())
                    {
                        var dirOptions = axis == TransformTools.Axis.X ? _directionOptionsX : axis == TransformTools.Axis.Y ? _directionOptionsY : _directionOptionsZ;
                        axisData.direction = EditorGUILayout.Popup("Direction:", axisData.direction == 1 ? 0 : 1, dirOptions) == 0 ? 1 : -1;
                        GUILayout.FlexibleSpace();
                    }
                }
                using (new GUILayout.HorizontalScope())
                {
                    axisData.cells = Stepper("Columns:", axisData.cells, 1, _selectionOrderedTopLevel.Count);
                }

                GUILayout.Label(axis == TransformTools.Axis.Y ? "Height": "Width:", EditorStyles.label);
                using (new GUILayout.HorizontalScope())
                {
                    EditorGUIUtility.fieldWidth = 100;
                    var sizeOptions = new List<string>(axis == TransformTools.Axis.Y ? _heightOptions : _widthOptions);
                    using (new EditorGUI.DisabledGroupScope(_data.alignProperty == BoundsUtils.ObjectProperty.PIVOT))
                        axisData.cellSizeType = (TransformTools.CellSizeType)EditorGUILayout.Popup((int)axisData.cellSizeType, sizeOptions.ToArray());
                    GUILayout.FlexibleSpace();
                }
                using (new GUILayout.HorizontalScope())
                {
                    using (new EditorGUI.DisabledGroupScope(axisData.cellSizeType != TransformTools.CellSizeType.CUSTOM))
                    {
                        using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                        {
                            EditorGUIUtility.labelWidth = 51;
                            EditorGUIUtility.fieldWidth = 90;
                            axisData.cellSize = EditorGUILayout.FloatField("Value:", axisData.cellSize, EditorStyles.numberField);
                            GUILayout.FlexibleSpace();
                        }
                    }
                    GUILayout.FlexibleSpace();
                }

                EditorGUIUtility.labelWidth = 55;
                EditorGUIUtility.fieldWidth = 100;
                GUILayout.Label("Alignment:", EditorStyles.label);
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Toggle(axisData.aligment == TransformTools.Bound.MIN, _alignBtnDict[axis][0], _buttonStyle)) axisData.aligment = TransformTools.Bound.MIN;
                    if (GUILayout.Toggle(axisData.aligment == TransformTools.Bound.CENTER, _alignBtnDict[axis][1], _buttonStyle)) axisData.aligment = TransformTools.Bound.CENTER;
                    if (GUILayout.Toggle(axisData.aligment == TransformTools.Bound.MAX, _alignBtnDict[axis][2], _buttonStyle)) axisData.aligment = TransformTools.Bound.MAX;
                    GUILayout.FlexibleSpace();
                }
                using (new GUILayout.HorizontalScope())
                {
                    axisData.spacing = EditorGUILayout.FloatField("Spacing:", axisData.spacing, EditorStyles.numberField);
                    GUILayout.FlexibleSpace();
                }
                GUILayout.Space(4);
                EditorGUILayout.EndToggleGroup();
            }
        }
        private void Arrange() => TransformTools.Arrange(_selectionOrderedTopLevel.ToArray(), _data);
        #endregion //WINDOW

        #region UTILS
        private int Stepper(string label, int value, int min, int max)
        {
            var retVal = value;
            using (new GUILayout.HorizontalScope())
            {
                EditorGUIUtility.fieldWidth = 63;
                retVal = Mathf.Clamp(EditorGUILayout.IntField(label, retVal, EditorStyles.numberField, GUILayout.Height(18)), min, max);
                GUILayout.Space(-4);
                if (GUILayout.Button("-", EditorStyles.miniButton, GUILayout.Width(20)))
                {
                    --retVal;
                    if (retVal < min) retVal = min;
                }
                GUILayout.Space(-4);
                if (GUILayout.Button("+", EditorStyles.miniButton, GUILayout.Width(20)))
                {
                    ++retVal;
                    if (retVal > max) retVal = max;
                }
                GUILayout.FlexibleSpace();
            }
            return retVal;
        }
        #endregion
    }
}
