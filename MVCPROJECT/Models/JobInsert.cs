using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCPROJECT.Models
{
    public class qualclass
    {
        public int sId { get; set; }
        public string sName { get; set; }
    }
    public class SkillsCheckBoxList
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Ischecked { get; set; }
    }

    public class JobInsert
    {
        public int SId { get; set; }
        public string SName { get; set; }
        public List<SkillsCheckBoxList> MyFavouriteSkills { get; set; }
        public string[] SelectedSkill { get; set; }
        [Required(ErrorMessage = "Enter a Jobtitle")]
        public string Jobtitle { get; set; }
        [Required(ErrorMessage = "Enter a Jobdescription")]
        public string Jobdesc { get; set; }
        [Required(ErrorMessage = "Enter a Location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Enter a Experience")]
        public decimal Exp { get; set; }
        public string Skills { get; set; }
        public string Msg { get; set; }


    }
}