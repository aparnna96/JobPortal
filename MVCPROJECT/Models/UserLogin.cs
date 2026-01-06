using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCPROJECT.Models
{
    public class UserLogin   
    {
        [Required(ErrorMessage = "enter the username")]
        public string Uname { set; get; }
        [Required(ErrorMessage = "enter the password")]
        public string Password { set; get; }
        public string Msg { set; get; }
        public string Ltype { set; get; }
    }

}