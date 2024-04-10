﻿#region

using System;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    [Serializable]
    public class ConsoleWindowConfig : ScriptableObject
    {
        private static ConsoleWindowConfig _instance;

        /// <summary>
        /// 白名单
        /// </summary>
        public string[] Assemblies;

        /// <summary>
        /// 黑名单
        /// </summary>
        public string[] BlackList;

        /// <summary>
        /// 获取本地资源包地址
        /// </summary>
        public static ConsoleWindowConfig GetOrCreate()
        {
            if (_instance != null) return _instance;
            var objects = EHelper.IO.GetScriptableObjects<ConsoleWindowConfig>();
            if (objects != null && objects.Length > 0)
                foreach (var asset in objects)
                {
                    if (asset is null) continue;
                    _instance = asset;
                    return _instance;
                }

            if (_instance is null)
            {
                _instance = CreateInstance<ConsoleWindowConfig>();
                _instance.Assemblies = new[]
                {
                    "AIO.Core.Runtime",
                    "AIO.Asset.Runtime",
                    "AIO.FGUI.Runtime",
                    "AIO.Hybridclr.Runtime",
                    "AIO.CLI.YooAsset.Runtime",
                    "Assembly-CSharp-Editor",
                    "Assembly-CSharp-Editor-firstpass",
                    "Assembly-CSharp-firstpass",
                    "Assembly-CSharp",
                };
                _instance.BlackList = new string[]
                    { };
                AssetDatabase.CreateAsset(_instance, $"Assets/Editor/{nameof(ConsoleWindowConfig)}.asset");
                AssetDatabase.SaveAssets();
            }

            return _instance;
        }

        public void Save()
        {
            EditorUtility.SetDirty(this);
        }
    }
}