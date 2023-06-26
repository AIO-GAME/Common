using System;

namespace UnityEditor
{
    /// <summary>
    /// 序列化属性扩展
    /// </summary>
    public static partial class SerializedPropertyExtend
    {
        /// <summary>
        /// 对每个SerializedObject使用一个新的SerializedProperty执行一次' action '。或者如果只有一个目标，它使用' property '。
        /// </summary>
        public static void ForEachTarget(this SerializedProperty property,
            Action<SerializedProperty> function,
            string undoName = "Inspector")
        {
            var targets = property.serializedObject.targetObjects;

            if (undoName != null)
                Undo.RecordObjects(targets, undoName);

            if (targets.Length == 1)
            {
                function(property);
                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                var path = property.propertyPath;
                for (var i = 0; i < targets.Length; i++)
                {
                    using (var serializedObject = new SerializedObject(targets[i]))
                    {
                        property = serializedObject.FindProperty(path);
                        function(property);
                        property.serializedObject.ApplyModifiedProperties();
                    }
                }
            }
        }
    }
}