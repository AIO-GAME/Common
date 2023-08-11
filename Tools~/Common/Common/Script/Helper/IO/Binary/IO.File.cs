/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


public partial class AHelper
{
    public partial class IO
    {
        /// <summary>
        /// 从文件中读取数据
        /// </summary>
        public static byte[] ReadFile(in string Path)
        {
            return Read(Path);
        }

        /// <summary>
        /// 将数据写入文件,是否追加到文件尾 默认覆盖文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Bytes">内容</param>
        /// <param name="Concat">true:拼接 | false:覆盖</param>
        public static bool WriteFile(
            in string Path,
            in byte[] Bytes,
            in bool Concat = false)
        {
            return Write(Path, Bytes, 0, Bytes.Length, Concat);
        }
    }
}