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
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;

namespace PluginMaster
{

    public class UnoverlapToolWindow : BaseToolWindow
    {
        private TransformTools.UnoverlapData _data = new TransformTools.UnoverlapData();
        private Thread _unoverlapThread = null;
        private TransformTools.Unoverlapper _unoverlapper = null;
        private float _loadingProgress = 0f;
        private static bool _repaint = false;
        private (int objId, Vector3 offset)[] _offsets = null;
        private Dictionary<int, GameObject> _objDictionary = new Dictionary<int, GameObject>();
        private const int LARGEST_SELECTION_COUNT = 50;
#if UNITY_2020_1_OR_NEWER
        private int _progressId = -1;
#endif

        [MenuItem("Tools/Plugin Master/Transform Tools/Remove Overlaps", false, 1700)]
        public static void ShowWindow() => GetWindow<UnoverlapToolWindow>();

#if UNITY_2019_1_OR_NEWER
        public const string SHORTCUT_ID = "Transform Tools/Remove Overlaps";
        [Shortcut(SHORTCUT_ID)]
        public static void ShortcutAction() => GetWindow<UnoverlapToolWindow>().Apply();
        public static string shortcut => ShortcutManager.instance.GetShortcutBinding(SHORTCUT_ID).ToString();
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
            minSize = new Vector2(280, 180);
            titleContent = new GUIContent("Remove Overlaps", null, "Remove Overlaps");
        }
        protected override void OnGUI()
        {
            base.OnGUI();
            
            if (_loadingProgress > 0f && _loadingProgress < 1f)
            {
#if UNITY_2020_1_OR_NEWER
                Progress.Report(_progressId, _loadingProgress);
#else
                EditorUtility.DisplayProgressBar("Removing Overlaps", ((int)(_loadingProgress * 100)).ToString() + " %", _loadingProgress);
#endif
            }

            EditorGUIUtility.labelWidth = 50;
            EditorGUIUtility.fieldWidth = 70;
            
                using (new GUILayout.VerticalScope(EditorStyles.helpBox)) //X
                {
                    using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Remove overlaps on X", _data.x.unoverlap))
                    {
                        _data.x.unoverlap = toggleGroup.enabled;
                        using (new GUILayout.HorizontalScope())
                        {
                            GUILayout.FlexibleSpace();
                            _data.x.minDistance = EditorGUILayout.FloatField("Spacing:", _data.x.minDistance, EditorStyles.textField);
                        }
                    }
                }
                GUILayout.Space(8);
                using (new GUILayout.VerticalScope(EditorStyles.helpBox)) //Y
                {
                    using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Remove overlaps on Y", _data.y.unoverlap))
                    {
                        _data.y.unoverlap = toggleGroup.enabled;
                        using (new GUILayout.HorizontalScope())
                        {
                            GUILayout.FlexibleSpace();
                            _data.y.minDistance = EditorGUILayout.FloatField("Spacing:", _data.y.minDistance, EditorStyles.textField);
                        }
                    }
                }
                GUILayout.Space(8);
                using (new GUILayout.VerticalScope(EditorStyles.helpBox)) //Z
                {
                    using (var toggleGroup = new EditorGUILayout.ToggleGroupScope("Remove overlaps on Z", _data.z.unoverlap))
                    {
                        _data.z.unoverlap = toggleGroup.enabled;
                        using (new GUILayout.HorizontalScope())
                        {
                            GUILayout.FlexibleSpace();
                            _data.z.minDistance = EditorGUILayout.FloatField("Spacing:", _data.z.minDistance, EditorStyles.textField);
                        }
                    }
                }
                GUILayout.Space(2);
                using (new GUILayout.HorizontalScope())
                {
                    var statusStyle = new GUIStyle(EditorStyles.label);
                    GUILayout.Space(8);
                    var statusMessage = "";
                    if (_selectionOrderedTopLevel.Count == 0 || _selectionOrderedTopLevel.Count > LARGEST_SELECTION_COUNT)
                    {
                        statusMessage = _selectionOrderedTopLevel.Count == 0 ? "No objects selected." : _selectionOrderedTopLevel.Count + " objects. (max = "+ LARGEST_SELECTION_COUNT + ")";
                        GUILayout.Label(new GUIContent(Resources.Load<Texture2D>("Sprites/Warning")), new GUIStyle() { alignment = TextAnchor.LowerLeft });
                    }
                    else
                    {
                        statusMessage = _selectionOrderedTopLevel.Count + " objects selected.";
                    }
                    GUILayout.Label(statusMessage, statusStyle);
                    GUILayout.FlexibleSpace();
                    using (new EditorGUI.DisabledScope(_selectionOrderedTopLevel.Count == 0 || _selectionOrderedTopLevel.Count > LARGEST_SELECTION_COUNT))
                    {
                        if (GUILayout.Button(new GUIContent("Remove Overlaps", string.Empty
#if UNITY_2019_1_OR_NEWER
                            + shortcut
#endif
                            ), EditorStyles.miniButtonRight)) Apply();
                    }
                }

        }

        private void Apply()
        {
            var bounds = _selectionOrderedTopLevel.Select(obj => (obj.GetInstanceID(), BoundsUtils.GetBounds(obj.transform))).ToArray();
            _unoverlapper = new TransformTools.Unoverlapper(bounds, _data);
            _unoverlapper.progressChanged += OnProgress;
            _unoverlapper.OnDone += OnDone;
            var threadDelegate = new ThreadStart(_unoverlapper.RemoveOverlaps);
            _unoverlapThread = new Thread(threadDelegate);
            _unoverlapThread.Name = "Unoverlap";
            _unoverlapThread.Start();
            _loadingProgress = 0f;
            _offsets = null;
#if UNITY_2020_1_OR_NEWER
                        _progressId = Progress.Start("Removing Overlaps");
#endif
        }
        private void OnProgress(float progress)
        {
            _loadingProgress = progress;
            _repaint = true;
        }

        private void OnDone((int objId, Vector3 offset)[] positions)
        {
            _unoverlapper.progressChanged -= OnProgress;
            _unoverlapper.OnDone -= OnDone;
            _unoverlapper = null;
            _loadingProgress = 1f;
            _offsets = positions;
        }

        private void Update()
        {
            if (_repaint)
            {
                Repaint();
                _repaint = false;
            }
            if (_offsets != null)
            {
#if UNITY_2020_1_OR_NEWER
                _progressId = Progress.Remove(_progressId);
#else
                EditorUtility.ClearProgressBar();
#endif
                var i = 0;
                foreach (var offsetObj in _offsets)
                {
                    var transform = _objDictionary[offsetObj.objId].transform;
                    Undo.RecordObject(transform, "Remove Overlap");
                    transform.position += offsetObj.offset;
                    ++i;
                }
                _offsets = null;
            }
        }

        private void OnDestroy()
        {
#if UNITY_2020_1_OR_NEWER
            if (_progressId > 0)
            {
                _progressId = Progress.Remove(_progressId);
            }
#else
            EditorUtility.ClearProgressBar();
#endif
            if (_unoverlapper != null)
            {
                _unoverlapper.progressChanged -= OnProgress;
                _unoverlapper.OnDone -= OnDone;
                _unoverlapper.Cancel();
                _unoverlapper = null;
                _unoverlapThread.Abort();
                _repaint = false;
            }
        }

        protected override void OnSelectionChange()
        {
            base.OnSelectionChange();
            _objDictionary.Clear();
            _objDictionary = _selectionOrderedTopLevel.ToDictionary(obj => obj.GetInstanceID());
        }
    }
}
