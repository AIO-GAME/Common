/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-30
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

namespace UnityEditor
{
    public abstract class GraphicRect<T> : GraphicRect where T : EditorWindow
    {
        public T Window { get;  }

        protected GraphicRect()
        {
            
        }

        public GraphicRect(T window)
        {
            Window = window;
        }
    }
}