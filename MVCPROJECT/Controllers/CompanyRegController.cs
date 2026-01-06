using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCPROJECT.Models;

namespace MVCPROJECT.Controllers
{
    public class CompanyRegController : Controller
    {
        // GET: CompanyReg
        MVCPOJECTEntities dbobj = new MVCPOJECTEntities();
        public ActionResult Insertcompany_pageload()
        {
            return View();
        }
        public ActionResult InsertCompany_Click(CompanyInsert clsobj)
        {
            if (ModelState.IsValid)
            {
                var getmaxid = dbobj.sp_maxloginid("", "").FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int Regid = 0;
                if (mid == 0)
                {
                    Regid = 1;
                }
                else
                {
                    Regid = mid + 1;
                }
                //get
                dbobj.sp_companyreg(clsobj.Companyname, clsobj.Phone, clsobj.Email, clsobj.Address);
                dbobj.sp_LoginInsert(Regid, clsobj.Username, clsobj.Password, "Company");
                clsobj.Companymsg = "Successfully inserted";
                return View("Insertcompany_pageload", clsobj);
            }
            return View("Insertcompany_pageload", clsobj);
        }

    }
}