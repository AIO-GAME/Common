#region

using System.Net.Mail;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 邮件 一对一 传入参数
    /// </summary>
    public struct MailMultipInfo
    {
        /// <summary>
        /// 发送者 地址
        /// </summary>
        public string Form;

        /// <summary>
        /// 发送者 端口
        /// </summary>
        public int FormPort;

        /// <summary>
        /// 发送者  昵称  如果没有则用地址代替
        /// </summary>
        public string FormDisplayName;

        /// <summary>
        /// 凭证授权码
        /// </summary>
        public string Passwrod;

        /// <summary>
        /// 编码格式
        /// </summary>
        public Encoding Encoding;

        /// <summary>
        /// 连接服务器 地址 例子:"smtp.qq.com"
        /// </summary>
        public string ClientHost;

        /// <summary>
        /// 连接服务器 SSL验证
        /// </summary>
        public bool ClientEnableSsl;

        /// <summary>
        /// 连接服务器 超时秒限制
        /// </summary>
        public int ClientTimeout;

        /// <summary>
        /// 连接服务器 连接方式
        /// </summary>
        public SmtpDeliveryMethod ClientDeliveryMethod;

        /// <summary>
        /// 连接服务器 是否使用默认凭证
        /// </summary>
        public bool ClientUseDefaultCredentials;

        /// <summary>
        /// 邮件
        /// </summary>
        /// <param name="form"></param>
        /// <param name="to"></param>
        /// <param name="passwrod"></param>
        public MailMultipInfo(string form, string to, string passwrod = "")
        {
            Form            = form;
            FormPort        = default;
            FormDisplayName = null;
            Passwrod        = passwrod;
            Encoding        = Encoding.UTF8;

            ClientDeliveryMethod        = SmtpDeliveryMethod.Network;
            ClientUseDefaultCredentials = false;
            ClientTimeout               = 5000;
            ClientHost                  = "smtp.qq.com";
            ClientEnableSsl             = false;
        }
    }
}