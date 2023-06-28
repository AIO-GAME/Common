namespace AIO.Unity
{
    using System;
    using System.Collections.Generic;

    using UnityObject = UnityEngine.Object;

    /// <inheritdoc/>
    public class UnityObjectConverter : fsConverter
    {
        private List<UnityObject> objectReferences => Serializer.Context.Get<List<UnityObject>>();

        /// <inheritdoc/>
        public override bool CanProcess(in Type type)
        {
            return typeof(UnityObject).IsAssignableFrom(type);
        }

        /// <inheritdoc/>
        public override bool RequestCycleSupport(in Type storageType)
        {
            return false;
        }

        /// <inheritdoc/>
        public override bool RequestInheritanceSupport(in Type storageType)
        {
            return false;
        }

        /// <inheritdoc/>
        public override fsResult TrySerialize(in object instance, out fsData serialized, in Type storageType)
        {
            var unityObject = (UnityObject)instance;
            var index = objectReferences.Count;
            serialized = new fsData(index);
            objectReferences.Add(unityObject);
            //Debug.Log($"<color=#88FF00>Serializing:\n<b>#{index}: {unityObject?.GetType().Name} [{unityObject?.GetHashCode()}]</b></color>");
            return fsResult.Success;
        }

        /// <inheritdoc/>
        public override fsResult TryDeserialize(in fsData storage, ref object instance, in Type storageType)
        {
            var index = (int)storage.AsInt64;

            var result = fsResult.Success;

            if (index >= 0 && index < objectReferences.Count)
            {
                var uo = objectReferences[index];
                instance = uo;

                //Debug.Log($"<color=#FF8800>Deserializing:\n<b>#{index}: {instance?.GetType().Name} [{instance?.GetHashCode()}]</b></color>");

                if (instance != null && !storageType.IsInstanceOfType(instance))
                {
                    // Skip the error message if it's just that the instance couldn't be deserialized anymore
                    // and it became a pseudo-null UnityEngine.Object, which seems to be a new thing in Unity 2018.3.
                    // This can happen, for instance, when a applying changes to a prefab that contained a scene object.
                    // IsUnityNull can't be called off the main thread, so we hack it with GetHashCode.
                    if (uo.GetHashCode() != 0)
                    {
                        result.AddMessage($"Object reference at index #{index} does not match target type ({instance.GetType()} != {storageType}). Defaulting to null.");
                    }

                    instance = null;
                }
            }
            else
            {
                result.AddMessage($"No object reference provided at index #{index}. Defaulting to null.");
                instance = null;
            }

            return result;
        }

        /// <inheritdoc/>
        public override object CreateInstance(in fsData data, in Type storageType)
        {
            return storageType;
        }
    }
}
