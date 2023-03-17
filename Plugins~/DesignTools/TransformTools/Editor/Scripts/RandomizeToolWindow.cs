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
    public abstract class RandomizeToolWindow : BaseToolWindow
    {
        protected TransformTools.RandomizeData _data = new TransformTools.RandomizeData();

        protected enum Attribute
        {
            POSITION,
            ROTATION,
            SCALE
        }
        protected Attribute _attribute = Attribute.POSITION;

        protected override void OnGUI()
        {
            base.OnGUI();
            
            EditorGUIUtility.labelWidth = 30;
            EditorGUIUtility.fieldWidth = 70;

            OnGUIValue();
            GUILayout.Space(8);
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                GUILayout.FlexibleSpace();
                EditorGUIUtility.labelWidth = 60;
                _data.multiplier = Mathf.Max(EditorGUILayout.FloatField("Multiplier:", _data.multiplier), 0f);
            }
            GUILayout.Space(8);
            using (new GUILayout.HorizontalScope())
            {
                var statusStyle = new GUIStyle(EditorStyles.label);
                GUILayout.Space(8);
                var statusMessage = "";
                if (_selectionOrderedTopLevel.Count == 0)
                {
                    statusMessage = "No objects selected.";
                    GUILayout.Label(new GUIContent(Resources.Load<Texture2D>("Sprites/Warning")), new GUIStyle() { alignment = TextAnchor.LowerLeft });
                }
                else
                {
                    statusMessage = _selectionOrderedTopLevel.Count + " objects selected.";
                }
                GUILayout.Label(statusMessage, statusStyle);
                GUILayout.FlexibleSpace();
                using (new EditorGUI.DisabledGroupScope(_selectionOrderedTopLevel.Count == 0))
                {
                    if (GUILayout.Button(new GUIContent("Randomize", string.Empty
#if UNITY_2019_1_OR_NEWER
                            + (this is RandomizePositionsWindow ? RandomizePositionsWindow.shortcut
                            : this is RandomizeRotationsWindow ? RandomizeRotationsWindow.shortcut 
                            : this is RandomizeScalesWindow ? RandomizeScalesWindow.shortcut : string.Empty)
#endif
                        ), EditorStyles.miniButtonRight)) Randomize();
                }
            }
        }

        protected virtual void OnGUIValue()
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox)) //X
            {
                using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Randomize X", _data.x.randomizeAxis))
                {
                    _data.x.randomizeAxis = toggleGroup.enabled;
                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();
                        _data.x.offset.min = EditorGUILayout.FloatField("min:", _data.x.offset.min, EditorStyles.textField);
                        GUILayout.Space(8);
                        _data.x.offset.max = EditorGUILayout.FloatField("max:", _data.x.offset.max, EditorStyles.numberField);
                    }
                }
            }
            GUILayout.Space(8);
            using (new GUILayout.VerticalScope(EditorStyles.helpBox)) //Y
            {
                using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Randomize Y", _data.y.randomizeAxis))
                {
                    _data.y.randomizeAxis = toggleGroup.enabled;
                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();
                        _data.y.offset.min = EditorGUILayout.FloatField("min:", _data.y.offset.min, EditorStyles.textField);
                        GUILayout.Space(8);
                        _data.y.offset.max = EditorGUILayout.FloatField("max:", _data.y.offset.max, EditorStyles.numberField);
                    }
                }
            }
            GUILayout.Space(8);
            using (new GUILayout.VerticalScope(EditorStyles.helpBox)) //Z
            {
                using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Randomize Z", _data.z.randomizeAxis))
                {
                    _data.z.randomizeAxis = toggleGroup.enabled;
                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();
                        _data.z.offset.min = EditorGUILayout.FloatField("min:", _data.z.offset.min, EditorStyles.textField);
                        GUILayout.Space(8);
                        _data.z.offset.max = EditorGUILayout.FloatField("max:", _data.z.offset.max, EditorStyles.numberField);
                    }
                }
            }
        }

        protected abstract void Randomize();
    }

    public class RandomizePositionsWindow : RandomizeToolWindow
    {
        [MenuItem("Tools/Plugin Master/Transform Tools/Randomize Positions", false, 1400)]
        public static void ShowWindow() => GetWindow<RandomizePositionsWindow>();

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Randomize Positions";
        [Shortcut(SHORTCUT_ID)]
        public static void RandomizePositions() => GetWindow<RandomizePositionsWindow>().Randomize();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
            titleContent = new GUIContent("Randomize Positions");
            _attribute = RandomizeToolWindow.Attribute.POSITION;
            _data.z.offset.min = _data.y.offset.min = _data.x.offset.min = -1f;
            _data.z.offset.max = _data.y.offset.max = _data.x.offset.max = 1f;
            minSize = new Vector2(240, 220);
        }

        protected override void Randomize() => TransformTools.RandomizePositions(_selectionOrderedTopLevel.ToArray(), _data);
    }

    public class RandomizeRotationsWindow : RandomizeToolWindow
    {
        [MenuItem("Tools/Plugin Master/Transform Tools/Randomize Rotations", false, 1400)]
        public static void ShowWindow() => GetWindow<RandomizeRotationsWindow>();

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Randomize Rotations";
        [Shortcut(SHORTCUT_ID)]
        public static void RandomizeRotations() => GetWindow<RandomizeRotationsWindow>().Randomize();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
            titleContent = new GUIContent("Randomize Rotations");
            _attribute = RandomizeToolWindow.Attribute.ROTATION;
            _data.z.offset.min = _data.y.offset.min = _data.x.offset.min = -180f;
            _data.z.offset.max = _data.y.offset.max = _data.x.offset.max = 180f;
            minSize = new Vector2(240, 220);
        }

        protected override void Randomize()
        {
            TransformTools.RandomizeRotations(_selectionOrderedTopLevel.ToArray(), _data);
        }
    }

    public class RandomizeScalesWindow : RandomizeToolWindow
    {
        private bool _separateAxes = false;

        [MenuItem("Tools/Plugin Master/Transform Tools/Randomize Scales", false, 1400)]
        public static void ShowWindow() => GetWindow<RandomizeScalesWindow>();

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Randomize Scales";
        [Shortcut(SHORTCUT_ID)]
        public static void RandomizeScales() => GetWindow<RandomizeScalesWindow>().Randomize();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif
        protected override void OnEnable()
        {
            base.OnEnable();
            titleContent = new GUIContent("Randomize Scales");
            _attribute = RandomizeToolWindow.Attribute.SCALE;
            _data.z.offset.min = _data.y.offset.min = _data.x.offset.min = -0.1f;
            _data.z.offset.max = _data.y.offset.max = _data.x.offset.max = 0.1f;
        }

        protected override void OnGUIValue()
        {
            EditorGUIUtility.labelWidth = 90;
            _separateAxes = EditorGUILayout.Toggle("Separate Axes", _separateAxes);
            EditorGUIUtility.labelWidth = 30;

            if (_separateAxes)
            {
                minSize = new Vector2(240, 235);
                base.OnGUIValue();
            }
            else
            {
                minSize = new Vector2(240, 105);
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();
                        _data.x.offset.min = _data.y.offset.min = _data.z.offset.min = EditorGUILayout.FloatField("min:", _data.x.offset.min, EditorStyles.textField);
                        GUILayout.Space(8);
                        _data.x.offset.max = _data.y.offset.max = _data.z.offset.max = EditorGUILayout.FloatField("max:", _data.x.offset.max, EditorStyles.numberField);

                    }
                }
            }
        }

        protected override void Randomize()
        {
            TransformTools.RandomizeScales(_selectionOrderedTopLevel.ToArray(), _data, _separateAxes);
        }
    }
}
