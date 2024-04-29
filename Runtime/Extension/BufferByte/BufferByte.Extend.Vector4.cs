#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    partial class ByteExtend
    {
        #region Vector4

        public static void SetVector4(this BufferByte buffer, ref int index, in Vector4 value, in bool all = false,
                                      in   bool       reverse = false)
        {
            buffer.SetFloat(ref index, value.x, all, reverse);
            buffer.SetFloat(ref index, value.y, all, reverse);
            buffer.SetFloat(ref index, value.z, all, reverse);
            buffer.SetFloat(ref index, value.w, all, reverse);
        }

        public static Vector4 ReadVector4(this BufferByte buffer, in bool all = false, in bool reverse = false)
        {
            return new Vector4(
                buffer.ReadFloat(all, reverse),
                buffer.ReadFloat(all, reverse),
                buffer.ReadFloat(all, reverse),
                buffer.ReadFloat(all, reverse));
        }

        public static Vector4 GetVector4(this IList<byte> array, ref int index, in bool all = false,
                                         in   bool        reverse = false)
        {
            return new Vector4(
                array.GetFloat(ref index, all, reverse),
                array.GetFloat(ref index, all, reverse),
                array.GetFloat(ref index, all, reverse),
                array.GetFloat(ref index, all, reverse));
        }

        public static void WriteVector4(this BufferByte buffer, in Vector4 value, in bool all = false,
                                        in   bool       reverse = false)
        {
            buffer.WriteFloat(value.x, all, reverse);
            buffer.WriteFloat(value.y, all, reverse);
            buffer.WriteFloat(value.z, all, reverse);
            buffer.WriteFloat(value.w, all, reverse);
        }

        #endregion
    }
}