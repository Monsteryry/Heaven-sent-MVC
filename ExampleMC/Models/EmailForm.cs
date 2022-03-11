using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class EmailForm
    {
        public string receipient = "Heavensent.ATGS@gmail.com";
        public string Receipient { get => receipient; set => receipient = value; }
        /*
        [Required, Display(Name = "Choose a category")]
        public List<string> Categories { 
            get => categories;
            set => categories.AddRange(value);
        }
        public List<string> categories = new List<string>
        {
            "In Game",
            "Technical",
            "Website",
            "Crash Report",
            "Bug Report",
            "Character Stuck",
        };*/
        public void ChooseReceipient(string category)
        {
            switch (category)
            {
                case "In Game":
                    emailRecipient = "appletreegamestudio@gmail.com";
                    break;
                case "Technical":
                    emailRecipient = "appletreegamestudio@gmail.com";
                    break;
                case "Website":
                    emailRecipient = "appletreegamestudio@gmail.com";
                    break;
                case "Crash Report":
                    emailRecipient = "appletreegamestudio@gmail.com";
                    break;
                case "Bug Report":
                    emailRecipient = "appletreegamestudio@gmail.com";
                    break;
                case "Character Stuck":
                    emailRecipient = "appletreegamestudio@gmail.com";
                    break;
                default:
                    emailRecipient = "appletreegamestudio@gmail.com";
                    break;
            }
        }

        [Required(ErrorMessage = "Field can't be empty"), Display(Name = "Choose an issue")]
        public string emailCategory { get; set; }

        [Required(ErrorMessage = "Field can't be empty"), Display(Name = "Your name in game")]
        public string emailUsername { get; set; }

        [Required(ErrorMessage = "Field can't be empty"), Display(Name = "Your email"), EmailAddress]
        public string emailSender { get; set; }

        [Required(ErrorMessage = "Field can't be empty"), Display(Name = "Your email"), EmailAddress]
        public string emailRecipient { get; set; }

        [Required(ErrorMessage = "Field can't be empty"), Display(Name = "Please provide a title as clear as possible for your issue.")]
        public string emailSubject { get; set; }

        [Required(ErrorMessage = "Field can't be empty"), Display(Name = "How can we help you? Please provide a detailed description of your issue.")]
        public string emailDescription { get; set; }
    }
}