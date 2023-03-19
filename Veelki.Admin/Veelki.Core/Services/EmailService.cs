using Microsoft.Extensions.Configuration;
using Veelki.Core.IServices;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Veelki.Core.Services
{
    public class EmailService : IEmailService
    {
        IConfiguration _configuration;
        ILoggerService _logger;
        public EmailService(IConfiguration configuration, ILoggerService logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Task<bool> SendEmailAsync(string _to, string _toname, string _sub, string _body, out string _msg)
        {
            bool _isSuccess = false;
            _msg = string.Empty;

            try
            {
                string _smtpUserName = Convert.ToString(_configuration["MailValues:SMTP_USERNAME"]);
                string _smtpPassword = Convert.ToString(_configuration["MailValues:SMTP_PASSWORD"]);
                string _smtpHost = Convert.ToString(_configuration["MailValues:HOST"]);
                int _smtpPort = Convert.ToInt32(_configuration["MailValues:PORT"]);
                string from = Convert.ToString(_configuration["MailValues:MAIL_FROM"]);
                string fromname = Convert.ToString(_configuration["MailValues:MAIL_FROMNAME"]);

                using (SmtpClient client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUserName, _smtpPassword);
                    client.EnableSsl = true;
                    MailAddress frommail = new MailAddress(from, fromname);
                    MailAddress tomail = new MailAddress(_to, _toname);
                    MailMessage myMail = new MailMessage(frommail, tomail);
                    myMail.Subject = _sub;
                    myMail.SubjectEncoding = Encoding.UTF8;
                    myMail.Body = _body;
                    myMail.BodyEncoding = Encoding.UTF8;
                    myMail.IsBodyHtml = true;
                    try { client.Send(myMail); _isSuccess = true; }
                    catch (Exception ex) { 
                        _msg = string.Format("SMTPException:{0}", ex.ToString());
                        _logger.LogException("Exception : EmailSender : SendEmailAsync() : MailSend()", ex);
                    }
                }
            }
            catch (Exception ex) { _msg = string.Format("SMTPException:{0}", ex.ToString());
                _logger.LogException("Exception : EmailSender : SendEmailAsync()", ex);
            }
            return Task.FromResult(_isSuccess);
        }
        //public Task SendEmailAsync(string email, string subject, string message)
        //{
        //    return Task.CompletedTask;
        //}
    }
}
