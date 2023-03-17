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
    public abstract class HomogenizeToolWindow : BaseToolWindow
    {
        protected TransformTools.HomogenizeData _data = new TransformTools.HomogenizeData();

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            EditorGUIUtility.labelWidth = 60;

            OnGUIValue();
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
                    if (GUILayout.Button(new GUIContent("Homogenize", string.Empty
#if UNITY_2019_1_OR_NEWER
                            + (this is HomogenizeSpacingWindow ? HomogenizeSpacingWindow.shortcut
                            : this is HomogenizeRotationWindow ? HomogenizeRotationWindow.shortcut
                            : this is HomogenizeScaleWindow ? HomogenizeScaleWindow.shortcut : string.Empty)
#endif
                        ), EditorStyles.miniButtonRight)) Homogenize();
                }
            }
        }

        protected virtual void OnGUIValue()
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox)) //X
            {
                using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Homogenize X", _data.x.homogenize))
                {
                    _data.x.homogenize = toggleGroup.enabled;
                    _data.x.strength = EditorGUILayout.Slider("Strength:", _data.x.strength, 0f, 1f);
                }
            }
            GUILayout.Space(8);
            using (new GUILayout.VerticalScope(EditorStyles.helpBox)) //Y
            {
                using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Homogenize Y", _data.y.homogenize))
                {
                    _data.y.homogenize = toggleGroup.enabled;
                    _data.y.strength = EditorGUILayout.Slider("Strength:", _data.y.strength, 0f, 1f);
                }
            }
            GUILayout.Space(8);
            using (new GUILayout.VerticalScope(EditorStyles.helpBox)) //Z
            {
                using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Homogenize Z", _data.z.homogenize))
                {
                    _data.z.homogenize = toggleGroup.enabled;
                    _data.z.strength = EditorGUILayout.Slider("Strength:", _data.z.strength, 0f, 1f);
                }
            }
        }
        protected abstract void Homogenize();
    }

    public class HomogenizeSpacingWindow : HomogenizeToolWindow
    {
        [MenuItem("Tools/Plugin Master/Transform Tools/Homogenize Spacing", false, 1450)]
        public static void ShowWindow() => GetWindow<HomogenizeSpacingWindow>();

        protected override void OnEnable()
        {
            base.OnEnable();
            titleContent = new GUIContent("Homogenize Spacing");
            minSize = new Vector2(240, 185);
        }

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Homogenize Spacing";
        [Shortcut(SHORTCUT_ID)]
        public static void ShortcutAction() => GetWindow<HomogenizeSpacingWindow>().Homogenize();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif
        protected override void Homogenize() => TransformTools.HomogenizeSpacing(_selectionOrderedTopLevel.ToArray(), _data);
    }

    public class HomogenizeRotationWindow : HomogenizeToolWindow
    {
        [MenuItem("Tools/Plugin Master/Transform Tools/Homogenize Rotation", false, 1450)]
        public static void ShowWindow() => GetWindow<HomogenizeRotationWindow>();

        protected override void OnEnable()
        {
            base.OnEnable();
            titleContent = new GUIContent("Homogenize Rotation");
            minSize = new Vector2(240, 185);
        }

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Homogenize Rotation";
        [Shortcut(SHORTCUT_ID)]
        public static void ShortcutAction() => GetWindow<HomogenizeRotationWindow>().Homogenize();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif
        protected override void Homogenize() => TransformTools.HomogenizeRotation(_selectionOrderedTopLevel.ToArray(), _data);
    }

    public class HomogenizeScaleWindow : HomogenizeToolWindow
    {
        private bool _separateAxes = false;

        [MenuItem("Tools/Plugin Master/Transform Tools/Homogenize Scale", false, 1450)]
        public static void ShowWindow() => GetWindow<HomogenizeScaleWindow>();

        protected override void OnEnable()
        {
            base.OnEnable();
            titleContent = new GUIContent("Homogenize Scale");
        }

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Homogenize Scale";
        [Shortcut(SHORTCUT_ID)]
        public static void ShortcutAction() => GetWindow<HomogenizeScaleWindow>().Homogenize();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif
        protected override void Homogenize() => TransformTools.HomogenizeScale(_selectionOrderedTopLevel.ToArray(), _data);

        protected override void OnGUIValue()
        {
            EditorGUIUtility.labelWidth = 90;
            _separateAxes = EditorGUILayout.Toggle("Separate Axes", _separateAxes);
            EditorGUIUtility.labelWidth = 60;

            if (_separateAxes)
            {
                minSize = new Vector2(240, 205);
                base.OnGUIValue();
            }
            else
            {
                minSize = new Vector2(240, 80);
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();
                        _data.x.strength = _data.y.strength = _data.z.strength = EditorGUILayout.Slider("Strength:", _data.x.strength, 0f, 1f);
                    }
                }
            }
        }
    }
}
