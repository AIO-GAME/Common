/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-08-10
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        public static string Path(string label, string value, string tips = "Please select the path",
            string defaultName = "")
        {
            Horizontal(() =>
            {
                Label(label);
                if (Button("Select", GUILayout.Width(50)))
                    value = EditorUtility.OpenFolderPanel(tips, value, defaultName);
                if (string.IsNullOrEmpty(value)) return;
                if (Button("Open", GUILayout.Width(50)))
                    PrPlatform.Open.Path(value).Async();
            });
            return value;
        }
    }
}