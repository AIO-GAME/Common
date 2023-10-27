/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-08-11
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIO.UEditor
{
    /// <summary>
    /// Dll Switcher Window
    /// </summary>
    [GWindow("Dll Switcher Window", Group = "Tools",
        MinSizeWidth = 600, MinSizeHeight = 600
    )]
    public class DllSwitcherWindow : GraphicWindow
    {
        private enum PathType
        {
            Reference,
            AbsolutePath
        }

        private enum DirectoryType
        {
            Root,
            SpecificDirectory
        }

        public const string DEFAULT_FILE_ID_OF_SCRIPT = "11500000";

        public Object dllFile;

        public Object replaceDir;

        public Object srcDir;

        private Dictionary<string, string> fileIDMappingTableFromDll;

        private Dictionary<string, string> guidMappingTableFromScripts;

        private string guidOfDllFile;

        private string dllFilePath;

        private const int PreLabelWidth = 140;

        private PathType dllInputPath;

        private DirectoryType srcDirectory;

        private DirectoryType resDirectory;

        [MenuItem("Tools/Window/Dll Switcher")]
        public static void ShowWindow()
        {
            EHelper.Window.Open<DllSwitcherWindow>(MenuItem_Tools.DockedWindowTypes);
        }

        protected override void OnGUI()
        {
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            //IL_00b2: Unknown result type (might be due to invalid IL or missing references)
            //IL_0145: Unknown result type (might be due to invalid IL or missing references)
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Dll File", GUILayout.MaxWidth(140f));
            dllInputPath = (PathType)EditorGUILayout.EnumPopup(dllInputPath, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();
            if (dllInputPath.Equals(PathType.Reference))
            {
                dllFile = EditorGUILayout.ObjectField(dllFile, typeof(DefaultAsset), false);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                dllFilePath = EditorGUILayout.TextField(dllFilePath);
                if (GUILayout.Button("Select", GUILayout.MaxWidth(50)))
                {
                    dllFilePath = EditorUtility.OpenFilePanel("Select Dll File", dllFilePath, "dll");
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Source Code Directory", GUILayout.MaxWidth(140f));
            srcDirectory = (DirectoryType)EditorGUILayout.EnumPopup(srcDirectory, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();
            if (srcDirectory.Equals(DirectoryType.SpecificDirectory))
            {
                srcDir = EditorGUILayout.ObjectField(srcDir, typeof(Object), false);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Replace Dirctory", GUILayout.MaxWidth(140f));
            resDirectory = (DirectoryType)EditorGUILayout.EnumPopup(resDirectory, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();
            if (resDirectory.Equals(DirectoryType.SpecificDirectory))
            {
                replaceDir = EditorGUILayout.ObjectField(replaceDir, typeof(Object), false);
            }

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Replace From Dll To Src"))
            {
                replaceSriptReference(true);
            }

            if (GUILayout.Button("Replace From Src To Dll"))
            {
                replaceSriptReference(false);
            }

            EditorGUILayout.EndHorizontal();
        }

        public void replaceSriptReference(bool dllToSrc)
        {
            if (resDirectory.Equals(DirectoryType.SpecificDirectory))
            {
                replaceSriptReferenceOfSelectDirectory(dllToSrc);
            }
            else
            {
                replaceSriptReferenceOfAllScripts(dllToSrc);
            }
        }

        public void replaceSriptReferenceOfSelectDirectory(bool dllToSrc = true)
        {
            var assetPath = AssetDatabase.GetAssetPath(replaceDir);
            initFileIDMappingTableOfDll(dllToSrc);
            InitGuidMappingTable(dllToSrc);
            ReplaceSriptReferenceOfPath(assetPath, dllToSrc);
        }

        public void replaceSriptReferenceOfAllScripts(bool dllToSrc = true)
        {
            initFileIDMappingTableOfDll(dllToSrc);
            InitGuidMappingTable(dllToSrc);
            ReplaceSriptReferenceOfPath(Application.dataPath, dllToSrc);
        }

        private void ReplaceSriptReferenceOfPath(string path, bool dllToSrc = true)
        {
            var list = FindAllFileWithSuffixs(path, new string[] { ".asset", ".prefab", ".unity" });
            for (var i = 0; i < list.Count; i++)
            {
                EditorUtility.DisplayProgressBar("Replace Dll", list[i], i * 1f / list.Count);
                ReplaceScriptReference(list[i], dllToSrc);
            }

            AssetDatabase.Refresh();
            EditorUtility.ClearProgressBar();
        }

        private void initFileIDMappingTableOfDll(bool dllToSrc = true)
        {
            if (dllInputPath.Equals(PathType.Reference))
            {
                dllFilePath = AssetDatabase.GetAssetPath(dllFile);
            }

            fileIDMappingTableFromDll = new Dictionary<string, string>();
            var assembly = Assembly.LoadFrom(dllFilePath);
            Type[] array;
            try
            {
                array = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                array = ex.Types.Where(t => t != null).ToArray();
                var loaderExceptions = ex.LoaderExceptions;
                for (var i = 0; i < loaderExceptions.Length; i++)
                {
                    Debug.LogWarning(loaderExceptions[i]);
                }
            }

            var array2 = array;
            foreach (var type in array2)
            {
                if (dllToSrc)
                {
                    if (fileIDMappingTableFromDll.ContainsKey(AHelper.FileID.Compute(type).ToString()))
                    {
                        Debug.LogWarning(string.Concat("Reduplicated GUID:", AHelper.FileID.Compute(type).ToString(),
                            ";Script Name:", type.Name));
                    }
                    else
                    {
                        fileIDMappingTableFromDll[AHelper.FileID.Compute(type).ToString()] = type.Name;
                    }
                }
                else
                {
                    fileIDMappingTableFromDll[type.Name] = AHelper.FileID.Compute(type).ToString();
                }
            }

            if (!dllToSrc)
            {
                initGuidOfDllFile();
            }
        }

        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            Debug.LogWarning(string.Concat("Need Loading:", args.Name));
            return Assembly.ReflectionOnlyLoad(string.Concat(
                dllFilePath.Substring(0, dllFilePath.LastIndexOf("\\", StringComparison.CurrentCulture)), "\\",
                args.Name));
        }

        private void InitGuidMappingTable(bool dllToSrc)
        {
            if (srcDirectory.Equals(DirectoryType.SpecificDirectory))
            {
                InitGuidMappingTableOfSelectScripts(dllToSrc);
            }
            else
            {
                InitGuidMappingTableOfAllScripts(dllToSrc);
            }
        }

        private void InitGuidMappingTableOfAllScripts(bool dllToSrc = true)
        {
            guidMappingTableFromScripts = new Dictionary<string, string>();
            InitGuidMappingTableOfPath(Application.dataPath, dllToSrc);
        }

        private void InitGuidMappingTableOfSelectScripts(bool dllToSrc = true)
        {
            guidMappingTableFromScripts = new Dictionary<string, string>();
            var assetPath = AssetDatabase.GetAssetPath(srcDir);
            InitGuidMappingTableOfPath(assetPath, dllToSrc);
        }

        private void InitGuidMappingTableOfPath(string path, bool dllToSrc = true)
        {
            foreach (var item in FindAllFileWithSuffixs(path, new string[] { ".cs.meta", ".js.meta" }))
            {
                if (dllToSrc)
                {
                    guidMappingTableFromScripts[getFileNameFromPath(item)] = GetGuidFromMeta(item);
                }
                else
                {
                    guidMappingTableFromScripts[GetGuidFromMeta(item)] = getFileNameFromPath(item);
                }
            }
        }

        private void initGuidOfDllFile()
        {
            guidOfDllFile = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(dllFile));
        }

        private void ReplaceScriptReference(string filePath, bool dllToSrc = true)
        {
            Debug.Log(string.Concat("Ready to replace:", filePath));
            var array = File.ReadAllLines(filePath);
            var i = 0;
            var flag = false;
            for (; i < array.Length; i++)
            {
                if (array[i].StartsWith("MonoBehaviour:"))
                {
                    do
                    {
                        i++;
                    } while (!array[i].TrimStart().StartsWith("m_Script:"));

                    flag = ((!dllToSrc)
                        ? (flag | replaceGUIDAnfFileIDFromSrcToDll(ref array[i]))
                        : (flag | replaceGUIDAnfFileIDFromDllToSrc(ref array[i])));
                }
            }

            if (flag)
            {
                File.WriteAllLines(filePath, array);
            }
        }

        private string GetGuidFromMeta(string filePath)
        {
            var result = "";
            using (var streamReader = new StreamReader(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    var text = streamReader.ReadLine();
                    if (text.StartsWith("guid:"))
                    {
                        result = text.Substring(text.IndexOf(":", StringComparison.CurrentCulture) + 2);
                        break;
                    }
                }

                streamReader.Close();
                return result;
            }
        }

        private bool replaceGUIDAnfFileIDFromDllToSrc(ref string lineStr)
        {
            var result = false;
            var fileIDFrommScriptReferenceLine = getFileIDFrommScriptReferenceLine(lineStr);
            if (fileIDFrommScriptReferenceLine == null || fileIDFrommScriptReferenceLine.Equals("11500000"))
            {
                return false;
            }

            if (fileIDMappingTableFromDll.TryGetValue(fileIDFrommScriptReferenceLine, out var value))
            {
                if (guidMappingTableFromScripts.TryGetValue(value, out var value2))
                {
                    var gUIDFrommScriptReferenceLine = getGUIDFrommScriptReferenceLine(lineStr);
                    Debug.Log(string.Concat("Replacing script reference:", value));
                    lineStr = lineStr.Replace(fileIDFrommScriptReferenceLine, "11500000");
                    lineStr = lineStr.Replace(gUIDFrommScriptReferenceLine, value2);
                    result = true;
                }
                else
                {
                    Debug.LogWarning(string.Concat("Can't find the GUID of file:", value));
                }
            }
            else
            {
                Debug.LogWarning(string.Concat("Can't find the file of fileID:", fileIDFrommScriptReferenceLine));
            }

            return result;
        }

        private bool replaceGUIDAnfFileIDFromSrcToDll(ref string lineStr)
        {
            var result = false;
            var gUIDFrommScriptReferenceLine = getGUIDFrommScriptReferenceLine(lineStr);
            if (gUIDFrommScriptReferenceLine == null)
            {
                return false;
            }

            if (guidMappingTableFromScripts.TryGetValue(gUIDFrommScriptReferenceLine, out var value))
            {
                if (fileIDMappingTableFromDll.TryGetValue(value, out var value2))
                {
                    Debug.Log(string.Concat("Replacing script reference:", value));
                    lineStr = lineStr.Replace("11500000", value2);
                    lineStr = lineStr.Replace(gUIDFrommScriptReferenceLine, guidOfDllFile);
                    result = true;
                }
                else
                {
                    Debug.LogWarning(string.Concat("Can't find the GUID of file:", value));
                }
            }
            else
            {
                Debug.LogWarning(string.Concat("Can't find the file of GUID:", gUIDFrommScriptReferenceLine));
            }

            return result;
        }

        private string getFileIDFrommScriptReferenceLine(string lineStr)
        {
            var num = lineStr.IndexOf("fileID:", StringComparison.CurrentCulture) + "fileID: ".Length;
            var num2 = lineStr.IndexOf(",", StringComparison.CurrentCulture) - num;
            if (num2 <= 0)
            {
                return null;
            }

            return lineStr.Substring(num, num2);
        }

        private string getGUIDFrommScriptReferenceLine(string lineStr)
        {
            var num = lineStr.IndexOf("guid:", StringComparison.CurrentCulture) + "guid: ".Length;
            var num2 = lineStr.LastIndexOf(",", StringComparison.CurrentCulture) - num;
            if (num2 <= 0)
            {
                return null;
            }

            return lineStr.Substring(num, num2);
        }

        private string getFileNameFromPath(string path)
        {
            var text = path.Substring(path.LastIndexOf("\\", StringComparison.CurrentCulture) + 1);
            return text.Substring(0, text.IndexOf(".", StringComparison.CurrentCulture));
        }

        private List<string> FindAllFileWithSuffixs(string path, string[] suffixs)
        {
            var resultList = new List<string>();
            FindAllFileWithSuffixs(path, suffixs, ref resultList);
            return resultList;
        }

        private void FindAllFileWithSuffixs(string path, string[] suffixs, ref List<string> resultList)
        {
            if (File.Exists(path))
            {
                resultList.Add(path);
            }
            else
            {
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                var files = Directory.GetFiles(path);
                foreach (var text in files)
                {
                    foreach (var value in suffixs)
                    {
                        if (text.EndsWith(value))
                        {
                            resultList.Add(text);
                            break;
                        }
                    }
                }

                files = Directory.GetDirectories(path);
                foreach (var path2 in files)
                {
                    FindAllFileWithSuffixs(path2, suffixs, ref resultList);
                }
            }
        }

        private void WriteDebugFile(string[] lines, string filename)
        {
        }
    }
}