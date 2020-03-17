using Microsoft.Extensions.Options;
using PCHUBStore.Services.EmailSenderOptions;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailAuthOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailAuthOptions Options { get; } //set only via Secret Manager

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            await this.Execute(subject, message, to);
        }

        public async Task Execute(string subject, string message, string to)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var fromEmailAddress = new EmailAddress("velislv@mail.bg", "Example User");
            var toEmailAddress = new EmailAddress(to, "Example User");
            var htmlContent = $"<strong>{message}</strong>";
            var msg = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddress, subject, message, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
