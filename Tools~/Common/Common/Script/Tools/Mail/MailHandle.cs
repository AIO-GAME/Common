/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2020-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Net;
using System.Net.Mail;

namespace AIO
{
    /// <summary>
    /// 邮件处理
    /// </summary>
    public abstract class MailHandle : IDisposable
    {
        /// <summary>
        /// Smtp客户端
        /// </summary>
        public SmtpClient SmtpClient { get; protected set; } = null;

        /// <summary>
        /// 邮件主体
        /// </summary>
        public MailMessage MailMessage { get; protected set; } = null;

        /// <summary>
        /// 发件人
        /// </summary>
        public MailAddress Addresser { get; protected set; } = null;

        #region MailMessage

        /// <summary>
        /// 是否支持内容为HTML
        /// </summary>
        public bool MailMessageIsBodyHtml
        {
            get => MailMessage.IsBodyHtml;
            set => MailMessage.IsBodyHtml = value;
        }

        /// <summary>
        /// 信息优先级
        /// </summary>
        public MailPriority MailMessagePriority
        {
            get => MailMessage.Priority;
            set => MailMessage.Priority = value;
        }

        #endregion

        #region Client

        /// <summary>
        /// Host Example : smtp.exmail.qq.com
        /// </summary>
        public string ClientHost
        {
            get => SmtpClient.Host;
            set => SmtpClient.Host = value;
        }

        /// <summary>
        /// 是否启用SSL
        /// </summary>
        public bool ClientEnableSsl
        {
            get => SmtpClient.EnableSsl;
            set => SmtpClient.EnableSsl = value;
        }

        /// <summary>
        /// 端口
        /// </summary>
        public int ClientPort
        {
            get => SmtpClient.Port;
            set => SmtpClient.Port = value;
        }

        /// <summary>
        /// 通行证
        /// </summary>
        public ICredentialsByHost ClientCredentials
        {
            get => SmtpClient.Credentials;
            set => SmtpClient.Credentials = value;
        }

        #endregion

        #region Custom Arribute

        /// <summary>
        /// 默认标题
        /// </summary>
        public string Subject { get; protected set; } = "";

        #endregion

        /// <summary>
        /// 添加 抄送人
        /// </summary>
        public void AddCRecipients(params MailAddress[] CRecipients)
        {
            foreach (var item in CRecipients)
            {
                if (!MailMessage.CC.Contains(item))
                {
                    MailMessage.CC.Add(item);
                }
            }
        }

        /// <summary>
        /// 添加 抄送人
        /// </summary>
        public void AddCRecipients(params string[] CRecipients)
        {
            foreach (var item in CRecipients)
            {
                if (!MailMessage.CC.Contains(new MailAddress(item)))
                {
                    MailMessage.CC.Add(item);
                }
            }
        }

        /// <summary>
        /// 清空抄送人
        /// </summary>
        public void ClearCRecipients(params MailAddress[] CRecipients)
        {
            MailMessage.CC.Clear();
        }

        /// <summary>
        /// 移除 抄送人
        /// </summary>
        public void RemoveCRecipients(params MailAddress[] CRecipients)
        {
            foreach (var item in CRecipients)
            {
                if (MailMessage.CC.Contains(item))
                {
                    MailMessage.CC.Remove(item);
                }
            }
        }

        /// <summary>
        /// 移除 抄送人
        /// </summary>
        public void RemoveCRecipients(params string[] CRecipients)
        {
            foreach (var item in CRecipients)
            {
                var t = new MailAddress(item);
                if (MailMessage.CC.Contains(t))
                {
                    MailMessage.CC.Remove(t);
                }
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        protected async void SendMail(Action CallBack = null)
        {
            try
            {
                await SmtpClient.SendMailAsync(MailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                CallBack?.Invoke();
            }
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            SmtpClient = null;
            MailMessage = null;
            Addresser = null;
        }
    }
}