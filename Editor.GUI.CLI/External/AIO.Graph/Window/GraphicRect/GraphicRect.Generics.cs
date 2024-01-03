/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-30
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;

namespace AIO.UEditor
{
    public abstract class GraphicRect<T> : GraphicRect where T : EditorWindow
    {
        public T Window { get; }

        protected GraphicRect()
        {
        }

        public GraphicRect(T window)
        {
            Window = window;
        }
    }
}