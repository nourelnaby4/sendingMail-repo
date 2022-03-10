using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using sendingEmails_.NetApi.settings_email;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace sendingEmails_.NetApi.Services
{
    public class MailServices : IMailServices
    {
        private readonly MailSettings _mailSettings;

    public MailServices(IOptions<MailSettings> mailsettings)
        {
            _mailSettings = mailsettings.Value;
        }
        public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = subject
            };
            email.To.Add(MailboxAddress.Parse(mailTo));
            var builder = new BodyBuilder();
            byte[] fileByte;
            if (attachments != null)
            {
                foreach(var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        using var ms=new MemoryStream();
                        file.CopyTo(ms);
                        fileByte = ms.ToArray();
                        builder.Attachments.Add(file.FileName, fileByte,ContentType.Parse(file.ContentType));
                    }
                    builder.HtmlBody = body;
                    email.Body = builder.ToMessageBody();
                    email.From.Add(new MailboxAddress(_mailSettings.DisplayName,_mailSettings.Email));

                    using var smtp = new SmtpClient();
                    smtp.Connect(_mailSettings.Host,_mailSettings.Port,SecureSocketOptions.StartTls);
                    smtp.Authenticate(_mailSettings.Email,_mailSettings.Password);
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                   
                }
            }

        }
    }
}
