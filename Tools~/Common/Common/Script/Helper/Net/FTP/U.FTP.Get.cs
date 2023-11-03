using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

public partial class AHelper
{
    public partial class Net
    {
        public partial class FTP
        {
            /// <summary>
            /// 获取FTP文件大小
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="remotePath">目标文件路径</param>
            /// <param name="timeout">超时</param>
            /// <returns>大小</returns>
            /// <exception cref="Exception"></exception>
            public static long GetFileSize(string uri, string username, string password, string remotePath,
                ushort timeout = TIMEOUT)
            {
                long fileSize = 0;
                try
                {
                    var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(string.Concat(uri, remotePath)));
                    reqFTP.Credentials = new NetworkCredential(username, password);
                    reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                    reqFTP.UseBinary = true;
                    using var response = (FtpWebResponse)reqFTP.GetResponse();
                    using (var ftpStream = response.GetResponseStream())
                    {
                        if (ftpStream != null)
                        {
                            fileSize = response.ContentLength;
                            ftpStream.Close();
                        }
                    }

                    response.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return fileSize;
            }

            /// <summary>
            /// 获取文件或文件夹列表
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="type">
            /// 1:获取文件列表
            /// 2:获取文件夹列表
            /// 3:获取文件和文件夹列表
            /// </param>
            /// <param name="detail">
            /// 获取文件或文件夹详细信息
            /// </param>
            /// <param name="keyword">
            /// 获取包含Keyword的文件或文件夹，若要list所有文件或文件夹，则该参数为空
            /// 若listType=FileAndFolder，则该参数无效
            /// </param>
            /// <param name="timeout">超时</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public static List<string> GetRemoteList(string uri, string username, string password,
                AHandle.FTP.ListType type, bool detail, string keyword, ushort timeout = TIMEOUT
            )
            {
                var infos = new List<string>();
                try
                {
                    var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                    reqFTP.Credentials = new NetworkCredential(username, password);
                    reqFTP.Method = detail
                        ? WebRequestMethods.Ftp.ListDirectoryDetails
                        : WebRequestMethods.Ftp.ListDirectory;
                    reqFTP.Timeout = timeout;

                    var response = reqFTP.GetResponse();
                    var stream = response.GetResponseStream();
                    if (stream is null) throw new Exception("FTP Stream is Null");
                    var reader = new StreamReader(stream); //中文文件名
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        switch (type)
                        {
                            case AHandle.FTP.ListType.File:
                            {
                                if (!line.Contains(".")) break;

                                if (keyword.Trim() == "*.*" || keyword.Trim() == "")
                                    infos.Add(line);
                                else if (line.IndexOf(keyword.Trim(), StringComparison.CurrentCulture) > -1)
                                    infos.Add(line);
                                break;
                            }
                            case AHandle.FTP.ListType.Folder:
                            {
                                if (line.Contains(".")) break;

                                if (keyword.Trim() == "*" || keyword.Trim() == "")
                                    infos.Add(line);
                                else if (line.IndexOf(keyword.Trim(), StringComparison.CurrentCulture) > -1)
                                    infos.Add(line);

                                break;
                            }
                            default:
                            case AHandle.FTP.ListType.ALL:
                                infos.Add(line);
                                break;
                        }

                        line = reader.ReadLine();
                    }

                    reader.Close();
                    response.Close();
                    return infos;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}