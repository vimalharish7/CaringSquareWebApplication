using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace CaringSquareApp.Utils
{
    public class EmailSender
    {

       //Enter your SendGrid API below
        private const String API_KEY = "{YOUR API KEY}";

        /*
         * Name: Send Email Page - Send()
         * Function implemented when Send Email is Clicked
         * Argument list - Email Address, Subject and Email Content
         * Uses SendGrid API Key to Send out Email 
         */
        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("admin@caringsquare.com", "Caring Square Application");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            
            var response = client.SendEmailAsync(msg);
        }

        /*
         * Not Part of Functionality
         * Added for Future Scope
         * Send out Bulk Email to Multiple Recepient
         */
        public void SendBulkEmail(List<String> toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("admin@caringsquare.com", "Caring Square Application");
            //recipients.Select(x => MailHelper.StringToEmailAddress(x)).ToList();
            foreach (String tempEmail in toEmail)
            {

                var to = new EmailAddress(tempEmail, "");
                var plainTextContent = contents;
                var htmlContent = "<p>" + contents + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                //var bytes = File.ReadAllBytes("C:\\Users\\LEGION\\Downloads\\event_report.pdf");
                //var file = Convert.ToBase64String(bytes);
                //msg.AddAttachment("event_report.pdf", file);
                var response = client.SendEmailAsync(msg);
            }

           
        }

    }
}