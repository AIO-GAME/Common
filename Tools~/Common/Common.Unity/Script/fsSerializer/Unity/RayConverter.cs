using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIO.Unity
{
    /// <summary>
    /// Ray 转换器
    /// </summary>
    public class RayConverter : fsDirectConverter<Ray>
    {
        protected override fsResult DoSerialize(in Ray model, in IDictionary<string, fsData> serialized)
        {
            var result = fsResult.Success;

            result += SerializeMember(serialized, null, "origin", model.origin);
            result += SerializeMember(serialized, null, "direction", model.direction);

            return result;
        }

        /// <inheritdoc/>
        protected override fsResult DoDeserialize(in IDictionary<string, fsData> data, ref Ray model)
        {
            var result = fsResult.Success;

            var origin = model.origin;
            result += DeserializeMember(data, null, "origin", out origin);
            model.origin = origin;

            var direction = model.direction;
            result += DeserializeMember(data, null, "direction", out direction);
            model.direction = direction;

            return result;
        }

        /// <inheritdoc/>
        public override object CreateInstance(in fsData data, in Type storageType)
        {
            return new Ray();
        }
    }
}