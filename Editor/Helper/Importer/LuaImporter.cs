using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_2019_1_OR_NEWER
using UnityEditor.AssetImporters;
#endif

#if UNITY_2018_1_OR_NEWER && !UNITY_2020_1_OR_NEWER
using UnityEditor.Experimental.AssetImporters;
#endif
namespace AIO.UEditor
{
    [ScriptedImporter(0, "lua")]
    public class LuaImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var asset = new TextAsset(File.ReadAllText(ctx.assetPath));
            ctx.AddObjectToAsset("main obj", asset);
            ctx.SetMainObject(asset);
        }
    }

    [CustomEditor(typeof(LuaImporter))]
    public class LuaImporterEditor : ScriptedImporterEditor
    {
#if UNITY_2019_1_OR_NEWER
        //Let the parent class know that the Apply/Revert mechanism is skipped.
        protected override bool needsApplyRevert => false;
#endif
        public override void OnInspectorGUI()
        {
        }
    }
}
//     public static class ImporterUtils
//     {
//         // [MenuItem("Tools/Test")]
//         // public static void Test()
//         // {
//         //     ChangeIcon(@"E:\TencentGit\AIO20200337\Assets\Script\Editor\Resources\test.lua", @"E:\TencentGit\AIO20200337\Assets\Script\Editor\Resources\code.ico");
//         // }
//
//         /// <summary>
//         /// 修改资源ICON
//         /// </summary>
//         /// <param name="assetPath"></param>
//         /// <param name="iconPath"></param>
//         public static void ChangeIcon(string assetPath, string iconPath)
//         {
//             var guid = GetGUId(iconPath);
//             if (string.IsNullOrEmpty(guid)) throw new ArgumentNullException(iconPath, "icon guid is null");
//
//             var assetdata = GetMetaData(assetPath);
//             if (assetdata == null) return;
//
//             var dictionary = (IDictionary)assetdata;
//             var ImporterName = "DefaultImporter";
//             foreach (string key in dictionary.Keys)
//             {
//                 if (key.Contains("Importer"))
//                 {
//                     ImporterName = key;
//                     break;
//                 }
//             }
//
//             var importer = (IDictionary)assetdata.Get(ImporterName);
//             var hash = new Hashtable();
//             hash.Set("instanceID", guid);
//             if (importer != null) importer.Set("icon", hash);
//             AHelper.IO.WriteUTF8(assetPath, AHelper.Yaml.Serialize(assetdata));
//         }
//
//         public static Hashtable GetMetaData(in string path)
//         {
//             FileInfo info;
//             if (!Path.GetExtension(path).Contains("meta"))
//                 info = new FileInfo(string.Concat(path, ".meta"));
//             else info = new FileInfo(path);
//             if (!info.Exists) return null;
//             return AHelper.Yaml.Deserialize<Hashtable>(AHelper.IO.ReadUTF8(info.FullName));
//         }
//
//         public static string GetGUId(in string path)
//         {
//             var data = GetMetaData(path);
//             if (data == null) return null;
//             var guid = data.Get<string>("guid");
//             if (string.IsNullOrEmpty(guid)) throw new ArgumentNullException(path, "icon guid is null");
//             return guid;
//         }
//     }
// }
