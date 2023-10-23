using UnityEditor;
using UnityEngine;

namespace AIO.RainbowCore
{
    internal abstract class CoreDraggablePopupWindow : EditorWindow
    {
        private Vector2 _offset;

        public static T GetDraggableWindow<T>() where T : CoreDraggablePopupWindow
        {
            var array = Resources.FindObjectsOfTypeAll(typeof(T)) as T[];
            T val = null;
            if (array != null && array.Length > 0) val = array[0];
            if (!val) return CreateInstance<T>();
            return val;
        }

        public void Show(Rect inPosition, bool focus = true)
        {
            minSize = inPosition.size;
            position = inPosition;
            ShowPopup();
            if (focus)
            {
                Focus();
            }
        }

        public virtual void OnGUI()
        {
            Event current = Event.current;
            if (current.button == 0 && current.type == EventType.MouseDown)
            {
                _offset = base.position.position - GUIUtility.GUIToScreenPoint(current.mousePosition);
            }

            if (current.button == 0 && current.type == EventType.MouseDrag)
            {
                Vector2 vector = GUIUtility.GUIToScreenPoint(current.mousePosition);
                base.position = new Rect(vector + _offset, base.position.size);
            }
        }
    }
}