using System;

namespace AIO
{
    /// <inheritdoc/>
    public class NamespaceConverter : fsDirectConverter
    {
        /// <inheritdoc/>
        public override Type ModelType => typeof(Namespace);

        /// <inheritdoc/>
        public override object CreateInstance(in fsData data, in Type storageType)
        {
            return new object();
        }

        /// <inheritdoc/>
        public override fsResult TrySerialize(in object instance, out fsData serialized, in Type storageType)
        {
            serialized = new fsData(((Namespace)instance).FullName);

            return fsResult.Success;
        }

        /// <inheritdoc/>
        public override fsResult TryDeserialize(in fsData data, ref object instance, in Type storageType)
        {
            if (!data.IsString)
            {
                return fsResult.Fail("Expected string in " + data);
            }

            instance = Namespace.FromFullName(data.AsString);

            return fsResult.Success;
        }
    }
}