using System;

namespace AIO
{
    public class fsTypeConverter : fsConverter
    {
        public override bool CanProcess(in Type type)
        {
            return typeof(Type).IsAssignableFrom(type);
        }

        public override bool RequestCycleSupport(in Type type)
        {
            return false;
        }

        public override bool RequestInheritanceSupport(in Type type)
        {
            return false;
        }

        public override fsResult TrySerialize(in object instance, out fsData serialized, in Type storageType)
        {
            var type = (Type)instance;
            serialized = new fsData(RuntimeCodebase.SerializeType(type));
            return fsResult.Success;
        }

        public override fsResult TryDeserialize(in fsData data, ref object instance, in Type storageType)
        {
            if (data.IsString == false)
            {
                return fsResult.Fail("Type converter requires a string");
            }

            if (RuntimeCodebase.TryDeserializeType(data.AsString, out var type))
            {
                instance = type;
            }
            else
            {
                return fsResult.Fail($"Unable to find type: '{data.AsString ?? "(null)"}'.");
            }

            return fsResult.Success;
        }

        public override object CreateInstance(in fsData data, in Type storageType)
        {
            return storageType;
        }
    }
}