using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Assignment2.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents, HttpPostedFileBase postedFile)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("huangshixin47@outlook.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            using (var memoryStream = new MemoryStream())
            {
                // file to input stream
                postedFile.InputStream.CopyTo(memoryStream);
                // input stream to arraay
                byte[] bytes = memoryStream.ToArray();
                Attachment attachment = new Attachment();
                //to 664 type string
                attachment.Content = Convert.ToBase64String(bytes);
                attachment.Filename = postedFile.FileName; 
                //file to message
                msg.AddAttachment(attachment.Filename, attachment.Content);

            }
            var response = client.SendEmailAsync(msg);
        }

    }
}