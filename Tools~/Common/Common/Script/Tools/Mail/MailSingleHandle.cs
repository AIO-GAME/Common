#region

using System;
using System.Net;
using System.Net.Mail;

#endregion

namespace AIO
{
    /// <summary>
    /// 单对单 直接发送
    /// </summary>
    public class MailSingleHandle : MailHandle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="subject"></param>
        public MailSingleHandle(MailSingleInfo Info, string subject = "")
        {
            Addresser = new MailAddress(Info.Form, Info.FormDisplayName, Info.Encoding);

            SmtpClient                       = new SmtpClient();
            SmtpClient.Host                  = Info.ClientHost;                                         //地址
            SmtpClient.EnableSsl             = Info.ClientEnableSsl;                                    //是否启用SSL
            SmtpClient.Timeout               = Info.ClientTimeout;                                      //超时
            SmtpClient.DeliveryMethod        = SmtpDeliveryMethod.Network;                              //连接方式
            SmtpClient.UseDefaultCredentials = false;                                                   //默认凭证
            SmtpClient.Credentials           = new NetworkCredential(Addresser.Address, Info.Passwrod); //创建网络凭证

            Recipients = new MailAddress(Info.To, Info.ToDisplayName, Info.Encoding);

            MailMessage      = new MailMessage();
            MailMessage.From = Addresser;               //发件人
            MailMessage.To.Add(Recipients);             //收件人
            MailMessage.IsBodyHtml = true;              //是否支持内容为HTML
            MailMessage.Priority   = MailPriority.High; //优先级

            Subject = subject;
        }

        /// <summary>
        /// 收件人
        /// </summary>
        public MailAddress Recipients { get; private set; }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Body">内容主体</param>
        /// <param name="CallBack">回调</param>
        public void Send(string Body, Action CallBack = null)
        {
            Send(Subject, Body, CallBack);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Label">邮件标题</param>
        /// <param name="Body">内容主体</param>
        /// <param name="CallBack">回调</param>
        public void Send(string Label, string Body, Action CallBack = null)
        {
            MailMessage.Subject = Label; //标题
            MailMessage.Body    = Body;  //内容
            SendMail(CallBack);
        }
    }
}