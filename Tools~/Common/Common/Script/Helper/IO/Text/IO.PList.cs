#region

using System.Runtime.CompilerServices;
using AIO.PList;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 读取 Plist
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static PListRoot ReadPList(in string path)
            {
                return PListRoot.Load(path);
            }

            /// <summary>
            /// 写入 Plist XML
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void WritePList(in string path, PListRoot value)
            {
                value.Save(path, PListFormat.Xml);
            }

            /// <summary>
            /// 写入 Plist XML
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void WritePList(in string path, PListDict value)
            {
                var root = new PListRoot { Root = value };
                root.Save(path, PListFormat.Xml);
            }

            /// <summary>
            /// 写入 Plist 二进制
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void WritePListBinary(in string path, PListRoot value)
            {
                value.Save(path, PListFormat.Binary);
            }

            /// <summary>
            /// 写入 Plist XML
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void WritePListBinary(in string path, PListDict value)
            {
                var root = new PListRoot { Root = value };
                root.Save(path, PListFormat.Binary);
            }
        }

        #endregion
    }
}