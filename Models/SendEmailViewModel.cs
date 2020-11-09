﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace CaringSquareApp.Models
{
    public class SendEmailViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ToEmail { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        [AllowHtml]
        public string Contents { get; set; }


    }

    public class SendEmailViewModelMultipleRecepients
    {
        [Display(Name = "Email addresses")]


        public List<string> ToEmail { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }

    }

}