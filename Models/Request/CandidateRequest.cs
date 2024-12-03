
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace JobPortalWebApplication.Models.Request
{
    public class CandidateRequest
    {
        public int? CandidateID { get; set; }
        public int? PlanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        //public string Password { get; set; }
        //public string ConfirmPassword { get; set; }   

        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalOrZipCode { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
        public string HighestEducationLevel { get; set; }

        public List<EducationDetails> Educations { get; set; }
        public List<EducationList> Education { get; set; }
        public List<WorkExperienceList> Experiences { get; set; }

        public List<WorkExperiences> WorkExperience { get; set; }
        public List<SkillList> SkillLists { get; set; }
        public Skills Skills { get; set; }
        //  public string SkillsOrExpertise { get; set; }
        public int? JobTypesID { get; set; }
        public int? JobLocationID { get; set; }
        public int? AvailabilityID { get; set; }
        public string JobTypes { get; set; }
        public string JobLocation { get; set; }
        public string Availability { get; set; }
        public bool FreeTraining { get; set; }
        public bool PaidTraining { get; set; }
        public bool CareerConsultantContact { get; set; }
        public string Resumepath { get; set; }
        public HttpPostedFile ResumeFile { get; set; }
        public string CoverLetter { get; set; } // Nullable
        public string LinkedInProfile { get; set; } // Nullable
        public string Portfolio { get; set; } // Nullable
        public DateTime? CreatedDate { get; set; }
        public DateTime? Updateddate { get; set; }

    }
    public class WorkExperienceList
    {
        public int WorkExperienceId {  get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Designation { get; set; }
        public string KeySkills { get; set; }
    }
    public class Skills
    {
        //public bool CoreSkill { get; set; }   // Indicates if it's a CoreSkill
        //public int CoreSkillIds { get; set; }   // List of CoreSkillIds

        //public float CoreSkillPercentages { get; set; }
        //public bool SoftSkill { get; set; }   // Indicates if it's a SoftSkill
        //public int SoftSkillIds { get; set; }   // List of SoftSkillIds

        //public float SoftSkillPercentages { get; set; }
        public bool CoreSkill { get; set; }   // Indicates if it's a CoreSkill
        public List<int> CoreSkillIds { get; set; }   // List of CoreSkillIds

        public List<float> CoreSkillPercentages { get; set; }
        public bool SoftSkill { get; set; }   // Indicates if it's a SoftSkill
        public List<int> SoftSkillIds { get; set; }   // List of SoftSkillIds

        public List<float> SoftSkillPercentages { get; set; }
        // public List<SoftSkills> SoftSkills { get; set; }
    }

    //public class SoftSkills
    //{
    //    public string SoftSkill { get; set; }
    //}

    //public class CoreSkills
    //{
    //    public string CoreSkill { get; set; }
    //}

    public class WorkExperiences
    {

        public string CompanyNameValue { get; set; }
        public string CompanyAddressValue { get; set; }
        public string DesignationValue { get; set; }
        public string KeySkillsPracticedValue { get; set; }
        public bool WorkExperienceDetails { get; set; }
        public List<string> CompanyName { get; set; }
        public List<string> CompanyAddress { get; set; }
        public List<string> Designation { get; set; }
        public List<string> KeySkillsPracticed { get; set; }
        public List<WorkExperiences> WorkExperience { get; set; }      // Comma-separated keywords


    }

    [Serializable]
    public class EducationDetails
    {
        public string DegreeName { get; set; }

        public string CollegeUniversity { get; set; }
        public string Address { get; set; }
        public string GraduatedOrPursuingValue { get; set; }
        public string KeySkillsValue { get; set; } // Comma-separated keywords
        public string AcademicProjectValue { get; set; } // Limited to 150 character

        public List<int?> DegreeId { get; set; }
        public List<string> CollegeUniversityName { get; set; }
        public List<string> PlaceAddress { get; set; }
        public List<string> GraduatedOrPursuing { get; set; }
        public List<string> KeySkills { get; set; } // Comma-separated keywords
        public List<string> AcademicProject { get; set; } // Limited to 150 character
                                                          // public List<Education> Education { get; set; }

    }
    //public class EducationDetails
    //{
    //    public int? EducationId { get; set; }
    //    public int? JobseekerId { get; set; }
    //    public int? DegreeId { get; set; }
    //    public string CollegeUniversityName { get; set; }
    //    public string PlaceAddress { get; set; }
    //    public string GraduatedOrPursuing { get; set; }
    //    public string KeySkills { get; set; } // Comma-separated keywords
    //    public string AcademicProject { get; set; } // Limited to 150 characters
    //}
}
