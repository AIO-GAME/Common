#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// Unity Settings Helper
    /// </summary>
    public static class SettingHelper
    {
        /// <summary>
        /// 加载相关的配置文件
        /// </summary>
        public static TSetting LoadSetting<TSetting>()
        where TSetting : ScriptableObject
        {
            var settingType = typeof(TSetting);
            var guids = AssetDatabase.FindAssets($"t:{settingType.Name}");
            if (guids.Length == 0)
            {
                Debug.LogWarning($"Create new {settingType.Name}.asset");
                var setting = ScriptableObject.CreateInstance<TSetting>();
                var filePath = $"Assets/{settingType.Name}.asset";
                AssetDatabase.CreateAsset(setting, filePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                return setting;
            }
            else
            {
                if (guids.Length != 1)
                {
                    foreach (var guid in guids)
                    {
                        var path = AssetDatabase.GUIDToAssetPath(guid);
                        Debug.LogWarning($"Found multiple file : {path}");
                    }

                    throw new Exception($"Found multiple {settingType.Name} files !");
                }

                var filePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                var setting = AssetDatabase.LoadAssetAtPath<TSetting>(filePath);
                return setting;
            }
        }

        /// <summary>
        /// 加载相关的配置文件
        /// </summary>
        public static ScriptableObject LoadSetting(Type settingType)
        {
            if (settingType is null || !settingType.IsSubclassOf(typeof(ScriptableObject)))
                throw new ArgumentNullException(nameof(settingType));

            var guids = AssetDatabase.FindAssets($"t:{settingType.Name}");
            if (guids.Length == 0)
            {
                Debug.LogWarning($"Create new {settingType.Name}.asset");
                var setting = ScriptableObject.CreateInstance(settingType);
                var filePath = $"Assets/{settingType.Name}.asset";
                AssetDatabase.CreateAsset(setting, filePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                return setting;
            }
            else
            {
                if (guids.Length != 1)
                {
                    foreach (var guid in guids)
                    {
                        var path = AssetDatabase.GUIDToAssetPath(guid);
                        Debug.LogWarning($"Found multiple file : {path}");
                    }

                    throw new Exception($"Found multiple {settingType.Name} files !");
                }

                var filePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                var setting = AssetDatabase.LoadAssetAtPath(filePath, settingType);
                return (ScriptableObject)setting;
            }
        }

        #region Nested type: Layer

        public static class Layer
        {
            /// <summary>
            /// 获取所有层级信息
            /// </summary>
            /// <returns>key层级值,value层级名</returns>
            public static IDictionary<int, string> GetInfo()
            {
                Type type = null;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
#if UNITY_2020_1_OR_NEWER
                    if (assembly.GetName().Name.StartsWith("UnityEditor.CoreModule"))
#else
                    if (assembly.GetName().Name.StartsWith("UnityEditor"))
#endif
                    {
                        type = assembly.GetType("UnityEditor.TagManager");
                        if (type != null) break;
                    }
                }

                if (type == null) return new Dictionary<int, string>();
                var GetDefinedLayerCount =
                    type.GetMethod("GetDefinedLayerCount", BindingFlags.Static | BindingFlags.NonPublic);
                if (GetDefinedLayerCount is null)
                {
                    Debug.LogError("GetDefinedLayerCount is null");
                    return new Dictionary<int, string>();
                }

                var GetDefinedLayers = type.GetMethod("Internal_GetDefinedLayers",
                                                      BindingFlags.Static | BindingFlags.NonPublic);
                if (GetDefinedLayers is null)
                {
                    Debug.LogError("GetDefinedLayers is null");
                    return new Dictionary<int, string>();
                }

                var definedLayerCount = (int)GetDefinedLayerCount.Invoke(null, null);
                var layerNames = new string[definedLayerCount];
                var layerValues = new int[definedLayerCount];
                var parameters = new object[] { layerNames, layerValues };
                layerNames  = (string[])parameters[0];
                layerValues = (int[])parameters[1];
                GetDefinedLayers.Invoke(null, parameters);
                var dic = new Dictionary<int, string>();

                for (var i = 0; i < definedLayerCount; i++) dic.Add(AHelper.Math.Log(layerValues[i]), layerNames[i]);

                return dic;
            }

            /// <summary>
            /// 设置层级信息
            /// </summary>
            /// <param name="layerIndex">0-31</param>
            /// <param name="nameValue">层级名</param>
            public static void Set(byte layerIndex, string nameValue)
            {
                if (layerIndex >= 31) return;
                var str = AHelper.IO.ReadUTF8("ProjectSettings/TagManager.asset");
                var headerIndex = str.IndexOf("TagManager:", StringComparison.CurrentCulture);
                var header = str.Substring(0, headerIndex);
                var sb = new StringBuilder(str.Substring(headerIndex));
                var asset = AHelper.Yaml.Deserialize<Hashtable>(sb.ToString());
                var TagManager = (Dictionary<object, object>)asset["TagManager"];
                var layers = (List<object>)TagManager["layers"];
                layers[layerIndex]   = nameValue;
                TagManager["layers"] = layers;
                sb.Clear();
                sb.Append(header);
                sb.Append(AHelper.Yaml.Serialize(asset));
                AHelper.IO.WriteUTF8("ProjectSettings/TagManager.asset", sb);
            }

            /// <summary>
            /// 添加层级信息
            /// </summary>
            /// <param name="agr">层级信息</param>
            /// <param name="order">Ture:从头开始 False:从尾开始</param>
            public static void Add(string agr, bool order = true)
            {
                Add(new[] { agr }, order);
            }

            /// <summary>
            /// 添加层级信息
            /// </summary>
            /// <param name="agrList">层级信息</param>
            /// <param name="order">Ture:从头开始 False:从尾开始</param>
            public static void Add(IList<string> agrList, bool order = true)
            {
                if (agrList.Count <= 0) return;
                var str = AHelper.IO.ReadUTF8("ProjectSettings/TagManager.asset");
                var headerIndex = str.IndexOf("TagManager:", StringComparison.CurrentCulture);
                var header = str.Substring(0, headerIndex);
                var sb = new StringBuilder(str.Substring(headerIndex));
                var asset = AHelper.Yaml.Deserialize<Hashtable>(sb.ToString());
                var TagManager = (Dictionary<object, object>)asset["TagManager"];
                var layers = (List<object>)TagManager["layers"];
                var index = 0;
                if (order)
                    for (var i = 0; i < 32; i++)
                    {
                        if (layers.Count <= i) layers.Add(null);
                        if (layers[i] != null) continue;
                        if (i <= 7) continue;
                        if (index < agrList.Count) layers[i] = agrList[index++];
                    }
                else
                    for (var i = 31; i >= 0; i--)
                    {
                        if (layers.Count <= i) layers.Add(null);
                        if (layers[i] != null) continue;
                        if (i <= 7) continue;
                        if (index < agrList.Count) layers[i] = agrList[index++];
                    }

                TagManager["layers"] = layers;
                sb.Clear();
                sb.Append(header);
                sb.Append(AHelper.Yaml.Serialize(asset));
                AHelper.IO.WriteUTF8("ProjectSettings/TagManager.asset", sb);
            }

            /// <summary>
            /// 判断是否有该层级
            /// </summary>
            public static bool Has(int value)
            {
                return GetInfo().ContainsKey(value);
            }

            /// <summary>
            /// 判断是否有该层级
            /// </summary>
            public static bool Has(string value)
            {
                return GetInfo().Any(item => value == item.Value);
            }

            /// <summary>
            /// 判断是否有该层级
            /// </summary>
            public static bool Has(int index, string value)
            {
                var info = GetInfo();
                return info.ContainsKey(index) && info[index] == value;
            }
        }

        #endregion
    }
}