using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCPROJECT.Models
{
    public class CompanyInsert
    {
        [Required(ErrorMessage = "enter the companyname")]
        public string Companyname { set; get; }

        [Required(ErrorMessage = "Enter the number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter a valid number")]
        public string Phone { set; get; }
        [EmailAddress(ErrorMessage = "Enter a valid mailid")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Enter the address")]
        public string Address { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string Cpassword { set; get; }
        public string Companymsg { set; get; }

    }
}
