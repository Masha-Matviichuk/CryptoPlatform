using System.Net.Mail;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        //We get this property from Program.cs as it's added as singleton. 
        //However in Program.cs we get all the data from secrets.json
        private MailSettings _mailSettings;

        public EmailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendSignUpConfirmationEmailAsync(MailRequest message)
        {
            var emailToSend = new MimeMessage();

            //Configuring email information (sender email, reciever email, subject, body)
            emailToSend.From.Add(new MailboxAddress(_mailSettings.MainDisplayName, _mailSettings.MainEmail));
            emailToSend.To.Add(new MailboxAddress("", message.ToAddress));
            emailToSend.Subject = message.Subject;

            //You can customize your body here (as it's HTML)
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Body
            };

            //Opening connection and sending email
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_mailSettings.MainHost, int.Parse(_mailSettings.MainPort), true);
                await client.AuthenticateAsync(_mailSettings.MainPort, _mailSettings.MainPassword);
                await client.SendAsync(emailToSend);

                //Closing connection
                await client.DisconnectAsync(true);
            }
        }
        //rewrite method
        public async Task SendMessageToManagerAsync(CustomerMailRequest message)
        {
            var emailToSend = new MimeMessage();

            //Configuring email information (sender email, reciever email, subject, body)
            emailToSend.From.Add(new MailboxAddress(_mailSettings.MiddleDisplayName, _mailSettings.MiddleEmail));
            emailToSend.To.Add(new MailboxAddress(_mailSettings.MainDisplayName, _mailSettings.MainEmail));
            emailToSend.Subject = message.Subject;

            //You can customize your body here (as it's HTML)
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Body + " /Email: " + message.FromAddress
            };

            //Opening connection and sending email
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_mailSettings.MiddleHost, int.Parse(_mailSettings.MiddlePort), true);
                await client.AuthenticateAsync(_mailSettings.MiddleEmail, _mailSettings.MiddlePassword);
                await client.SendAsync(emailToSend);

                //Closing connection
                await client.DisconnectAsync(true);
            }
        }
    }
}