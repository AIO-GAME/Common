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
using UnityEngine;

namespace PluginMaster
{
    public class SimulateGravityWindow : BaseToolWindow
    {
        private SimulateGravityData _data = new SimulateGravityData();

        [MenuItem("Tools/Plugin Master/Transform Tools/Simulate Gravity", false, 1600)]
        public static void ShowWindow() => GetWindow<SimulateGravityWindow>();

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Simulate Gravity";
        [Shortcut(SHORTCUT_ID)]
        public static void ShortcutAction() => GetWindow<SimulateGravityWindow>().Apply();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
            minSize = new Vector2(190, 185);
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            
            titleContent = new GUIContent("Simulate Gravity", null, "Simulate Gravity");

            EditorGUIUtility.labelWidth = 120;
            EditorGUIUtility.fieldWidth = 100;
            using (new GUILayout.VerticalScope())
            {
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    _data.gravity = EditorGUILayout.Vector3Field("Gravity:", _data.gravity);
                    _data.maxIterations = EditorGUILayout.IntField("Max Iterations:", _data.maxIterations);
                    _data.maxSpeed = EditorGUILayout.FloatField("Max Speed:", _data.maxSpeed);
                    _data.maxAngularSpeed = EditorGUILayout.FloatField("Max Angular Speed:", _data.maxAngularSpeed);
                    _data.mass = EditorGUILayout.FloatField("Mass:", _data.mass);
                    _data.drag = EditorGUILayout.FloatField("Drag:", _data.drag);
                    _data.angularDrag = EditorGUILayout.FloatField("Angular Drag:", _data.angularDrag);
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
                        if (GUILayout.Button(new GUIContent("Apply"), EditorStyles.miniButtonRight)) Apply();
                    }
                }
            }
        }

        private void Apply() => GravityUtils.SimulateGravity(_selectionOrderedTopLevel.ToArray(), _data, true);
    }
}
