using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using System.Net;

namespace MVC.Controllers
{
    public class CaptchaController : Controller
    {
        //
        // GET: /Captcha/

        public ActionResult Index()
        {
            return View();
        }

        //need to write post here

        [HttpPost]
        public ActionResult FormSubmit()
        {
            //Validate Google recaptcha below

            var response = Request["g-recaptcha-response"];
            string secretKey = "6Le3hzoaAAAAAMfh6TdvNFrmMiQ2Hii9WE-QGSP7";
            var client = new WebClient();

            ViewData["Message"] = "Google reCaptcha validation success";


            //Here I am returning to Index view:

            return View("Index");
        }
    }
}