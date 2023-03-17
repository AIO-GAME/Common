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
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEditorInternal;
using UnityEngine;

namespace PluginMaster
{
    public class PlaceOnSurfaceWindow : BaseToolWindow
    {
        private TransformTools.PlaceOnSurfaceData _data = new TransformTools.PlaceOnSurfaceData();
        private static Vector3[] _directions =
        {
            Vector3.right, Vector3.left,
            Vector3.up, Vector3.down,
            Vector3.forward, Vector3.back
        };
        private int _dirIdx = 3;
        private int _orientDirIdx = 3;

        [MenuItem("Tools/Plugin Master/Transform Tools/Place On The Surface", false, 1500)]
        public static void ShowWindow() => GetWindow<PlaceOnSurfaceWindow>();

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Place On Surface";
        [Shortcut(SHORTCUT_ID)]
        public static void ShortcutAction() => GetWindow<PlaceOnSurfaceWindow>().Apply();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif

        protected override void OnGUI()
        {
            base.OnGUI();
            minSize = new Vector2(244, 170);
            titleContent = new GUIContent("Place On The Surface", null, "Place On The Surface");

            EditorGUIUtility.labelWidth = 120;
            EditorGUIUtility.fieldWidth = 100;
            using (new GUILayout.VerticalScope())
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        _data.projectionDirectionSpace = (Space)EditorGUILayout.Popup("Projection Space:", (int)_data.projectionDirectionSpace, new string[] { "Global", "Local" });
                        GUILayout.FlexibleSpace();
                    }
                    using (new GUILayout.HorizontalScope())
                    {
                        _dirIdx = EditorGUILayout.Popup("Pojection Direction:", _dirIdx, new string[] { "+X", "-X", "+Y", "-Y", "+Z", "-Z" });
                        _data.projectionDirection = _directions[_dirIdx];
                        GUILayout.FlexibleSpace();
                    }
                    using (new GUILayout.HorizontalScope())
                    {
                        using (var changeCheck = new EditorGUI.ChangeCheckScope())
                        {
                            var mask = EditorGUILayout.MaskField("Surface Layer:", EditorGUIUtils.LayerMaskToField(_data.mask), InternalEditorUtility.layers);
                            if(changeCheck.changed)
                            {
                                _data.mask = EditorGUIUtils.FieldToLayerMask(mask);
                            }
                        }
                        GUILayout.FlexibleSpace();
                    }
                    using (new GUILayout.HorizontalScope())
                    {
                        _data.placeOnColliders = !EditorGUILayout.ToggleLeft("Place on meshes without colliders", !_data.placeOnColliders);
                        GUILayout.FlexibleSpace();
                    }
                    using (new GUILayout.HorizontalScope())
                    {
                        _data.surfaceDistance = EditorGUILayout.FloatField("Surface Distance:", _data.surfaceDistance);
                        GUILayout.FlexibleSpace();
                    }
                    using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                    {
                        using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Rotate to the Surface", _data.rotateToSurface))
                        {
                            _data.rotateToSurface = toggleGroup.enabled;
                            using (new GUILayout.HorizontalScope())
                            {
                                EditorGUIUtility.labelWidth -= 4;
                                _orientDirIdx = EditorGUILayout.Popup("Object axis:", _orientDirIdx, new string[] { "+X", "-X", "+Y", "-Y", "+Z", "-Z" });
                                EditorGUIUtility.labelWidth += 4;
                                _data.objectOrientation = _directions[_orientDirIdx];
                                GUILayout.FlexibleSpace();
                            }
                        }
                    }
                }
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
                        statusMessage = _selectionOrderedTopLevel.Count.ToString() + " objects selected.";
                    }
                    GUILayout.Label(statusMessage, statusStyle);
                    GUILayout.FlexibleSpace();
                    using (new EditorGUI.DisabledGroupScope(_selectionOrderedTopLevel.Count == 0))
                    {
                        if (GUILayout.Button(new GUIContent("Apply", string.Empty
#if UNITY_2019_1_OR_NEWER
                            + shortcut
#endif
                            ), EditorStyles.miniButtonRight)) Apply();
                    }
                }
            }
        }

        private void Apply() => TransformTools.PlaceOnSurface(_selectionOrderedTopLevel.ToArray(), _data);
    }
}
