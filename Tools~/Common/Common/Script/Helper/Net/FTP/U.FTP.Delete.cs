using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        public partial class FTP
        {
            /// <summary>
            /// FTP指定删除文件
            /// </summary>
            /// <param name="uri">网址</param>
            /// <param name="username">账号</param>
            /// <param name="password">密码</param>
            /// <param name="filePath">文件路径</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="Exception">异常信息</exception>
            /// <returns>删除返回信息</returns>
            public static string DeleteFile(string uri, string username, string password, string filePath,
                ushort timeout = TIMEOUT)
            {
                try
                {
                    var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(uri, filePath)));
                    reqFTP.Credentials = new NetworkCredential(username, password);
                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                    reqFTP.KeepAlive = false;
                    reqFTP.Timeout = timeout;
                    var response = (FtpWebResponse)reqFTP.GetResponse();
                    var size = response.ContentLength;
                    var datastream = response.GetResponseStream();
                    if (datastream is null) throw new Exception("FTP Stream is Null");
                    var sr = new StreamReader(datastream);
                    var result = sr.ReadToEnd();
                    sr.Close();
                    datastream.Close();
                    response.Close();
                    return result;
                }
                catch (WebException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            /// <summary>
            /// FTP指定删除文件
            /// </summary>
            /// <param name="uri">网址</param>
            /// <param name="username">账号</param>
            /// <param name="password">密码</param>
            /// <param name="dirPath">文件夹路径</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="Exception">异常信息</exception>
            /// <returns>删除返回信息</returns>
            public static string RemoveFolder(string uri, string username, string password, string dirPath,
                ushort timeout = TIMEOUT)
            {
                try
                {
                    var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(uri, dirPath)));
                    reqFTP.Credentials = new NetworkCredential(username, password);
                    reqFTP.KeepAlive = false;
                    reqFTP.Timeout = timeout;
                    reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                    var response = (FtpWebResponse)reqFTP.GetResponse();
                    var size = response.ContentLength;
                    var datastream = response.GetResponseStream();
                    if (datastream is null) throw new Exception("FTP Stream is Null");
                    var sr = new StreamReader(datastream);
                    var result = sr.ReadToEnd();
                    sr.Close();
                    datastream.Close();
                    response.Close();
                    return result;
                }
                catch (WebException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}