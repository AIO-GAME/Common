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
        [LnkTools("Save Scene", "#00BFFF", "SaveAs", LnkToolsMode.OnlyEditor, -5)]
        private static void SaveScene()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
        }

        [LnkTools("Save Project", "#00BFFF", "SaveAs", LnkToolsMode.OnlyEditor, -4)]
        private static void SaveProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save Project");
        }
    }
}