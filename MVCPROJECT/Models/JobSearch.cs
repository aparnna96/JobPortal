using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPROJECT.Models
{
    public class JobSearch
    {
        public JobSearch()
        {
            Selectjob = new List<JobList>();
            Insertise = new JobList();
        }
        public JobList Insertise { set; get; }
        public List<JobList> Selectjob { set; get; }
    }
    public class JobList
    {
        public int JobId { set; get; }
        public int Companyid { set; get; }
        public string JobTitle { set; get; }
        public string JobDescription { set; get; }
        public string Experience{ set; get; }
        public string Skills { set; get; }
        public System.DateTime EndDate { set; get; }
        public string Location { set; get; }
        public string JobStatus { set; get; }
        public int Jobtype_id { set; get; }
        public string Jobtype_name { set; get; }
    }
}
        