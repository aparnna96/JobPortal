using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCPROJECT.Models
{
    public class Applicationcls
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Resume { get; set; }
        public string Status { get; set; }
        public string Msg { get; set; }
    }

}
