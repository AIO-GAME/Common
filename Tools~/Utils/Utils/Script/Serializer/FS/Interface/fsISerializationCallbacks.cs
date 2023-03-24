using System;

namespace AIO
{
    /// <summary>
    /// Extend this interface on your type to receive notifications about
    /// serialization/deserialization events. If you don't have access to the
    /// type itself, then you can write an fsObjectProcessor instead.
    /// </summary>
    public interface fsISerializationCallbacks
    {
        /// <summary>
        /// Called before serialization.
        /// </summary>
        void OnBeforeSerialize(Type storageType);

        /// <summary>
        /// Called after serialization.
        /// </summary>
        /// <param name="storageType">
        /// The field/property type that is storing the instance.
        /// </param>
        /// <param name="data">The data that was serialized.</param>
        void OnAfterSerialize(Type storageType, ref fsData data);

        /// <summary>
        /// Called before deserialization.
        /// </summary>
        /// <param name="storageType">
        /// The field/property type that is storing the instance.
        /// </param>
        /// <param name="data">
        /// The data that will be used for deserialization.
        /// </param>
        void OnBeforeDeserialize(Type storageType, ref fsData data);

        /// <summary>
        /// Called after deserialization.
        /// </summary>
        /// <param name="storageType">
        /// The field/property type that is storing the instance.
        /// </param>
        /// <param name="instance">The type of the instance.</param>
        void OnAfterDeserialize(Type storageType);
    }
}