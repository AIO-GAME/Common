using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace AIO.UEditor
{
    [EditorTool("Pivot Tool", typeof(Transform))]
    internal class PivotTool : EditorTool
    {
        private Transform _target;
        private List<Transform> Childs = new List<Transform>();
        private GUIContent _gc;

        public override GUIContent toolbarIcon
        {
            get
            {
                if (_gc == null)
                {
                    _gc = new GUIContent
                    {
                        image = EditorGUIUtility.IconContent("ToolHandlePivot").image,
                        tooltip = "Pivot Tool"
                    };
                }

                return _gc;
            }
        }

        private void OnEnable()
        {
#if UNITY_2021_1_OR_NEWER
            ToolManager.activeToolChanged += ActiveToolChanged;
#endif
            Selection.selectionChanged += ActiveToolChanged;
        }

        private void OnDisable()
        {
#if UNITY_2021_1_OR_NEWER
            ToolManager.activeToolChanged -= ActiveToolChanged;
#endif
            Selection.selectionChanged -= ActiveToolChanged;
        }

        private void ActiveToolChanged()
        {
#if UNITY_2021_1_OR_NEWER
            if (!ToolManager.IsActiveTool(this))
                return;
#endif
            _target = target as Transform;
            Childs.Clear();
            for (int i = 0; i < _target.childCount; i++)
            {
                Childs.Add(_target.GetChild(i));
            }
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if (_target == null) return;

            using (new Handles.DrawingScope())
            {
                var temp = _target.position;
                Handles.Label(temp, "       Pivot");

                EditorGUI.BeginChangeCheck();
                var newValue = Handles.PositionHandle(temp, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(_target, "Move Pivot");
                    var dir = newValue - _target.position;
                    _target.position = newValue;
                    foreach (var trans in Childs)
                    {
                        Undo.RecordObject(trans, "Move Pivot");
                        trans.position -= dir;
                    }
                }
            }
        }
    }
}