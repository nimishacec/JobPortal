using System;

namespace JobPortalWebApplication.Models.Response
{
   public class JobSearchResult
{
    public int JobID { get; set; }
    public string JobTitle { get; set; }
    public string JobDescription { get; set; }
    public string JobLocation { get; set; }
    public decimal? Salary { get; set; }
    public string CompanyName { get; set; }
        public string Experience {  get; set; }
        public string Qualifications {  get; set; }
    public string JobType { get; set; }
    public DateTime? ApplicationDeadline { get; set; }
    public string RequiredSkills { get; set; }
        public string ApplicationStatus { get; set; }
    }

}
