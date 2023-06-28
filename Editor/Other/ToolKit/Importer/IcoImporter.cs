using UnityEditor.AssetImporters;

#if !UNITY_2020_1_OR_NEWER
using UnityEditor.Experimental.AssetImporters;
#endif

namespace AIO.Unity.Editor
{
    [ScriptedImporter(0, "ico")]
    public class IcoImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
        }
    }
}