/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-07
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.IO;
using UnityEngine;
#if UNITY_2019_1_OR_NEWER
using UnityEditor.AssetImporters;
#endif

#if UNITY_2018_1_OR_NEWER && !UNITY_2020_1_OR_NEWER
using UnityEditor.Experimental.AssetImporters;
#endif
namespace AIO.UEditor
{
    [ScriptedImporter(0, "sql")]
    public class SqlImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var asset = new TextAsset(File.ReadAllText(ctx.assetPath));
            ctx.AddObjectToAsset("main obj", asset);
            ctx.SetMainObject(asset);
        }
    }
}