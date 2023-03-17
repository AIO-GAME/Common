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
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;

namespace PluginMaster
{
    public class RadialArrangeToolWindow : BaseToolWindow
    {
        private TransformTools.RadialArrangeData _data = new TransformTools.RadialArrangeData();
        private static readonly Vector3[] _axes =
        {
            Vector3.right, Vector3.left,
            Vector3.up, Vector3.down,
            Vector3.forward, Vector3.back
        };
        private static readonly string[] _axesOptions = { "+X", "-X", "+Y", "-Y", "+Z", "-Z" };

        private int _axisIdx = 4;
        private int _orientDirIdx = 0;
        private List<Vector3> _parallelAxes = null;
        private int _parallelDirIdx = 0;
        private List<string> _parallelAxesOptions = null;
        private bool _lastUpdateSpacing = true;

        private GUIContent _warningIcon = null;


        [MenuItem("Tools/Plugin Master/Transform Tools/Radial Arrangement", false, 1201)]
        public static void ShowWindow() => GetWindow<RadialArrangeToolWindow>();

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Radial Arrangement";
        [Shortcut(SHORTCUT_ID)]
        public static void ShortcutAction() => GetWindow<RadialArrangeToolWindow>().Arrange();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
            _warningIcon = new GUIContent(Resources.Load<Texture2D>("Sprites/Warning"));
            titleContent = new GUIContent("Radial Arrangement");
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            minSize = new Vector2(250, 305);

            EditorGUIUtility.labelWidth = 74;
            EditorGUIUtility.fieldWidth = 110;

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                _data.arrangeBy = (TransformTools.ArrangeBy)EditorGUILayout.Popup("Arrange by:", (int)_data.arrangeBy, new string[] { "Selection order", "Hierarchy order" });
            }

