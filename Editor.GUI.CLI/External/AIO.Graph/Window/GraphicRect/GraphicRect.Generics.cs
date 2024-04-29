#region

using UnityEditor;

#endregion

namespace AIO.UEditor
{
    public abstract class GraphicRect<T> : GraphicRect
    where T : EditorWindow
    {
        protected GraphicRect() { }

        public GraphicRect(T window)
        {
            Window = window;
        }

        public T Window { get; }
    }
}