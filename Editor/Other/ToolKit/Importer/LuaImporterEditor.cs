using UnityEditor;
#if UNITY_2019_1_OR_NEWER
using UnityEditor.AssetImporters;
#endif
#if UNITY_2018_1_OR_NEWER && !UNITY_2020_1_OR_NEWER
using UnityEditor.Experimental.AssetImporters;
#endif
namespace AIO.UEditor
{
    [CustomEditor(typeof(LuaImporter))]
    public class LuaImporterEditor : ScriptedImporterEditor
    {
#if UNITY_2019_1_OR_NEWER
        //Let the parent class know that the Apply/Revert mechanism is skipped.
        protected override bool needsApplyRevert => false;
#endif
        public override void OnInspectorGUI()
        {
        }
    }
}
