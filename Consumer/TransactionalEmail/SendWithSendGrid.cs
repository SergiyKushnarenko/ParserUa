using Parser.Publisher;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Consumer.TransactionalEmail
{
    public class SendWithSendGrid
    {
        private const string FromEmailAddress = "";
        private const string FromName = "";
        public static async Task SendMessage(Message message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmailAddress, FromName),
                Subject = "Sending with Twilio SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, especially with C#"
            };
            msg.AddTo(new EmailAddress("sergiikushnarenko@gmail.com", "Sergii Kushnarenko"));
            var response = await client.SendEmailAsync(msg);
        }
    }
}
