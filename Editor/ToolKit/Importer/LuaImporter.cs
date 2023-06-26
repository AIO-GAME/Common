using System;
using System.Collections;
using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;
using AIO;

namespace UnityEditor
{
    [ScriptedImporter(0, "lua")]
    public class LuaImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // MonoImporter
            var asset = new TextAsset(File.ReadAllText(ctx.assetPath));
            ctx.AddObjectToAsset("main obj", asset);
            ctx.SetMainObject(asset);
        }
    }

    public static class ImporterUtils
    {
        // [MenuItem("Tools/Test")]
        // public static void Test()
        // {
        //     ChangeIcon(@"E:\TencentGit\AIO20200337\Assets\Script\Editor\Resources\test.lua", @"E:\TencentGit\AIO20200337\Assets\Script\Editor\Resources\code.ico");
        // }

        /// <summary>
        /// 修改资源ICON
        /// </summary>
        /// <param name="assetPath"></param>
        /// <param name="iconPath"></param>
        public static void ChangeIcon(string assetPath, string iconPath)
        {
            var guid = GetGUId(iconPath);
            if (string.IsNullOrEmpty(guid)) throw new ArgumentNullException(iconPath, "icon guid is null");

            var assetdata = GetMetaData(assetPath);
            if (assetdata == null) return;

            var dictionary = (IDictionary)assetdata;
            var ImporterName = "DefaultImporter";
            foreach (string key in dictionary.Keys)
            {
                if (key.Contains("Importer"))
                {
                    ImporterName = key;
                    break;
                }
            }

            var importer = (IDictionary)assetdata.Get(ImporterName);
            var hash = new Hashtable();
            hash.Set("instanceID", guid);
            if (importer != null) importer.Set("icon", hash);
            UtilsGen.IO.WriteUTF8(assetPath, UtilsGen.Yaml.Serialize(assetdata));
        }

        public static Hashtable GetMetaData(in string path)
        {
            FileInfo info;
            if (!Path.GetExtension(path).Contains("meta"))
                info = new FileInfo(string.Concat(path, ".meta"));
            else info = new FileInfo(path);
            if (!info.Exists) return null;
            return UtilsGen.Yaml.Deserialize<Hashtable>(UtilsGen.IO.ReadUTF8(info.FullName));
        }

        public static string GetGUId(in string path)
        {
            var data = GetMetaData(path);
            if (data == null) return null;
            var guid = data.Get<string>("guid");
            if (string.IsNullOrEmpty(guid)) throw new ArgumentNullException(path, "icon guid is null");
            return guid;
        }
    }
}