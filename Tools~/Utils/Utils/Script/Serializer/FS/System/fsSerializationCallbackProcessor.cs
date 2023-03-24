using System;

namespace AIO
{
    public class fsSerializationCallbackProcessor : fsObjectProcessor
    {
        public override bool CanProcess(in Type type)
        {
            return typeof(fsISerializationCallbacks).IsAssignableFrom(type);
        }

        public override void OnBeforeSerialize(in Type storageType, in object instance)
        {
            // Don't call the callback on null instances.
            if (instance == null)
            {
                return;
            }

            ((fsISerializationCallbacks)instance).OnBeforeSerialize(storageType);
        }

        public override void OnAfterSerialize(in Type storageType, in object instance, ref fsData data)
        {
            // Don't call the callback on null instances.
            if (instance == null)
            {
                return;
            }

            ((fsISerializationCallbacks)instance).OnAfterSerialize(storageType, ref data);
        }

        public override void OnBeforeDeserializeAfterInstanceCreation(in Type storageType, in object instance, ref fsData data)
        {
            if (instance is fsISerializationCallbacks == false)
            {
                throw new InvalidCastException("Please ensure the converter for " + storageType + " actually returns an instance of it, not an instance of " + instance.GetType());
            }

            ((fsISerializationCallbacks)instance).OnBeforeDeserialize(storageType, ref data);
        }

        public override void OnAfterDeserialize(in Type storageType, in object instance)
        {
            // Don't call the callback on null instances.
            if (instance == null)
            {
                return;
            }

            ((fsISerializationCallbacks)instance).OnAfterDeserialize(storageType);
        }
    }
}