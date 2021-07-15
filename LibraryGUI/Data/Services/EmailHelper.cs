using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryGUI.Models;

namespace LibraryGUI.Data.Services
{
    public class EmailHelper
    {
      
            private string _host;
            private string _from;
            private string _alias;

            public EmailHelper(IConfiguration iconfiguration)
            {
                var smtpSection = iconfiguration.GetSection("SMTP");
                if (smtpSection != null)
                {
                    _host = smtpSection.GetSection("Host").Value;
                    _from = smtpSection.GetSection("From").Value;
                    _alias = smtpSection.GetSection("Alias").Value;
                }
            }

            public void SendEmail(EmailModel emailModel)
            {
            try
            {
                using (SmtpClient client = new SmtpClient(_host))
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_from, _alias);
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.To.Add(emailModel.To);
                    mailMessage.Body = emailModel.Message;
                    mailMessage.Subject = emailModel.Subject;
                    mailMessage.IsBodyHtml = emailModel.IsBodyHtml;
                    client.Send(mailMessage);
                }
            }
            catch
            {
                throw;
            }
            }
        }
}
