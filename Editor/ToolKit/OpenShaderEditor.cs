// /* * * * * * * * * * * * * * * * * * * * * * * *
// * FileName:         OpenShaderEditor
// * Author:           XiNan
// * Version:          14.4.1
// * UnityVersion:     2018.2.21f1
// * Date:             2021-03-25
// * Time:             16:06:59
// * E-Mail:           1398581458@qq.com
// * Description:      使用 VS Code 编辑 Shader文件
// * History:
// * * * * * * * * * * * * * * * * * * * * * * * * */
//
// namespace AIO.Unity.Editor
// {
//     using UnityEditor;
//     using UnityEngine;
//     using UnityEditor.Callbacks;
//     using System.IO;
//
//     public class ShaderEditor
//     {
//         public static System.Diagnostics.Process vscode;
//
//         public static bool step1(int instanceID, int line)
//         {
//             return false;
//         }
//
// #if UNITY_2020
//         [OnOpenAssetAttribute(1)]
// #else
//         [OnOpenAssetAttribute(2)]
// #endif
//         public static bool step2(int instanceID, int line)
//         {
//             var strFilePath = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceID));
//             var strFileName = Directory.GetParent(Application.dataPath) + "/" + strFilePath;
//             if (strFileName.EndsWith(".shader"))    //文件扩展名类型
//             {
//                 if (vscode == null || System.Diagnostics.Process.GetProcessesByName("Code").Length <= 0)
//                 {
//                     vscode = new System.Diagnostics.Process();
//                     vscode.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
//                     vscode.StartInfo.UseShellExecute = true;
//                     vscode.StartInfo.WorkingDirectory = strFileName;
//                     vscode.StartInfo.LoadUserProfile = true;
//                     vscode.StartInfo.FileName = @"H:\Sotfware-Compiler\Microsoft VS Code\Code.exe";
//                 }
//                 if (vscode != null)
//                 {
//                     vscode.StartInfo.Arguments = string.Concat("\"", strFileName, "\"");
//                     vscode.Start();
//                     return true;
//                 }
//             }
//             return false;
//         }
//     }
// }
