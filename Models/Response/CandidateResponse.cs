
using JobPortalWebApplication.Models.Request;
using JobPortalWebApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Web;

namespace JobPortalWebApplication.Models.Response
{
    public class CandidateResponse { 

        public int CandidateId { get; set; }
        public string PlanType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string EmailStatus { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalOrZipCode { get; set; }
        public string Country { get; set; }
        public string HighestEducationLevel { get; set; }
        public List<EducationDetail> Educations { get; set; }
        public List<WorkExperienceList> Experiences { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }
        public List<Skill> Skill {  get; set; }
       
        public string JobType { get; set; }
        public string JobLocation { get; set; }
        public string Availability { get; set; }
        public bool WillingToTakeFreeTraining { get; set; }
        public bool WillingToTakePaidTraining { get; set; }
        public bool WillingToBeContactedByCareerConsultant { get; set; }
        public HttpPostedFile ResumeFile { get; set; }
        public string ResumeFilePath { get; set; }
        public string CoverLetter { get; set; }
        public string LinkedInProfile { get; set; }
        public string Portfolio { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? Updateddate { get; set; }
        public string Message { get; set; }
    }
    public class EducationDetail
    {
        //public int? EducationId { get; set; }
        //public int? JobseekerId { get; set; }
        public string Degree{ get; set; }
        public string CollegeUniversityName { get; set; }
        public string PlaceAddress { get; set; }
        public string GraduatedOrPursuing { get; set; }
        public string KeySkills { get; set; } // Comma-separated keywords
        public string AcademicProject { get; set; } // Limited to 150 characters
    }
    public class WorkExperience
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Designation { get; set; }
        public string KeySkillsPracticed { get; set; }
    }
    public class Skill
    {
        public string CoreSkills { get; set; }
        public string SoftSkills { get; set; }
        public string CoreSkillPercentage { get; set; }
        public string SoftSkillpercentage { get; set; }
    }
}
