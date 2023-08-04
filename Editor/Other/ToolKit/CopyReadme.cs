namespace AIO.UEditor
{
    using System.IO;

    using UnityEditor;
    using UnityEditor.Callbacks;

    using UnityEngine;

    public class CopyReadme
    {
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target != BuildTarget.StandaloneWindows)
                return;
            File.Copy(Application.dataPath + "/README.md", Path.GetDirectoryName(pathToBuiltProject) + "/README.md", true);
        }
    }

}
