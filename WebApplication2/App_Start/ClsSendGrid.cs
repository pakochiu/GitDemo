using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace WebApplication2.App_Start
{
    public class ClsSendGrid
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = $"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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mist126gradproj@gmail.com", "MSIT126 鋁");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("pakochiu@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}