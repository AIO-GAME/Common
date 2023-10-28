/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-07-04
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// Unity Settings Helper
    /// </summary>
    public static partial class SettingHelper
    {
        public static partial class Layer
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
                var GetDefinedLayerCount = type.GetMethod("GetDefinedLayerCount", BindingFlags.Static | BindingFlags.NonPublic);
                if (GetDefinedLayerCount is null)
                {
                    Debug.LogError("GetDefinedLayerCount is null");
                    return new Dictionary<int, string>();
                }

                var GetDefinedLayers = type.GetMethod("Internal_GetDefinedLayers", BindingFlags.Static | BindingFlags.NonPublic);
                if (GetDefinedLayers is null)
                {
                    Debug.LogError("GetDefinedLayers is null");
                    return new Dictionary<int, string>();
                }

                var definedLayerCount = (int)GetDefinedLayerCount.Invoke(null, null);
                var layerNames = new string[definedLayerCount];
                var layerValues = new int[definedLayerCount];
                var parameters = new object[] { layerNames, layerValues };
                layerNames = (string[])parameters[0];
                layerValues = (int[])parameters[1];
                GetDefinedLayers.Invoke(null, parameters);
                var dic = new Dictionary<int, string>();

                for (var i = 0; i < definedLayerCount; i++)
                {
                    dic.Add(AHelper.Math.Log(layerValues[i]), layerNames[i]);
                }

                return dic;
            }

            /// <summary>
            /// 设置层级信息
            /// </summary>
            /// <param name="layerIndex">0-31</param>
            /// <param name="nameValue">层级名</param>
            public static void Set(byte layerIndex, string nameValue)
            {
#if !UNITY_2020_1_OR_NEWER
                return;
#else
                if (layerIndex >= 31) return;
                var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("ProjectSettings/TagManager.asset");
                var objects = new SerializedObject(asset);
                var layers = objects.FindProperty("layers");
                layers.GetArrayElementAtIndex(layerIndex).stringValue = nameValue;
                objects.UpdateIfRequiredOrScript();
                objects.ApplyModifiedProperties();
                AssetDatabase.SaveAssetIfDirty(asset);
#endif
            }

            /// <summary>
            /// 添加层级信息
            /// </summary>
            /// <param name="agr">层级信息</param>
            /// <param name="order">Ture:从头开始 False:从尾开始</param>
            public static void Add(string agr, bool order = true)
            {
                Add(new string[] { agr }, order);
            }

            /// <summary>
            /// 添加层级信息
            /// </summary>
            /// <param name="agrList">层级信息</param>
            /// <param name="order">Ture:从头开始 False:从尾开始</param>
            public static void Add(IList<string> agrList, bool order = true)
            {
#if !UNITY_2020_1_OR_NEWER
                return;
#else
                if (agrList.Count <= 0) return;
                var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("ProjectSettings/TagManager.asset");
                var objects = new SerializedObject(asset);
                var layers = objects.FindProperty("layers");
                var index = 0;
                if (order)
                {
                    for (var i = 4; i < layers.arraySize && index < agrList.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(layers.GetArrayElementAtIndex(i).stringValue)) continue;
                        layers.GetArrayElementAtIndex(i).stringValue = agrList[index++];
                    }
                }
                else
                {
                    for (var i = layers.arraySize - 1; i > 4 && index < agrList.Count; i--)
                    {
                        if (!string.IsNullOrEmpty(layers.GetArrayElementAtIndex(i).stringValue)) continue;
                        layers.GetArrayElementAtIndex(i).stringValue = agrList[index++];
                    }
                }

                objects.UpdateIfRequiredOrScript();
                objects.ApplyModifiedProperties();
                AssetDatabase.SaveAssetIfDirty(asset);
#endif
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
        }
    }
}