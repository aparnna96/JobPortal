using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCPROJECT.Models
{
    public class CheckBoxListHelper
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Ischecked { get; set; }
    }

    public class UserInsert
    {
        public List<CheckBoxListHelper> MyFavouriteQual { get; set; }
        public string[] SelectedQual { get; set; }
        [Required(ErrorMessage = "enter the name")]
        public string Name { set; get; }
        [Range(18, 50, ErrorMessage = "Enter the age")]
        public int Age { set; get; }
        [Required(ErrorMessage = "enter the address")]
        public string Address { set; get; }
        [EmailAddress(ErrorMessage = "enter avalid emailid")]
        public string Email { set; get; }
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter a valid number")]
        public string Phone { set; get; }
        public string Gender { get; set; }
        public string Qual { get; set; }
        public int Experience { get; set; }
        public string Skills { get; set; }

        public string Username { set; get; }
        public string Password { set; get; }
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string Cpassword { set; get; }
        public string Usermsg { set; get; }

    }
}