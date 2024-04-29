#region

using System.Runtime.CompilerServices;
using System.Text;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 读取 Yaml 文件 根据编码
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static T ReadYaml<T>(
                in string path,
                Encoding  charset = null)
            {
                return Yaml.Deserialize<T>(ReadText(path, charset));
            }

            /// <summary>
            /// 读取 Yaml 文件 编码utf-8
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static T ReadYamlUTF8<T>(
                in string path)
            {
                return Yaml.Deserialize<T>(ReadUTF8(path));
            }

            /// <summary>
            /// 写入 Yaml 文件 根据编码
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void WriteYaml<T>(
                in string path,
                in T      value,
                Encoding  charset = null)
            {
                if (value == null) return;
                WriteText(path, Yaml.Serialize(value), charset);
            }

            /// <summary>
            /// 写入 Yaml 文件 编码utf-8
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void WriteYamlUTF8<T>(
                in string path,
                in T      value)
            {
                if (value == null) return;
                WriteUTF8(path, Yaml.Serialize(value));
            }
        }

        #endregion
    }
}