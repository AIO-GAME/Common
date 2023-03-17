using UnityEditor;

namespace Shader.Control.Editor
{
    public class SCMenu : UnityEditor.Editor
    {
        [MenuItem("Assets/Browse Shaders...", false, 200)]
        private static void BrowseShaders(MenuCommand command)
        {
            SCWindow.ShowWindow();
        }
    }
}