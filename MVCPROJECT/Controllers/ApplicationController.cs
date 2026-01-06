using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MVCPROJECT.Models;

namespace MVCPROJECT.Controllers
{
    public class ApplicationController : Controller
    {
        MVCPOJECTEntities dbobj = new MVCPOJECTEntities();

        // GET: AddApplication
        public ActionResult Application_load(int cid, int jid)
        {
            // DEBUG: Log incoming parameters
            System.Diagnostics.Debug.WriteLine($"=== Application_load Called ===");
            System.Diagnostics.Debug.WriteLine($"Incoming cid: {cid}");
            System.Diagnostics.Debug.WriteLine($"Incoming jid: {jid}");

            // Store in session
            Session["cid"] = cid;
            Session["jid"] = jid;
            TempData["jid"] = jid;

            // DEBUG: Verify session was set
            System.Diagnostics.Debug.WriteLine($"Session cid after setting: {Session["cid"]}");
            System.Diagnostics.Debug.WriteLine($"Session jid after setting: {Session["jid"]}");

            // Check if uid exists in session
            if (Session["uid"] == null)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: uid is NULL in session!");
                return RedirectToAction("Login", "Account"); // Redirect to login
            }

            int uid = Convert.ToInt32(Session["uid"]);
            System.Diagnostics.Debug.WriteLine($"User ID: {uid}");

            ViewBag.msg = TempData["msg"];

            // DEBUG: Log database query parameters
            System.Diagnostics.Debug.WriteLine($"Calling sp_jobview with jid={jid}, cid={cid}");

            var getdata = dbobj.sp_jobview(jid, cid).FirstOrDefault();

            // DEBUG: Check if data was retrieved
            if (getdata == null)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: sp_jobview returned NULL!");
                ViewBag.msg = "Job not found! ";
                return View(new JobInsert());
            }

            System.Diagnostics.Debug.WriteLine($"Job Title: {getdata.JobTitle}");
            System.Diagnostics.Debug.WriteLine($"Skills: {getdata.Skills}");
            System.Diagnostics.Debug.WriteLine($"Experience: {getdata.Experience}");
            System.Diagnostics.Debug.WriteLine($"Location: {getdata.Location}");

            return View(new JobInsert
            {
                Jobtitle = getdata.JobTitle,
                Skills = getdata.Skills,
                Exp = getdata.Experience,
                Location = getdata.Location
            });
        }
        //insert application + resume
        public ActionResult Application_Click(HttpPostedFileBase file, Applicationcls clsobj)
        {
            int cid = Convert.ToInt32(Session["cid"]);
            int jid = Convert.ToInt32(Session["jid"]);
            int uid = Convert.ToInt32(Session["uid"]);
            int count = Convert.ToInt32(dbobj.sp_IsApplied(uid, jid).FirstOrDefault());
            //if already applied
            if (count > 0)
            {
                TempData["msg"] = "You have already applied for this job";
                return RedirectToAction("Application_load", new { cid = cid, jid = jid });

            }
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    string folder = Server.MapPath("~/Resume/");

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string fullpath = Path.Combine(folder, fname);
                    file.SaveAs(fullpath);

                    clsobj.Resume = "~/Resume" + fname;
                }
                DateTime CurrentDate = DateTime.Today;
                clsobj.ApplicationDate = CurrentDate;
                clsobj.UserID = uid;
                clsobj.CompanyID = cid;
                int jobid = Convert.ToInt32(Session["jid"]);
                dbobj.sp_PostApplication(clsobj.UserID, jobid, clsobj.ApplicationDate, clsobj.Resume, "Applied");

                TempData["msg"] = "Application submitted successfully!";
                return RedirectToAction("Application_load", new { cid = cid, jid = jid });

            }
            TempData["msg"] = "Something went wrong!";
            return RedirectToAction("Application_load", new { cid = cid, jid = jid });

        }

    }
}