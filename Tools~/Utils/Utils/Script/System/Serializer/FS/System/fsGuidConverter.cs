namespace AIO
{
    using System;

    /// <summary>
    /// Serializes and deserializes guids.
    /// </summary>
    public class fsGuidConverter : fsConverter
    {
        /// <inheritdoc/>
        public override bool CanProcess(in Type type)
        {
            return type == typeof(Guid);
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
            var guid = (Guid)instance;
            serialized = new fsData(guid.ToString());
            return fsResult.Success;
        }

        /// <inheritdoc/>
        public override fsResult TryDeserialize(in fsData data, ref object instance, in Type storageType)
        {
            if (data.IsString)
            {
                instance = new Guid(data.AsString);
                return fsResult.Success;
            }

            return fsResult.Fail("fsGuidConverter encountered an unknown JSON data type");
        }

        /// <inheritdoc/>
        public override object CreateInstance(in fsData data, in Type storageType)
        {
            return new Guid();
        }
    }
}
