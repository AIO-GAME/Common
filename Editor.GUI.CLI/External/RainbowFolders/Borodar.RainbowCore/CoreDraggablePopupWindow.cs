using UnityEditor;
using UnityEngine;

namespace AIO.RainbowCore
{
    internal abstract class CoreDraggablePopupWindow : EditorWindow
    {
        private Vector2 _offset;

        public static T GetDraggableWindow<T>()
        where T : CoreDraggablePopupWindow
        {
            var array                                  = Resources.FindObjectsOfTypeAll(typeof(T)) as T[];
            T   val                                    = null;
            if (array != null && array.Length > 0) val = array[0];
            return !val ? CreateInstance<T>() : val;
        }

        public void Show(Rect inPosition, bool focus = true)
        {
            minSize  = inPosition.size;
            position = inPosition;
            ShowPopup();
            if (focus)
            {
                Focus();
            }
        }

        public virtual void OnGUI()
        {
            var current = Event.current;
            if (current.button == 0 && current.type == EventType.MouseDown)
            {
                _offset = position.position - GUIUtility.GUIToScreenPoint(current.mousePosition);
            }

            if (current.button == 0 && current.type == EventType.MouseDrag)
            {
                var vector = GUIUtility.GUIToScreenPoint(current.mousePosition);
                position = new Rect(vector + _offset, position.size);
            }
        }
    }
}