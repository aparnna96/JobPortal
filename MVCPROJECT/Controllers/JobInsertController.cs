using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCPROJECT.Models;

namespace MVCPROJECT.Controllers
{
    public class JobInsertController : Controller
    {
        // GET: JobInsert
        MVCPOJECTEntities dbobj = new MVCPOJECTEntities();
        public ActionResult Jobinsert_Pageload()
        {
            List<qualclass> qualList = new List<qualclass>
            {
                new qualclass{sId=1,sName="Bsc"},
                new qualclass{sId=2,sName="Msc"},
                new qualclass{sId=3,sName="Btech"},
                new qualclass{sId=3,sName="Mtech"}
            };
            ViewBag.Selqual = new SelectList(qualList, "sId", "sName");
            //checkboxlist
            JobInsert user = new JobInsert();
            user.MyFavouriteSkills = getSkillsData();
            return View(user);
        }
        public List<SkillsCheckBoxList> getSkillsData()
        {
            List<SkillsCheckBoxList> sts = new List<SkillsCheckBoxList>()
            {
                new SkillsCheckBoxList{Value="Java",Text="Java",Ischecked=false},
                new SkillsCheckBoxList{Value="Python",Text="Python",Ischecked=false},
                new SkillsCheckBoxList{Value="Dotnet",Text="Dotnet",Ischecked=false},
                new SkillsCheckBoxList{Value="PHP",Text="PHP",Ischecked=false},
                new SkillsCheckBoxList{Value="Angular",Text="Angular",Ischecked=false},
            };
            return sts;
        }
        public ActionResult Jobinsert_Click(JobInsert clsobj, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                List<qualclass> qualList = new List<qualclass>
            {
                new qualclass{sId=1,sName="Bsc"},
                new qualclass{sId=2,sName="Msc"},
                new qualclass{sId=3,sName="Btech"},
                 new qualclass{sId=4,sName="Mtech"}
            };
                ViewBag.Selqual = new SelectList(qualList, "sId", "sName");
                int selectedId = Convert.ToInt32(form["ddlqual"]);
                qualclass selectedItem = qualList.FirstOrDefault(c => c.sId == selectedId);
                clsobj.SId = selectedItem.sId;//set
                clsobj.SName = selectedItem.sName;//set

                var quid = string.Join(",", clsobj.SelectedSkill);
                clsobj.Skills = quid;//set
                clsobj.MyFavouriteSkills = getSkillsData();//get
                                                           // Check what's in the session
                var sessionValue = Session["uid"];
                System.Diagnostics.Debug.WriteLine($"Session CompanyId: {sessionValue}");

                if (sessionValue == null)
                {
                    clsobj.Msg = "Error: CompanyId not found in session";
                    return View("Jobinsert_Pageload", clsobj);
                }

                int companyid = Convert.ToInt32(sessionValue);
                System.Diagnostics.Debug.WriteLine($"Converted CompanyId: {companyid}");

                dbobj.sp_addjob(companyid, clsobj.Jobtitle, clsobj.Jobdesc, clsobj.Location, clsobj.Exp, clsobj.Skills,
                    clsobj.SName,Convert.ToDateTime("2025-11-16"), Convert.ToDateTime("2025-12-16"), "Active");
                clsobj.Msg = "Successfully inserted";

                return View("Jobinsert_Pageload", clsobj);
            }
            else
            {
                List<qualclass> qualList = new List<qualclass>
                      {
                new qualclass{sId=1,sName="Bsc"},
                new qualclass{sId=2,sName="Msc"},
                new qualclass{sId=3,sName="Btech"},
                new qualclass{sId=4,sName="Mtech"}
            };
                ViewBag.Selqual = new SelectList(qualList, "sId", "sName");
                clsobj.MyFavouriteSkills = getSkillsData();

                return View("Jobinsert_Pageload", clsobj);

            }
        }
    }
}