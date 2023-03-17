/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.IO;

namespace AIO
{
    public static partial class IOUtils
    {
        /// <summary>
        /// 拷贝子节点
        /// </summary>
        public static void CopyChildNode(string Source, string Dest, bool Overwrite = false)
        {
            if (!DirExists(Source)) return;
            foreach (var SFile in Directory.GetFileSystemEntries(Source)) //遍历所有的文件和目录
            {
                //如果是文件夹 则继续拷贝
                if (DirExists(SFile)) CopyFolderPart(SFile, Dest, Overwrite);
                //如果是文件 则直接放入当前文件夹下
                else CopyFile(SFile, Path.Combine(Dest, GetFileName(SFile)), Overwrite);
            }
        }

        /// <summary>
        /// 复制文件夹及文件 部分 根文件名不会复制 适合重命名
        /// </summary>
        /// <param name="Source">原文件路径</param>
        /// <param name="Dest">目标文件路径</param>
        /// <param name="Overwrite"></param>
        public static void CopyFolderPart(string Source, string Dest, bool Overwrite = false)
        {
            if (!DirExists(Source)) return;
            var DestDir = Path.Combine(Dest, GetFileName(Source));
            foreach (var SFile in Directory.GetFileSystemEntries(Source)) //遍历所有的文件和目录
            {
                if (DirExists(SFile))
                {
                    var currentdir = Path.Combine(DestDir, GetFileName(SFile));
                    if (!DirExists(currentdir)) CreateFloder(currentdir);
                    CopyFolderPart(SFile, DestDir, Overwrite);
                }
                else
                {
                    var srcfileName = Path.Combine(DestDir, GetFileName(SFile));
                    if (!DirExists(DestDir)) CreateFloder(DestDir);
                    CopyFile(SFile, srcfileName, Overwrite);
                }
            }
        }

        /// <summary>
        /// 复制文件夹及文件 全部
        /// </summary> 根文件名一起复制
        /// <param name="Source">原文件路径</param>
        /// <param name="Dest">目标文件路径</param>
        /// <param name="Overwrite"></param>
        public static void CopyFolderAll(string Source, string Dest, bool Overwrite = false)
        {
            //如果目标路径不存在,则创建目标路径
            if (!DirExists(Dest)) CreateFloder(Dest);
            foreach (var file in GetFiles(Source))
            {
                //得到原文件根目录下的所有文件
                var dest = Path.Combine(Dest, GetFileName(file));
                CopyFile(file, dest, Overwrite); //复制文件
            }

            foreach (var folder in GetFloders(Source))
            {
                //得到原文件根目录下的所有文件夹
                var dest = Path.Combine(Dest, GetFileName(folder));
                CopyFolderAll(folder, dest, Overwrite); //构建目标路径,递归复制文件
            }
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        public static bool CopyFile(string FromPath, string ToPath, bool Overwrite = false)
        {
            if (!FileExists(FromPath)) return false; //源文件路径 不存在
            var dir = Path.GetDirectoryName(ToPath);
            if (!DirExists(dir)) Directory.CreateDirectory(dir);
            File.Copy(FromPath, ToPath, Overwrite);
            return true;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFilePath">源文件的路径</param>
        /// <param name="destinationFilePath">目标文件的路径</param>
        /// <param name="bufferSize">缓冲区大小，用于每次读取和写入的字节数</param>
        /// <returns>是否成功复制文件</returns>
        public static bool CopyFile(string sourceFilePath, string destinationFilePath, int bufferSize = 81920)
        {
            if (!FileExists(sourceFilePath)) return false;

            // 获取目标目录路径，并创建目标目录
            var destinationFolder = Path.GetDirectoryName(destinationFilePath);
            if (!DirExists(destinationFolder)) Directory.CreateDirectory(destinationFolder);

            // 打开源文件和目标文件的文件流
            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (var destinationStream = new FileStream(destinationFilePath, FileMode.CreateNew, FileAccess.Write))
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