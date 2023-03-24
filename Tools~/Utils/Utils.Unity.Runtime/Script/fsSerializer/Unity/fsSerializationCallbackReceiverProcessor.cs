using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace AIO.Unity
{
    public class fsSerializationCallbackReceiverProcessor : fsObjectProcessor
    {
        public override bool CanProcess(in Type type)
        {
            return typeof(ISerializationCallbackReceiver).IsAssignableFrom(type);
        }

        public override void OnBeforeSerialize(in Type storageType, in object instance)
        {
            // Don't call the callback on null instances.
            if (instance == null || instance is UnityObject)
            {
                return;
            }

            ((ISerializationCallbackReceiver)instance).OnBeforeSerialize();
        }

        public override void OnAfterDeserialize(in Type storageType, in object instance)
        {
            // Don't call the callback on null instances.
            if (instance == null || instance is UnityObject)
            {
                return;
            }

            ((ISerializationCallbackReceiver)instance).OnAfterDeserialize();
        }
    }
}