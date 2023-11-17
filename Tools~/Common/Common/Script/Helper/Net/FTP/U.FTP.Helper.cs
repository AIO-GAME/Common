/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-17
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Net;

public partial class AHelper
{
    public partial class Net
    {
        public partial class FTP
        {
            private static string FixShortcuts(string remote)
            {
                return remote
                    .Replace('\\', '/')
                    .Replace("//", "/")
                    .Replace("///", "/")
                    .Replace("ftp:/", "ftp://")
                    .Trim('/', '\\', ' ');
            }

            private static FtpWebRequest CreateRequest(string remote, string username, string password, string method,
                ushort timeout = TIMEOUT)
            {
                var request = (FtpWebRequest)WebRequest.Create(new Uri(FixShortcuts(remote)));
                request.Credentials = new NetworkCredential(username, password);
                request.Timeout = timeout;
                request.Method = method;
                request.Proxy = null;
                request.UseBinary = true;
                request.KeepAlive = false;
                return request;
            }
        }
    }
}