using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;

namespace VaccineTrackerServer.Helper
{
    class SMTPHelper
    {
        public void SendMail(string from, string to, string subject = "", string Body = "")
        {
            from = string.IsNullOrEmpty(from) ? Environment.GetEnvironmentVariable("SENDER_EMAIL") : from;
            string SMTP_UserName = Environment.GetEnvironmentVariable("SMTP_USERNAME");
            string SMTP_Password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            string sMTP_Port = Environment.GetEnvironmentVariable("SMTP_PORT");
            string SMTP_Host = Environment.GetEnvironmentVariable("SMTP_HOST");
            MailMessage mailMessageObject = new MailMessage(from, to, subject, Body) { IsBodyHtml = true };
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Port = Convert.ToInt32(sMTP_Port);
                smtpClient.Host = SMTP_Host;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(SMTP_UserName, SMTP_Password);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessageObject);
            }
        }
        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
