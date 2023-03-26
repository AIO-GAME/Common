/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.IO;
using System.Runtime.CompilerServices;

public partial class Utils
{
    public partial class IO
    {
        /// <summary>
        /// 拷贝子节点
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyChildNode(
            in string sourceFilePath,
            in string destinationFilePath,
            in bool overwrite = false)
        {
            if (!ExistsFolder(sourceFilePath)) return;
            foreach (var SFile in Directory.GetFileSystemEntries(sourceFilePath)) //遍历所有的文件和目录
            {
                //如果是文件夹 则继续拷贝
                if (ExistsFolder(SFile)) CopyFolderPart(SFile, destinationFilePath, overwrite);
                //如果是文件 则直接放入当前文件夹下
                else CopyFile(SFile, Path.Combine(destinationFilePath, GetFileName(SFile)), overwrite);
            }
        }

        /// <summary>
        /// 复制文件夹及文件 部分 根文件名不会复制 适合重命名
        /// </summary>
        /// <param name="sourceFilePath">原文件路径</param>
        /// <param name="destinationFilePath">目标文件路径</param>
        /// <param name="overwrite"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyFolderPart(
            in string sourceFilePath,
            in string destinationFilePath,
            in bool overwrite = false)
        {
            if (!ExistsFolder(sourceFilePath)) return;
            var DestDir = Path.Combine(destinationFilePath, GetFileName(sourceFilePath));
            foreach (var SFile in Directory.GetFileSystemEntries(sourceFilePath)) //遍历所有的文件和目录
            {
                if (ExistsFolder(SFile))
                {
                    var current = Path.Combine(DestDir, GetFileName(SFile));
                    if (!ExistsFolder(current)) CreateFolder(current);
                    CopyFolderPart(SFile, DestDir, overwrite);
                }
                else
                {
                    var scaleName = Path.Combine(DestDir, GetFileName(SFile));
                    if (!ExistsFolder(DestDir)) CreateFolder(DestDir);
                    CopyFile(SFile, scaleName, overwrite);
                }
            }
        }

        /// <summary>
        /// 复制文件夹及文件 全部
        /// </summary> 根文件名一起复制
        /// <param name="sourceFilePath">原文件路径</param>
        /// <param name="destinationFilePath">目标文件路径</param>
        /// <param name="overwrite"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyFolderAll(
            in string sourceFilePath,
            in string destinationFilePath,
            in bool overwrite = false)
        {
            //如果目标路径不存在,则创建目标路径
            if (!ExistsFolder(destinationFilePath)) CreateFolder(destinationFilePath);
            foreach (var file in GetFiles(sourceFilePath))
            {
                //得到原文件根目录下的所有文件
                var dest = Path.Combine(destinationFilePath, GetFileName(file));
                CopyFile(file, dest, overwrite); //复制文件
            }

            foreach (var folder in GetFolders(sourceFilePath))
            {
                //得到原文件根目录下的所有文件夹
                var dest = Path.Combine(destinationFilePath, GetFileName(folder));
                CopyFolderAll(folder, dest, overwrite); //构建目标路径,递归复制文件
            }
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyFile(
            in string fromPath,
            in string toPath,
            in bool overwrite = false)
        {
            if (!ExistsFile(fromPath)) return false; //源文件路径 不存在
            var dir = Path.GetDirectoryName(toPath);
            if (!string.IsNullOrEmpty(dir) && !ExistsFolder(dir))
                Directory.CreateDirectory(dir);
            File.Copy(fromPath, toPath, overwrite);
            return true;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFilePath">源文件的路径</param>
        /// <param name="destinationFilePath">目标文件的路径</param>
        /// <param name="bufferSize">缓冲区大小，用于每次读取和写入的字节数</param>
        /// <returns>是否成功复制文件</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CopyFile(
            in string sourceFilePath,
            in string destinationFilePath,
            in int bufferSize = 81920
        )
        {
            if (string.IsNullOrEmpty(destinationFilePath)) return false;
            if (!ExistsFile(sourceFilePath)) return false;

            // 获取目标目录路径，并创建目标目录
            var destinationFolder = Path.GetDirectoryName(destinationFilePath);
            if (!string.IsNullOrEmpty(destinationFolder) && !ExistsFolder(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            // 打开源文件和目标文件的文件流
            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (var destinationStream =
                   new FileStream(destinationFilePath, FileMode.CreateNew, FileAccess.Write))
            {
                var buffer = new byte[bufferSize];
                int bytesRead;

                // 循环读取源文件的内容，并写入目标文件的文件流
                while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    destinationStream.Write(buffer, 0, bytesRead);
                }
            }

            return true;
        }
    }
}
