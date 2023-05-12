using System.IO;

public partial class Utils
{
    /// <summary>
    /// IO工具类
    /// </summary>
    public partial class IO
    {
        /// <summary>
        /// 移动文件
        /// </summary>
        public static void MoveFile(
            in string source,
            in string target,
            in bool overlay = false
        )
        {
            if (!File.Exists(source)) return;
            if (File.Exists(target))
            {
                if (overlay) File.Delete(target);
                else return;
            }

            File.Move(source, target);
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        public static void MoveFolder(
            in string source,
            in string target,
            in bool overlay = false
        )
        {
            if (!Directory.Exists(source)) return;
            if (Directory.Exists(target))
            {
                if (overlay) Directory.Delete(target);
                else return;
            }

            Directory.Move(source, target);
        }
    }
}