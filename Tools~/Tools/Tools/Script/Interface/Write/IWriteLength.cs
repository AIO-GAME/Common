using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteLength
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

        /// <summary>
        /// 写入Sbyte数组
        /// </summary>
        /// <param name="value">输入源</param>
        void WriteLenArray(in ICollection<int> value);
    }
}