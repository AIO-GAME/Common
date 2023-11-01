using System;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        #region 检查

        /// <summary>
        /// 判断FTP连接
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="timeout">超时</param>
        /// <returns>Ture:有效 False:无效</returns>
        public static bool FTPCheck(string uri, string username, string password, ushort timeout = 3000)
        {
            try
            {
                // ftp用户名和密码
                var request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Timeout = timeout;
                var ftpResponse = (FtpWebResponse)request.GetResponse();
                ftpResponse.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断FTP连接
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="timeout">超时</param>
        /// <returns>Ture:有效 False:无效</returns>
        public static async Task<bool> FTPCheckAsync(string uri, string username, string password,
            ushort timeout = 3000)
        {
            try
            {
                // ftp用户名和密码
                var request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Timeout = timeout;
                var ftpResponse = (FtpWebResponse)await request.GetResponseAsync();
                ftpResponse.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
    }
}