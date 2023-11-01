using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="timeout">超时</param>
        /// <param name="dirName">文件夹路径</param>
        public static void FTPCreateDir(string uri, string username, string password, string dirName,
            ushort timeout = 3000)
        {
            try
            {
                var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(uri, dirName)));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Timeout = timeout;
                reqFTP.Credentials = new NetworkCredential(username, password);
                var response = (FtpWebResponse)reqFTP.GetResponse();
                response.GetResponseStream()?.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="currentName">当前名称</param>
        /// <param name="newName">新名称</param>
        /// <param name="timeout">超时</param>
        public static void Move(string uri, string username, string password, string currentName, string newName,
            ushort timeout = 3000)
        {
            FTPReName(uri, username, password, currentName, newName);
        }

        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="currentName">当前名称</param>
        /// <param name="newName">新名称</param>
        public static void FTPReName(string uri, string username, string password, string currentName, string newName,
            ushort timeout = 3000)
        {
            try
            {
                var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(uri, currentName)));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newName;
                reqFTP.Timeout = timeout;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(username, password);
                var response = (FtpWebResponse)reqFTP.GetResponse();
                response.GetResponseStream()?.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}