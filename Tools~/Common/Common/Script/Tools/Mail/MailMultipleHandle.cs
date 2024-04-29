#region

using System;
using System.Net;
using System.Net.Mail;

#endregion

namespace AIO
{
    /// <summary>
    /// 邮件 一对多
    /// </summary>
    public class MailMultipleHandle : MailHandle
    {
        /// <summary>
        /// 邮件
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="subject"></param>
        public MailMultipleHandle(MailMultipInfo Info, string subject = "")
        {
            Addresser = new MailAddress(Info.Form, Info.FormDisplayName, Info.Encoding);

            SmtpClient                       = new SmtpClient();
            SmtpClient.Host                  = Info.ClientHost;                                         //地址
            SmtpClient.EnableSsl             = Info.ClientEnableSsl;                                    //是否启用SSL
            SmtpClient.Timeout               = Info.ClientTimeout;                                      //超时
            SmtpClient.DeliveryMethod        = SmtpDeliveryMethod.Network;                              //连接方式
            SmtpClient.UseDefaultCredentials = false;                                                   //默认凭证
            SmtpClient.Credentials           = new NetworkCredential(Addresser.Address, Info.Passwrod); //创建网络凭证

            MailMessage            = new MailMessage();
            MailMessage.From       = Addresser;         //发件人
            MailMessage.IsBodyHtml = true;              //是否支持内容为HTML
            MailMessage.Priority   = MailPriority.High; //优先级

            Subject = subject;
        }

        /// <summary>
        /// 添加 收件人
        /// </summary>
        public void AddRecipients(params MailAddress[] Recipients)
        {
            foreach (var item in Recipients)
                if (!MailMessage.To.Contains(item))
                    MailMessage.To.Add(item);
        }

        /// <summary>
        /// 添加 收件人
        /// </summary>
        public void AddRecipients(params string[] Recipients)
        {
            foreach (var item in Recipients)
                if (!MailMessage.To.Contains(new MailAddress(item)))
                    MailMessage.To.Add(item);
        }

        /// <summary>
        /// 移除 收件人
        /// </summary>
        public void RemoveRecipients(params MailAddress[] Recipients)
        {
            foreach (var item in Recipients)
                if (MailMessage.To.Contains(item))
                    MailMessage.To.Remove(item);
        }

        /// <summary>
        /// 移除 收件人
        /// </summary>
        public void RemoveRecipients(params string[] Recipients)
        {
            foreach (var item in Recipients)
            {
                var t = new MailAddress(item);
                if (MailMessage.To.Contains(t)) MailMessage.To.Remove(t); //收件人
            }
        }


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
        /// <param name="Body">内容主体</param>
        /// <param name="CallBack">回调</param>
        /// <param name="Addresses">接收者</param>
        public void Send(string Body, MailAddress[] Addresses, Action CallBack = null)
        {
            AddRecipients(Addresses);
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
            if (MailMessage.To.Count == 0)
            {
                Console.WriteLine("Warning : 当前目标发送人数为0");
                return;
            }

            MailMessage.Subject = Label; //标题
            MailMessage.Body    = Body;  //内容
            SendMail(CallBack);
        }
    }
}