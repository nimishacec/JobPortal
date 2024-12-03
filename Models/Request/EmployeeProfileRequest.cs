using System.ComponentModel.DataAnnotations;

namespace JobPortalWebApplication.Models.Request
{
    public class EmployeeProfileRequest
    {
        public int CompanyID {  get; set; }
        [Required]
        public string CompanyName { get; set; }

        public string CompanyRegistrationNumber { get; set; } // Optional: Use image upload for registration document

        [Required]
        [EmailAddress]
        public string CompanyEmail { get; set; }

        [Required]
        [Phone]
        public string CompanyPhoneNumber { get; set; }

        [Required]
   
        public string WebsiteUrl { get; set; }

        [Required]
        public string PhysicalAddress { get; set; }

        [Required]
        public string CompanyDescription { get; set; }

        [Required]
        public string IndustryType { get; set; }

        [Required]
        public string CompanySize { get; set; }

        [Required]
        public string ContactPersonName { get; set; }

        [Required]
        [EmailAddress]
        public string ContactPersonEmail { get; set; }

        [Required]
        [Phone]
        public string ContactPersonPhoneNumber { get; set; }

        public string CompanyLogo { get; set; }

        [Required]
        public bool AgreementToTerms { get; set; }

        [Required]
        public bool TrainingAndPlacementProgram { get; set; }

        [Required]
        public int PlanId { get; set; }
    
}
}