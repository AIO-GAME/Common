using UnityEditor.AssetImporters;

namespace UnityEditor
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