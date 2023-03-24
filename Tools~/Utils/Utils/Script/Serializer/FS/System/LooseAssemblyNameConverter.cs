namespace AIO
{
    using System;

    /// <inheritdoc/>
    public class LooseAssemblyNameConverter : fsDirectConverter
    {
        /// <inheritdoc/>
        public override Type ModelType => typeof(LooseAssemblyName);

        /// <inheritdoc/>
        public override object CreateInstance(in fsData data, in Type storageType)
        {
            return new object();
        }

        /// <inheritdoc/>
        public override fsResult TrySerialize(in object instance, out fsData serialized, in Type storageType)
        {
            serialized = new fsData(((LooseAssemblyName)instance).Name);

            return fsResult.Success;
        }

        /// <inheritdoc/>
        public override fsResult TryDeserialize(in fsData data, ref object instance, in Type storageType)
        {
            if (!data.IsString)
            {
                return fsResult.Fail("Expected string in " + data);
            }

            instance = new LooseAssemblyName(data.AsString);

            return fsResult.Success;
        }
    }
}
