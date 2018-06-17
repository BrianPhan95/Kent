using Kent.Business.Services;
using Kent.Entities.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kent.Business.BackgroundTask
{
    public class EmailBackgroundTask
    {
        //private static IEmailQueueServices _emailQueueService { get; set; }
        //public EmailBackgroundTask(IEmailQueueServices emailQueueService)
        //{
        //    _emailQueueService = emailQueueService;
        //}

        private static readonly string EmailFrom = "tin.phan281@gmail.com";
        private static readonly string EmailFromPassword = "PhanThanhTin";

        public static void Run(List<EmailQueue> emailQueueIds)
        {

            try
            {
                foreach (var emailQueue in emailQueueIds)
                {
                    //EmailQueue emailQueue = _emailQueueService.GetEmailByID(queueID);
                    Thread email = new Thread(delegate ()
                    {
                        SendEmail(emailQueue.To, EmailFrom, EmailFromPassword, emailQueue.Subject, emailQueue.Body);
                    });

                    email.IsBackground = true;
                    email.Start();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private static void SendEmail(string to, string from, string password, string subject, string body)
        {
            using (MailMessage mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = body;
                //if (postedFile.ContentLength > 0)
                //{
                //    string fileName = Path.GetFileName(postedFile.FileName);
                //    mm.Attachments.Add(new Attachment(postedFile.InputStream, fileName));
                //}
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(from, password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                //smtp.Send(mm);
                smtp.SendMailAsync(mm);
            }
        }
    }
}
