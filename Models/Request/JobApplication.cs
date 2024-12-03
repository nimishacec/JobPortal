using System;
using System.Web;

namespace JobPortalWebApplication.Models.Request
{
    public class JobApplication
    {
        public int JobID { get; set; }
        public int CandidateID { get; set; }
      
        public DateTime ApplicationDate { get; set; }
       
        public HttpPostedFile ResumeFile { get; set; }
    }
}
