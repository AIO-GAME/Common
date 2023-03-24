using System;

namespace AIO
{
    /// <summary>
    /// The reflected converter will properly serialize nullable types. However,
    /// we do it here instead as we can emit less serialization data.
    /// </summary>
    public class fsNullableConverter : fsConverter
    {
        public override bool CanProcess(in Type type)
        {
            return
                type.Resolve().IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public override fsResult TrySerialize(in object instance, out fsData serialized, in Type storageType)
        {
            // null is automatically serialized
            return Serializer.TrySerialize(Nullable.GetUnderlyingType(storageType), instance, out serialized);
        }

        public override fsResult TryDeserialize(in fsData data, ref object instance, in Type storageType)
        {
            // null is automatically deserialized
            return Serializer.TryDeserialize(data, Nullable.GetUnderlyingType(storageType), ref instance);
        }

        public override object CreateInstance(in fsData data, in Type storageType)
        {
            return storageType;
        }
    }
}