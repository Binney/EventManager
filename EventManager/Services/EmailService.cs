using System.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;


namespace EventManager.Services
{
    internal class EmailService
    {
        public static async Task InvitationEmail(string userEmail, string userName, string invitationCode)
        {
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_API_KEY"];
            dynamic sg = new SendGridAPIClient(apiKey);

            var from = new Email("softwire.events@softwire.com");
            const string subject = "Invitation to a Softwire Event";
            var content = new Content("text/html", "<strong>It's not all hard work at Softwire!</strong>");
            var to = new Email(userEmail);
            var mail = new Mail(from, subject, to, content);

            mail.TemplateId = ConfigurationManager.AppSettings["InvitationEmailTemplate"];
            mail.Personalization[0].AddSubstitution("-name-", userName);
            mail.Personalization[0].AddSubstitution("-invitationCode-", invitationCode);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }

        public static async Task CancellationEmail(string userEmail, string userName, string eventName)
        {
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_API_KEY"];
            dynamic sg = new SendGridAPIClient(apiKey);

            var from = new Email("softwire.events@softwire.com");
            const string subject = "Cancellation of a Softwire Event";
            var content = new Content("text/html", "<strong>We have some Bad News!</strong>");
            var to = new Email(userEmail);
            var mail = new Mail(from, subject, to, content);

            mail.TemplateId = ConfigurationManager.AppSettings["CancelationEmailTemplate"];
            mail.Personalization[0].AddSubstitution("-name-", userName);
            mail.Personalization[0].AddSubstitution("-event-", eventName);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}