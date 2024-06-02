#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    partial class ByteExtend
    {
        #region Vector3 Int

        public static Vector3Int GetVector3Int(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return new Vector3Int(
                                  array.GetInt32(ref index, reverse),
                                  array.GetInt32(ref index, reverse),
                                  array.GetInt32(ref index, reverse));
        }

        public static void SetVector3Int(this BufferByte buffer,
                                         ref  int        index,
                                         in   Vector3Int value,
                                         in   bool       reverse = false)
        {
            buffer.SetInt32(ref index, value.x, reverse);
            buffer.SetInt32(ref index, value.y, reverse);
            buffer.SetInt32(ref index, value.z, reverse);
        }

        public static Vector3Int ReadVector3Int(this BufferByte buffer, in bool reverse = false)
        {
            return new Vector3Int(
                                  buffer.ReadInt32(reverse),
                                  buffer.ReadInt32(reverse),
                                  buffer.ReadInt32(reverse));
        }

        public static void WriteVector3Int(this BufferByte buffer, in Vector3Int value, in bool reverse = false)
        {
            buffer.WriteInt32(value.x, reverse);
            buffer.WriteInt32(value.y, reverse);
            buffer.WriteInt32(value.z, reverse);
        }

        #endregion

        #region Vector3

        public static Vector3 GetVector3(this IList<byte> array,
                                         ref  int         index,
                                         in   bool        all     = false,
                                         in   bool        reverse = false)
        {
            return new Vector3(array.GetFloat(ref index, all, reverse),
                               array.GetFloat(ref index, all, reverse),
                               array.GetFloat(ref index, all, reverse));
        }

        public static void SetVector3(this BufferByte buffer,
                                      ref  int        index,
                                      in   Vector3    value,
                                      in   bool       all     = false,
                                      in   bool       reverse = false)
        {
            buffer.SetFloat(ref index, value.x, all, reverse);
            buffer.SetFloat(ref index, value.y, all, reverse);
            buffer.SetFloat(ref index, value.z, all, reverse);
        }

        public static Vector3 ReadVector3(this BufferByte buffer, in bool all = false, in bool reverse = false)
        {
            return new Vector3(buffer.ReadFloat(all, reverse),
                               buffer.ReadFloat(all, reverse),
                               buffer.ReadFloat(all, reverse));
        }

        public static void WriteVector3(this BufferByte buffer,
                                        in   Vector3    value,
                                        in   bool       all     = false,
                                        in   bool       reverse = false)
        {
            buffer.WriteFloat(value.x, all, reverse);
            buffer.WriteFloat(value.y, all, reverse);
            buffer.WriteFloat(value.z, all, reverse);
        }

        #endregion
    }
}