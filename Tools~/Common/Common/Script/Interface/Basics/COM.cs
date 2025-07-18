using System;
using System.Collections.Generic;

namespace AIO
{
    #region Read

    /// <summary>
    /// 读取 <see cref="bool"/>
    /// </summary>
    public partial interface IReadBool
    {
        /// <summary>
        /// 读取 <see cref="bool"/>
        /// </summary>
        bool ReadBool();

        /// <summary>
        /// 读取 <see cref="bool"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="bool"/> 数组</returns>
        bool[] ReadBoolArray(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="byte"/>
    /// </summary>
    public partial interface IReadByte
    {
        /// <summary>
        /// 读取 <see cref="byte"/>
        /// </summary>
        byte ReadByte();

        /// <summary>
        /// 读取 <see cref="byte"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="byte"/> 数组</returns>
        byte[] ReadByteArray(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="sbyte"/>
    /// </summary>
    public partial interface IReadSbyte
    {
        /// <summary>
        /// 读取 <see cref="sbyte"/>
        /// </summary>
        sbyte ReadSbyte();

        /// <summary>
        /// 读取 <see cref="sbyte"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="sbyte"/> 数组</returns>
        sbyte[] ReadSbyteArray(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="char"/>
    /// </summary>
    public partial interface IReadChar
    {
        /// <summary>
        /// 读取 <see cref="char"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        char ReadChar(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="char"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="char"/> 数组</returns>
        char[] ReadCharArray(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="Int16"/>
    /// </summary>
    public partial interface IReadInt16
    {
        /// <summary>
        /// 读取 <see cref="Int16"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        Int16 ReadInt16(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="Int16"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="Int16"/> 数组</returns>
        Int16[] ReadInt16Array(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="Int32"/>
    /// </summary>
    public partial interface IReadInt32
    {
        /// <summary>
        /// 读取 <see cref="Int32"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        Int32 ReadInt32(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="Int32"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="Int32"/> 数组</returns>
        Int32[] ReadInt32Array(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="Int64"/>
    /// </summary>
    public partial interface IReadInt64
    {
        /// <summary>
        /// 读取 <see cref="Int64"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        Int64 ReadInt64(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="Int64"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="Int64"/> 数组</returns>
        Int64[] ReadInt64Array(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="float"/>
    /// </summary>
    public partial interface IReadFloat
    {
        /// <summary>
        /// 读取 <see cref="float"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        float ReadFloat(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="float"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="float"/> 数组</returns>
        float[] ReadFloatArray(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="double"/>
    /// </summary>
    public partial interface IReadDouble
    {
        /// <summary>
        /// 读取 <see cref="double"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        double ReadDouble(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="double"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="double"/> 数组</returns>
        double[] ReadDoubleArray(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="decimal"/>
    /// </summary>
    public partial interface IReadDecimal
    {
        /// <summary>
        /// 读取 <see cref="decimal"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        decimal ReadDecimal(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="decimal"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="decimal"/> 数组</returns>
        decimal[] ReadDecimalArray(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="UInt16"/>
    /// </summary>
    public partial interface IReadUInt16
    {
        /// <summary>
        /// 读取 <see cref="UInt16"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        UInt16 ReadUInt16(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="UInt16"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="UInt16"/> 数组</returns>
        UInt16[] ReadUInt16Array(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="UInt32"/>
    /// </summary>
    public partial interface IReadUInt32
    {
        /// <summary>
        /// 读取 <see cref="UInt32"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        UInt32 ReadUInt32(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="UInt32"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="UInt32"/> 数组</returns>
        UInt32[] ReadUInt32Array(bool reverse = false);

    }

    /// <summary>
    /// 读取 <see cref="UInt64"/>
    /// </summary>
    public partial interface IReadUInt64
    {
        /// <summary>
        /// 读取 <see cref="UInt64"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        UInt64 ReadUInt64(bool reverse = false);

        /// <summary>
        /// 读取 <see cref="UInt64"/> 数组
        /// </summary>
        /// <param name="reverse"> 是否反转</param>
        /// <returns> <see cref="UInt64"/> 数组</returns>
        UInt64[] ReadUInt64Array(bool reverse = false);

    }
    #endregion

    #region Write

    /// <summary>
    /// 写入 <see cref="bool"/>
    /// </summary>
    public partial interface IWriteBool
    {
        /// <summary>
        /// 写入 <see cref="bool"/>
        /// </summary>
        /// <param name="value"><see cref="bool"/></param>
        void WriteBool(bool value);

        /// <summary>
        /// 写入 <see cref="bool"/> 数组
        /// </summary>
        /// <param name="value"><see cref="bool"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteBoolArray(ICollection<bool> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="byte"/>
    /// </summary>
    public partial interface IWriteByte
    {
        /// <summary>
        /// 写入 <see cref="byte"/>
        /// </summary>
        /// <param name="value"><see cref="byte"/></param>
        void WriteByte(byte value);

        /// <summary>
        /// 写入 <see cref="byte"/> 数组
        /// </summary>
        /// <param name="value"><see cref="byte"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteByteArray(ICollection<byte> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="sbyte"/>
    /// </summary>
    public partial interface IWriteSbyte
    {
        /// <summary>
        /// 写入 <see cref="sbyte"/>
        /// </summary>
        /// <param name="value"><see cref="sbyte"/></param>
        void WriteSbyte(sbyte value);

        /// <summary>
        /// 写入 <see cref="sbyte"/> 数组
        /// </summary>
        /// <param name="value"><see cref="sbyte"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteSbyteArray(ICollection<sbyte> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="char"/>
    /// </summary>
    public partial interface IWriteChar
    {
        /// <summary>
        /// 写入 <see cref="char"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="char"/></param>
        void WriteChar(char value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="char"/> 数组
        /// </summary>
        /// <param name="value"><see cref="char"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteCharArray(ICollection<char> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="Int16"/>
    /// </summary>
    public partial interface IWriteInt16
    {
        /// <summary>
        /// 写入 <see cref="Int16"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="Int16"/></param>
        void WriteInt16(Int16 value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="Int16"/> 数组
        /// </summary>
        /// <param name="value"><see cref="Int16"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteInt16Array(ICollection<Int16> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="Int32"/>
    /// </summary>
    public partial interface IWriteInt32
    {
        /// <summary>
        /// 写入 <see cref="Int32"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="Int32"/></param>
        void WriteInt32(Int32 value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="Int32"/> 数组
        /// </summary>
        /// <param name="value"><see cref="Int32"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteInt32Array(ICollection<Int32> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="Int64"/>
    /// </summary>
    public partial interface IWriteInt64
    {
        /// <summary>
        /// 写入 <see cref="Int64"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="Int64"/></param>
        void WriteInt64(Int64 value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="Int64"/> 数组
        /// </summary>
        /// <param name="value"><see cref="Int64"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteInt64Array(ICollection<Int64> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="float"/>
    /// </summary>
    public partial interface IWriteFloat
    {
        /// <summary>
        /// 写入 <see cref="float"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="float"/></param>
        void WriteFloat(float value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="float"/> 数组
        /// </summary>
        /// <param name="value"><see cref="float"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteFloatArray(ICollection<float> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="double"/>
    /// </summary>
    public partial interface IWriteDouble
    {
        /// <summary>
        /// 写入 <see cref="double"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="double"/></param>
        void WriteDouble(double value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="double"/> 数组
        /// </summary>
        /// <param name="value"><see cref="double"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteDoubleArray(ICollection<double> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="decimal"/>
    /// </summary>
    public partial interface IWriteDecimal
    {
        /// <summary>
        /// 写入 <see cref="decimal"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="decimal"/></param>
        void WriteDecimal(decimal value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="decimal"/> 数组
        /// </summary>
        /// <param name="value"><see cref="decimal"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteDecimalArray(ICollection<decimal> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="UInt16"/>
    /// </summary>
    public partial interface IWriteUInt16
    {
        /// <summary>
        /// 写入 <see cref="UInt16"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="UInt16"/></param>
        void WriteUInt16(UInt16 value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="UInt16"/> 数组
        /// </summary>
        /// <param name="value"><see cref="UInt16"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteUInt16Array(ICollection<UInt16> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="UInt32"/>
    /// </summary>
    public partial interface IWriteUInt32
    {
        /// <summary>
        /// 写入 <see cref="UInt32"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="UInt32"/></param>
        void WriteUInt32(UInt32 value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="UInt32"/> 数组
        /// </summary>
        /// <param name="value"><see cref="UInt32"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteUInt32Array(ICollection<UInt32> value, bool reverse = false);

}

    /// <summary>
    /// 写入 <see cref="UInt64"/>
    /// </summary>
    public partial interface IWriteUInt64
    {
        /// <summary>
        /// 写入 <see cref="UInt64"/>
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <param name="value"><see cref="UInt64"/></param>
        void WriteUInt64(UInt64 value, bool reverse = false);

        /// <summary>
        /// 写入 <see cref="UInt64"/> 数组
        /// </summary>
        /// <param name="value"><see cref="UInt64"/>数组</param>
        /// <param name="reverse">是否反转</param>
        void WriteUInt64Array(ICollection<UInt64> value, bool reverse = false);

}
    #endregion
}