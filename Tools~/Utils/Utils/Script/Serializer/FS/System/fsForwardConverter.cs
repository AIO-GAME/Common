using System;

namespace AIO
{
    public class fsForwardConverter : fsConverter
    {
        public fsForwardConverter(fsForwardAttribute attribute)
        {
            _memberName = attribute.MemberName;
        }

        private string _memberName;

        public override bool CanProcess(in Type type)
        {
            throw new NotSupportedException("Please use the [fsForward(...)] attribute.");
        }

        private fsResult GetProperty(object instance, out fsMetaProperty property)
        {
            var properties = fsMetaType.Get(Serializer.Config, instance.GetType()).Properties;
            for (var i = 0; i < properties.Length; ++i)
            {
                if (properties[i].MemberName == _memberName)
                {
                    property = properties[i];
                    return fsResult.Success;
                }
            }

            property = default(fsMetaProperty);
            return fsResult.Fail("No property named \"" + _memberName + "\" on " + fsTypeExtensions.CSharpName(instance.GetType()));
        }

        public override fsResult TrySerialize(in object instance, out fsData serialized, in Type storageType)
        {
            serialized = fsData.Null;
            var result = fsResult.Success;

            fsMetaProperty property;
            if ((result += GetProperty(instance, out property)).Failed)
            {
                return result;
            }

            var actualInstance = property.Read(instance);
            return Serializer.TrySerialize(property.StorageType, actualInstance, out serialized);
        }

        public override fsResult TryDeserialize(in fsData data, ref object instance, in Type storageType)
        {
            var result = fsResult.Success;

            fsMetaProperty property;
            if ((result += GetProperty(instance, out property)).Failed)
            {
                return result;
            }

            object actualInstance = null;
            if ((result += Serializer.TryDeserialize(data, property.StorageType, ref actualInstance)).Failed)
            {
                return result;
            }

            property.Write(instance, actualInstance);
            return result;
        }

        public override object CreateInstance(in fsData data, in Type storageType)
        {
            return fsMetaType.Get(Serializer.Config, storageType).CreateInstance();
        }
    }
}