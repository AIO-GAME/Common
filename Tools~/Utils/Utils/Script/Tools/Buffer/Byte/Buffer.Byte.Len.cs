using System;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public int ReadLen()
        {
            // 判定字节
            var n = Arrays[ReadIndex] & 0xFF;
            // 第一个字节若小于 0x20否则该字节不能表示为长度
            if (n < 0x20) return -1;
            // 第一个字节若大于 0x20(二进制为:0010 0000)则减去 1<<5<<(8*3)    (补3个字节位数)即为长度值 int.MaxValue - int.MinValue  
            if (n < 0x40) return Arrays.GetInt32(ref ReadIndex) & 0x1FFFFFFF; //1 << 29 - 1 = 536870911 = 0x1FFFFFFF
            // 第一个字节若大于 0x40(二进制为:0100 0000)则减去 1<<6<<8        (补1个字节位数)即为长度值 ushort.MaxValue - ushort.MinValue
            if (n < 0x80) return Arrays.GetUInt16(ref ReadIndex) & 0x3FFF; //1 << 14 - 1 = 16383 = 0x3FFF
            // 第一个字节若大于 0x80(二进制为:1000 0000)则减去 1<<7                        即为长度值                       
            else return ReadByte() & 0x7F; //1 << 7 - 1 = 127 = 0x7F
        }

        /// <inheritdoc/> 
        public void WriteLen(in int value)
        {
            // 写入时,加上判定位表示的值 
            if (value >= 0x20000000 || value < 0) throw new SystemException("value overflow , current max overflow = (2^29-1) ! invalid len:" + value);
            if (value < 0x80) WriteByte((byte)(value | 0x80)); // 1xx 范围0~(2^7-1)  0~(128-1)
            else if (value < 0x4000) WriteUInt16((ushort)(value | 0x4000)); // 01x 范围0~(2^14-1) 0~(16384-1)
            else WriteInt32(value | 0x20000000); // 001 范围0~(2^29-1) 0~(536870912-1)
        }

        /// <inheritdoc/> 
        public void WriteLen(in ushort value)
        {
            if (value < 0x80) WriteByte((byte)(value | 0x80)); // 1xx 范围0~(2^7-1)  0~(128-1)
            else if (value < 0x4000) WriteUInt16((ushort)(value | 0x4000)); // 01x 范围0~(2^14-1) 0~(16384-1)
            else WriteInt32(value | 0x20000000); // 001 范围0~(2^29-1) 0~(536870912-1)
        }

        /// <inheritdoc/> 
        public void WriteLen(in short value)
        {
            if (value < 0) throw new SystemException("value overflow , current max overflow = (2^29-1) ! invalid len:" + value);
            if (value < 0x80) WriteByte((byte)(value | 0x80)); // 1xx 范围0~(2^7-1)  0~(128-1)
            else if (value < 0x4000) WriteUInt16((ushort)(value | 0x4000)); // 01x 范围0~(2^14-1) 0~(16384-1)
            else WriteInt32((ushort)value | 0x20000000); // 001 范围0~(2^29-1) 0~(536870912-1)
        }

        /// <inheritdoc/> 
        public void WriteLen(in byte value)
        {
            if (value < 0x80) WriteByte((byte)(value | 0x80)); // 1xx 范围0~(2^7-1)  0~(128-1)
            else WriteUInt16((ushort)(value | 0x4000)); // 01x 范围0~(2^14-1) 0~(16384-1)
        }

        /// <inheritdoc/> 
        public void WriteLen(in sbyte value)
        {
            if (value < 0) throw new SystemException("value overflow , current max overflow = (2^29-1) ! invalid len:" + value);
            WriteByte((byte)(value | 0x80)); // 1xx 范围0~(2^7-1)  0~(128-1)
        }
    }

    public partial interface IWrite
    {
        /// <summary> 写入一个长度, 0至512M </summary>
        /// 原理: 以第一个字节二进制前三位来决定长度值占用的字节数(x表示0或1)
        /// 1xx 开头:长度值占1个字节,且值只能是剩下的 07=(08-1) 位能表示的范围,即:0~(2^07-1)=0~127
        /// 01x 开头:长度值占2个字节,且值只能是剩下的 14=(16-2) 位能表示的范围,即:0~(2^14-1)=0~163,83
        /// 001 开头:长度值占4个字节,且值只能是剩下的 29=(32-3) 位能表示的范围,即:0~(2^29-1)=0~536,870,91
        void WriteLen(in byte value);

        /// <summary> 写入一个长度, 0至512M </summary>
        /// 原理: 以第一个字节二进制前三位来决定长度值占用的字节数(x表示0或1)
        /// 1xx 开头:长度值占1个字节,且值只能是剩下的 07=(08-1) 位能表示的范围,即:0~(2^07-1)=0~127
        /// 01x 开头:长度值占2个字节,且值只能是剩下的 14=(16-2) 位能表示的范围,即:0~(2^14-1)=0~163,83
        /// 001 开头:长度值占4个字节,且值只能是剩下的 29=(32-3) 位能表示的范围,即:0~(2^29-1)=0~536,870,91
        void WriteLen(in sbyte value);

        /// <summary> 写入一个长度, 0至512M </summary>
        /// 原理: 以第一个字节二进制前三位来决定长度值占用的字节数(x表示0或1)
        /// 1xx 开头:长度值占1个字节,且值只能是剩下的 07=(08-1) 位能表示的范围,即:0~(2^07-1)=0~127
        /// 01x 开头:长度值占2个字节,且值只能是剩下的 14=(16-2) 位能表示的范围,即:0~(2^14-1)=0~163,83
        /// 001 开头:长度值占4个字节,且值只能是剩下的 29=(32-3) 位能表示的范围,即:0~(2^29-1)=0~536,870,91
        void WriteLen(in ushort value);

        /// <summary> 写入一个长度, 0至512M </summary>
        /// 原理: 以第一个字节二进制前三位来决定长度值占用的字节数(x表示0或1)
        /// 1xx 开头:长度值占1个字节,且值只能是剩下的 07=(08-1) 位能表示的范围,即:0~(2^07-1)=0~127
        /// 01x 开头:长度值占2个字节,且值只能是剩下的 14=(16-2) 位能表示的范围,即:0~(2^14-1)=0~163,83
        /// 001 开头:长度值占4个字节,且值只能是剩下的 29=(32-3) 位能表示的范围,即:0~(2^29-1)=0~536,870,91
        void WriteLen(in short value);

        /// <summary> 写入一个长度, 0至512M </summary>
        /// 原理: 以第一个字节二进制前三位来决定长度值占用的字节数(x表示0或1)
        /// 1xx 开头:长度值占1个字节,且值只能是剩下的 07=(08-1) 位能表示的范围,即:0~(2^07-1)=0~127
        /// 01x 开头:长度值占2个字节,且值只能是剩下的 14=(16-2) 位能表示的范围,即:0~(2^14-1)=0~163,83
        /// 001 开头:长度值占4个字节,且值只能是剩下的 29=(32-3) 位能表示的范围,即:0~(2^29-1)=0~536,870,91
        void WriteLen(in int value);
    }

    public partial interface IRead
    {
        /// <summary> 读取一个长度 0至512M </summary>
        /// 原理 : 以第一个字节二进制前三位来决定长度值占用的字节数(x表示0或1)
        /// 1xx 开头:长度值占1个字节,且值只能是剩下的 07=(08-1) 位能表示的范围,即:0~(2^07-1)=0~127
        /// 01x 开头:长度值占2个字节,且值只能是剩下的 14=(16-2) 位能表示的范围,即:0~(2^14-1)=0~163,83
        /// 001 开头:长度值占4个字节,且值只能是剩下的 29=(32-3) 位能表示的范围,即:0~(2^29-1)=0~536,870,91
        int ReadLen();
    }
}