            EditorGUIUtility.labelWidth = 90;
            EditorGUIUtility.fieldWidth = 140;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new GUILayout.HorizontalScope())
                {
                    _data.rotateAround = (TransformTools.RotateAround)EditorGUILayout.Popup("Rotate around:", (int)_data.rotateAround, new string[] { "Selection Center", "Transform position", "Object bounds center", "Custom position" });
                    GUILayout.FlexibleSpace();
                }

                var disableCenterField = _data.rotateAround != TransformTools.RotateAround.CUSTOM_POSITION;
                if (_data.rotateAround == TransformTools.RotateAround.TRANSFORM_POSITION
                    || _data.rotateAround == TransformTools.RotateAround.OBJECT_BOUNDS_CENTER)
                {
                    minSize += new Vector2(0, 20);
                    using (new GUILayout.HorizontalScope())
                    {
                        _data.centerTransform = (Transform)EditorGUILayout.ObjectField("Transform:", _data.centerTransform, typeof(Transform), true);
                        GUILayout.FlexibleSpace();
                    }
                }
                else if (_data.rotateAround == TransformTools.RotateAround.SELECTION_CENTER) _data.UpdateCenter(_selectionOrderedTopLevel.ToArray());
                using (new EditorGUI.DisabledGroupScope(disableCenterField))
                {
                    _data.center = EditorGUILayout.Vector3Field("Center", _data.center);
                }

                _axisIdx = EditorGUILayout.Popup("Rotation axis:", _axisIdx, _axesOptions, GUILayout.Width(235));
                _data.axis = _axes[_axisIdx];

                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Label("Ovewrite:", GUILayout.Width(87));
                    EditorGUIUtility.labelWidth = 10;
                    _data.overwriteX = EditorGUILayout.Toggle("X", _data.overwriteX);
                    _data.overwriteY = EditorGUILayout.Toggle("Y", _data.overwriteY);
                    _data.overwriteZ = EditorGUILayout.Toggle("Z", _data.overwriteZ);
                    GUILayout.FlexibleSpace();
                    EditorGUIUtility.labelWidth = 90;
                }
            }

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new GUILayout.HorizontalScope())
                {
                    _data.shape = (TransformTools.Shape)EditorGUILayout.Popup("Shape:", (int)_data.shape, new string[] { "Circle", "Circular Spiral", "Ellipse", "Elliptical Spiral" });
                    GUILayout.FlexibleSpace();
                }
                switch (_data.shape)
                {
                    case TransformTools.Shape.CIRCLE:
                        minSize += new Vector2(0, 40);
                        using (var check = new EditorGUI.ChangeCheckScope())
                        {
                            _data.endEllipseAxes = _data.startEllipseAxes = Vector2.one * EditorGUILayout.FloatField("Radius:", _data.startEllipseAxes.x, GUILayout.Width(235));
                            if (check.changed)
                            {
                                _data.UpdateCircleSpacing(_selectionOrderedTopLevel.Count);
                                _lastUpdateSpacing = true;
                            }
                        }
                        using (var check = new EditorGUI.ChangeCheckScope())
                        {
                            _data.spacing = EditorGUILayout.FloatField("Spacing:", _data.spacing, GUILayout.Width(235));
                            if (check.changed)
                            {
                                _data.UpdateCircleRadius(_selectionOrderedTopLevel.Count);
                                _lastUpdateSpacing = false;
                            }
                        }
                        break;
                    case TransformTools.Shape.CIRCULAR_SPIRAL:
                        minSize += new Vector2(0, 40);
                        _data.startEllipseAxes = Vector2.one * EditorGUILayout.FloatField("Start Radius:", _data.startEllipseAxes.x, GUILayout.Width(235));
                        _data.endEllipseAxes = Vector2.one * EditorGUILayout.FloatField("End Radius:", _data.endEllipseAxes.x, GUILayout.Width(235));
                        break;
                    case TransformTools.Shape.ELLIPSE:
                        minSize += new Vector2(0, 40);
                        _data.endEllipseAxes = _data.startEllipseAxes = EditorGUILayout.Vector2Field("Ellipse axes:", _data.startEllipseAxes, GUILayout.Width(235));
                        break;
                    case TransformTools.Shape.ELLIPTICAL_SPIRAL:
                        _data.startEllipseAxes = EditorGUILayout.Vector2Field("Start ellipse axes:", _data.startEllipseAxes, GUILayout.Width(235));
                        _data.endEllipseAxes = EditorGUILayout.Vector2Field("End ellipse axes:", _data.endEllipseAxes, GUILayout.Width(235));
                        minSize += new Vector2(0, 80);
                        break;
                }
            }

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                _data.startAngle = EditorGUILayout.FloatField("Start angle:", _data.startAngle, GUILayout.Width(235));
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    _data.maxArcAngle = EditorGUILayout.FloatField("Max arc angle:", _data.maxArcAngle, GUILayout.Width(235));
                    EditorGUIUtility.labelWidth = 170;
                    _data.lastSpotEmpty = EditorGUILayout.ToggleLeft("Add an empty spot at the end", _data.lastSpotEmpty);
                    if (check.changed) UpdateCircleRadiusAndSpacing();
                }
                EditorGUIUtility.labelWidth = 90;
            }

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Orient to the center", _data.orientToRadius))
                {
                    _data.orientToRadius = toggleGroup.enabled;
                    EditorGUI.BeginChangeCheck();
                    _orientDirIdx = EditorGUILayout.Popup("Radial axis:", _orientDirIdx, _axesOptions, GUILayout.Width(235));
                    _data.orientDirection = _axes[_orientDirIdx];
                    if (EditorGUI.EndChangeCheck() || _parallelAxes == null)
                    {
                        _parallelAxes = new List<Vector3>(_axes);
                        _parallelAxesOptions = new List<string>(_axesOptions);
                        if (_orientDirIdx < 2)
                        {
                            _parallelAxes.RemoveAt(0);
                            _parallelAxes.RemoveAt(0);
                            _parallelAxesOptions.RemoveAt(0);
                            _parallelAxesOptions.RemoveAt(0);
                        }
                        else if (_orientDirIdx < 4)
                        {
                            _parallelAxes.RemoveAt(2);
                            _parallelAxes.RemoveAt(2);
                            _parallelAxesOptions.RemoveAt(2);
                            _parallelAxesOptions.RemoveAt(2);
                        }
                        else
                        {
                            _parallelAxes.RemoveAt(4);
                            _parallelAxes.RemoveAt(4);
                            _parallelAxesOptions.RemoveAt(4);
                            _parallelAxesOptions.RemoveAt(4);
                        }
                        _parallelDirIdx = 0;
                    }
                    _parallelDirIdx = EditorGUILayout.Popup("Parallel axis:", _parallelDirIdx, _parallelAxesOptions.ToArray(), GUILayout.Width(235));
                    _data.parallelDirection = _parallelAxes[_parallelDirIdx];
                }
            }

            GUILayout.Space(2);
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(8);
                var statusMessage = "";
                if (_selectionOrderedTopLevel.Count == 0)
                {
                    statusMessage = "No objects selected.";
                    GUILayout.Label(_warningIcon, new GUIStyle() { alignment = TextAnchor.LowerLeft });
                }
                else statusMessage = _selectionOrderedTopLevel.Count + " objects selected.";
                GUILayout.Label(statusMessage, EditorStyles.label);
                GUILayout.FlexibleSpace();
                using (new EditorGUI.DisabledGroupScope(_selectionOrderedTopLevel.Count == 0))
                {
                    if (GUILayout.Button("Apply", EditorStyles.miniButtonRight)) Arrange();
                }
            }
        }

        private void Arrange() => TransformTools.RadialArrange(_selectionOrderedTopLevel.ToArray(), _data);

        private void UpdateCircleRadiusAndSpacing()
        {
            if (_data.shape == TransformTools.Shape.CIRCLE)
            {
                if (_lastUpdateSpacing) _data.UpdateCircleSpacing(_selectionOrderedTopLevel.Count);
                else _data.UpdateCircleRadius(_selectionOrderedTopLevel.Count);
            }
        }

        protected override void OnSelectionChange()
        {
            base.OnSelectionChange();
            _data.UpdateCenter(_selectionOrderedTopLevel.ToArray());
            UpdateCircleRadiusAndSpacing();
        }

        private void Update() => _data.UpdateCenter();
    }
}
