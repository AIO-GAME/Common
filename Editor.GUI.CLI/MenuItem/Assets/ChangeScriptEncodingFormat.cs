#region

using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    public class ChangeScriptEncodingFormat
    {
        // 添加一个右键菜单。
        // % 按下ctrl时显示菜单。（Windows: control, macOS: command）
        // & 按下alt时显示菜单。(Windows/Linux: alt, macOS: option)
        // _ 按下shift时显示菜单。(Windows/Linux/macOS: shift)
        [MenuItem("Assets/TextAsset/GB2312->UTF8 No BOM", false, 100)]
        private static void CustomMenu()
        {
            // 例如: 获取Project视图中选定的对象
            var selectedObject = Selection.activeObject;

            if (selectedObject != null)
            {
                // 获取选定对象的相对路径
                var relativeAssetPath = AssetDatabase.GetAssetPath(selectedObject);
                // 获取项目根目录路径
                var projectPath = Path.GetDirectoryName(Application.dataPath);
                if (string.IsNullOrEmpty(projectPath)) throw new DirectoryNotFoundException("项目根目录路径为空");
                // 获取选定对象的绝对路径
                var absoluteAssetPath = Path.Combine(projectPath, relativeAssetPath);
                // 获取选定对象的文件名（包括后缀）
                var fileName = Path.GetFileName(relativeAssetPath);

                Debug.Log("执行自定义操作: " + selectedObject.name +
                          ", 相对路径: " + relativeAssetPath +
                          ", 绝对路径: " + absoluteAssetPath +
                          ", 文件名: " + fileName);

                //判断是否是文本文件
                if (IsTextTypeFile(fileName))
                    ChangeFormat(absoluteAssetPath);
                else
                    Debug.Log("不是文本文件");
            }
            else
            {
                Debug.LogWarning("没有选中任何对象.");
            }
        }

        // 如果项目视图中有选中的对象，则启用右键菜单项
        [MenuItem("Assets/脚本改格式：GB2312->UTF8无BOM", true)]
        private static bool ValidateCustomMenu() { return Selection.activeObject != null; }

        /// <summary>
        /// 判断该文件是否是CSharp文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool IsTextTypeFile(string fileName)
        {
            // 将扩展名转换为小写并与 ".cs" 进行比较
            var ext = Path.GetExtension(fileName).ToLower();
            return ext == ".cs" ||
                   ext == ".txt" ||
                   ext == ".json" ||
                   ext == ".xml" ||
                   ext == ".lua" ||
                   ext == ".shader" ||
                   ext == ".html" ||
                   ext == ".htm" ||
                   ext == ".js" ||
                   ext == ".css" ||
                   ext == ".md" ||
                   ext == ".yml" ||
                   ext == ".yaml" ||
                   ext == ".py"
                ;
        }

        /// <summary>
        /// 文件格式转码：GB2312转成UTF8
        /// 读取指定的文件，转换成UTF8（无BOM标记）格式后，回写覆盖原文件
        /// </summary>
        /// <param name="sourceFilePath">文件路径</param>
        public static void ChangeFormat(string sourceFilePath)
        {
            var fileContent = File.ReadAllText(sourceFilePath, Encoding.GetEncoding("GB2312"));
            File.WriteAllText(sourceFilePath, fileContent, Encoding.UTF8);
            Debug.Log("处理结束！");
        }
    }
}