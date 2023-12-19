using System;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class FTP
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="timeout">超时</param>
        public static bool CreateDir(string uri, string user, string pass,
            ushort timeout = Net.TIMEOUT)
        {
            if (Check(uri, user, pass, timeout)) return true;
            try
            {
                var request = CreateRequestFile(uri, user, pass, "MKD", timeout);
                using var response = (FtpWebResponse)request.GetResponse();
                var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                request.Abort();
                return status;
            }
            catch (WebException ex)
            {
#if DEBUG
                Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                    nameof(CreateDir), ex.Response.ResponseUri, user, pass, ex.Message);
#endif
                return false;
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="remote">路径</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="timeout">超时</param>
        public static async Task<bool> CreateDirAsync(string remote, string user, string pass,
            ushort timeout = Net.TIMEOUT)
        {
            try
            {
                var request = CreateRequestFile(remote, user, pass, "MKD", timeout);
                using var response = (FtpWebResponse)await request.GetResponseAsync();
                var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                request.Abort();
                return status;
            }
            catch (WebException ex)
            {
#if DEBUG
                Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                    nameof(CreateDirAsync), ex.Response.ResponseUri, user, pass, ex.Message);
#endif
                return false;
            }
        }

        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="currentName">当前名称</param>
        /// <param name="newName">新名称</param>
        /// <param name="timeout">超时</param>
        public static bool ReName(string uri, string user, string pass, string currentName,
            string newName, ushort timeout = Net.TIMEOUT)
        {
            try
            {
                var remote = string.Concat(uri, '/', currentName);
                var request = CreateRequestFile(remote, user, pass, "RENAME", timeout);
                request.RenameTo = newName;
                var response = (FtpWebResponse)request.GetResponse();
                var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                request.Abort();
                return status;
            }
            catch (WebException ex)
            {
#if DEBUG
                Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                    nameof(ReName), ex.Response.ResponseUri, user, pass, ex.Message);
#endif
                return false;
            }
        }

        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="currentName">当前名称</param>
        /// <param name="newName">新名称</param>
        /// <param name="timeout">超时</param>
        public static async Task<bool> ReNameAsync(string uri, string user, string pass, string currentName,
            string newName, ushort timeout = Net.TIMEOUT)
        {
            try
            {
                var remote = string.Concat(uri, '/', currentName);
                var request = CreateRequestFile(remote, user, pass, "RENAME", timeout);
                request.RenameTo = newName;
                using var response = (FtpWebResponse)await request.GetResponseAsync();
                var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                request.Abort();
                return status;
            }
            catch (WebException ex)
            {
#if DEBUG
                Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                    nameof(ReNameAsync), ex.Response.ResponseUri, user, pass, ex.Message);
#endif
                return false;
            }
        }


        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="currentName">当前名称</param>
        /// <param name="newName">新名称</param>
        /// <param name="timeout">超时</param>
        public static bool Move(string uri, string user, string pass, string currentName, string newName,
            ushort timeout = Net.TIMEOUT)
        {
            return ReName(uri, user, pass, currentName, newName, timeout);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="currentName">当前名称</param>
        /// <param name="newName">新名称</param>
        /// <param name="timeout">超时</param>
        public static Task<bool> MoveAsync(string uri, string user, string pass, string currentName,
            string newName, ushort timeout = Net.TIMEOUT)
        {
            return ReNameAsync(uri, user, pass, currentName, newName, timeout);
        }
    }
}