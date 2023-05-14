using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// The direct converter is similar to a regular converter, except that it
    /// targets specifically only one type. This means that it can be used
    /// without performance impact when discovering converters. It is strongly
    /// recommended that you derive from fsDirectConverter{TModel}.
    /// </summary>
    /// <remarks>
    /// Due to the way that direct converters operate, inheritance is *not*
    /// supported. Direct converters will only be used with the exact ModelType
    /// object.
    /// </remarks>
    public abstract class fsDirectConverter : fsBaseConverter
    {
        /// <summary>
        /// 模块类型
        /// </summary>
        public abstract Type ModelType { get; }
    }

    /// <inheritdoc/>
    public abstract class fsDirectConverter<TModel> : fsDirectConverter
    {
        /// <inheritdoc/>
        public override Type ModelType => typeof(TModel);

        /// <inheritdoc/>
        public sealed override fsResult TrySerialize(in object instance, out fsData serialized, in Type storageType)
        {
            var serializedDictionary = new Dictionary<string, fsData>();
            var result = DoSerialize((TModel)instance, serializedDictionary);
            serialized = new fsData(serializedDictionary);
            return result;
        }

        /// <inheritdoc/>
        public sealed override fsResult TryDeserialize(in fsData data, ref object instance, in Type storageType)
        {
            var result = fsResult.Success;
            if ((result += CheckType(data, fsDataType.Object)).Failed)
            {
                return result;
            }

            var obj = (TModel)instance;
            result += DoDeserialize(data.AsDictionary, ref obj);
            instance = obj;
            return result;
        }

        protected abstract fsResult DoSerialize(in TModel model, in IDictionary<string, fsData> serialized);

        protected abstract fsResult DoDeserialize(in IDictionary<string, fsData> data, ref TModel model);
    }
}