using System;
using System.Collections.Generic;

namespace AIO
{
    public class fsKeyValuePairConverter : fsConverter
    {
        public override bool CanProcess(in Type type)
        {
            return
                type.Resolve().IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
        }

        public override bool RequestCycleSupport(in Type storageType)
        {
            return false;
        }

        public override bool RequestInheritanceSupport(in Type storageType)
        {
            return false;
        }

        public override fsResult TryDeserialize(in fsData data, ref object instance, in Type storageType)
        {
            var result = fsResult.Success;

            fsData keyData, valueData;
            if ((result += CheckKey(data, "Key", out keyData)).Failed)
            {
                return result;
            }

            if ((result += CheckKey(data, "Value", out valueData)).Failed)
            {
                return result;
            }

            var genericArguments = storageType.GetGenericArguments();
            Type keyType = genericArguments[0], valueType = genericArguments[1];

            object keyObject = null, valueObject = null;
            result.AddMessages(Serializer.TryDeserialize(keyData, keyType, ref keyObject));
            result.AddMessages(Serializer.TryDeserialize(valueData, valueType, ref valueObject));

            instance = Activator.CreateInstance(storageType, keyObject, valueObject);
            return result;
        }

        public override fsResult TrySerialize(in object instance, out fsData serialized, in Type storageType)
        {
            var keyProperty = storageType.GetDeclaredProperty("Key");
            var valueProperty = storageType.GetDeclaredProperty("Value");

            var keyObject = keyProperty.GetValue(instance, null);
            var valueObject = valueProperty.GetValue(instance, null);

            var genericArguments = storageType.GetGenericArguments();
            Type keyType = genericArguments[0], valueType = genericArguments[1];

            var result = fsResult.Success;

            fsData keyData, valueData;
            result.AddMessages(Serializer.TrySerialize(keyType, keyObject, out keyData));
            result.AddMessages(Serializer.TrySerialize(valueType, valueObject, out valueData));

            serialized = fsData.CreateDictionary();
            if (keyData != null)
            {
                serialized.AsDictionary["Key"] = keyData;
            }

            if (valueData != null)
            {
                serialized.AsDictionary["Value"] = valueData;
            }

            return result;
        }
    }
}