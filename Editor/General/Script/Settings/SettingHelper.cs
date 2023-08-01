/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-04
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEditor
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
                    if (assembly.GetName().Name.StartsWith("UnityEditor.CoreModule"))
                    {
                        type = assembly.GetType("UnityEditor.TagManager");
                        break;
                    }
                }

                var GetDefinedLayerCount = type.GetMethod("GetDefinedLayerCount", BindingFlags.Static | BindingFlags.NonPublic);
                var GetDefinedLayers = type.GetMethod("Internal_GetDefinedLayers", BindingFlags.Static | BindingFlags.NonPublic);
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
                    dic.Add(UtilsGen.Math.Log(layerValues[i]), layerNames[i]);
                }

                return dic;
            }

            /// <summary>
            /// 设置层级信息
            /// </summary>
            /// <param name="layerindex">0-31</param>
            /// <param name="namevalue">层级名</param>
            public static void Set(byte layerindex, string namevalue)
            {
                if (layerindex < 0 || layerindex >= 31) return;

                var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("ProjectSettings/TagManager.asset");
                var objects = new SerializedObject(asset);
                var layers = objects.FindProperty("layers");
                layers.GetArrayElementAtIndex(layerindex).stringValue = namevalue;
                objects.UpdateIfRequiredOrScript();
                objects.ApplyModifiedProperties();
#if UNITY_2020_1_OR_NEWER
                AssetDatabase.SaveAssetIfDirty(asset);
#else
                AssetDatabase.SaveAssets();
#endif
            }

            /// <summary>
            /// 添加层级信息
            /// </summary>
            /// <param name="agrs">层级信息</param>
            /// <param name="order">Ture:从头开始 False:从尾开始</param>
            public static void Add(string agrs, bool order = true)
            {
                Add(new string[] { agrs }, order);
            }

            /// <summary>
            /// 添加层级信息
            /// </summary>
            /// <param name="agrs">层级信息</param>
            /// <param name="order">Ture:从头开始 False:从尾开始</param>
            public static void Add(IList<string> agrs, bool order = true)
            {
                if (agrs.Count <= 0) return;
                var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("ProjectSettings/TagManager.asset");
                var objects = new SerializedObject(asset);
                var layers = objects.FindProperty("layers");
                var index = 0;
                if (order)
                {
                    for (var i = 4; i < layers.arraySize && index < agrs.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(layers.GetArrayElementAtIndex(i).stringValue)) continue;
                        layers.GetArrayElementAtIndex(i).stringValue = agrs[index++];
                    }
                }
                else
                {
                    for (var i = layers.arraySize - 1; i > 4 && index < agrs.Count; i--)
                    {
                        if (!string.IsNullOrEmpty(layers.GetArrayElementAtIndex(i).stringValue)) continue;
                        layers.GetArrayElementAtIndex(i).stringValue = agrs[index++];
                    }
                }

                objects.UpdateIfRequiredOrScript();
                objects.ApplyModifiedProperties();
#if UNITY_2020_1_OR_NEWER
                AssetDatabase.SaveAssetIfDirty(asset);
#else
                AssetDatabase.SaveAssets();
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
                foreach (var item in GetInfo())
                {
                    if (value == item.Value) return true;
                }

                return false;
            }
        }
    }
}