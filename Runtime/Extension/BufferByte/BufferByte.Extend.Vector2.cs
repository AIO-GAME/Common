using System.Collections.Generic;
using UnityEngine;

namespace AIO.UEngine
{
    partial class ByteExtend
    {
        
        #region Vector2 Int

        public static Vector2Int GetVector2Int(this IList<byte> array, ref int index, in bool reverse = false)
        {
            return new Vector2Int(
                array.GetInt32(ref index, reverse),
                array.GetInt32(ref index, reverse));
        }

        public static void SetVector2Int(this BufferByte buffer, ref int index, in Vector2Int value,
            in bool reverse = false)
        {
            buffer.SetInt32(ref index, value.x, reverse);
            buffer.SetInt32(ref index, value.y, reverse);
        }

        public static Vector2Int ReadVector2Int(this BufferByte buffer, in bool reverse = false)
        {
            return new Vector2Int(
                buffer.ReadInt32(reverse),
                buffer.ReadInt32(reverse));
        }

        public static void WriteVector2Int(this BufferByte buffer, in Vector2Int value, in bool reverse = false)
        {
            buffer.WriteInt32(value.x, reverse);
            buffer.WriteInt32(value.y, reverse);
        }

        #endregion
        #region Vector2

        public static Vector2 GetVector2(this IList<byte> array, ref int index, in bool all = false,
            in bool reverse = false)
        {
            return new Vector3(
                array.GetFloat(ref index, all, reverse),
                array.GetFloat(ref index, all, reverse));
        }

        public static void SetVector2(this IList<byte> buffer, ref int index, in Vector2 value, in bool all = false,
            in bool reverse = false)
        {
            buffer.SetFloat(ref index, value.x, all, reverse);
            buffer.SetFloat(ref index, value.y, all, reverse);
        }

        public static Vector2 ReadVector2(this BufferByte buffer, in bool all = false, in bool reverse = false)
        {
            return new Vector2(
                buffer.ReadFloat(all, reverse),
                buffer.ReadFloat(all, reverse));
        }

        public static void WriteVector2(this BufferByte buffer, in Vector2 value, in bool all = false,
            in bool reverse = false)
        {
            buffer.WriteFloat(value.x, all, reverse);
            buffer.WriteFloat(value.y, all, reverse);
        }

        #endregion

    }
}