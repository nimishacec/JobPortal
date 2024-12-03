using System;
using System.Web;

namespace JobPortalWebApplication.Models.Request
{
    public class JobApplicationResult
    {
       
        public int JobID { get; set; }
        public int CandidateID { get; set; }
        public string CandidateName { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationCode {  get; set; }
        public string CompanyName { get; set; }
        public string ResumePath {  get; set; }
        public string CandidateEmailAddress { get; set; }
        public string CandidatePhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string JobType { get; set; }
        public decimal? Salary { get; set; }
        public int Vacancy { get; set; }
        public string Experience { get; set; }

        public string HighestEducationLevel { get; set; }
        public DateTime? JobApplicationLastDate { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; }
        public HttpPostedFile ResumeFile { get; set; }
    }
}
