using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Script.Serialization;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;


namespace EventManager.Services
{
    internal class EmailService
    {
        public static async Task InvitationEmail(string userEmail, string userName, string invitationCode)
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGridAPIClient(apiKey);

            Email from = new Email("softwire.events@softwire.com");
            String subject = "Invitation to a Softwire Event";
            Content content = new Content("text/html", "<strong>It's not all hard work at Softwire!</strong>");
            Email to = new Email(userEmail);
            Mail mail = new Mail(from, subject, to, content);

            mail.TemplateId = "1828f98a-4cea-40f2-8fa2-378a54efd8c4";
            mail.Personalization[0].AddSubstitution("-name-", userName);
            mail.Personalization[0].AddSubstitution("-invitationCode-", invitationCode);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }

        public static async Task CancellationEmail(string userEmail, string userName, string eventName)
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY", EnvironmentVariableTarget.User);
            dynamic sg = new SendGridAPIClient(apiKey);

            Email from = new Email("softwire.events@softwire.com");
            String subject = "Cancellation of a Softwire Event";
            Content content = new Content("text/html", "<strong>We have some Bad News!</strong>");
            Email to = new Email(userEmail);
            Mail mail = new Mail(from, subject, to, content);

            mail.TemplateId = "3881a0fc-18f7-4af9-afce-19c50ccc79f8";
            mail.Personalization[0].AddSubstitution("-name-", userName);
            mail.Personalization[0].AddSubstitution("-event-", eventName);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}