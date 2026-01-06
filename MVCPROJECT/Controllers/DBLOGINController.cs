using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCPROJECT.Models;

namespace MVCPROJECT.Controllers
{
    public class DBLOGINController : Controller
    {
        // GET: DBLOGIN
        MVCPOJECTEntities dbobj = new MVCPOJECTEntities();
        public ActionResult Login_PageLoad()
        {
            return View();
        }
        public ActionResult UserHome()
        {
            return RedirectToAction("Display_Pageload", "DisplayAll");
            
        }
        public ActionResult CompanyHome()
        {
            return View();
        }
        public ActionResult LoginClick(UserLogin clsobj)
        {
            if (ModelState.IsValid)
            {
                var val = dbobj.sp_logincountid(clsobj.Uname, clsobj.Password).First();
                if (val == 1)
                {
                    var uid = dbobj.sp_loginid(clsobj.Uname, clsobj.Password).FirstOrDefault();
                    Session["uid"] = uid;

                    var lt = dbobj.sp_logintype(clsobj.Uname, clsobj.Password).FirstOrDefault();
                    if (lt == "user")
                    {
                        return RedirectToAction("UserHome");
                    }
                    else if (lt == "Company")
                    {
                        return RedirectToAction("CompanyHome");
                    }
                }
                else
                {
                    ModelState.Clear();
                    clsobj.Msg = "Invalid username and password";
                    return View("Login_pageload", clsobj);
                }

            }
            return View("Login_pageload", model: clsobj);
        }

    }
}