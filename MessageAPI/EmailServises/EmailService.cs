using MailKit.Net.Smtp;
using MessageAPI.ViewModels;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageAPI.EmailServises
{
    /// <summary>
    /// Предоставляет функционал по отправке сообщений.
    /// </summary>
    public class EmailService : IEmailService
    {
        readonly string smtpServer;
        readonly string smtpPassword;
        readonly string smtpLogin;
        readonly int smtpPort;
        /// <summary>
        /// Получает параметры из конфигурационного файла для отправки сообщений.
        /// </summary>
        /// <param name="configuration"></param>
        public EmailService(IConfiguration configuration)
        {
            smtpServer = configuration["EmailConfiguration:SmtpServer"];
            smtpLogin = configuration["EmailConfiguration:SmtpUsername"];
            smtpPassword = configuration["EmailConfiguration:SmtpPassword"];
            smtpPort = configuration.GetValue<int>("EmailConfiguration:SmtpPort");
        }

        public async Task<bool> SendAsync(MessagePostViewModel message)
        {
            var mime_message = MimeMessageBuilder(message);
            using var emailClient = new SmtpClient();
            emailClient.Connect(smtpServer, smtpPort, true);

            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

            emailClient.Authenticate(smtpLogin, smtpPassword);

            try
            {
                await emailClient.SendAsync(mime_message);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                emailClient.Disconnect(true);
            }
        }
        /// <summary>
        /// Преобразует полученное сообщение в Mime Ссообщение
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        MimeMessage MimeMessageBuilder(MessagePostViewModel message)
        {
            var emailMessage = new MimeMessage();
            var allRecipients = new List<MailboxAddress>();

            var from = new MailboxAddress("Me", smtpLogin);
            emailMessage.From.Add(from);

            var to = new MailboxAddress("recipient", message.Recipient);
            allRecipients.Add(to);

            if (message.Carbon_copy_recipients != null)
            {
                foreach (string addr in message.Carbon_copy_recipients)
                {
                    allRecipients.Add(new MailboxAddress($"recipient{addr}", addr));
                }
            }
            emailMessage.To.AddRange(allRecipients);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = message.Text;

            emailMessage.Subject = message.Subject;
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }
    }
}
