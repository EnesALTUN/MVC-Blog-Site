using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Gmail
    {
        public static void SendMail(string body)
        {
            var fromAddress = new MailAddress("mvcblogtez@gmail.com", "İletişim Formu");
            var toAddress = new MailAddress("mvcblogtez@gmail.com");
            const string subject = "Konu";

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "12.123qw")
                //trololol kısmı e-posta adresinin şifresi
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}