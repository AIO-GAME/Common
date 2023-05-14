namespace AIO
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 反射转换器
    /// </summary>
    public class fsReflectedConverter : fsConverter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool CanProcess(in Type type)
        {
            if (type.Resolve().IsArray ||
                typeof(ICollection).IsAssignableFrom(type))
            {
                return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override fsResult TrySerialize(in object instance, out fsData serialized, in Type storageType)
        {
            serialized = fsData.CreateDictionary();
            var result = fsResult.Success;

            var metaType = fsMetaType.Get(Serializer.Config, instance.GetType());
            metaType.EmitAotData();

            for (var i = 0; i < metaType.Properties.Length; ++i)
            {
                var property = metaType.Properties[i];
                if (property.CanRead == false)
                {
                    continue;
                }

                fsData serializedData;

                var itemResult = Serializer.TrySerialize(property.StorageType, property.OverrideConverterType,
                    property.Read(instance), out serializedData);
                result.AddMessages(itemResult);
                if (itemResult.Failed)
                {
                    continue;
                }

                serialized.AsDictionary[property.JsonName] = serializedData;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override fsResult TryDeserialize(in fsData data, ref object instance, in Type storageType)
        {
            var result = fsResult.Success;

            // Verify that we actually have an Object
            if ((result += CheckType(data, fsDataType.Object)).Failed)
            {
                return result;
            }

            var metaType = fsMetaType.Get(Serializer.Config, storageType);
            metaType.EmitAotData();

            for (var i = 0; i < metaType.Properties.Length; ++i)
            {
                var property = metaType.Properties[i];
                if (property.CanWrite == false)
                {
                    continue;
                }

                fsData propertyData;
                if (data.AsDictionary.TryGetValue(property.JsonName, out propertyData))
                {
                    object deserializedValue = null;

                    // We have to read in the existing value, since we need to
                    // support partial deserialization. However, this is bad for
                    // perf.
                    // TODO: Find a way to avoid this call when we are not doing
                    //       a partial deserialization Maybe through a new
                    //       property, ie, Serializer.IsPartialSerialization,
                    //       which just gets set when starting a new
                    //       serialization? We cannot pipe the information
                    //       through CreateInstance unfortunately.
                    if (property.CanRead)
                    {
                        deserializedValue = property.Read(instance);
                    }

                    var itemResult = Serializer.TryDeserialize(propertyData, property.StorageType,
                        property.OverrideConverterType, ref deserializedValue);
                    result.AddMessages(itemResult);
                    if (itemResult.Failed)
                    {
                        continue;
                    }

                    property.Write(instance, deserializedValue);
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override object CreateInstance(in fsData data, in Type storageType)
        {
            var metaType = fsMetaType.Get(Serializer.Config, storageType);
            return metaType.CreateInstance();
        }
    }
}
