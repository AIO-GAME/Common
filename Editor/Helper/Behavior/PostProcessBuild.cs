#region

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    public static partial class Behavior
    {
        #region Nested type: PostProcessBuild

        public static class PostProcessBuild
        {
            [PostProcessBuild(1)]
            public static void CopyReadme(BuildTarget target, string pathToBuiltProject)
            {
                if (target != BuildTarget.StandaloneWindows)
                    return;
                File.Copy(Application.dataPath + "/README.md", Path.GetDirectoryName(pathToBuiltProject) + "/README.md", true);
            }
        }

        #endregion
    }
}