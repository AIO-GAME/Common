/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-10
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace AIO.UEditor
{
    public static partial class Behavior
    {
        /// <summary>
        /// OnOpenAssetAttribute
        /// </summary>
        public class OnOpenAsset
        {
            public class ShaderEditor
            {
                public static Process vscode;

                public static bool step1(int instanceID, int line)
                {
                    return false;
                }

#if UNITY_2020
                [OnOpenAsset(1)]
#else
                [OnOpenAsset(2)]
#endif
                public static bool step2(int instanceID, int line)
                {
                    var strFilePath = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceID));
                    var strFileName = Directory.GetParent(Application.dataPath) + "/" + strFilePath;
                    if (strFileName.EndsWith(".shader")) //文件扩展名类型
                    {
                        if (vscode == null || Process.GetProcessesByName("Code").Length <= 0)
                        {
                            vscode = new Process();
                            vscode.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            vscode.StartInfo.UseShellExecute = true;
                            vscode.StartInfo.WorkingDirectory = strFileName;
                            vscode.StartInfo.LoadUserProfile = true;
                            vscode.StartInfo.FileName = @"H:\Sotfware-Compiler\Microsoft VS Code\Code.exe";
                        }

                        if (vscode != null)
                        {
                            vscode.StartInfo.Arguments = string.Concat("\"", strFileName, "\"");
                            vscode.Start();
                            return true;
                        }
                    }

                    return false;
                }
            }
        }
    }
}