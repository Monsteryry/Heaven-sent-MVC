using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class SupportController : Controller
    {
        #region
        string password = "";
        #endregion
        // GET: Support
        public ActionResult Support()
        {
            ViewBag.Message = "Support";
            return View();
        }
        public ActionResult MailSent()
        {
            ViewBag.MailSent = "Success! Mail was sent to the Support team.";
            return View();
        }
        /*[ValidateAntiForgeryToken]*/
        [HttpPost]
        public ActionResult Support(MVC.Models.EmailForm email)
        {
            //dodawać kategorię w nazwie, a w body dać od kogo i treść
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email.emailRecipient);
                mail.From = new MailAddress(email.emailSender);
                mail.Subject = email.emailCategory + ": " + email.emailSubject;
                string description = ""
                    + "<br /> From: " + email.emailUsername + ", " + email.emailSender
                    + "<br /> Description: <br />";
                    //+ "<br /> Category: " + email.emailCategory + "<br />";
                description += "<i>" + email.emailDescription + "</i>";
                mail.Body = description.Replace(Environment.NewLine, "<br />");
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("", password); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("Support", email);
            }
            else
            {
                return View();
            }
        }
    }
}
