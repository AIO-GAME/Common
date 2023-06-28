using UnityEditor;
using UnityEditor.AssetImporters;
#if !UNITY_2020_1_OR_NEWER
using UnityEditor.Experimental.AssetImporters;
#endif
namespace AIO.Unity.Editor
{
    [CustomEditor(typeof(LuaImporter))]
    public class LuaImporterEditor : ScriptedImporterEditor
    {
        //Let the parent class know that the Apply/Revert mechanism is skipped.
        protected override bool needsApplyRevert => false;

        public override void OnInspectorGUI()
        {
        }
    }
}