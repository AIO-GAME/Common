/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using UnityEditor;

namespace AIO.UEditor
{
    /// <summary>
    /// nameof(LnkToolsHelper_List)
    /// </summary>
    internal static partial class LnkToolsHelper
    {
        [LnkTools(
            LnkToolsMode.OnlyEditor,
            Priority = -5,
            BackgroundColor = "#616161",
            Tooltip = "Save Scene",
            IconBuiltin = "SaveAs")]
        private static void SaveScene()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
        }
        
        [LnkTools(
            LnkToolsMode.OnlyEditor,
            Priority = -4,
            Tooltip = "Save Project",
            BackgroundColor = "#616161",
            IconBuiltin = "SaveAs")]
        private static void SaveProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save Project");
        }
    }
}