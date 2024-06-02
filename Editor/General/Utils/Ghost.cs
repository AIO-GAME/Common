#region

using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;

#endregion

namespace AIO.UEditor
{
    public static partial class EHelper
    {
        #region Nested type: Ghost

        /// <summary>
        /// 安装管理 Ghost
        /// </summary>
        public static class Ghost
        {
            private const string ScopeName_OpenUpmCN = "package.openupm.cn";
            private const string ScopeName_OpenUpm   = "package.openupm.com";
            private const string ScopeUrl_OpenUpmCN  = "https://package.openupm.cn";
            private const string ScopeUrl_OpenUpm    = "https://package.openupm.com";

            /// <summary>
            /// OpenUpm 安装
            /// </summary>
            /// <param name="scopes">Scopes</param>
            /// <param name="version">版本</param>
            /// <param name="isCN">是否为国区</param>
            public static async void InstallOpenUpm(string scopes, string version, bool isCN = false)
            {
                AssetDatabase.SaveAssets();
                string name;
                string url;
                if (isCN)
                {
                    name = ScopeName_OpenUpmCN;
                    url  = ScopeUrl_OpenUpmCN;
                }
                else
                {
                    name = ScopeName_OpenUpm;
                    url  = ScopeUrl_OpenUpm;
                }

                var path = System.IO.Path.Combine(Path.Packages, "manifest.json");
                var manifest = await AHelper.IO.ReadJsonUTF8Async<JObject>(path);
                if (manifest.Value<JObject>("dependencies") is JObject dependencies) dependencies[scopes] = version;
                if (manifest["scopedRegistries"] is JArray scopedRegistries)
                {
                    foreach (var table in scopedRegistries.Where(entry => entry.Value<string>("name") == name))
                    {
                        table["url"] = url;
                        if (table.Value<JArray>("scopes") is JArray array) array.Add(scopes);
                        else table["scopes"] = new JArray { scopes };
                        goto save;
                    }

                    scopedRegistries.Add(new JObject
                    {
                        ["name"]   = name,
                        ["url"]    = url,
                        ["scopes"] = new JArray { scopes }
                    });
                }
                else
                {
                    manifest["scopedRegistries"] = new JArray
                    {
                        new JObject
                        {
                            ["name"]   = name,
                            ["url"]    = url,
                            ["scopes"] = new JArray { scopes }
                        }
                    };
                }

                save: ;
                await AHelper.IO.WriteJsonUTF8Async(path, manifest);
                // 判断是否允许自动刷新
                if (EditorPrefs.GetBool("AllowAutoRefresh")) AssetDatabase.Refresh();
            }

            /// <summary>
            /// OpenUpm 安装
            /// </summary>
            /// <param name="scopes">Scopes</param>
            /// <param name="isCN">是否为国区</param>
            public static async void UnInstallOpenUpm(string scopes, bool isCN = false)
            {
                AssetDatabase.SaveAssets();
                var name     = isCN ? ScopeName_OpenUpmCN : ScopeName_OpenUpm;
                var path     = System.IO.Path.Combine(Path.Packages, "manifest.json");
                var manifest = await AHelper.IO.ReadJsonUTF8Async<JObject>(path);
                manifest.Value<JObject>("dependencies")?.Remove(scopes);
                if (manifest.Value<JArray>("scopedRegistries") is JArray scopedRegistries)
                {
                    for (var index = scopedRegistries.Count - 1; index >= 0; index--)
                    {
                        if (scopedRegistries[index].Value<string>("name") != name) continue;
                        if (scopedRegistries[index].Value<JArray>("scopes") is JArray scopesArray)
                        {
                            for (var i = scopesArray.Count - 1; i >= 0; i--)
                                if (scopesArray[i].Value<string>() == scopes)
                                {
                                    scopesArray.RemoveAt(i);
                                    break;
                                }

                            if (scopesArray.Count == 0) scopedRegistries.RemoveAt(index);
                        }
                        else
                        {
                            scopedRegistries.RemoveAt(index);
                        }
                    }

                    if (scopedRegistries.Count == 0) manifest.Remove("scopedRegistries");
                }

                await AHelper.IO.WriteJsonUTF8Async(path, manifest);
                AssetDatabase.Refresh();
            }
        }

        #endregion
    }
}