using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MVCPROJECT.Models;

namespace MVCPROJECT.Controllers
{
    public class DisplayAllController : Controller
    {
        // GET: DisplayAll
        MVCPOJECTEntities dbobj = new MVCPOJECTEntities();
        public ActionResult Display_Pageload()
        {
            var data = dbobj.Jobtab1.ToList();

            // DEBUG: Check raw data from database
            System.Diagnostics.Debug.WriteLine("=== Display_Pageload Data ===");
            foreach (var d in data)
            {
                System.Diagnostics.Debug.WriteLine($"JobId: {d.JobId}, CompanyId: {d.CompanyId}, Title: {d.JobTitle}");
            }

            JobSearch js = new JobSearch();
            foreach (var d in data)
            {
                var job = new JobList
                {
                    JobId = d.JobId,
                    Companyid = d.CompanyId,
                    JobTitle = d.JobTitle,
                    JobDescription = d.JobDescription,
                    Experience = d.Experience.ToString(),
                    Skills = d.Skills,
                    EndDate = d.Enddate,
                    Location = d.Location
                };

                // DEBUG: Check mapped data
                System.Diagnostics.Debug.WriteLine($"Mapped - JobId: {job.JobId}, CompanyId: {job.Companyid}");

                js.Selectjob.Add(job);
            }

            js.Insertise = new JobList();

            // DEBUG: Check final list
            System.Diagnostics.Debug.WriteLine($"Total jobs in list: {js.Selectjob.Count}");

            return View(js);
        }
        public ActionResult searchjob_click(JobSearch clsobj)
        {
            string qry = "";
            if (!string.IsNullOrWhiteSpace(clsobj.Insertise.Experience))
            {
                qry += " and Experience like '%" + clsobj.Insertise.Experience + "%'";
            }
            if (!string.IsNullOrWhiteSpace(clsobj.Insertise.Skills))
            {
                qry += " and Skills like '%" + clsobj.Insertise.Skills + "%'";
            }

            if (!string.IsNullOrWhiteSpace(clsobj.Insertise.Location))
            {
                qry += " and Location like '%" + clsobj.Insertise.Location + "%'";
            }
            return View("Display_Pageload", getdata(clsobj, qry));
        }
        private JobSearch getdata(JobSearch clsobj, string qry)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["importdataconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Jobsearch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qry", qry);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var joblist = new JobSearch();
                while (dr.Read())
                {
                    var jobcls = new JobList();
                    jobcls.JobId = Convert.ToInt32(dr["JobId"].ToString());
                    jobcls.Companyid = Convert.ToInt32(dr["CompanyId"].ToString());
                    jobcls.JobTitle = dr["JobTitle"].ToString();
                    jobcls.JobDescription = dr["JobDescription"].ToString();
                    jobcls.Experience = dr["Experience"].ToString();
                    jobcls.Skills = dr["Skills"].ToString();
                    jobcls.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    jobcls.Location = dr["Location"].ToString();

                    joblist.Selectjob.Add(jobcls);
                }
                con.Close();
                return joblist;
            }
        }
    }
}