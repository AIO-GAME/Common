/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-07
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

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
    // [ScriptedImporter(1, "xml")]
    // public class XMLImporter : ScriptedImporter
    // {
    //     public override void OnImportAsset(AssetImportContext ctx)
    //     {
    //         var asset = new TextAsset(File.ReadAllText(ctx.assetPath));
    //         ctx.AddObjectToAsset("main obj", asset);
    //         ctx.SetMainObject(asset);
    //     }
    // }

    public class XMLImporterEditor : ScriptedImporterEditor
    {
#if UNITY_2019_1_OR_NEWER
        //Let the parent class know that the Apply/Revert mechanism is skipped.
        protected override bool needsApplyRevert => base.needsApplyRevert;
#endif
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}